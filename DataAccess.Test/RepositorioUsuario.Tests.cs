using Domain;

namespace DataAccess.Test;

[TestClass]
public class RepositorioUsuario_Tests
{
    private RepositorioUsuario _repositorio;
    private ContextoSql _contexto;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();

    [TestInitialize]
    public void Setup()
    {
        _contexto = _contextFactory.CrearContextoDb();
        _repositorio = new RepositorioUsuario(_contexto);
    }

    [TestMethod]
    public void AgregarUsuario()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        _repositorio.Agregar(usuario2);
        Assert.AreEqual(1, _repositorio.CantidadUsuarios());
    }

    [TestMethod]
    public void BuscarUsuario()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        _repositorio.Agregar(usuario2);
        Assert.AreEqual(usuario2, _repositorio.Buscar("gonzalo@gmail.com"));
    }

    [TestMethod]
    public void EliminarUsuario()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        _repositorio.Agregar(usuario);
        _repositorio.Eliminar(usuario);
        Assert.AreEqual(0, _repositorio.CantidadUsuarios());
    }

    [TestMethod]
    public void ObtenerTodosLosUsuarios()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo2@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Usuario segundoUsuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        _repositorio.Agregar(usuario);
        _repositorio.Agregar(segundoUsuario);
        Assert.AreEqual(2, _repositorio.CantidadUsuarios());
        
    }

}