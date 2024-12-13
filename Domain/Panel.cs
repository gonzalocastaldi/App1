namespace Domain;

public class Panel
{
    public int Id { get; set; }
    private string _nombre;
    private string _descripcion;
    private List <Tarea> _tareas = new List<Tarea>();
    private List <Epica> _epicas = new List<Epica>();
    private string _equipoAlQuePertenece;
    public int EquipoId { get; set; }
    public Equipo Equipo { get; set; }
    public int? PapeleraId { get; set; }
    
    
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El nombre del panel no puede ser vacío");
            }

            _nombre = value;
        }
    }
    public string Descripcion
    {
        get => _descripcion;
        set => _descripcion = value;
    }
    public List<Tarea> Tareas
    {
        get => _tareas;
    }
    
    public List<Epica> Epicas
    {
        get => _epicas;
    }
    
    public void AgregarTarea(Tarea tarea)
    {
        _tareas.Add(tarea);
    }

    public void AgregarEpica(Epica epica)
    {
        _epicas.Add(epica);
    }
    
    public Panel(string nombre, string descripcion, string equipo)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        EquipoAlQuePertenece = equipo;
    }

    public string EquipoAlQuePertenece
    {
        get => _equipoAlQuePertenece;
        set => _equipoAlQuePertenece = value;
    }

    public Panel()
    {
        
    }

    public void EliminarTodasLasTareas()
    {
        _tareas.Clear();
    }

    public void EliminarTarea(Tarea tarea)
    {
        _tareas.Remove(tarea);
    }
    
    public void EliminarEpica(Epica epica)
    {
        _epicas.Remove(epica);
    }
}