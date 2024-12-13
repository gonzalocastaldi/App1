using BusinessLogic.Interfaces;
using DataAccess;
using Domain;

namespace BusinessLogic
{
    public class EquipoLogic : IEquipoLogic
    {
        private RepositorioEquipo _equipos;
        private RepositorioUsuario _usuarios;
        private readonly ContextoSql _context;
        public EquipoLogic(RepositorioEquipo repositorioEquipo, RepositorioUsuario repositorioUsuario, ContextoSql context)
        {
            _equipos = repositorioEquipo;
            _usuarios = repositorioUsuario;
            _context = context;
        }

        
        public void AgregarUsuario(Usuario usuarioNuevo, string nombreEquipo, Usuario usuarioModificador)
        {
            var equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");

            bool tienePermisos = usuarioModificador.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para agregar un usuario al equipo.");
            
            bool noSeAlcanzoCantMax = equipo.CantidadActualUsuarios < equipo.CantidadMaxima;
            ManejoExcepciones.ChequearPermisos(noSeAlcanzoCantMax, "No se pueden agregar más usuarios, se ha alcanzado la cantidad máxima.");
            
            equipo.AgregarUsuario(usuarioNuevo);
            _equipos.Actualizar(equipo);
        }

        public void CambiarNombre(string nombreEquipoActual, string nuevoNombre, Usuario usuario)
        {
            var equipo = _equipos.Buscar(nombreEquipoActual);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para cambiar el nombre del equipo.");

            bool elNombreEsVacio = string.IsNullOrEmpty(nuevoNombre);
            ManejoExcepciones.ChequearCondiciones(!elNombreEsVacio, "El nombre del equipo no puede ser vacío.");
            
            bool existeEquipoConNuevoNombre = _equipos.Buscar(nuevoNombre) != null;
            ManejoExcepciones.ChequearPermisos(!existeEquipoConNuevoNombre, "Ya existe un equipo con ese nombre.");
            
            equipo.CambiarNombre(nuevoNombre);
            _equipos.Actualizar(equipo);
            foreach (var panel in _equipos.Buscar(nuevoNombre).Paneles)
            {
                panel.EquipoAlQuePertenece = nuevoNombre;
            }
            _equipos.Actualizar(equipo);
        }

        public void CambiarDescripcion(string nombreEquipo, string descripcionNueva, Usuario usuario)
        {
            var equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para cambiar la descripción del equipo.");
            
            bool laDescripcionEsVacia = string.IsNullOrEmpty(descripcionNueva);
            ManejoExcepciones.ChequearCondiciones(!laDescripcionEsVacia, "El nombre del equipo no puede ser vacío.");
            
            equipo.CambiarDescripcion(descripcionNueva);
            _equipos.Actualizar(equipo);
        }

        public void CambiarCantidadMaximaDeUsuarios(string nombreEquipo, int cantidadMaxima, Usuario usuario)
        {
            var equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para cambiar la cantidad máxima del equipo.");

            bool elNumeroEsMenorQueCero = cantidadMaxima < 0;
            ManejoExcepciones.ChequearCondiciones(!elNumeroEsMenorQueCero, "La cantidad máxima de usuarios no puede ser negativa.");
            
            bool elNumeroEsMayorOIgualQueLaCantDeUsuariosActuales = equipo.CantidadActualUsuarios <= cantidadMaxima;
            ManejoExcepciones.ChequearCondiciones(elNumeroEsMayorOIgualQueLaCantDeUsuariosActuales, "La cantidad máxima de usuarios no puede ser menor a la cantidad actual de usuarios.");
            
            equipo.CambiarCantidadMaximaDeUsuarios(cantidadMaxima);
            _equipos.Actualizar(equipo);
        }

