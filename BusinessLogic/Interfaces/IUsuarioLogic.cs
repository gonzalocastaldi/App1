using Domain;
using DTOs;

namespace BusinessLogic.Interfaces;

public interface IUsuarioLogic
{
    public void ModificarApellido(Usuario usuarioModificador, Usuario usuario, string nuevoApellido);
    public void ModificarNombre(Usuario usuarioModificador, Usuario usuario, string nuevoNombre);
    public void ReiniciarContraseña(Usuario usuarioModificador, Usuario usuario);
    public void ModificarEmail(Usuario usuarioAModificar, Usuario usuarioModificador, string nuevoEmail);
    void CrearUsuario(UsuarioDTO usuariodto);
    public void EliminarUsuario(Usuario usuarioAEliminar, Usuario usuario);
    public Usuario IniciarSesion(Usuario usuario);
    public List<Usuario> ListarUsuarios(Usuario usuario);
    public string? ValidarYGuardarUsuario(UsuarioDTO usuario);
    public void EliminarNotificacion(Notificacion notificacion);
    public List<Notificacion> ListarNotificaciones(Usuario usuario);
    public void actualizar(Usuario usuario);
    public Papelera ObtenerPapeleraDeUsuario(Usuario usuario);
}