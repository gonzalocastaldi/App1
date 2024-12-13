using Domain;

namespace DataAccess.Test;

[TestClass]
public class RepositorioEquipoTests
{
    private RepositorioEquipo _repositorio;
    private ContextoSql _contexto;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();    
    
    [TestInitialize]
    public void Setup()
    {
        _contexto = _contextFactory.CrearContextoDb();
        _repositorio = new RepositorioEquipo(_contexto);
    }
    
    [TestMethod]
    public void AgregarEquipo()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        Equipo equipo = new Equipo("Equipo1", 4, "desc", usuario2);
        _repositorio.Agregar(equipo);
        Assert.AreEqual(1, _repositorio.CantidadEquipos());
    }

    [TestMethod]
    public void BuscarEquipo()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("Equipo1", 4, "desc", usuario2);
        _repositorio.Agregar(equipo);
        Assert.AreEqual(equipo, _repositorio.Buscar("Equipo1"));
    }

    [TestMethod]
    public void ObtenerCantidadDeEquipos()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("Equipo1", 4, "desc", usuario2);
        Equipo segundoEquipo = new Equipo("Equipo1", 4, "desc", usuario2);
        _repositorio.Agregar(equipo);
        _repositorio.Agregar(segundoEquipo);
        Assert.AreEqual(2, _repositorio.CantidadEquipos());
    }

    [TestMethod]
    public void EliminarEquipo()
    {
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("Equipo1", 4, "desc", usuario2);
        _repositorio.Agregar(equipo);
        _repositorio.Eliminar(equipo);
        Assert.AreEqual(0, _repositorio.CantidadEquipos());
    }

    [TestMethod]
    public void ObtenerTodosLosEquipos()
    {
        List<Equipo> equipos = new List<Equipo>();
        Usuario usuario2 = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("Equipo1", 4, "desc", usuario2);
        Equipo segundoEquipo = new Equipo("Equipo1", 4, "desc", usuario2);
        _repositorio.Agregar(equipo);
        _repositorio.Agregar(segundoEquipo);
        equipos = _repositorio.ObtenerTodosLosEquipos();
        Assert.AreEqual(2, equipos.Count);
    }
}