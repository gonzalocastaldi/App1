namespace Domain.Test;

[TestClass]
public class DateTimeWrapperTests
{

    [TestMethod]
    public void ValidarTimeWrapper()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        Equipo equipo = new Equipo("equipo1", 10, "des", usuario);
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2025, 12, 23), "desc", 1, panel, panel);
        panel.AgregarTarea(tarea1);
        DateTimeWrapper.FixedDate = new DateTime(2010, 7, 2);
        var fechaPasada = new DateTime(2020, 9, 5);
        tarea1.FechaExpiracion = fechaPasada;
        Assert.AreEqual(tarea1.FechaExpiracion, fechaPasada);
    }
}
    
