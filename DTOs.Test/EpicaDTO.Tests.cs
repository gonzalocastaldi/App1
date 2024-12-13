using Domain;

namespace DTOs.Test;

[TestClass]
public class EpicaDTO_Tests
{
    [TestMethod]
    public void CrearEpicaDto()
    {
        List<TareaDTO> tareas = new List<TareaDTO>();
        EpicaDTO epicadto = new EpicaDTO("epica1", 1, "desc", new DateTime(2025, 12, 12), tareas, 1, 1);
        Assert.AreEqual("epica1", epicadto.Titulo);
    }
    
    [TestMethod]
    public void DeEpicaDTOAEpica()
    {
        List<TareaDTO> tareas = new List<TareaDTO>();
        EpicaDTO epicadto = new EpicaDTO("epica1", 1, "desc", new DateTime(2025, 12, 12), tareas, 1, 1);
        Epica epicaEntidad = epicadto.AEntidad();
        Assert.AreEqual("epica1", epicaEntidad.Titulo);
    }
}