        public void CrearEquipo(string nombre, int cantidadMaxima, string descripcion, Usuario usuario)
        {
            var equipoExistente = _equipos.Buscar(nombre);
            bool existeEquipo = equipoExistente != null;
            ManejoExcepciones.ChequearPermisos(!existeEquipo, "Ya existe un equipo con ese nombre.");
            
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para crear un equipo.");
            
            Equipo equipo = new Equipo(nombre, cantidadMaxima, descripcion, usuario);

            _equipos.Agregar(equipo);
            AgregarPanelTareasVencidas(equipo);
            _equipos.Actualizar(equipo);
            _context.SaveChanges();
        }

        public void AgregarPanelTareasVencidas(Equipo equipo)
        {
            Panel? tareasVencidas = new Panel("Tareas Vencidas", "Panel para tareas vencidas", equipo.Nombre);
            equipo.AgregarPanel(tareasVencidas);
            _equipos.Actualizar(equipo);
        }

        public void EliminarEquipo(string nombreEquipo, Usuario usuario)
        {
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para eliminar un equipo.");

            var equipoAEliminar = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipoAEliminar != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            List<Panel> paneles = _context.Paneles.Where(p => p.EquipoAlQuePertenece == nombreEquipo).ToList();
            bool tieneUnSoloPanel = paneles.Count() == 1;
            ManejoExcepciones.ChequearPermisos(tieneUnSoloPanel, "No se puede eliminar un equipo con paneles. Elimine los paneles antes de continuar.");
            
            _equipos.Actualizar(equipoAEliminar);
            _equipos.Eliminar(equipoAEliminar);
            var panelTareasVencidas = _context.Paneles.FirstOrDefault(p =>
                p.EquipoAlQuePertenece == nombreEquipo && p.Nombre == "Tareas Vencidas");
            _context.Paneles.Remove(panelTareasVencidas);
            _context.SaveChanges();
        }

        public void AgregarPanel(string nombreEquipo, Panel? panel, Usuario usuario)
        {
            Equipo? equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            
            bool nombreVacio = string.IsNullOrEmpty(panel.Nombre);
            ManejoExcepciones.ChequearCondiciones(!nombreVacio, "El nombre del panel no puede ser vacío");

            bool existePanel = _equipos.ObtenerPanelesDeEquipoYPapelera(nombreEquipo).Any(p => p.Nombre == panel.Nombre);
            ManejoExcepciones.ChequearPermisos(!existePanel, "Ya existe un panel con ese nombre. (Puede estar en la papelera)");
            
            equipo.AgregarPanel(panel);
            _equipos.Actualizar(equipo);
            _context.SaveChanges();
            }
        
        public void EliminarPanel(string nombreEquipo, string nombrePanel, Usuario usuario)
        {
            bool tienePermisos = usuario.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para eliminar paneles.");
            
            var equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
            
            Panel? panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
            bool panelExistente = panel != null;
            ManejoExcepciones.ChequearCondiciones(panelExistente, "El panel no existe.");

            bool esPanelTareasVencidas = panel.Nombre == "Tareas Vencidas";
            ManejoExcepciones.ChequearPermisos(!esPanelTareasVencidas, "No se puede eliminar el panel de tareas vencidas.");
            
            bool papeleraEstaLlena = _usuarios.PapeleraLlena(usuario);
            if (papeleraEstaLlena)
            {
                Console.Out.Write("La papelera está llena.");
            }
            ManejoExcepciones.ChequearCondiciones(!papeleraEstaLlena, "La papelera está llena.");
            
            bool panelTieneTareas = _equipos.ObtenerTareasDePanel(equipo.Nombre, panel.Nombre).Any();
            ManejoExcepciones.ChequearPermisos(!panelTieneTareas, "No se puede eliminar un panel con tareas.");
            
            _usuarios.AgregarPanelAPapelera(panel, usuario);
        }

