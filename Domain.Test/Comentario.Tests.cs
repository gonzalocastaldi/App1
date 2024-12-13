namespace Domain.Test
{
    [TestClass]
    public class ComentarioTests
    {
        [TestMethod]
        public void PruebaCreacionComentario()
        {
            Tarea tarea = new Tarea();
            Usuario user1 = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
                "AAAAaAAAAA1_");
            Usuario user2 = new Usuario(false, "Juan2", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
                "AAAAaAAAAA1_");
            Comentario comm = new Comentario("titulo1", true, user1, user2, new DateTime(2024, 5, 3),
                "Comentario de prueba", tarea);
            Assert.IsNotNull(comm);
        }

        [TestMethod]
        public void PruebaSetYGetTitulo()
        {
            Comentario comentario = new Comentario();
            comentario.Titulo = "Nuevo Titulo";
            Assert.AreEqual("Nuevo Titulo", comentario.Titulo);
        }

        [TestMethod]
        public void PruebaSetYGetEstado()
        {
            Comentario comentario = new Comentario();
            comentario.Estado = true;
            Assert.IsTrue(comentario.Estado);
        }

        [TestMethod]
        public void PruebaSetYGetUsuarioCreador()
        {
            Comentario comentario = new Comentario();
            Usuario user = new Usuario();
            comentario.UsuarioCreador = user;
            Assert.AreEqual(user, comentario.UsuarioCreador);
        }

        [TestMethod]
        public void PruebaSetYGetUsuarioResolvedor()
        {
            Comentario comentario = new Comentario();
            Usuario user = new Usuario();
            comentario.UsuarioResolvedor = user;
            Assert.AreEqual(user, comentario.UsuarioResolvedor);
        }

        [TestMethod]
        public void PruebaSetYGetFechaResolucion()
        {
            Comentario comentario = new Comentario();
            DateTime fecha = new DateTime(2024, 12, 25);
            comentario.FechaResolucion = fecha;
            Assert.AreEqual(fecha, comentario.FechaResolucion);
        }

        [TestMethod]
        public void PruebaSetYGetDescripcion()
        {
            Comentario comentario = new Comentario();
            comentario.Descripcion = "Nueva descripcion";
            Assert.AreEqual("Nueva descripcion", comentario.Descripcion);
        }

        [TestMethod]
        public void PruebaSetYGetTarea()
        {
            Comentario comentario = new Comentario();
            Tarea tarea = new Tarea();
            comentario.Tarea = tarea;
            Assert.AreEqual(tarea, comentario.Tarea);
        }

        [TestMethod]
        public void PruebaConstructorConParametros()
        {
            Tarea tarea = new Tarea();
            Usuario user1 = new Usuario();
            Usuario user2 = new Usuario();
            DateTime fecha = new DateTime(2024, 5, 3);
            Comentario comentario = new Comentario("Titulo", true, user1, user2, fecha, "Descripcion", tarea);

            Assert.AreEqual("Titulo", comentario.Titulo);
            Assert.IsTrue(comentario.Estado);
            Assert.AreEqual(user1, comentario.UsuarioCreador);
            Assert.AreEqual(user2, comentario.UsuarioResolvedor);
            Assert.AreEqual(fecha, comentario.FechaResolucion);
            Assert.AreEqual("Descripcion", comentario.Descripcion);
            Assert.AreEqual(tarea, comentario.Tarea);
        }

    }
}