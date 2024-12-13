namespace Domain;

public class Notificacion
{
    public int Id { get; set; }
    private Comentario _comentario;
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public Comentario Comentario
    {
        get => _comentario;
        set => _comentario = value;
    }
    
    public Notificacion(Comentario comentario)
    {
        _comentario = comentario;
    }
    public Notificacion()
    {
    }
}

