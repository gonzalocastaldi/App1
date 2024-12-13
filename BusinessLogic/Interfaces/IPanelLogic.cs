using Domain;

namespace BusinessLogic.Interfaces;

public interface IPanelLogic
{
    public void AgregarTarea(string nombrePanel, Tarea tarea, string nombreEquipo);
    public void CambiarNombre(string nombrePanel, string nuevoNombre, Usuario usuario, string nombreEquipo);
    public void CambiarDescripcion(string nombrePanel, string nuevaDescripcion, Usuario usuario, string nombreEquipo);
    public void EliminarTarea(string nombreEquipo, string nombrePanel, string nombreTarea, Usuario usuario);
    public string? ValidarYGuardarTarea(Tarea tarea, Panel panel, Usuario usuario);
    public void EliminarTareaDeFormaPermanente(string nombreEquipo, string nombrePanel, string nombreTarea,
        Usuario usuario);
    public void AgregarEpica(string nombreEquipo, string nombrePanel, Epica epica, Usuario usuario);
    public void EliminarEpica(string nombreEquipo, string nombrePanel, Epica epica);
    public string? ValidarYGuardarEpica(string nombreEquipo, Epica epica, Panel panel, Usuario usuario);
    public List<Epica> ListarEpicas(string nombrePanel, string nombreEquipo);
    public List<Tarea> ListarTareasSinEpicaAsociada(string nombrePanel, string nombreEquipo);
    public List<Tarea> ListarTareasPorEpica(string nombrePanel, string nombreEquipo, string tituloEpica);
}