using BusinessLogic.Interfaces;
using DataAccess;
using Domain;

namespace BusinessLogic;

public class TareaLogic : ITareaLogic
{
    private RepositorioEquipo _equipos;
    private readonly ContextoSql _context;
    public TareaLogic(RepositorioEquipo repositorio, ContextoSql contexto)
    {
        _equipos = repositorio;
        _context = contexto;
    }

    public void ModificarTitulo(string tituloActual, string nuevoTitulo)
    {
        var tarea = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).SelectMany(p => p.Tareas).FirstOrDefault(t => t.Titulo == tituloActual);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearPermisos(existeTarea, "La tarea no existe.");
        
        bool existeTareaConEsteNombre = tarea.PanelActual.Tareas.Any(t => t.Titulo == nuevoTitulo);
        ManejoExcepciones.ChequearPermisos(!existeTareaConEsteNombre, "Ya existe una tarea con ese nombre.");
        
        tarea.Titulo = nuevoTitulo;
        _context.SaveChanges();
    }

    public void ModificarDescripcion(string tituloActual, string nuevaDescripcion)
    {
        var tarea = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).SelectMany(p => p.Tareas).FirstOrDefault(t => t.Titulo == tituloActual);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearPermisos(existeTarea, "La tarea no existe.");
        
        tarea.Descripcion = nuevaDescripcion;
        _context.SaveChanges();
    }
    
    public void ModificarEsfuerzoInvertido(string tituloTarea, int nuevoEsfuerzo)
    {
        var tarea = _equipos.ObtenerTodosLosEquipos().SelectMany(e => e.Paneles).SelectMany(p => p.Tareas).FirstOrDefault(t => t.Titulo == tituloTarea);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearPermisos(existeTarea, "La tarea no existe.");

        bool esfuerzoValido = nuevoEsfuerzo >= 0;
        ManejoExcepciones.ChequearCondiciones(esfuerzoValido, "El esfuerzo invertido debe ser un valor no negativo.");
        bool perteneceAEpica = tarea.Epica != null;
        if (perteneceAEpica)
        {
            var epica = tarea.Epica;
            epica.EsfuerzoInvertido += nuevoEsfuerzo - tarea.EsfuerzoInvertido;
        }
        tarea.EsfuerzoInvertido = nuevoEsfuerzo;
        _context.SaveChanges();
    }
    
    public void AgregarComentario(string nombreEquipo, string nombrePanel, string tituloTarea,string tituloComentario, string descripcionComentario, Usuario usuario)
    {
        bool elComentarioTieneNombre = tituloComentario != null;
        ManejoExcepciones.ChequearCondiciones(elComentarioTieneNombre, "El comentario debe tener un nombre.");
        
        bool laDescripcionDelComentarioEsValida = descripcionComentario != null;
        ManejoExcepciones.ChequearCondiciones(laDescripcionDelComentarioEsValida, "El comentario debe tener una descripción.");
        
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearPermisos(existeEquipo, "El equipo no existe.");
        
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearPermisos(existePanel, "El panel no existe.");
        
        Tarea? tarea = panel.Tareas.FirstOrDefault(t => t.Titulo == tituloTarea);
        bool existeTarea = tarea != null;
        ManejoExcepciones.ChequearPermisos(existeTarea, "La tarea no existe.");
        
        bool existeComentarioConEsteNombre = _equipos.ObtenerComentariosDeTarea(equipo.Nombre, nombrePanel, tituloTarea).Any(c => c.Titulo == tituloComentario);
        ManejoExcepciones.ChequearPermisos(!existeComentarioConEsteNombre, "Ya existe un comentario con ese nombre.");

        Comentario comentario = new Comentario(tituloComentario,false, usuario, null, DateTime.Now, descripcionComentario, tarea);
        tarea.AgregarComentario(comentario);
        _context.SaveChanges();
    }
    
    public List<Comentario> ObtenerComentariosDeTarea(string nombreEquipo, string nombrePanel, string nombreTarea)
    {
        var comentarios = _equipos.ObtenerComentariosDeTarea(nombreEquipo, nombrePanel, nombreTarea);
        return comentarios;
    }
}