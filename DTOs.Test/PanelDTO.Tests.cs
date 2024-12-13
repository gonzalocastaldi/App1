using Domain;

namespace DTOs.Test;

[TestClass]
public class PanelDTO_Tests
{
    [TestMethod]
    public void CrearPanelDto()
    {
        List<TareaDTO> tareas = new List<TareaDTO>();
        List<EpicaDTO> epicas = new List<EpicaDTO>();
        PanelDTO panel = new PanelDTO("Panel1", "Descripcion", tareas, epicas, "equipo1");
        Assert.AreEqual("Panel 1", panel.Nombre);
    }
    
    [TestMethod]
    public void DePanelDTOAPanel()
    {
        List<TareaDTO> tareas = new List<TareaDTO>();
        List<EpicaDTO> epicas = new List<EpicaDTO>();
        PanelDTO panel = new PanelDTO("Panel1", "Descripcion", tareas, epicas, "equipo1");
        Panel panelEntidad = panel.AEntidad();
        Assert.AreEqual("Panel1", panelEntidad.Nombre);
    }
}