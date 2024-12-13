using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RepositorioEquipo
{
    private ContextoSql _contexto;
    
    public RepositorioEquipo(ContextoSql contexto)
    {
        _contexto = contexto;
    }

    public List<Equipo> ObtenerTodosLosEquiposConPaneles()
    {
        return _contexto.Equipos.Include(e => e.Paneles).ToList();
    }
    
    public List<Panel> ObtenerPanelesDeEquipo(string nombreEquipo)
    {
        return _contexto.Paneles.Include(p => p.Equipo).Where(p => p.Equipo.Nombre == nombreEquipo && p.PapeleraId == null).ToList();
    }
    
    public List<Panel> ObtenerPanelesDeEquipoYPapelera(string nombreEquipo)
    {
        return _contexto.Paneles.Include(p => p.Equipo).Where(p => p.Equipo.Nombre == nombreEquipo).ToList();
    }
    
    public List<Comentario> ObtenerComentariosDeTarea(string nombreEquipo, string nombrePanel, string nombreTarea)
    {
        return _contexto.Comentarios.Include(c => c.Tarea).Where(c => c.Tarea.Titulo == nombreTarea && c.Tarea.PanelActual.Nombre == nombrePanel && c.Tarea.PanelActual.Equipo.Nombre == nombreEquipo).ToList();
    }

    public List<Tarea> ObtenerTareasDePanel(string nombreEquipo, string nombrePanel)
    {
        return _contexto.Tareas.Include(t => t.PanelActual).Where(t => t.PanelActual.Nombre == nombrePanel && t.PanelActual.Equipo.Nombre == nombreEquipo && t.PapeleraId == null).ToList();
    }
    
    public List<Tarea> ObtenerTareasDePanelYPapelera(string nombreEquipo, string nombrePanel)
    {
        return _contexto.Tareas.Include(t => t.PanelActual).Where(t => t.PanelActual.Nombre == nombrePanel && t.PanelActual.Equipo.Nombre == nombreEquipo).ToList();
    }
    
    public List<Epica> ObtenerEpicasDePanel(string nombreEquipo, string nombrePanel)
    {
        return _contexto.Epicas.Include(e => e.Panel).Where(e => e.Panel.Nombre == nombrePanel && e.Panel.Equipo.Nombre == nombreEquipo).ToList();
    }
    
    public Panel? ObtenerPanelPorId(int id)
    {
        return _contexto.Paneles.Include(p => p.Equipo).FirstOrDefault(p => p.Id == id);
    }
    
    public List<Tarea> ObtenerTareasSinEpicaDePanel(string nombreEquipo, string nombrePanel)
    {
        return _contexto.Tareas.Include(t => t.PanelActual).Where(t => t.PanelActual.Nombre == nombrePanel && t.PanelActual.Equipo.Nombre == nombreEquipo && t.EpicaId == null && t.PapeleraId == null).ToList();
    }
    
    public List<Tarea> ObtenerTareasPorEpica(string nombreEquipo, string nombrePanel, string tituloEpica)
    {
        return _contexto.Tareas.Include(t => t.PanelActual).Where(t => t.PanelActual.Nombre == nombrePanel && t.PanelActual.Equipo.Nombre == nombreEquipo && t.Epica.Titulo == tituloEpica && t.PapeleraId == null).ToList();
    }   
    
    public bool TareaPerteneceAEpicas(string nombreEquipo, string nombrePanel, string nombreTarea)
    {
        return _contexto.Tareas.Include(t => t.PanelActual).Any(t => t.PanelActual.Nombre == nombrePanel && t.PanelActual.Equipo.Nombre == nombreEquipo && t.Titulo == nombreTarea && t.EpicaId != null);
    }
    
    public Epica ObtenerEpicaPorTarea(string nombreEquipo, string nombrePanel, string nombreTarea)
    {
        return _contexto.Epicas.Include(e => e.Panel).FirstOrDefault(e => e.Panel.Nombre == nombrePanel && e.Panel.Equipo.Nombre == nombreEquipo && e.Tareas.Any(t => t.Titulo == nombreTarea));
    }
    
    public void AgregarPanel(Panel panel)
    {
        _contexto.Paneles.Add(panel);
        _contexto.SaveChanges();
    }

    public void ActualizarPanel(Panel panel)
    {
        _contexto.Paneles.Update(panel);
        _contexto.SaveChanges();
    }

    public void EliminarPanel(Panel panel)
    {
        var papelera = _contexto.Papeleras.FirstOrDefault(p => p.Id == panel.PapeleraId);
        papelera.CantidadObjetosEnPapelera--;
        _contexto.Paneles.Remove(panel);
        _contexto.SaveChanges();
    }
    
    public Equipo? Buscar(string nombre)
    {
        return _contexto.Equipos.FirstOrDefault(e => e.Nombre == nombre);
    }
    
    public void Agregar(Equipo equipo)
    {
        _contexto.Equipos.Add(equipo);
        _contexto.SaveChanges();
    }
    
    public int CantidadEquipos()
    {
        return _contexto.Equipos.Count();
    }
    
    public void Eliminar(Equipo equipo)
    {
        _contexto.Equipos.Remove(equipo);
        _contexto.SaveChanges();
    }
    
    public List<Equipo> ObtenerTodosLosEquipos()
    {
        return _contexto.Equipos.ToList();
    }
    
    public void Actualizar(Equipo equipo)
    {
        _contexto.Equipos.Update(equipo);
        _contexto.SaveChanges();
    }
    
    public void EliminarTarea(Tarea tarea)
    {
        var papelera = _contexto.Papeleras.FirstOrDefault(p => p.Id == tarea.PapeleraId);
        papelera.CantidadObjetosEnPapelera--;
        _contexto.Tareas.Remove(tarea);
        _contexto.SaveChanges();
    }
    
    public void RestaurarTarea(Tarea tarea)
    {
        var papelera = _contexto.Papeleras.FirstOrDefault(p => p.Id == tarea.PapeleraId);
        papelera.CantidadObjetosEnPapelera--;
        tarea.PapeleraId = null;
        _contexto.Tareas.Update(tarea);
        _contexto.SaveChanges();
    }
    
    public void RestaurarPanel(Panel panel)
    {
        var papelera = _contexto.Papeleras.FirstOrDefault(p => p.Id == panel.PapeleraId);
        papelera.CantidadObjetosEnPapelera--;
        panel.PapeleraId = null;
        _contexto.Paneles.Update(panel);
        _contexto.SaveChanges();
    }
    
    public Papelera? ObtenerPapeleraPorUsuarioId(int usuarioId)
    {
        return _contexto.Papeleras
            .Include(p => p.Tareas)
            .Include(p => p.Paneles)
            .FirstOrDefault(p => p.UsuarioId == usuarioId);
    }
    
}