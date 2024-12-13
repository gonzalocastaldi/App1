namespace Domain;

public class Papelera
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    private const int MaxCantidad = 10;
    private List <Tarea> _tareas = new List <Tarea>();
    private List <Panel> _paneles = new List <Panel>();
    public int CantidadObjetosEnPapelera { get; set; } = 0;
    

    public int CantidadMaximaObjetos
    {
        get => MaxCantidad;
    }
    
    public List<Panel> Paneles
    {
        get => _paneles;
    }

    public void AgregarPanelAPapelera(Panel panel)
    {
        if (CantidadObjetosEnPapelera < MaxCantidad)
        {
            _paneles.Add(panel);
            CantidadObjetosEnPapelera++;
        }
    } 

    public void EliminarPanelDePapelera(Panel panel)
    {
        if (_paneles.Contains(panel))
        {
            _paneles.Remove(panel);
            CantidadObjetosEnPapelera--;
        }
        else
        {
            throw new ArgumentException("Este panel no está en la papelera");
        }
    }

    public List<Tarea> Tareas
    {
        get => _tareas;
    }

    public void AgregarTareaAPapelera(Tarea tarea)
    {
        if (CantidadObjetosEnPapelera < MaxCantidad)
        {
            _tareas.Add(tarea);
            CantidadObjetosEnPapelera++;
        }
    }

    public void EliminarTareaDePapelera(Tarea tarea)
    {
        if (_tareas.Contains(tarea))
        {
            _tareas.Remove(tarea);
            CantidadObjetosEnPapelera--;
        }
        else
        {
            throw new ArgumentException("Esta tarea no está en la papelera");
        }
    }
    
    public void VaciarPapelera()
    {
        while (_paneles.Count > 0)
        {
            EliminarPanelDePapelera(_paneles[0]);
        }

        while (_tareas.Count > 0)
        {
            EliminarTareaDePapelera(_tareas[0]);
        }
    }
}