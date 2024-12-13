namespace Domain.Test;

[TestClass]
public class Notificacion_Tests
{
    [TestMethod]
    public void CrearNotificacion()
    {
        Notificacion notificacion = new Notificacion();
        Assert.IsNotNull(notificacion);
    }

    [TestMethod]
    public void AgregarNotificacionAUsuario()
    {
        Tarea tarea = new Tarea();
        Comentario comentario = new Comentario("titulo", true, new Usuario(), new Usuario(), new DateTime(2025, 11, 11), "descripcion", tarea);
        Notificacion notificacion = new Notificacion(comentario);
        Usuario usuario = new Usuario();
        usuario.Notificaciones.Add(notificacion);
        Assert.AreEqual(1, usuario.Notificaciones.Count);
    }
    
    [TestMethod]
    public void SetYGetId()
    {
        Notificacion notificacion = new Notificacion();
        int expectedId = 1;
        notificacion.Id = expectedId;
        Assert.AreEqual(expectedId, notificacion.Id);
    }

    [TestMethod]
    public void SetYGetUsuarioId()
    {
        Notificacion notificacion = new Notificacion();
        int expectedUsuarioId = 10;
        notificacion.UsuarioId = expectedUsuarioId;
        Assert.AreEqual(expectedUsuarioId, notificacion.UsuarioId);
    }

    [TestMethod]
    public void SetYGetUsuario()
    {
        Notificacion notificacion = new Notificacion();
        Usuario expectedUsuario = new Usuario();
        notificacion.Usuario = expectedUsuario;
        Assert.AreEqual(expectedUsuario, notificacion.Usuario);
    }

    [TestMethod]
    public void ConstructorComentario()
    {
        Comentario expectedComentario = new Comentario();
        Notificacion notificacion = new Notificacion(expectedComentario);
        Assert.AreEqual(expectedComentario, notificacion.Comentario);
    }

    [TestMethod]
    public void SetYGetComentario()
    {
        Notificacion notificacion = new Notificacion();
        Comentario expectedComentario = new Comentario();
        notificacion.Comentario = expectedComentario;
        Assert.AreEqual(expectedComentario, notificacion.Comentario);
    }

    [TestMethod]
    public void SetUsuarioNull()
    {
        Notificacion notificacion = new Notificacion();
        notificacion.Usuario = new Usuario();
        notificacion.Usuario = null;
        Assert.IsNull(notificacion.Usuario);
    }

    [TestMethod]
    public void SetComentarioNull()
    {
        Notificacion notificacion = new Notificacion(new Comentario());
        notificacion.Comentario = null;
        Assert.IsNull(notificacion.Comentario);
    }

    [TestMethod]
    public void DefaultConstructorComentarioNull()
    {
        Notificacion notificacion = new Notificacion();
        Assert.IsNull(notificacion.Comentario);
    }
}
