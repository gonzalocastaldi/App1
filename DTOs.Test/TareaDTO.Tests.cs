using Domain;

namespace DTOs.Test;

[TestClass]
public class TareaDTO_Tests
{
    [TestMethod]
    public void CrearTareaDto()
    {
        PanelDTO panel = new PanelDTO("Panel1", "Descripcion", new List<TareaDTO>(), new List<EpicaDTO>(), "equipo1");
        TareaDTO tarea = new TareaDTO("tarea1", new DateTime(2026, 5, 5),"desc", 1, panel, panel, 2);
        Assert.AreEqual("tarea1", tarea.Titulo);
    }
    
    [TestMethod]
    public void DeTareaDTOATarea()
    {
        PanelDTO panel = new PanelDTO("Panel1", "Descripcion", new List<TareaDTO>(), new List<EpicaDTO>(), "equipo1");
        TareaDTO tarea = new TareaDTO("tarea1", new DateTime(2026, 5, 5),"desc", 1, panel, panel, 2);
        Tarea tareaEntidad = tarea.AEntidad();
        Assert.AreEqual("tarea1", tareaEntidad.Titulo);
    }
}