using BusinessLogic;

namespace Domain.Test
{
    [TestClass]
    public class EquipoTests
    {
        private Usuario _usuario;

        [TestInitialize]
        public void Setup()
        {
            _usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        }

        [TestMethod]
        public void PruebaCreacionClaseEquipo()
        {
            Equipo equipo = new Equipo(_usuario);
            Assert.IsNotNull(equipo);
        }

        [TestMethod]
        public void PruebaAsignarNombre()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.Nombre = "Equipo 1";
            Assert.AreEqual("Equipo 1", equipo.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaAsignarNombreVacio()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.Nombre = "";
        }

        [TestMethod]
        public void PruebaAsignarFechaPredeterminada()
        {
            Equipo equipo = new Equipo(_usuario);
            Assert.AreEqual(DateTime.Today, equipo.FechaCreacion);
        }

        [TestMethod]
        public void PruebaAgregarDescripcion()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.Descripcion = "Equipo de prueba";
            Assert.AreEqual("Equipo de prueba", equipo.Descripcion);
        }

        [TestMethod]
        public void PruebaAgregarCantidadMaxima()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.CantidadMaxima = 10;
            Assert.AreEqual(10, equipo.CantidadMaxima);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaAgregarCantidadMaximaConValorNegativo()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.CantidadMaxima = -10;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaAgregarCantidadMaximaConValorCero()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.CantidadMaxima = 0;
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaAgregarCantidadMaximaConValorExcesivo()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.CantidadMaxima = 1000;
        }

        [TestMethod]
        public void PruebaCantidadActualUsuarios()
        {
            Equipo equipo = new Equipo(_usuario);
            Assert.AreEqual(1, equipo.CantidadActualUsuarios);
        }

        [TestMethod]
        public void PruebaConstructorVacioFechaCreacion()
        {
            Equipo equipo = new Equipo(_usuario);
            Assert.AreEqual(DateTime.Today, equipo.FechaCreacion);
        }

        [TestMethod]
        public void PruebaConstructorVacioCantidadActualUsuarios()
        {
            Equipo equipo = new Equipo(_usuario);
            Assert.AreEqual(1, equipo.CantidadActualUsuarios);
        }

        [TestMethod]
        public void PruebaConstructorCon3ParametrosNombre()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "Descripción equipo", _usuario);
            Assert.AreEqual("Equipo 1", equipo.Nombre);
        }

        [TestMethod]
        public void PruebaConstructorCon3ParametrosCantidadMaxima()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "Descripción equipo", _usuario);
            Assert.AreEqual(10, equipo.CantidadMaxima);
        }

        [TestMethod]
        public void PruebaConstructorCon3ParametrosDescripcion()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "Descripción equipo", _usuario);
            Assert.AreEqual("Descripción equipo", equipo.Descripcion);
        }

        [TestMethod]
        public void PruebaCrearEquipoConUsuario()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "descrip", _usuario);
            Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            equipo.AgregarUsuario(usuario);
            Assert.AreEqual(2, equipo.CantidadActualUsuarios);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaCrearEquipoSinSerAdmin()
        {
            Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
            Equipo equipo = new Equipo("Equipo 1", 10, "descrip", usuario);
        }

        [TestMethod]
        public void PruebaCrearEquipoYCrearPanel()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "descrip", _usuario);
            Panel panel = new Panel("panel1", "des", equipo.Nombre);
            equipo.AgregarPanel(panel);
            Assert.AreEqual(panel, equipo.Paneles[0]);
        }

        [TestMethod]
        public void PruebaConstructor1ConUsuarioManager()
        {
            Equipo equipo = new Equipo(_usuario);
        }

        [TestMethod]
        public void PruebaConstructor2ConUsuarioManager()
        {
            Equipo equipo = new Equipo("Equipo 1", 3,"descrip" ,_usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaCambiarNombreVacio()
        {
            Usuario usuario = new Usuario(true, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5),"123456Aa=");
            Equipo equipo = new Equipo("Equipo1",10, "Descripcion", usuario);
            equipo.CambiarNombre("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PruebaCambiarCantidadMaximaNumeroNegativo()
        {
            Usuario usuario = new Usuario(true, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5),"123456Aa=");
            Equipo equipo = new Equipo("Equipo1",10, "Descripcion", usuario);
            equipo.CambiarCantidadMaximaDeUsuarios(-5);
        }

        [TestMethod]
        public void PruebaEquipo()
        {
            Equipo equipo = new Equipo();
        }
        
        [TestMethod]
        public void PruebaSetYGetNombre()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.Nombre = "Nuevo Nombre";
            Assert.AreEqual("Nuevo Nombre", equipo.Nombre);
        }

        [TestMethod]
        public void PruebaSetYGetDescripcion()
        {
            Equipo equipo = new Equipo(_usuario);
            equipo.Descripcion = "Nueva Descripcion";
            Assert.AreEqual("Nueva Descripcion", equipo.Descripcion);
        }

        [TestMethod]
        public void PruebaAgregarUsuarioAlEquipo()
        {
            Equipo equipo = new Equipo(_usuario);
            Usuario nuevoUsuario = new Usuario(false, "Carlos", "Perez", "carlos@gmail.com", new DateTime(2000, 1, 1), "123456Aa=");
            equipo.AgregarUsuario(nuevoUsuario);
            Assert.AreEqual(1, equipo.CantidadActualUsuarios);
        }

        [TestMethod]
        public void PruebaSetYGetPaneles()
        {
            Equipo equipo = new Equipo(_usuario);
            Panel panel = new Panel("Panel 1", "Descripcion", equipo.Nombre);
            equipo.AgregarPanel(panel);
            Assert.AreEqual(1, equipo.Paneles.Count);
            Assert.AreEqual(panel, equipo.Paneles[0]);
        }

        [TestMethod]
        public void PruebaEliminarPanelPorNombre()
        {
            Equipo equipo = new Equipo(_usuario);
            Panel panel = new Panel("Panel 1", "Descripcion", equipo.Nombre);
            equipo.AgregarPanel(panel);
            equipo.EliminarPanel("Panel 1");
            Assert.AreEqual(0, equipo.Paneles.Count);
        }

        [TestMethod]
        public void PruebaCantidadUsuariosDespuesDeEliminarUsuario()
        {
            Equipo equipo = new Equipo("Equipo 1", 10, "Descripcion", _usuario);
            Usuario nuevoUsuario = new Usuario(false, "Carlos", "Perez", "carlos@gmail.com", new DateTime(2000, 1, 1), "123456Aa=");
            equipo.AgregarUsuario(nuevoUsuario);
            equipo.Usuarios.Remove(nuevoUsuario);
            Assert.AreEqual(2, equipo.CantidadActualUsuarios);
        }

    }
}