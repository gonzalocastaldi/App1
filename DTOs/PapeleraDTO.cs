using Domain;

namespace DTOs;

public class PapeleraDTO
{
    public const int MaxCantidad = 10;
    public List<TareaDTO> Tareas { get; set; }
    public List<PanelDTO> Paneles { get; set; }
    public int Cantidad { get; set; }

    public PapeleraDTO()
    {
        
    }
    
    public Papelera AEntidad()
    {
        Papelera papelera = new Papelera();
        foreach (var tarea in Tareas)
        { 
            papelera.AgregarTareaAPapelera(tarea.AEntidad());   
        }

        foreach (var panel in Paneles)
        {
            papelera.AgregarPanelAPapelera(panel.AEntidad());
        }
        return papelera;
    }
}