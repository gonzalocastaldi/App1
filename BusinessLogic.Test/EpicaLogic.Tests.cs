using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic.Test;

[TestClass]
public class EpicaLogic_Tests
{
    private RepositorioEquipo _repositorioEquipo;
    private RepositorioUsuario _repositorioUsuario;
    private EpicaLogic _epicaLogic;
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
    public void CrearEpicaEnPanel()
    {
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        equipoLogic.CrearEquipo("equipo1", 10, "asdasdasdasd", usuario);
        Panel? panel = new Panel("panel1", "descripcion1", "equipo1");
        equipoLogic.AgregarPanel("equipo1", panel, usuario);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        //panelLogic.AgregarEpica("equipo1","panel1", epica, usuario);
        Assert.AreEqual(1, panel.Epicas.Count);
    }

    [TestMethod]
    public void CambiarTituloEpica()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.AgregarEpica("equipo1","nombre", epica, usuarioCreador);
        EpicaLogic epicaLogic = new EpicaLogic(_repositorioEquipo);
        CambiarTituloEpicaDTO cambiarTituloEpicaDto =
            new CambiarTituloEpicaDTO("equipo1", "nombre", "nuevoTitulo", "titulo");
        epicaLogic.CambiarTitulo(cambiarTituloEpicaDto);
        Assert.AreEqual("nuevoTitulo", panel.Epicas[0].Titulo);
    }

    [TestMethod]
    public void CambiarDescripcionEpica()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.AgregarEpica("equipo1","nombre", epica, usuarioCreador);
        EpicaLogic epicaLogic = new EpicaLogic(_repositorioEquipo);
        epicaLogic.CambiarDescripcion("equipo1", "nombre", "nuevaDesc", "titulo");
        Assert.AreEqual("nuevaDesc", panel.Epicas[0].Descripcion);
    }

    [TestMethod]
    public void AgregarTareaAEpica()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.AgregarEpica("equipo1","nombre", epica, usuarioCreador);
        EpicaLogic epicaLogic = new EpicaLogic(_repositorioEquipo);
        Tarea tarea = new Tarea("titulo", new DateTime(2025, 11, 11), "descripcion", 1, panel, panel, 10);
        Tarea tarea2 = new Tarea("titulo", new DateTime(2025, 11, 11), "descripcion", 1, panel, panel, 10);
        epicaLogic.AgregarTarea("equipo1", "nombre", "titulo", tarea);
        Assert.AreEqual(1, epica.Tareas.Count);
    }

    [TestMethod]
    public void EliminarEpicaDePanel()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.AgregarEpica("equipo1","nombre", epica, usuarioCreador);
        panelLogic.EliminarEpica("equipo1", "nombre", epica);
        Assert.AreEqual(0, panel.Epicas.Count);
    }

    [TestMethod]
    public void ValidarYGuardarEpica()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.ValidarYGuardarEpica("equipo1", epica, panel, usuarioCreador);
        Assert.AreEqual(1, panel.Epicas.Count);
    }
    
    [TestMethod]
    public void Eliminar_Tarea_De_Epica()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel? panel = new Panel("nombre", "desc", "equipo1");
        equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        PanelLogic panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        //panelLogic.AgregarEpica("equipo1","nombre", epica, usuarioCreador);
        EpicaLogic epicaLogic = new EpicaLogic(_repositorioEquipo);
        Tarea tarea = new Tarea("titulo", new DateTime(2025, 11, 11), "descripcion", 1, panel, panel, 10);
        panelLogic.AgregarTarea("nombre", tarea, "equipo1");
        epicaLogic.AgregarTarea("equipo1", "nombre", "titulo", tarea);
        epicaLogic.EliminarTarea("equipo1", "nombre", "titulo", tarea);
        Assert.AreEqual(0, epica.Tareas.Count);
        Assert.AreEqual(1, panel.Tareas.Count);
    }

}