using Domain;

namespace DTOs;

public class EpicaDTO
{
    public string Titulo { get; set; }
    public int Prioridad { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public List<TareaDTO> Tareas { get; set; }
    public int EsfuerzoEstimado { get; set; }
    public int EsfuerzoInvertido { get; set; }
    
    public EpicaDTO(string titulo, int prioridad, string descripcion, DateTime fechaVencimiento, List<TareaDTO> tareas, int esfuerzoEstimado, int esfuerzoInvertido)
    {
        Titulo = titulo;
        Prioridad = prioridad;
        Descripcion = descripcion;
        FechaVencimiento = fechaVencimiento;
        Tareas = tareas;
        EsfuerzoEstimado = esfuerzoEstimado;
        EsfuerzoInvertido = esfuerzoInvertido;
    }
    
    public EpicaDTO(){}
    
    public Epica AEntidad()
    {
        Epica epica = new Epica(Titulo, (Prioridad)Prioridad, Descripcion, FechaVencimiento);
        foreach (var tarea in Tareas)
        {
            epica.Tareas.Add(tarea.AEntidad());
        }
        return epica;
    }
}