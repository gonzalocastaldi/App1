namespace DTOs.Test;

[TestClass]
public class NotificacionDTO_Tests
{
    [TestMethod]
    public void CrearNotificacionDto()
    {
        NotificacionDTO notificacion = new NotificacionDTO();
        Assert.IsNotNull(notificacion);
    }
}