        public void EliminarPanelDeFormaPermanente(string nombreEquipo, string nombrePanel, Usuario usuario)
        {
            var equipo = _equipos.Buscar(nombreEquipo);
            bool existeEquipo = equipo != null;
            ManejoExcepciones.ChequearCondiciones(existeEquipo, "El equipo no existe");
                
            Panel panel = equipo.Paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
            bool panelExistente = panel != null;
            ManejoExcepciones.ChequearCondiciones(panelExistente, "El panel no existe.");

            bool esPanelTareasVencidas = panel.Nombre == "Tareas Vencidas";
            ManejoExcepciones.ChequearPermisos(!esPanelTareasVencidas, "No se puede eliminar el panel de tareas vencidas.");
            
            equipo.EliminarPanel(nombrePanel);
            _equipos.Actualizar(equipo);
        }
        
        public void MoverTareasVencidas()
        {
            foreach (var equipo in _equipos.ObtenerTodosLosEquipos())
            {
                var panelTareasVencidas = equipo.Paneles.FirstOrDefault(p => p.Nombre == "Tareas Vencidas");
                if (panelTareasVencidas == null)
                {
                    panelTareasVencidas = new Panel("Tareas Vencidas", "Panel para tareas vencidas", equipo.Nombre);
                    equipo.AgregarPanel(panelTareasVencidas);
                }

                foreach (var panel in equipo.Paneles)
                {
                    if (panel.Nombre != "Tareas Vencidas")
                    {
                        var tareasVencidas = panel.Tareas.Where(t => t.FechaExpiracion.Date == DateTime.Now.Date).ToList();
                        foreach (var tarea in tareasVencidas)
                        {
                            panel.EliminarTarea(tarea);
                            tarea.PanelActual=panelTareasVencidas;
                            panelTareasVencidas.AgregarTarea(tarea);
                        }
                    }
                }
            }
        }
        
        public void ReactivarTareaVencida(string nombreEquipo, string tituloTarea, DateTime nuevaFechaVencimiento, Usuario usuario)
        {
            if (!usuario.EsAdmin)
            {
                throw new InvalidOperationException("No tiene permisos para reactivar tareas.");
            }
            var equipo = _equipos.Buscar(nombreEquipo);
            if (equipo == null)
            {
                throw new ArgumentException("El equipo no existe.");
            }
            var panelTareasVencidas = equipo.Paneles.FirstOrDefault(p => p.Nombre == "Tareas Vencidas");
            var tarea = panelTareasVencidas.Tareas.FirstOrDefault(t => t.Titulo == tituloTarea);
            if (tarea == null)
            {
                throw new ArgumentException("La tarea no existe.");
            }
            tarea.FechaExpiracion = nuevaFechaVencimiento;
            panelTareasVencidas.EliminarTarea(tarea);
            tarea.PanelActual = tarea.PanelOriginal;
            tarea.PanelOriginal.AgregarTarea(tarea);
        }

        public List<Equipo> ListarEquipos(Usuario usuario)
        {
            if (usuario.EsAdmin)
            {
                return _equipos.ObtenerTodosLosEquipos();
            }
            else
            {
                return _equipos.ObtenerTodosLosEquipos().Where(e => e.Usuarios.Contains(usuario)).ToList();
            }
        }
        
        public List<Tarea> ListarTareas(string nombreEquipo, string nombrePanel)
        {
            return _equipos.ObtenerTareasDePanel(nombreEquipo, nombrePanel);
        }
        
        public List<Panel> ListarPaneles(string nombreEquipo)
        {
            List<Panel> paneles = _equipos.ObtenerPanelesDeEquipo(nombreEquipo);
            return paneles;
        }
        
        
        public string? ValidarYGuardarEquipo(Equipo equipo, Usuario usuario)
        {
            try
            {
                CrearEquipo(equipo.Nombre, equipo.CantidadMaxima, equipo.Descripcion, usuario);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public string? ValidarYGuardarPanel(Panel? panel, Equipo equipo, Usuario usuario)
        {
            try
            {
                AgregarPanel(equipo.Nombre, panel, usuario);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
    
}