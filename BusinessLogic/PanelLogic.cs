using BusinessLogic.Interfaces;
using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic;

public class PanelLogic : IPanelLogic
{
    private RepositorioEquipo _equipos;
    private RepositorioUsuario _usuarios;
    public PanelLogic(RepositorioEquipo repositorioEquipo, RepositorioUsuario repositorioUsuario)
    {
        _equipos = repositorioEquipo;
        _usuarios = repositorioUsuario;
    }

    public void AgregarTarea(string nombrePanel, Tarea tarea, string nombreEquipo)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
        
        var panel = _equipos.ObtenerPanelesDeEquipo(nombreEquipo).FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearPermisos(existePanel, "El panel no existe.");
        
        bool existeTareaConNombre = _equipos.ObtenerTareasDePanelYPapelera(nombreEquipo, nombrePanel).Any(t => t.Titulo == tarea.Titulo);
        ManejoExcepciones.ChequearPermisos(!existeTareaConNombre, "Ya existe una tarea con ese nombre. (Puede estar en la papelera)");
        
        panel.AgregarTarea(tarea);
        _equipos.Actualizar(equipo);
    }
    
    public void AgregarEpica(AgregarEpicaDto agregarEpicaDto) 
    {
        var panel = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).FirstOrDefault(p => p.Nombre == agregarEpicaDto.PanelSeleccionado.Nombre);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        bool existeEpicaConNombre = _equipos.ObtenerEpicasDePanel(agregarEpicaDto.EquipoSeleccionado.Nombre, agregarEpicaDto.PanelSeleccionado.Nombre).Any(t => t.Titulo == agregarEpicaDto.Epica.Titulo);
        ManejoExcepciones.ChequearCondiciones(!existeEpicaConNombre, "Ya existe una epica con ese nombre.");
        
        panel.AgregarEpica(agregarEpicaDto.Epica);
        _equipos.Actualizar(panel.Equipo);
    }

    public void EliminarEpica(string nombreEquipo, string nombrePanel, Epica epica)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
        
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        bool existeEpica = _equipos.ObtenerEpicasDePanel(nombreEquipo, nombrePanel).Any(e => e.Titulo == epica.Titulo);
        ManejoExcepciones.ChequearCondiciones(existeEpica, "La epica no existe.");
        
        bool epicaTieneTareas = epica.Tareas.Count > 0;
        ManejoExcepciones.ChequearCondiciones(!epicaTieneTareas, "La epica tiene tareas asociadas, borrá las tareas antes de eliminar la épica.");
        
        panel.EliminarEpica(epica);
        _equipos.Actualizar(panel.Equipo);
    }

    public void CambiarNombre(string nombrePanel, string nuevoNombre, Usuario usuario, string nombreEquipo)
    {
        Equipo equipo = _equipos.Buscar(nombreEquipo);
        
        Panel panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        bool existePanelConNombre = equipo.Paneles.Any(p => p.Nombre == nuevoNombre);
        ManejoExcepciones.ChequearPermisos(!existePanelConNombre, "Ya existe un panel con ese nombre.");

        bool elPanelEsTareasVencidas = nombrePanel == "Tareas Vencidas";
        ManejoExcepciones.ChequearPermisos(!elPanelEsTareasVencidas, "No se puede cambiar el nombre de este panel.");

        panel.Nombre = nuevoNombre;
        _equipos.Actualizar(equipo);
    }

    public void CambiarDescripcion(string nombrePanel, string nuevaDescripcion, Usuario usuario, string nombreEquipo)
    {
        Equipo equipo = _equipos.Buscar(nombreEquipo);
        
        Panel panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        panel.Descripcion = nuevaDescripcion;
        _equipos.Actualizar(equipo);
    }

    public void EliminarTarea(string nombreEquipo, string nombrePanel, string nombreTarea, Usuario usuario)
    {
        
        var panel = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearPermisos(existePanel, "El panel no existe.");
        
        Tarea? tarea = _equipos.ObtenerTareasDePanel(nombreEquipo, nombrePanel).FirstOrDefault(t => t.Titulo == nombreTarea);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearPermisos(existeTarea, "La tarea no existe.");
        
        bool perteneceAEpica = tarea.Epica != null;
        if (perteneceAEpica)
        {
            var epica = tarea.Epica;
            epica.EsfuerzoEstimado -= tarea.EsfuerzoEstimado;
            epica.EsfuerzoInvertido -= tarea.EsfuerzoInvertido;
        }
        _usuarios.AgregarTareaAPapelera(tarea, usuario);
    }
    
    public void EliminarTareaDeFormaPermanente(string nombreEquipo, string nombrePanel, string nombreTarea, Usuario usuario)
    {
        var panel = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        var tarea = _equipos.ObtenerTareasDePanel(nombreEquipo, nombrePanel).FirstOrDefault(t => t.Titulo == nombreTarea);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearCondiciones(existeTarea, "La tarea no existe.");
        
        panel.Tareas.Remove(tarea);
    }

     public List<Epica> ListarEpicas(string nombrePanel, string nombreEquipo)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");

        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");

        return _equipos.ObtenerEpicasDePanel(nombreEquipo, nombrePanel);
    }
     
    public List<Tarea> ListarTareasSinEpicaAsociada(string nombrePanel, string nombreEquipo)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");

        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");

        return _equipos.ObtenerTareasSinEpicaDePanel(nombreEquipo, nombrePanel);
    }

    public List<Tarea> ListarTareasPorEpica(string nombrePanel, string nombreEquipo, string tituloEpica)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");

        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        var epica = panel.Epicas.FirstOrDefault(e => e.Titulo == tituloEpica);
        bool existeEpica = epica != null;
        ManejoExcepciones.ChequearCondiciones(existeEpica, "La epica no existe.");

        return _equipos.ObtenerTareasPorEpica(nombreEquipo, nombrePanel, tituloEpica);
    }
    
    public string? ValidarYGuardarTarea(Tarea tarea, Panel? panel, Usuario usuario)
    {
        try
        {
            AgregarTarea(panel.Nombre, tarea, panel.EquipoAlQuePertenece);
            return null;
        }catch(Exception e)
        {
            return e.Message;
        }
    }

    public string? ValidarYGuardarEpica(AgregarEpicaDto agregarEpicaDto)
    {
        try
        {
            AgregarEpica(agregarEpicaDto);
            return null;
        }catch(Exception e)
        {
            return e.Message;
        }
    }
}