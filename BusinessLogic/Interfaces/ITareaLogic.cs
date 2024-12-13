using Domain;

namespace BusinessLogic.Interfaces;

public interface ITareaLogic
{
    public void ModificarTitulo(string tituloActual, string nuevoTitulo);
    public void ModificarDescripcion(string tituloActual, string nuevaDescripcion);

    public void AgregarComentario(string nombreEquipo, string nombrePanel, string tituloTarea, string tituloComentario,
        string descripcionComentario, Usuario usuario);
    public void ModificarEsfuerzoInvertido(string tituloTarea, int nuevoEsfuerzo);
    public List<Comentario> ObtenerComentariosDeTarea(string nombreEquipo, string nombrePanel, string nombreTarea);
}