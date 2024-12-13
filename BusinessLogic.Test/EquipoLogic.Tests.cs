using DataAccess;
using Domain;

namespace BusinessLogic.Test;

[TestClass]
public class EquipoLogicTests
{
        private RepositorioEquipo _repositorioEquipo;
        private RepositorioUsuario _repositorioUsuario;
        private EquipoLogic _equipoLogic;
        private Usuario _usuario;
        private Usuario _usuarioNoAdmin;
        private ContextoSql _context;
        private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria(); 

        [TestInitialize]
        public void Setup()
        {
            _context = _contextFactory.CrearContextoDb();
            _repositorioEquipo = new RepositorioEquipo(_context);
            _repositorioUsuario = new RepositorioUsuario(_context);
            
            _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
            _usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _usuarioNoAdmin= new Usuario(false, "Martin", "Esteves", "martin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        }
        
        [TestMethod]
        public void CreacionDeEquipoConTodosLosParametros()
        {
            Equipo equipo = new Equipo("equipo1", 10, "descripcion", _usuario);
            Assert.IsNotNull(equipo);
        }

        [TestMethod]
        public void ModificarNombreDeEquipo()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarNombre("equipo1", "equipo2", _usuario);
            Assert.IsTrue(_repositorioEquipo.ObtenerTodosLosEquipos().Any(e => e.Nombre == "equipo2"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModificarNombreDeEquipo_Cuando_Ya_Existe_El_Nuevo_Nombre()
        {
            Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CrearEquipo("equipo2", 10, "descripcion", _usuario);
            _equipoLogic.CambiarNombre("equipo1", "equipo2", usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModificarNombreDeEquipo_Cuando_No_Es_Manager_o_Administrador()
        {
            Usuario usuario2 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarNombre("equipo1", "equipo2", usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CambiarNombreEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarNombre("equipoNoExiste", "equipo2", _usuario);
        }

        [TestMethod]
        public void ModificarDescripcionDeEquipo()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarDescripcion("equipo1", "descripcion2", _usuario);
            Assert.AreEqual("descripcion2", _repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Descripcion);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModificarDescripcion_Cuando_No_Es_Manager_o_Administrador()
        {
            Usuario usuarioModificador = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarDescripcion("equipo1", "descripcion2", usuarioModificador);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModificarDescripcionDeEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarDescripcion("equipoNoExiste", "descripcion2", _usuario);
        }

        [TestMethod]
        public void ModificarCantidadMaximaDeUsuarios()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarCantidadMaximaDeUsuarios("equipo1", 15, _usuario);
            Assert.AreEqual(15, _repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").CantidadMaxima);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModificarCantidadMaximaDeUsuarios_Cuando_No_Es_Manager_o_Administrador()
        {
            Usuario usuario2 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
                "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarCantidadMaximaDeUsuarios("equipo1", 15, usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModificarCantidadMaximaDeUsuarios_Cuando_CantidadIngresadaEsNegativa()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarCantidadMaximaDeUsuarios("equipo1", -15, _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModificarCantidadMaximaDeUsuariosEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CambiarCantidadMaximaDeUsuarios("equipoNoExiste", 15, _usuario);
        }

        [TestMethod]
        public void CrearEquipo_Cuando_Es_Administrador()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Assert.IsTrue(_repositorioEquipo.ObtenerTodosLosEquipos().Any(e => e.Nombre == "equipo1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CrearEquipo_Cuando_No_Es_Administrador()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuarioNoAdmin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CrearEquipoConNombreRepetido()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.CrearEquipo("equipo1", 11, "descripcion2", _usuario);
        }

        [TestMethod]
        public void AgregarUsuarioAEquipo_Cuando_Es_Admin()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.AgregarUsuario(_usuarioNoAdmin, "equipo1", _usuario);
            Assert.IsTrue(_repositorioEquipo.ObtenerTodosLosEquipos().Any(e => e.Usuarios.Contains(_usuarioNoAdmin)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarUsuarioAEquipo_Cuando_No_Es_Admin()
        {
            Usuario user = new Usuario();
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.AgregarUsuario(user, "equipo1", _usuarioNoAdmin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgregarUsuarioAEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.AgregarUsuario(_usuarioNoAdmin, "equipoNoExiste", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarUsuarioConCantidadMaximaAlcanzada()
        {
            Usuario user1 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo1@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario user2 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo2@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario user3 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo3@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario user4 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo4@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario user5 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo5@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario user6 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo6@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 5, "descripcion", _usuario);
            _equipoLogic.AgregarUsuario(user1, "equipo1", _usuario);
            _equipoLogic.AgregarUsuario(user2, "equipo1", _usuario);
            _equipoLogic.AgregarUsuario(user3, "equipo1", _usuario);
            _equipoLogic.AgregarUsuario(user4, "equipo1", _usuario);
            _equipoLogic.AgregarUsuario(user5, "equipo1", _usuario);
            _equipoLogic.AgregarUsuario(user6, "equipo1", _usuario);
        }

        [TestMethod]
        public void EliminarUnEquipo_Cuando_NoTienePanelesCreados()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.EliminarEquipo("equipo1", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarUnEquipo_Cuando_TienePanelesCreados()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarEquipo("equipo1", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarUnEquipo_Cuando_NoTienePermisos()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.EliminarEquipo("equipo1", _usuarioNoAdmin);
        }

        [TestMethod]
        public void EliminarUnEquipo_Cuando_TienePermisos()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.EliminarEquipo("equipo1", _usuario);
            Assert.IsFalse(_repositorioEquipo.ObtenerTodosLosEquipos().Any(e => e.Nombre == "equipo1"));
        }

        [TestMethod]
        public void AgregarPanelAEquipo()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Assert.IsTrue(_repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Paneles.Contains(panel));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarPanelAEquipo_Cuando_Ya_ExisteUnPanelConElMismoNombre()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Panel panel2 = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel2, _usuario);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgregarPanelAEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipoNoExiste", panel, _usuario);
        }

        [TestMethod]
        public void EliminarPanel()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarPanel("equipo1", "panel1", _usuario);
            Assert.IsFalse(_repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Paneles.Count > 1);
        }

        [TestMethod]
        public void EliminarPanelDeFormaPermanente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarPanelDeFormaPermanente("equipo1", "panel1", _usuario);
            Assert.IsFalse(_repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Paneles.Count > 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EliminarPanelDeEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarPanel("equipoNoExiste", "panel1", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EliminarPanelNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarPanel("equipo1", "panelNoExiste", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarPanelTareasVencidas()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.EliminarPanel("equipo1", "Tareas Vencidas", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarPanelSinPermisos()
        {
            Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Usuario admin = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", admin);
            _equipoLogic.EliminarPanel("equipo1", "Tareas Vencidas", usuario);
        }

        [TestMethod]
        public void CreacionDePanelTareasVencidas()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Assert.IsTrue(_repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Paneles.Any(p => p.Nombre == "Tareas Vencidas"));
        }

        [TestMethod]
        public void ReactivarTareaVencida_Cuando_EsAdmin()
        {
            DateTimeWrapper.FixedDate = new DateTime(2010, 7, 2);
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Tarea tarea = new Tarea("tarea1", new DateTime(2020, 10, 10), "descripcion", 2, panel, panel);
            PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
            _panelLogic.AgregarTarea("Tareas Vencidas", tarea, "equipo1");
            _equipoLogic.ReactivarTareaVencida("equipo1", "tarea1",new DateTime(2030, 10, 10), _usuario);
            Assert.IsFalse(_repositorioEquipo.ObtenerTodosLosEquipos().First(e => e.Nombre == "equipo1").Paneles.First(p => p.Nombre == "Tareas Vencidas").Tareas.Contains(tarea));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReactivarTareaVencida_Cuando_NoEsAdmin()
        {
            DateTimeWrapper.FixedDate = new DateTime(2010, 7, 2);
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Tarea tarea = new Tarea("tarea1", new DateTime(2020, 10, 10), "descripcion", 2, panel, panel);
            PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
            _panelLogic.AgregarTarea("Tareas Vencidas", tarea, "equipo1");
            _equipoLogic.ReactivarTareaVencida("equipo1", "tarea1", new DateTime(2030, 10, 10), _usuarioNoAdmin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReactivarTareaDeEquipoNoExistente()
        {
            DateTimeWrapper.FixedDate = new DateTime(2010, 7, 2);
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Tarea tarea = new Tarea("tarea1", new DateTime(2020, 10, 10), "descripcion", 2, panel, panel);
            PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
            _panelLogic.AgregarTarea("Tareas Vencidas", tarea, "equipo1");
            _equipoLogic.ReactivarTareaVencida("equipoNoExiste", "tarea1",new DateTime(2030, 10, 10), _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReactivarTareaNoExistente()
        {
            DateTimeWrapper.FixedDate = new DateTime(2010, 7, 2);
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            Tarea tarea = new Tarea("tarea1", new DateTime(2020, 10, 10), "descripcion", 2, panel, panel);
            PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
            _panelLogic.AgregarTarea("Tareas Vencidas", tarea, "equipo1");
            _equipoLogic.ReactivarTareaVencida("equipo1", "tareaNoExiste",new DateTime(2030, 10, 10), _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarEquipoConPaneles()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel = new Panel("panel1", "descripcion", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel, _usuario);
            _equipoLogic.EliminarEquipo("equipo1", _usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EliminarEquipoNoExistente()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            _equipoLogic.EliminarEquipo("equipoNoExiste", _usuario);
        }

        [TestMethod]
        public void PruebaListaEquiposSiendoAdmin()
        {
            EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
            Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
                "123456Aa=");
            _repositorioUsuario.Agregar(usuario);
            List<Equipo> equipos = equipoLogic.ListarEquipos(usuario);
            CollectionAssert.AreEqual(_repositorioEquipo.ObtenerTodosLosEquipos().ToList(), equipos);
        }

        [TestMethod]
        public void PruebaValidarYGuardarEquipo()
        {
            int cantEquipos = _repositorioEquipo.ObtenerTodosLosEquipos().Count();
            Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _repositorioUsuario.Agregar(usuario);
            EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
            Equipo equipo = new Equipo("EquipoPrueba", 10, "desc", usuario);
            _repositorioEquipo.Agregar(equipo);
            equipoLogic.ValidarYGuardarEquipo(equipo, usuario);
            Assert.AreEqual(cantEquipos + 1, _repositorioEquipo.CantidadEquipos());
        }

        [TestMethod]
        public void PruebaValidarYGuardarPanel()
        {
            EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
            Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            _repositorioUsuario.Agregar(usuario);
            Equipo equipo = new Equipo("EquipoPrueba", 10, "desc", usuario);
            _repositorioEquipo.Agregar(equipo);
            int paneles = equipo.Paneles.Count;
            Panel panel = new Panel("PanelPrueba", "desc", "EquipoPrueba");
            equipoLogic.ValidarYGuardarPanel(panel, equipo, usuario);
            Assert.AreEqual(paneles + 1, equipo.Paneles.Count());
        }

        [TestMethod]
        public void ListarPaneles_DeberiaDevolverListaDePaneles()
        {
            _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", _usuario);
            Panel panel1 = new Panel("panel1", "descripcion1", "equipo1");
            Panel panel2 = new Panel("panel2", "descripcion2", "equipo1");
            _equipoLogic.AgregarPanel("equipo1", panel1, _usuario);
            _equipoLogic.AgregarPanel("equipo1", panel2, _usuario);
            Equipo equipo = _repositorioEquipo.ObtenerTodosLosEquipos().FirstOrDefault(e => e.Nombre == "equipo1");
            var paneles = _equipoLogic.ListarPaneles(equipo.Nombre);
            int cantidadPaneles = paneles.Count();
            Assert.AreEqual(3, cantidadPaneles);
        }
}