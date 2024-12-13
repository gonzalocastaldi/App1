namespace Domain;

public class Comentario
{
    public int Id { get; set; }
    private string _titulo;
    private bool _estado;
    private Usuario _usuarioCreador;
    private Usuario? _usuarioResolvedor;
    private DateTime _fechaResolucion;
    private string _descripcion;
    private Tarea _tarea;
    
    public string Titulo
    {
        get => _titulo;
        set => _titulo = value;
    }
    public Tarea Tarea
    {
        get => _tarea;
        set => _tarea = value;
    }
    public bool Estado
    {
        get => _estado;
        set => _estado = value;
    }
    
    public Usuario UsuarioCreador
    {
        get => _usuarioCreador;
        set => _usuarioCreador = value;
    }
    
    public Usuario? UsuarioResolvedor
    {
        get => _usuarioResolvedor;
        set => _usuarioResolvedor = value;
    }
    
    public DateTime FechaResolucion
    {
        get => _fechaResolucion;
        set => _fechaResolucion = value;
    }
    
    public string Descripcion
    {
        get => _descripcion;
        set => _descripcion = value;
    }
    
    public Comentario(string titulo, bool estado, Usuario usuarioCreador, Usuario usuarioResolvedor, DateTime fechaResolucion, string comentario, Tarea tarea)
    {
        Titulo = titulo;
        Estado = estado;
        UsuarioCreador = usuarioCreador;
        UsuarioResolvedor = usuarioResolvedor;
        FechaResolucion = fechaResolucion;
        Descripcion = comentario;
        Tarea = tarea;
    }

    public Comentario()
    {
    }
}