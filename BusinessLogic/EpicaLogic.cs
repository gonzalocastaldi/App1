using BusinessLogic.Interfaces;
using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic;

public class EpicaLogic : IEpicaLogic
{
    private RepositorioEquipo _equipos;
    public EpicaLogic(RepositorioEquipo repositorio)
    {
        _equipos = repositorio;
    }

    public void CambiarTitulo(CambiarTituloEpicaDTO cambiarTituloEpicaDto)
    {
        var equipo = _equipos.Buscar(cambiarTituloEpicaDto.nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
        
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == cambiarTituloEpicaDto.nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        var epica = _equipos.ObtenerEpicasDePanel(cambiarTituloEpicaDto.nombreEquipo, cambiarTituloEpicaDto.nombrePanel).FirstOrDefault(e => e.Titulo == cambiarTituloEpicaDto.tituloOriginal);
        bool existeEpica = epica != null;
        ManejoExcepciones.ChequearCondiciones(existeEpica, "La epica no existe.");
        
        bool existeEpicaConEsteNombre = _equipos.ObtenerEpicasDePanel(cambiarTituloEpicaDto.nombreEquipo, cambiarTituloEpicaDto.nombrePanel).Any(e => e.Titulo == cambiarTituloEpicaDto.nuevoTitulo);
        ManejoExcepciones.ChequearCondiciones(!existeEpicaConEsteNombre, "Ya existe una epica con ese nombre.");
        
        epica.Titulo = cambiarTituloEpicaDto.nuevoTitulo;
        _equipos.Actualizar(equipo);
    }

    public void CambiarDescripcion(string nombreEquipo, string nombrePanel, string nuevaDesc, string tituloEpica)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
        
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        var epica = _equipos.ObtenerEpicasDePanel(nombreEquipo, nombrePanel).FirstOrDefault(e => e.Titulo == tituloEpica);
        bool existeEpica = epica != null;
        ManejoExcepciones.ChequearCondiciones(existeEpica, "La epica no existe.");
        
        epica.Descripcion = nuevaDesc;
        _equipos.Actualizar(equipo);
    }

    public void AgregarTarea(string nombreEquipo, string nombrePanel, string tituloEpica, Tarea tarea)
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
        
        bool existeTareaEnEpica = !_equipos.ObtenerEpicasDePanel(nombreEquipo, nombrePanel).Any(t => t.Titulo == tarea.Titulo);
        ManejoExcepciones.ChequearCondiciones(existeTareaEnEpica, "La tarea ya pertenece a esta épica.");
        
        epica.AgregarTarea(tarea);
        _equipos.Actualizar(equipo);
    }

    public void EliminarTarea(string nombreEquipo, string nombrePanel, string tituloEpica, Tarea tarea)
    {
        var equipo = _equipos.Buscar(nombreEquipo);
        bool existeEquipo = equipo != null;
        ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
        
        var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        bool existePanel = panel != null;
        ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
        
        var epica = _equipos.ObtenerEpicasDePanel(nombreEquipo, nombrePanel).FirstOrDefault(e => e.Titulo == tituloEpica);
        bool existeEpica = epica != null;
        ManejoExcepciones.ChequearCondiciones(existeEpica, "La epica no existe.");
        
        epica.EliminarTarea(tarea);
        _equipos.Actualizar(equipo);
    }
    
}