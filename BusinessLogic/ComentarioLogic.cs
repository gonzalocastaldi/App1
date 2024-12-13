using BusinessLogic.Interfaces;
using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic;

public class ComentarioLogic : IComentarioLogic
{
    private RepositorioEquipo _equipos;
    private RepositorioUsuario _usuarios;
    public ComentarioLogic(RepositorioEquipo repositorioEquipos, RepositorioUsuario repositorioUsuarios)
    {
        _equipos = repositorioEquipos;
        _usuarios = repositorioUsuarios;
    }

    public void ResolverComentario(AgregarComentarioDTO agregarComentarioDtoio)
    {
        Equipo? equipo = _equipos.ObtenerTodosLosEquipos().FirstOrDefault(e => e.Nombre == agregarComentarioDtoio.Equipo);
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == agregarComentarioDtoio.PanelSeleccionado);
        if (panel == null)
        {
            throw new ArgumentException("El panel no existe.");
        }
        var tarea = panel.Tareas.FirstOrDefault(t => t.Titulo == agregarComentarioDtoio.TareaSeleccionada);
        if (tarea == null)
        {
            throw new ArgumentException("La tarea no existe.");
        }
        var comentario = tarea.Comentarios.FirstOrDefault(c => c.Titulo == agregarComentarioDtoio.ComentarioTitulo);
        comentario.Estado = true;
        comentario.UsuarioResolvedor = _usuarios.Buscar(agregarComentarioDtoio.Usuario.Email);
        comentario.FechaResolucion = DateTime.Now;
        Notificacion notificacion = new Notificacion(comentario);
        _usuarios.AgregarNotificacion(notificacion, comentario.UsuarioCreador);
    }
}