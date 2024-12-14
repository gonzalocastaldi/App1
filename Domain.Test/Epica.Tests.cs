namespace Domain.Test;

[TestClass]
public class Epica_Tests
{
    [TestMethod]
    public void CrearEpicaVacia()
    {
        Epica epica = new Epica();
        Assert.IsNotNull(epica);
    }

    [TestMethod]
    public void CrearEpicaConAtributos()
    {
        Epica epica = new Epica("titulo", Prioridad.Baja, "descripcion", new DateTime(2025, 11, 11));
        Assert.AreEqual("titulo", epica.Titulo);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaCrearEpicaConTituloVacio()
    {
        Epica epica = new Epica("", Prioridad.Urgente, "desc", new DateTime(2025, 12, 31));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaCrearEpicaConPrioridadInvalida()
    {
        Epica epica = new Epica("titulo", (Prioridad)4, "desc", new DateTime(2025, 12, 31));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaCrearEpicaConFechaVencimientoInvalida()
    {
        Epica epica = new Epica("titulo", Prioridad.Urgente, "desc", new DateTime(2020, 12, 31));
    }

    [TestMethod]
    public void PruebaAgregarTareaSumaEsfuerzoEstimadoYEsfuerzoInvertido()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("equipo1", 10, "des", usuario);
        Panel? panel = new Panel("panel", "desc", equipo.Nombre);
        Epica epica = new Epica("titulo", Prioridad.Urgente, "desc", new DateTime(2026, 12, 31));
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel, panel, 20);
        tarea1.EsfuerzoInvertido = 25;
        epica.AgregarTarea(tarea1);
        Assert.AreEqual(20, epica.EsfuerzoEstimado);
        Assert.AreEqual(25, epica.EsfuerzoInvertido);
        Assert.AreEqual(1, epica.Tareas.Count);
    }

    [TestMethod]
    public void PruebaSetAndGetTitulo()
    {
        Epica epica = new Epica();
        epica.Titulo = "Nuevo Titulo";
        Assert.AreEqual("Nuevo Titulo", epica.Titulo);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaSetTituloVacio()
    {
        Epica epica = new Epica();
        epica.Titulo = "";
    }

    [TestMethod]
    public void PruebaSetAndGetDescripcion()
    {
        Epica epica = new Epica();
        epica.Descripcion = "Nueva Descripcion";
        Assert.AreEqual("Nueva Descripcion", epica.Descripcion);
    }

    [TestMethod]
    public void PruebaSetAndGetFechaVencimiento()
    {
        Epica epica = new Epica();
        DateTime fecha = new DateTime(2025, 11, 11);
        epica.FechaVencimiento = fecha;
        Assert.AreEqual(fecha, epica.FechaVencimiento);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaSetFechaVencimientoInvalida()
    {
        Epica epica = new Epica();
        epica.FechaVencimiento = new DateTime(2020, 11, 11);
    }

    [TestMethod]
    public void PruebaSetAndGetPrioridad()
    {
        Epica epica = new Epica();
        epica.Prioridad = Prioridad.Media;
        Assert.AreEqual(Prioridad.Media, epica.Prioridad);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaSetPrioridadInvalida()
    {
        Epica epica = new Epica();
        epica.Prioridad = (Prioridad)4;
    }

    [TestMethod]
    public void PruebaEliminarTareaRestaEsfuerzoEstimadoYEsfuerzoInvertido()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3),
            "123456Aa=");
        Equipo equipo = new Equipo("equipo1", 10, "des", usuario);
        Panel? panel = new Panel("panel", "desc", equipo.Nombre);
        Epica epica = new Epica("titulo", Prioridad.Urgente, "desc", new DateTime(2026, 12, 31));
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel, panel, 20);
        tarea1.EsfuerzoInvertido = 25;
        epica.AgregarTarea(tarea1);
        epica.EliminarTarea(tarea1);
        Assert.AreEqual(0, epica.EsfuerzoEstimado);
        Assert.AreEqual(0, epica.EsfuerzoInvertido);
        Assert.AreEqual(0, epica.Tareas.Count);
    }

}