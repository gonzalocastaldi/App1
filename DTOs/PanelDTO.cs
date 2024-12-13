using Domain;

namespace DTOs;

public class PanelDTO
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public List<TareaDTO> Tareas { get; set; }
    public List<EpicaDTO> Epicas { get; set; }
    public string EquipoAlQuePertenece { get; set; }
    
    public PanelDTO(string nombre, string descripcion, List<TareaDTO> tareas, List<EpicaDTO> epicas, string equipo)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Tareas = tareas;
        Epicas = epicas;
        EquipoAlQuePertenece = equipo;
    }
    
    public PanelDTO(){}
    
    public Panel AEntidad()
    {
        Panel panel = new Panel(Nombre, Descripcion, EquipoAlQuePertenece);
        foreach (var tarea in Tareas)
        {
            panel.Tareas.Add(tarea.AEntidad());
        }
        foreach (var epica in Epicas)
        {
            panel.Epicas.Add(epica.AEntidad());
        }
        return panel;
    }
}