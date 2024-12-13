namespace Domain.Test;

[TestClass]
public class UsuarioTests
{
    [TestMethod]
    public void PruebaCreacionClaseUsuario()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        Assert.IsNotNull(usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoEsValido_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3), "123");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoTieneOchoCaracteres_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "1234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoTieneMayuscula_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoTieneMinuscula_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "AAAAAAAAAA");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoTieneNumero_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "AAAAaAAAAA");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_PassNoTieneCaracterEspecial_Entonces_LanzaExcepcion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "AAAAaAAAAA1");
    }

    [TestMethod]
    public void CrearPapeleraUsuario()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        Assert.IsNotNull(usuario.Papelera);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_FechaNacimientoNoEsValida()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(1880, 5, 3),
            "12345678aA_");
        Assert.IsNotNull(usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_FechaNacimientoNoEsValida2()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2025, 5, 3),
            "12345678aA_");
        Assert.IsNotNull(usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearUsuario_Cuando_EmailNoValido()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez", new DateTime(2008, 5, 3), "12345678aA_");
    }

    [TestMethod]
    public void CrearUsuario_Cuando_EmailValido()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2008, 5, 3),
            "12345678aA_");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ValidarEmailVacio()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "", new DateTime(2008, 5, 3), "12345678aA_");
    }

    [TestMethod]
    public void PruebaSetYGetNombre()
    {
        Usuario usuario = new Usuario();
        usuario.Nombre = "Nuevo Nombre";
        Assert.AreEqual("Nuevo Nombre", usuario.Nombre);
    }

    [TestMethod]
    public void PruebaSetYGetApellido()
    {
        Usuario usuario = new Usuario();
        usuario.Apellido = "Nuevo Apellido";
        Assert.AreEqual("Nuevo Apellido", usuario.Apellido);
    }

    [TestMethod]
    public void PruebaObtenerNombreCompleto()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        Assert.AreEqual("Juan Perez", usuario.NombreCompleto);
    }

    [TestMethod]
    public void PruebaSetYGetEmail()
    {
        Usuario usuario = new Usuario();
        usuario.Email = "nuevoemail@gmail.com";
        Assert.AreEqual("nuevoemail@gmail.com", usuario.Email);
    }

    [TestMethod]
    public void PruebaSetYGetFechaNacimiento()
    {
        Usuario usuario = new Usuario();
        DateTime fecha = new DateTime(2000, 1, 1);
        usuario.FechaNacimiento = fecha;
        Assert.AreEqual(fecha, usuario.FechaNacimiento);
    }

    [TestMethod]
    public void PruebaSetYGetContrasena()
    {
        Usuario usuario = new Usuario();
        usuario.Contrasena = "12345678aA_";
        Assert.AreEqual("12345678aA_", usuario.Contrasena);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaSetContrasenaInvalida()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        usuario.Contrasena = "123";
    }

    [TestMethod]
    public void PruebaSetYGetEsAdmin()
    {
        Usuario usuario = new Usuario(true, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        Assert.IsTrue(usuario.EsAdmin);
    }

    [TestMethod]
    public void PruebaAgregarNotificacion()
    {
        Usuario usuario = new Usuario(false, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        Notificacion notificacion = new Notificacion();
        usuario.Notificaciones.Add(notificacion);
        Assert.AreEqual(1, usuario.Notificaciones.Count);
    }

    [TestMethod]
    public void PruebaSetYGetNotificaciones()
    {
        Usuario usuario = new Usuario(true, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        List<Notificacion> notificaciones = new List<Notificacion>
        {
            new Notificacion(),
            new Notificacion()
        };
        usuario.Notificaciones = notificaciones;
        Assert.AreEqual(2, usuario.Notificaciones.Count);
    }

    [TestMethod]
    public void PruebaSetYGetEquipos()
    {
        Usuario usuario = new Usuario(true, "Juan", "Perez", "juanperez@gmail.com", new DateTime(2004, 5, 3),
            "12345678aA_");
        List<Equipo> equipos = new List<Equipo>
        {
            new Equipo("Equipo 1", 5, "Descripcion", usuario),
            new Equipo("Equipo 2", 10, "Descripcion", usuario)
        };
        usuario.Equipos = equipos;
        Assert.AreEqual(2, usuario.Equipos.Count());
    }

}
