using BusinessLogic.Interfaces;
using DataAccess;
using Domain;

namespace BusinessLogic
{
    public class PapeleraLogic : IPapeleraLogic
    {
        private RepositorioEquipo _equipos;
        private readonly IEquipoLogic _equipoLogic;
        private readonly IPanelLogic _panelLogic;

        public PapeleraLogic(RepositorioEquipo repositorio, IEquipoLogic equipoLogic, IPanelLogic panelLogic)
        {
            
            _equipos = repositorio;
            _equipoLogic = equipoLogic;
            _panelLogic = panelLogic;
        }
        
        public void AgregarPanelAPapelera(Panel? panel, Papelera papelera)
        {
            papelera.AgregarPanelAPapelera(panel);
        }
        
        public int ObtenerCantidadObjetosEnPapelera(Papelera papelera)
        {
            return papelera.CantidadObjetosEnPapelera;
        }
        
        public int ObtenerCantidadMaximaObjetos(Papelera papelera)
        {
            return papelera.CantidadMaximaObjetos;
        }
        
        public List<Panel?> ListaPaneles(Papelera papelera)
        {
            return papelera.Paneles;
        }
        
        public void EliminarPanelDePapelera(Panel panel, Papelera papelera)
        {
            _equipos.EliminarPanel(panel);
        }
        
        public void AgregarTareaAPapelera(Tarea tarea, Papelera papelera)
        {
            papelera.AgregarTareaAPapelera(tarea);
        }
        
        public List<Tarea> ListaTareas(Papelera papelera)
        {
            return papelera.Tareas;
        }
        
        public void EliminarTareaDePapelera(Tarea tarea, Papelera papelera)
        {
            _equipos.EliminarTarea(tarea);
        }
        
        public void VaciarPapelera(Papelera papelera)
        {
            papelera.VaciarPapelera();
        }
        
        public void RestaurarPanel(Panel panel, Papelera papelera, Usuario usuario)
        {
            try
            {
                var equipo = _equipos.Buscar(panel.EquipoAlQuePertenece);
                bool existeEquipo = equipo != null;
                ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
                _equipos.RestaurarPanel(panel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void RestaurarTarea(Tarea tarea, Papelera papelera, Usuario usuario)
        {
            try
            {
                var equipo = _equipos.Buscar(tarea.PanelOriginal.EquipoAlQuePertenece);
                bool existeEquipo = equipo != null;
                ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe.");
                var panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == tarea.PanelOriginal.Nombre);
                bool existePanel = _equipos.ObtenerPanelesDeEquipo(equipo.Nombre).Contains(panel);
                ManejoExcepciones.ChequearCondiciones(existePanel, "El panel no existe.");
                _equipos.RestaurarTarea(tarea);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}