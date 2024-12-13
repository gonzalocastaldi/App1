using Domain;
using DTOs;

namespace BusinessLogic.Interfaces;

public interface IEpicaLogic
{
    public void CambiarDescripcion(string nombreEquipo, string nombrePanel, string nuevaDesc, string tituloEpica);
    public void CambiarTitulo(CambiarTituloEpicaDTO cambiarTituloEpicaDto);
    public void AgregarTarea(string nombreEquipo, string nombrePanel, string tituloEpica, Tarea tarea);
    public void EliminarTarea(string nombreEquipo, string nombrePanel, string tituloEpica, Tarea tarea);
}