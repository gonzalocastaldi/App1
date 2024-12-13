using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RepositorioUsuario
{
    private ContextoSql _contexto;
    
    public RepositorioUsuario(ContextoSql contexto)
    {
        _contexto = contexto;
    }

    public void Agregar(Usuario usuario)
    {
        _contexto.Usuarios.Add(usuario);
        _contexto.SaveChanges();
    }
    
    public Papelera? ObtenerPapeleraPorUsuarioId(int usuarioId)
    {
        return _contexto.Papeleras
            .Include(p => p.Tareas)
            .Include(p => p.Paneles)
            .FirstOrDefault(p => p.UsuarioId == usuarioId);
    }
    
    public void AgregarPanelAPapelera(Panel? panel, Usuario usuario)
    {
        var papelera = ObtenerPapeleraPorUsuarioId(usuario.Id);
        if (panel == null)
        {
            throw new ArgumentNullException(nameof(panel), "La tarea no puede ser null.");
        }

        if (papelera == null)
        {
            throw new ArgumentNullException(nameof(papelera), "La papelera no puede ser null.");
        }
        papelera.CantidadObjetosEnPapelera++;
        papelera.Paneles.Add(panel);
        _contexto.SaveChanges();
    }
    
    public void AgregarTareaAPapelera(Tarea tarea, Usuario usuario)
    {
        var papelera = ObtenerPapeleraPorUsuarioId(usuario.Id);
        if (tarea == null)
        {
            throw new ArgumentNullException(nameof(tarea), "La tarea no puede ser null.");
        }

        if (papelera == null)
        {
            throw new ArgumentNullException(nameof(papelera), "La papelera no puede ser null.");
        }
        papelera.CantidadObjetosEnPapelera++;
        papelera.Tareas.Add(tarea);
        _contexto.SaveChanges();
    }
    
    public bool PapeleraLlena(Usuario usuario)
    {
        var papelera = ObtenerPapeleraPorUsuarioId(usuario.Id);
        return papelera.CantidadObjetosEnPapelera >= papelera.CantidadMaximaObjetos;
    }

    public int CantidadUsuarios()
    {
        return _contexto.Usuarios.Count();
    }
    
    public Usuario? Buscar(string email)
    {
        return _contexto.Usuarios.FirstOrDefault(u => u.Email == email);
    }

    public void Eliminar(Usuario usuario)
    {
        if (usuario == null)
        {
            throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser null.");
        }

        var papeleras = _contexto.Papeleras.Where(p => p.UsuarioId == usuario.Id).ToList();
        foreach (var papelera in papeleras)
        {
            _contexto.Papeleras.Remove(papelera);
        }

        _contexto.Usuarios.Remove(usuario);
        _contexto.SaveChanges();
    }
    
    public List<Usuario> ObtenerTodosLosUsuarios()
    {
        return _contexto.Usuarios.ToList();
    }
    
    public void Actualizar(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        _contexto.SaveChanges();
    }
    
    public Usuario? Login(string email, string contrasena)
    {
        return _contexto.Usuarios.FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);
    }
    
    public List<Notificacion> ObtenerNotificaciones(Usuario usuario)
    {
        return _contexto.Notificaciones.Where(n => n.Usuario.Email == usuario.Email).ToList();
    }
    
    public void AgregarNotificacion(Notificacion notificacion, Usuario usuario)
    {
        usuario.Notificaciones.Add(notificacion);
        _contexto.SaveChanges();
    }
    
    public void EliminarNotificacion(Notificacion notificacion)
    {
        _contexto.Notificaciones.Remove(notificacion);
        _contexto.SaveChanges();
    }
}