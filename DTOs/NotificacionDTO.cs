using Domain;

namespace DTOs;

public class NotificacionDTO
{
    public ComentarioDTO Comentario { get; set; }
    
    public NotificacionDTO(ComentarioDTO comentario)
    {
        Comentario = comentario;
    }
    
    public NotificacionDTO(){}
    
    public Notificacion AEntidad()
    {
        return new Notificacion(Comentario.AEntidad());
    }
}