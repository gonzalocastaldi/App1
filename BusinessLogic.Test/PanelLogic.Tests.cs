using DataAccess;
using Domain;

namespace BusinessLogic.Test;

[TestClass]
public class PanelLogic_Tests
{
    
    private RepositorioEquipo _repositorioEquipo;
    private RepositorioUsuario _repositorioUsuario;
    private ContextoSql _context;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();

    [TestInitialize]
    public void Setup()
    {
        _context = _contextFactory.CrearContextoDb();
        _repositorioEquipo = new RepositorioEquipo(_context);
        _repositorioUsuario = new RepositorioUsuario(_context);
    }
   
    [TestMethod]
    public void Agregar_Tarea_A_Panel()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel panel = new Panel("nombre", "desc", "equipo1");
        _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
        PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
        Assert.AreEqual(1, panel.Tareas.Count);
    }
    
   [TestMethod]
   [ExpectedException(typeof(InvalidOperationException))]
   public void AgregarTareaAPanelQueNoExiste()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombreNoExiste", tarea, "equipo1");
   }

   [TestMethod]
   public void Cambiar_Nombre_Panel()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarNombre("nombre", "nuevoNombre", usuarioCreador, "equipo1");
       Assert.AreEqual("nuevoNombre", panel.Nombre);
   }

   [TestMethod]
   [ExpectedException(typeof(ArgumentException))]
   public void CambiarNombreAPanelQueNoExiste()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarNombre("nombreNoExiste", "nuevoNombre", usuarioCreador, "equipo1");
   }

   [TestMethod]
   [ExpectedException(typeof(InvalidOperationException))]

   public void CambiarNombrePanelATareasVencidas()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarNombre("Tareas Vencidas", "nuevoNombre", usuarioCreador, "equipo1");
   }

   [TestMethod]
   [ExpectedException(typeof(InvalidOperationException))]

   public void CambiarNombrePanelRepetido()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Panel panel2 = new Panel("nombre2", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel2, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarNombre("nombre2", "nombre", usuarioCreador, "equipo1");
   }

   [TestMethod]
   public void Cambiar_Descripcion_Panel()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
           "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarDescripcion("nombre", "nuevaDesc", usuarioCreador, "equipo1");
       Assert.AreEqual("nuevaDesc", panel.Descripcion);
   }

   [TestMethod]
   [ExpectedException(typeof(ArgumentException))]
   public void CambiarDescripcionAPanelQueNoExiste()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.CambiarDescripcion("nombreNoExiste", "nuevaDesc", usuarioCreador, "equipo1");
   }

   [TestMethod]
   public void Eliminar_Tarea_De_Panel()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       _panelLogic.EliminarTarea("equipo1","nombre", "titulo", usuarioCreador);
       Assert.AreEqual(0, panel.Tareas.Count);
   }

   [TestMethod]
   public void EliminarTareaDeFormaPermanente()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       _panelLogic.EliminarTareaDeFormaPermanente("equipo1","nombre", "titulo", usuarioCreador);
       Assert.AreEqual(0, panel.Tareas.Count);
   }

   [TestMethod]
   [ExpectedException(typeof(InvalidOperationException))]
   public void EliminarTareaDePanelQueNoExiste()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       _panelLogic.EliminarTarea("equipo1","nombreNoExiste", "titulo", usuarioCreador);
   }

   [TestMethod]
   [ExpectedException(typeof(InvalidOperationException))]
   public void EliminarTareaQueNoExiste()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       _panelLogic.EliminarTarea("equipo1","nombre", "tituloNoExiste", usuarioCreador);
   }

   [TestMethod]
   public void AgregarMasDeUnaTareaConMismoNombre()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       Tarea tarea2 = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       Assert.ThrowsException<InvalidOperationException>(() => _panelLogic.AgregarTarea("nombre", tarea2, "equipo1"));
   }

   [TestMethod]
   public void AgregarMasDeUnaTarea()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("nombre", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       Tarea tarea2 = new Tarea("titulo2",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
       _panelLogic.AgregarTarea("nombre", tarea2, "equipo1");
       Assert.AreEqual(2, panel.Tareas.Count);
   }

   [TestMethod]
   public void PruebaValidarYGuardarTarea()
   {
       PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       _repositorioUsuario.Agregar(usuario);
       Equipo equipo = new Equipo("EquipoPrueba", 10, "desc", usuario);
       _repositorioEquipo.Agregar(equipo);
       EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       Panel panel = new Panel("PanelPrueba", "desc", "EquipoPrueba");
       equipoLogic.AgregarPanel("EquipoPrueba", panel, usuario);
       //int tareas = panel.Tareas.Count();
       Tarea tarea = new Tarea("TituloPrueba",  new DateTime(2030, 10, 10),"desc",1, panel, panel);
       panelLogic.ValidarYGuardarTarea(tarea, panel, usuario);
       Assert.AreEqual(1, _repositorioEquipo.ObtenerTareasDePanel("EquipoPrueba", "PanelPrueba").Count());
   }

   [TestMethod]
   public void Listar_Epicas_De_Panel()
   {
       Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
       EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
       _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
       Panel panel = new Panel("panel", "desc", "equipo1");
       _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
       Epica epica1 = new Epica("Epica1", Prioridad.Baja, "Descripcion1", new DateTime(2025, 11, 11));
       Epica epica2 = new Epica("Epica2", Prioridad.Baja ,"Descripcion1", new DateTime(2025, 11, 11));
       panel.AgregarEpica(epica1);
       panel.AgregarEpica(epica2);
       PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
       var epicas = _panelLogic.ListarEpicas("panel", "equipo1");
       int cantidadDeEpicas = epicas.Count();
       Assert.AreEqual(2, cantidadDeEpicas);
   }
}