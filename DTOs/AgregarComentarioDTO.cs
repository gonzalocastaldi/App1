using Domain;

namespace DTOs;

public class AgregarComentarioDTO
{
    public string PanelSeleccionado { get; set; }
    public string TareaSeleccionada { get; set; }
    public string ComentarioTitulo { get; set; }
    public UsuarioDTO Usuario { get; set; }
    public string Equipo { get; set; }
    
    public AgregarComentarioDTO(string panelSeleccionado, string tareaSeleccionada, string comentarioTitulo, UsuarioDTO usuario, string equipo)
    {
        PanelSeleccionado = panelSeleccionado;
        TareaSeleccionada = tareaSeleccionada;
        ComentarioTitulo = comentarioTitulo;
        Usuario = usuario;
        Equipo = equipo;
    }
    
    public AgregarComentarioDTO()
    {
    }
}