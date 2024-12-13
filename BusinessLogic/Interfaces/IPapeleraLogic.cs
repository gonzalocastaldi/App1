using Domain;

namespace BusinessLogic.Interfaces;

public interface IPapeleraLogic
{
    public void AgregarPanelAPapelera(Panel panel, Papelera papelera);
    public int ObtenerCantidadObjetosEnPapelera(Papelera papelera);
    public int ObtenerCantidadMaximaObjetos(Papelera papelera);
    public List<Panel> ListaPaneles(Papelera papelera);
    public void EliminarPanelDePapelera(Panel panel, Papelera papelera);
    public void AgregarTareaAPapelera(Tarea tarea, Papelera papelera);
    public List<Tarea> ListaTareas(Papelera papelera);
    public void EliminarTareaDePapelera(Tarea tarea, Papelera papelera);
    public void VaciarPapelera(Papelera papelera);
    public void RestaurarPanel(Panel panel, Papelera papelera, Usuario usuario);
    public void RestaurarTarea(Tarea tarea, Papelera papelera, Usuario usuario);

}