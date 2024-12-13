namespace DTOs.Test;

[TestClass]
public class PapeleraDTO_Tests
{
    [TestMethod]
    public void CrearPapeleraDto()
    {
        PapeleraDTO papelera = new PapeleraDTO();
        Assert.IsNotNull(papelera);
    }
}