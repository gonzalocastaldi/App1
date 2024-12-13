namespace Domain;

public class Epica
{
    public int Id { get; set; }
    private string _titulo;
    private Prioridad _prioridad;
    private string _descripcion;
    private DateTime _fechaVencimiento;
    private List<Tarea> _tareas = new();
    private int _esfuerzoEstimado = 0;
    private int _esfuerzoInvertido = 0;
    public Panel Panel { get; set; }
    
    public string Titulo
    {
        get => _titulo;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El título de la épica no puede ser vacío");
            }

            _titulo = value;
        }
    }
    
    public Prioridad Prioridad
    {
        get => _prioridad;
        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new ArgumentException("La prioridad tiene que ser Urgente, Media o Baja");
            }
            _prioridad = value;
        }
    }
    
    public string Descripcion
    {
        get => _descripcion;
        set => _descripcion = value;
    }
    
    public DateTime FechaVencimiento
    {
        get => _fechaVencimiento;
        set
        {
            if (value <= DateTimeWrapper.Now())
            {
                throw new ArgumentException("No se puede crear una épica con fecha de vencimiento anterior a la actual");
            }
            _fechaVencimiento = value;
        }
    }
    
    public List<Tarea> Tareas
    {
        get => _tareas;
    }
    
    public int EsfuerzoEstimado
    {
        get => _esfuerzoEstimado;
        set => _esfuerzoEstimado = value;
    }
    
    public int EsfuerzoInvertido
    {
        get => _esfuerzoInvertido;
        set => _esfuerzoInvertido = value;
    }

    public Epica()
    {
        
    }
    
    public Epica(string titulo, Prioridad prioridad, string descripcion, DateTime fechaVencimiento)
    {
        Titulo = titulo;
        Prioridad = prioridad;
        Descripcion = descripcion;
        FechaVencimiento = fechaVencimiento;
    }
    
    public void AgregarTarea(Tarea tarea)
    {
        _tareas.Add(tarea);
        _esfuerzoEstimado = _esfuerzoEstimado + tarea.EsfuerzoEstimado;
        _esfuerzoInvertido = _esfuerzoInvertido + tarea.EsfuerzoInvertido;
    }

    public void EliminarTarea(Tarea tarea)
    {
        _tareas.Remove(tarea);
        _esfuerzoEstimado = _esfuerzoEstimado - tarea.EsfuerzoEstimado;
        _esfuerzoInvertido = _esfuerzoInvertido - tarea.EsfuerzoInvertido;
    }
}