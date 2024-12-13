using Domain;

namespace DTOs;

public class ComentarioDTO
{
    public string Titulo { get; set; }
    public bool Estado { get; set; }
    public UsuarioDTO UsuarioCreador { get; set; }
    public UsuarioDTO UsuarioResolvedor { get; set; }
    public DateTime FechaResolucion { get; set; }
    public string Descripcion { get; set; }
    public TareaDTO Tarea { get; set; }
    
    public ComentarioDTO(string titulo, bool estado, UsuarioDTO usuarioCreador, UsuarioDTO usuarioResolvedor, DateTime fechaResolucion, string descripcion, TareaDTO tarea)
    {
        Titulo = titulo;
        Estado = estado;
        UsuarioCreador = usuarioCreador;
        UsuarioResolvedor = usuarioResolvedor;
        FechaResolucion = fechaResolucion;
        Descripcion = descripcion;
        Tarea = tarea;
    }
    
    public ComentarioDTO(){}

    public Comentario AEntidad()
    {
        Comentario comentario = new Comentario(Titulo, Estado, UsuarioCreador.AEntidad(), UsuarioResolvedor.AEntidad(), FechaResolucion, Descripcion, Tarea.AEntidad());
        return comentario;
    }
}