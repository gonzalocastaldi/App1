namespace Domain.Test;

[TestClass]
public class PapeleraTests
{
    
    private Papelera papelera;
    private Equipo equipo;
    private Usuario usuario;
    
    [TestInitialize]
    public void Setup()
    {
        papelera = new Papelera();
        usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        equipo = new Equipo("equipo", 10, "des", usuario);
    }
    
    [TestMethod]
    public void CrearPapeleraVacia()
    {
        Assert.IsNotNull(papelera);
    }

    [TestMethod]
    public void AgregarTareaAPapelera()
    {
        Panel panel = new Panel("panel1", "Descripcion", equipo.Nombre);
        Tarea tarea = new Tarea("Tarea", new DateTime(2025, 11, 11), "Descripcion", 0, panel, panel);
        papelera.AgregarTareaAPapelera(tarea);
        Assert.AreEqual(1, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void AgregarPanelAPapelera()
    {
        Panel panel = new Panel("Panel", "Descripcion", equipo.Nombre);
        papelera.AgregarPanelAPapelera(panel);
        Assert.AreEqual(1, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void EliminarTareaExistente()
    {
        Panel panel = new Panel("panel1", "Descripcion", equipo.Nombre);
        Tarea tarea = new Tarea("Tarea", new DateTime(2025, 11, 11), "Descripcion", 0, panel, panel);
        papelera.AgregarTareaAPapelera(tarea);
        papelera.EliminarTareaDePapelera(tarea);
        Assert.AreEqual(0, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EliminarTareaNoExistente()
    {
        Panel panel = new Panel("panel1", "Descripcion", equipo.Nombre);
        Tarea tarea = new Tarea("Tarea", new DateTime(2025, 11, 11), "Descripcion", 0, panel, panel);
        papelera.EliminarTareaDePapelera(tarea);
    }

    [TestMethod]
    public void EliminarPanelExistente()
    {
        Panel panel = new Panel("Panel", "Descripcion", equipo.Nombre);
        papelera.AgregarPanelAPapelera(panel);
        papelera.EliminarPanelDePapelera(panel);
        Assert.AreEqual(0, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EliminarPanelNoExistente()
    {
        Panel panel = new Panel("Panel", "Descripcion", equipo.Nombre);
        papelera.EliminarPanelDePapelera(panel);
    }

    [TestMethod]
    public void AgregarTareaConPapeleraLlena()
    {
        Panel panel = new Panel("panel1", "Descripcion", equipo.Nombre);
        Tarea tarea1 = new Tarea("Tarea1", new DateTime(2025, 11, 11), "Descripcion1", 0, panel, panel);
        Tarea tarea2 = new Tarea("Tarea2", new DateTime(2025, 11, 11), "Descripcion2", 0, panel, panel);
        Tarea tarea3 = new Tarea("Tarea3", new DateTime(2025, 11, 11), "Descripcion3", 0, panel, panel);
        Tarea tarea4 = new Tarea("Tarea4", new DateTime(2025, 11, 11), "Descripcion4", 0, panel, panel);
        Tarea tarea5 = new Tarea("Tarea5", new DateTime(2025, 11, 11), "Descripcion5", 0, panel, panel);
        Tarea tarea6 = new Tarea("Tarea6", new DateTime(2025, 11, 11), "Descripcion6", 0, panel, panel);
        Tarea tarea7 = new Tarea("Tarea7", new DateTime(2025, 11, 11), "Descripcion7", 0, panel, panel);
        Tarea tarea8 = new Tarea("Tarea8", new DateTime(2025, 11, 11), "Descripcion8", 0, panel, panel);
        Tarea tarea9 = new Tarea("Tarea9", new DateTime(2025, 11, 11), "Descripcion9", 0, panel, panel);
        Tarea tarea10 = new Tarea("Tarea10", new DateTime(2025, 11, 11), "Descripcion10", 0, panel, panel);
        Tarea tarea11 = new Tarea("Tarea11", new DateTime(2025, 11, 11), "Descripcion11", 0, panel, panel);
        papelera.AgregarTareaAPapelera(tarea1);
        papelera.AgregarTareaAPapelera(tarea2);
        papelera.AgregarTareaAPapelera(tarea3);
        papelera.AgregarTareaAPapelera(tarea4);
        papelera.AgregarTareaAPapelera(tarea5);
        papelera.AgregarTareaAPapelera(tarea6);
        papelera.AgregarTareaAPapelera(tarea7);
        papelera.AgregarTareaAPapelera(tarea8);
        papelera.AgregarTareaAPapelera(tarea9);
        papelera.AgregarTareaAPapelera(tarea10);
        papelera.AgregarTareaAPapelera(tarea11);
        Assert.AreEqual(10, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void AgregarPanelConPapeleraLlena()
    {
        Panel panel1 = new Panel("Panel1", "Descripcion1", equipo.Nombre);
        Panel panel2 = new Panel("Panel2", "Descripcion2", equipo.Nombre);
        Panel panel3 = new Panel("Panel3", "Descripcion3", equipo.Nombre);
        Panel panel4 = new Panel("Panel4", "Descripcion4", equipo.Nombre);
        Panel panel5 = new Panel("Panel5", "Descripcion5", equipo.Nombre);
        Panel panel6 = new Panel("Panel6", "Descripcion6", equipo.Nombre);
        Panel panel7 = new Panel("Panel7", "Descripcion7", equipo.Nombre);
        Panel panel8 = new Panel("Panel8", "Descripcion8", equipo.Nombre);
        Panel panel9 = new Panel("Panel9", "Descripcion9", equipo.Nombre);
        Panel panel10 = new Panel("Panel10", "Descripcion10", equipo.Nombre);
        Panel panel11 = new Panel("Panel11", "Descripcion11", equipo.Nombre);
        papelera.AgregarPanelAPapelera(panel1);
        papelera.AgregarPanelAPapelera(panel2);
        papelera.AgregarPanelAPapelera(panel3);
        papelera.AgregarPanelAPapelera(panel4);
        papelera.AgregarPanelAPapelera(panel5);
        papelera.AgregarPanelAPapelera(panel6);
        papelera.AgregarPanelAPapelera(panel7);
        papelera.AgregarPanelAPapelera(panel8);
        papelera.AgregarPanelAPapelera(panel9);
        papelera.AgregarPanelAPapelera(panel10);
        papelera.AgregarPanelAPapelera(panel11);
        Assert.AreEqual(10, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void AgregarPanelYTareaAPapelera()
    {
        Panel panel2 = new Panel("panel1", "Descripcion", equipo.Nombre);
        Tarea tarea = new Tarea("Tarea", new DateTime(2025, 11, 11), "Descripcion", 0, panel2, panel2);
        Panel panel= new Panel("Panel", "Descripcion", equipo.Nombre);
        papelera.AgregarTareaAPapelera(tarea);
        papelera.AgregarPanelAPapelera(panel);
        Assert.AreEqual(2, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void VaciarPapelera()
    {
        Panel panel = new Panel("Panel", "Descripcion", equipo.Nombre);
        papelera.AgregarPanelAPapelera(panel);
        papelera.VaciarPapelera();
        Assert.AreEqual(0, papelera.CantidadObjetosEnPapelera);
    }

    [TestMethod]
    public void PruebaCantidadMaximaObjetos()
    {
        Assert.AreEqual(10, papelera.CantidadMaximaObjetos);
    }
}
