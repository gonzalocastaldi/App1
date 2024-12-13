namespace Domain;

public enum Prioridad
{
    Baja,
    Media,
    Urgente
}
public class Tarea
{
    public int Id { get; set; }
    private string _titulo;
    private DateTime _fechaExpiracion;
    private string _descripcion;
    private Prioridad _prioridad;
    private Panel _panelActual;
    private Panel _panelOriginal;
    private List <Comentario> _comentarios = new List<Comentario>();
    private int _esfuerzoEstimado;
    private int _esfuerzoInvertido = 0;
    public int? EpicaId { get; set; }
    public Epica? Epica { get; set; }
    public int? PapeleraId { get; set; }

    public string Titulo
    {
        get => _titulo;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("La tarea tiene que tener título");
            }
            _titulo = value;
        }
    }

    public DateTime FechaExpiracion
    {
        get => _fechaExpiracion;
        set
        {
            if (value <= DateTimeWrapper.Now())
            {
                throw new ArgumentException("No se puede crear una tarea ya expirada");
            }
            _fechaExpiracion = value;
        }
    }

    public string Descripcion
    {
        get => _descripcion;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Describa la tarea por favor");
            }
            _descripcion = value;
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
    
    public Panel PanelActual
    {
        get => _panelActual;
        set => _panelActual = value;
    }
    
    public Panel PanelOriginal
    {
        get => _panelOriginal;
        set => _panelOriginal = value;
    }
    
    public List<Comentario> Comentarios
    {
        get => _comentarios;
    }
    
    public int EsfuerzoEstimado
    {
        get => _esfuerzoEstimado;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("El esfuerzo estimado no puede ser negativo");
            }
            _esfuerzoEstimado = value;
        }
    }
    
    public int EsfuerzoInvertido
    {
        get => _esfuerzoInvertido;
        set => _esfuerzoInvertido = value;
    }

    public void AgregarComentario(Comentario comentario)
    {
        _comentarios.Add(comentario);
    }

    public Tarea(string titulo, DateTime fechaExpiracion, string descripcion, int prioridad, Panel panelActual, Panel panelOriginal) //sacar
    {
        Titulo = titulo;
        FechaExpiracion = fechaExpiracion;
        Descripcion = descripcion;
        Prioridad = (Prioridad)prioridad;
        _panelActual = panelActual;
        _panelOriginal = panelOriginal;
        _comentarios = new List<Comentario>();
    }
    
    public Tarea(string titulo, DateTime fechaExpiracion, string descripcion, int prioridad, Panel panelActual,
        Panel panelOriginal, int esfuerzoEstimado)
    {
        Titulo = titulo;
        FechaExpiracion = fechaExpiracion;
        Descripcion = descripcion;
        Prioridad = (Prioridad)prioridad;
        _panelActual = panelActual;
        _panelOriginal = panelOriginal;
        _comentarios = new List<Comentario>();
        EsfuerzoEstimado = esfuerzoEstimado;
    }
    
    public Tarea() {}
}