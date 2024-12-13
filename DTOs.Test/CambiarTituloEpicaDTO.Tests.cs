namespace DTOs.Test;

[TestClass]
public class CambiarTituloEpicaDTO_Tests
{
    [TestMethod]
    public void CambiarTituloEpicaDTO_Crear()
    {
        CambiarTituloEpicaDTO cambiarTituloEpicaDTO = new CambiarTituloEpicaDTO("nombreEquipo", "nombrePanel", "nuevoTitulo", "tituloOriginal");
        Assert.AreEqual("nombreEquipo", cambiarTituloEpicaDTO.nombreEquipo);
    }
}