using Domain;

namespace DTOs;

public class TareaDTO
{
    public string Titulo { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public string Descripcion { get; set; }
    public int Prioridad { get; set; }
    public PanelDTO PanelActual { get; set; }
    public PanelDTO PanelOriginal { get; set; }
    public List<ComentarioDTO> Comentarios { get; set; }
    public int EsfuerzoEstimado { get; set; }
    public int EsfuerzoInvertido { get; set; }
    
    public TareaDTO(string titulo, DateTime fechaExpiracion, string descripcion, int prioridad, PanelDTO panelActual, PanelDTO panelOriginal, int esfuerzoEstimado)
    {
        Titulo = titulo;
        FechaExpiracion = fechaExpiracion;
        Descripcion = descripcion;
        Prioridad = prioridad;
        PanelActual = panelActual;
        PanelOriginal = panelOriginal;
        EsfuerzoEstimado = esfuerzoEstimado;
    }
    
    public TareaDTO(){}

    public Tarea AEntidad()
    {
        Tarea tarea = new Tarea(Titulo, FechaExpiracion, Descripcion, Prioridad, PanelActual.AEntidad(), PanelOriginal.AEntidad(), EsfuerzoEstimado);
        return tarea;
    }
}