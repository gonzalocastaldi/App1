using DataAccess;
using Domain;

namespace BusinessLogic.Test;

[TestClass]
public class PapeleraLogic_Tests
{
    private RepositorioEquipo _repositorioEquipo;
    private RepositorioUsuario _repositorioUsuario;
    private ContextoSql _context;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();
    private EquipoLogic equipoLogic;
    private PanelLogic panelLogic;
    private PapeleraLogic papeleraLogic;
    private Usuario usuario;
    private Panel panel;
    private Tarea tarea;
    private Papelera papelera;

    [TestInitialize]
    public void Setup()
    {
        _context = _contextFactory.CrearContextoDb();
        _repositorioEquipo = new RepositorioEquipo(_context);
        equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        papeleraLogic = new PapeleraLogic(_repositorioEquipo, equipoLogic, panelLogic);
        usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        panel = new Panel("Panel 1", "des", "equipo1");
        tarea = new Tarea();
        papelera = usuario.Papelera;
    }

    [TestMethod]
    public void AgregarPanelAPapelera()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        Assert.AreEqual(1, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void ObtenerCantidadObjetosEnPapelera()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        Assert.AreEqual(1, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void ObtenerCanitdadMaximaObjetosEnPapelera()
    {
        Assert.AreEqual(10, papeleraLogic.ObtenerCantidadMaximaObjetos(papelera));
    }

    [TestMethod]
    public void AgregarPanelAPapeleraYComprobarPanelEnLista()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        var paneles = papeleraLogic.ListaPaneles(papelera);
        Assert.IsTrue(paneles.Contains(panel));
    }

    [TestMethod]
    public void AgregarTareaAPapelera()
    {
        papeleraLogic.AgregarTareaAPapelera(tarea, papelera);
        Assert.AreEqual(1, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void AgregarTareaAPapeleraYComprobarTareaEnLista()
    {
        papeleraLogic.AgregarTareaAPapelera(tarea, papelera);
        var tareas = papeleraLogic.ListaTareas(papelera);
        Assert.IsTrue(tareas.Contains(tarea));
    }

    [TestMethod]
    public void VaciarPapelera()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        papeleraLogic.AgregarTareaAPapelera(tarea, papelera);
        papeleraLogic.VaciarPapelera(papelera);
        Assert.AreEqual(0, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void AgregarPanelAPapelera_ComprobarCantidadObjetos()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        Assert.AreEqual(1, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void AgregarTareaAPapelera_ComprobarCantidadObjetos()
    {
        papeleraLogic.AgregarTareaAPapelera(tarea, papelera);
        Assert.AreEqual(1, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

    [TestMethod]
    public void VaciarPapelera_ComprobarListaVacia()
    {
        papeleraLogic.AgregarPanelAPapelera(panel, papelera);
        papeleraLogic.AgregarTareaAPapelera(tarea, papelera);
        papeleraLogic.VaciarPapelera(papelera);
        Assert.AreEqual(0, papeleraLogic.ListaPaneles(papelera).Count);
        Assert.AreEqual(0, papeleraLogic.ListaTareas(papelera).Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RestaurarPanel_ComprobarExcepcion_EquipoNoExiste()
    {
        Panel panelInexistente = new Panel("Panel Inexistente", "desc", "Equipo Inexistente");
        papeleraLogic.RestaurarPanel(panelInexistente, papelera, usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RestaurarTarea_ComprobarExcepcion_EquipoNoExiste()
    {
        Tarea tareaInexistente = new Tarea { PanelOriginal = new Panel("Panel", "desc", "Equipo Inexistente") };
        papeleraLogic.RestaurarTarea(tareaInexistente, papelera, usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RestaurarTarea_ComprobarExcepcion_PanelNoExiste()
    {
        Equipo equipo = new Equipo("Equipo", 10, "Descripcion", usuario);
        _repositorioEquipo.Agregar(equipo);

        Tarea tareaSinPanel = new Tarea
        {
            PanelOriginal = new Panel("Panel Inexistente", "desc", equipo.Nombre)
        };

        papeleraLogic.RestaurarTarea(tareaSinPanel, papelera, usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RestaurarTarea_ComprobarExcepcion_TareaYaExiste()
    {
        Equipo equipo = new Equipo("Equipo1", 10, "Descripcion", usuario);
        _repositorioEquipo.Agregar(equipo);
        Panel panelEquipo = new Panel("Panel1", "Descripcion", equipo.Nombre);
        equipo.AgregarPanel(panelEquipo);
        Tarea tareaDuplicada = new Tarea { PanelOriginal = panelEquipo };
        panelEquipo.Tareas.Add(tareaDuplicada);
        papeleraLogic.AgregarTareaAPapelera(tareaDuplicada, papelera);
        papeleraLogic.RestaurarTarea(tareaDuplicada, papelera, usuario);
    }

    [TestMethod]
    public void ObtenerCantidadObjetosEnPapelera_SinObjetos()
    {
        Assert.AreEqual(0, papeleraLogic.ObtenerCantidadObjetosEnPapelera(papelera));
    }

}
