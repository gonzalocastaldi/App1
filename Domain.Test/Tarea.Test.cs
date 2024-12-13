namespace Domain.Test;

[TestClass]
public class TareaTests
{
    private Equipo equipo;
    private Usuario usuario;

    [TestInitialize]
    public void Setup()
    {
        usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        equipo = new Equipo("equipo", 10, "des", usuario);
    }
    [TestMethod]
    public void CrearTareaVacia()
    {
        Tarea tarea = new Tarea();
        Assert.IsNotNull(tarea);
    }

    [TestMethod]
    public void PruebaAsignarTitulo()
    {
        Tarea tarea = new Tarea();
        tarea.Titulo = "Tarea 1";
        Assert.AreEqual("Tarea 1", tarea.Titulo);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearTareaSinTitulo()
    {
        Tarea tarea = new Tarea();
        tarea.Titulo = "";
    }

    [TestMethod]
    public void PruebaAsignarFechaExpiracion()
    {
        Tarea tarea = new Tarea();
        tarea.FechaExpiracion = new DateTime(2025, 11, 11);
        Assert.AreEqual(new DateTime(2025, 11, 11), tarea.FechaExpiracion);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearTareaConFechaExpiracionPasada()
    {
        Tarea tarea = new Tarea();
        tarea.FechaExpiracion = new DateTime(2005, 11, 11);
    }

    [TestMethod]
    public void PruebaAsignarDescripcion()
    {
        Tarea tarea = new Tarea();
        tarea.Descripcion = "Descripcion";
        Assert.AreEqual("Descripcion", tarea.Descripcion);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CrearTareaConDescripcionVacia()
    {
        Tarea tarea = new Tarea();
        tarea.Descripcion = "";
    }

    [TestMethod]
    public void PruebaAsignarPrioridad()
    {
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel, panel);
        Assert.AreEqual(Prioridad.Urgente, tarea1.Prioridad);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaAsignarPrioridadNoValida()
    {
        Tarea tarea = new Tarea();
        tarea.Prioridad = (Prioridad)9;
    }

    [TestMethod]
    public void CrearTareaConConstructor()
    {
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel, panel);
        Assert.AreEqual("titulo1", tarea1.Titulo);
    }

    [TestMethod]
    public void CrearTareaConComentario()
    {
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel, panel);
        Usuario usuario1 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        Usuario usuario2 = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        Comentario comentario = new Comentario("titulo", false, usuario1, usuario2, new DateTime(2024, 12, 23), "comentario", tarea1);
        tarea1.AgregarComentario(comentario);
        Assert.AreEqual(1, tarea1.Comentarios.Count);
    }

    [TestMethod]
    public void AgregarTareaAPanel()
    {
        Panel panel2 = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea1 = new Tarea("titulo1", new DateTime(2024, 12, 23), "desc", 2, panel2, panel2);
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        panel.AgregarTarea(tarea1);
        Assert.AreEqual(1, panel.Tareas.Count);
    }
    
    [TestMethod]
    public void PruebaSetYGetId()
    {
        Tarea tarea = new Tarea();
        tarea.Id = 1;
        Assert.AreEqual(1, tarea.Id);
    }
    
    [TestMethod]
    public void PruebaSetYGetPanelActual()
    {
        Panel panel = new Panel("panel1", "des", equipo.Nombre);
        Tarea tarea = new Tarea();
        tarea.PanelActual = panel;
        Assert.AreEqual(panel, tarea.PanelActual);
    }
}