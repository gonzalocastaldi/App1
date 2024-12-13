namespace Domain.Test
{
    [TestClass]
    public class PanelTests
    {

        private Usuario _usuario;
        private Equipo _equipo;

        [TestInitialize]
        public void Setup()
        {
            _usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
                "123456Aa=");
            _equipo = new Equipo("equipo1", 10, "des", _usuario);
        }

        [TestMethod]
        public void PruebaCreacionClasePanel()
        {
            Usuario user1 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
                "123456Aa=");
        }

        [TestMethod]
        public void CreacionPanelConEquipo()
        {
            Panel? panel1 = new Panel("panel1", "descripcion1", _equipo.Nombre);
            _equipo.AgregarPanel(panel1);
            Assert.AreEqual(1, _equipo.Paneles.Count);
        }

        [TestMethod]
        public void CreacionDePanelConTareas()
        {
            Panel? panel = new Panel("panel1", "des", _equipo.Nombre);
            Panel panel1 = new Panel("panel1", "descripcion1", _equipo.Nombre);
            Tarea tarea1 = new Tarea("tarea1", new DateTime(2024, 12, 3), "descripcion1", 2, panel, panel);
            panel1.Tareas.Add(tarea1);
            Assert.AreEqual(1, panel1.Tareas.Count);
        }

        [TestMethod]
        public void AgregarTareaAPanel()
        {
            Panel? panel = new Panel("panel1", "des", _equipo.Nombre);
            Panel panel1 = new Panel("panel1", "descripcion1", _equipo.Nombre);
            Tarea tarea1 = new Tarea("tarea1", new DateTime(2024, 12, 3), "descripcion1", 2, panel, panel);
            panel1.AgregarTarea(tarea1);
            Assert.AreEqual(1, panel1.Tareas.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CrearPanelConNombreVacio()
        {
            Panel panel = new Panel("", "des", _equipo.Nombre);
        }

        [TestMethod]
        public void ProbarPanel()
        {
            Panel panel = new Panel();
        }

        [TestMethod]
        public void AgregarEpicaAPanel()
        {
            Panel panel = new Panel("panel1", "des", _equipo.Nombre);
            Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
            panel.AgregarEpica(epica);
            Assert.AreEqual(1, panel.Epicas.Count);
        }

        [TestMethod]
        public void EliminarEpicaDePanel()
        {
            Panel panel = new Panel("panel1", "des", _equipo.Nombre);
            Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
            panel.AgregarEpica(epica);
            panel.EliminarEpica(epica);
            Assert.AreEqual(0, panel.Epicas.Count);
        }

        [TestMethod]
        public void ObtenerEquipoAlQuePerteneceElPanel()
        {
            Panel panel = new Panel("panel1", "des", _equipo.Nombre);
            Assert.AreEqual(_equipo.Nombre, panel.EquipoAlQuePertenece);
        }

        [TestMethod]
        public void PruebaSetAndGetNombre()
        {
            Panel panel = new Panel();
            panel.Nombre = "Nuevo Nombre";
            Assert.AreEqual("Nuevo Nombre", panel.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaSetNombreVacio()
        {
            Panel panel = new Panel();
            panel.Nombre = "";
        }

        [TestMethod]
        public void PruebaSetYGetDescripcion()
        {
            Panel panel = new Panel();
            panel.Descripcion = "Nueva Descripcion";
            Assert.AreEqual("Nueva Descripcion", panel.Descripcion);
        }

        [TestMethod]
        public void EliminarTareaDePanel()
        {
            Panel? panel = new Panel("panel1", "des", _equipo.Nombre);
            Tarea tarea = new Tarea("tarea1", new DateTime(2024, 12, 3), "descripcion1", 2, panel, panel);
            panel.AgregarTarea(tarea);
            panel.EliminarTarea(tarea);
            Assert.AreEqual(0, panel.Tareas.Count);
        }

        [TestMethod]
        public void PruebaSetYGetEquipoAlQuePertenece()
        {
            Panel panel = new Panel();
            panel.EquipoAlQuePertenece = "Nuevo Equipo";
            Assert.AreEqual("Nuevo Equipo", panel.EquipoAlQuePertenece);
        }

    }
}
