using Domain;

namespace BusinessLogic.Interfaces;

public interface IEquipoLogic
{
    public void AgregarUsuario(Usuario usuarioNuevo, String nombreEquipo, Usuario usuarioModificador);
    public void CambiarNombre(String nombreEquipoActual, string nuevoNombre, Usuario usuario);
    public void CambiarDescripcion(string nombreEquipo, string descripcionNueva, Usuario usuario);
    public void CambiarCantidadMaximaDeUsuarios(String nombreEquipo, int cantidadMaxima, Usuario usuario);
    public void CrearEquipo(string nombre, int cantidadMaxima, string descripcion, Usuario usuario);
    public void EliminarEquipo(string nombreEquipo, Usuario usuario);
    public void AgregarPanel(string nombreEquipo, Panel? panel, Usuario usuario);
    public void MoverTareasVencidas();
    public List<Equipo> ListarEquipos(Usuario usuario);
    public string? ValidarYGuardarEquipo(Equipo equipo, Usuario usuario);
    public string? ValidarYGuardarPanel(Panel? panel, Equipo equipo, Usuario usuario);
    public void EliminarPanel(string nombreEquipo, string nombrePanel, Usuario usuario);
    public void EliminarPanelDeFormaPermanente(string equipoSeleccionadoNombre, string panelSeleccionadoNombre, Usuario sesionLogicUsuarioLogueado);
    public List<Panel> ListarPaneles(string nombreEquipo);
    public List<Tarea> ListarTareas(string nombreEquipo, string nombrePanel);
}