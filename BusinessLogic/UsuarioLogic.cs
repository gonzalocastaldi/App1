using BusinessLogic.Interfaces;
using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic
{
    public class UsuarioLogic : IUsuarioLogic
    {
        private SesionLogic _sesionLogic;
        private RepositorioUsuario _usuarios;
        public UsuarioLogic(RepositorioUsuario repositorio, SesionLogic sesionLogic)
        {
            _usuarios = repositorio;
            _sesionLogic = sesionLogic;
        }

        public void CrearUsuario(UsuarioDTO usuariodto)
        {
            var usuarioExistente = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == usuariodto.Email);
            ManejoExcepciones.ChequearPermisos(usuarioExistente == null, "El email ya existe.");
            Usuario usuario = usuariodto.AEntidad();
            _usuarios.Agregar(usuario);
        }

        public Papelera ObtenerPapeleraDeUsuario(Usuario usuario)
        {
            return _usuarios.ObtenerPapeleraPorUsuarioId(usuario.Id);
        }
        
        public void ModificarNombre(Usuario usuarioModificador, Usuario usuario, string nuevoNombre)
        {
            bool existeUsuario = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == usuario.Email) != null;
            ManejoExcepciones.ChequearPermisos(existeUsuario, "El usuario no existe.");
            
            bool esAdmin = usuarioModificador.EsAdmin;
            bool esElUsuario = usuarioModificador == usuario;
            bool tienePermisos = esAdmin || esElUsuario;
            ManejoExcepciones.ChequearPermisos(tienePermisos,"No tiene permisos para modificar el nombre del usuario.");
            
            usuario.Nombre = nuevoNombre;
            _usuarios.Actualizar(usuario);
        }
        
        public void ModificarApellido(Usuario usuarioModificador, Usuario usuario, string nuevoApellido)
        {
            bool existeUsuario = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == usuario.Email) != null;
            ManejoExcepciones.ChequearPermisos(existeUsuario, "El usuario no existe.");
            
            bool esAdmin = usuarioModificador.EsAdmin;
            bool esElUsuario = usuarioModificador == usuario;
            bool tienePermisos = esAdmin || esElUsuario;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para modificar el apellido del usuario.");
            
            usuario.Apellido = nuevoApellido;
            _usuarios.Actualizar(usuario);
        }

        public void EliminarNotificacion(Notificacion notificacion)
        {
            _usuarios.EliminarNotificacion(notificacion);
        }
        
        public List<Notificacion> ListarNotificaciones(Usuario usuario)
        {
            return _usuarios.ObtenerNotificaciones(usuario);
        }
        
        public void ReiniciarContraseña( Usuario usuarioModificador, Usuario usuario)
        {
            bool tienePermisos = usuarioModificador.EsAdmin;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para reiniciar la contraseña del usuario.");
            
            usuario.Contrasena = GenerarContrasenaAleatoria();
            _usuarios.Actualizar(usuario);
        }
        
        private string GenerarContrasenaAleatoria()
        {
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string especiales = "!@#$%^&*()";
            const string todosCaracteres = mayusculas + minusculas + numeros + especiales;

            Random random = new Random();
            char[] contrasena = new char[10];

            contrasena[0] = mayusculas[random.Next(mayusculas.Length)];
            contrasena[1] = minusculas[random.Next(minusculas.Length)];
            contrasena[2] = numeros[random.Next(numeros.Length)];
            contrasena[3] = especiales[random.Next(especiales.Length)];

            for (int i = 4; i < contrasena.Length; i++)
            {
                contrasena[i] = todosCaracteres[random.Next(todosCaracteres.Length)];
            }

            return new string(contrasena.OrderBy(c => random.Next()).ToArray());
        }

        public void ModificarEmail(Usuario usuarioAModificar, Usuario usuarioModificador, string nuevoEmail)
        {
            
            bool existeUsuario = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == usuarioAModificar.Email) != null;
            ManejoExcepciones.ChequearCondiciones(existeUsuario, "El usuario no existe.");

            bool emailEstaVacio = string.IsNullOrEmpty(nuevoEmail);
            ManejoExcepciones.ChequearCondiciones(!emailEstaVacio, "El email no puede ser vacío.");

            bool emailYaExiste = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == nuevoEmail) != null;
            ManejoExcepciones.ChequearPermisos(!emailYaExiste, "El email ya existe.");

            bool esAdmin = usuarioModificador.EsAdmin;
            bool esElUsuario = usuarioModificador.Email == usuarioAModificar.Email;
            bool tienePermisos = esAdmin || esElUsuario;
            ManejoExcepciones.ChequearPermisos(tienePermisos, "No tiene permisos para modificar el email del usuario.");
            
            bool contieneCaracteresNecesarios = nuevoEmail.Contains("@") && nuevoEmail.Contains(".com");
            ManejoExcepciones.ChequearCondiciones(contieneCaracteresNecesarios, "El email debe contener '@' y '.com' para ser válido.");
            
            usuarioAModificar.Email = nuevoEmail;
            _usuarios.Actualizar(usuarioAModificar);
        }
        
        public void actualizar(Usuario usuario)
        {
            _usuarios.Actualizar(usuario);
        }
        
        public void EliminarUsuario(Usuario usuarioAEliminar, Usuario usuarioActual)
        {
            bool esAdmin = usuarioActual.EsAdmin;
            ManejoExcepciones.ChequearPermisos(esAdmin, "No tiene permisos para eliminar el usuario.");

            bool existeUsuario = _usuarios.ObtenerTodosLosUsuarios().Find(u => u.Email == usuarioAEliminar.Email) != null;
            ManejoExcepciones.ChequearPermisos(existeUsuario, "El usuario no existe.");
            
            _usuarios.Eliminar(usuarioAEliminar);
        }
        
        public Usuario IniciarSesion(Usuario usuario)
        {
            var usuarioEncontrado = _usuarios.Login(usuario.Email, usuario.Contrasena);
            if (usuarioEncontrado == null)
            {
                throw new InvalidOperationException("El email y/o contraseña son incorrectos.");
            }
            else
            {
                _sesionLogic.IniciarSesion(usuarioEncontrado);
            }
            return usuarioEncontrado;
        }
        
        public List<Usuario> ListarUsuarios(Usuario usuario)
        {
            return _usuarios.ObtenerTodosLosUsuarios();
        }

        public UsuarioDTO DeUsuarioAUsuarioDTO(Usuario usuario)
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO(usuario.EsAdmin, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.FechaNacimiento, usuario.Contrasena);
            foreach (var notificacion in usuario.Notificaciones)
            {
                usuarioDTO.Notificaciones.Add(DeNotificacionANotificacionDTO((notificacion)));
            }
            usuarioDTO.Papelera = DePapeleraAPapeleraDTO(usuario.Papelera);
            return usuarioDTO;
        }
        
        public NotificacionDTO DeNotificacionANotificacionDTO(Notificacion notificacion)
        {
            NotificacionDTO notificacionDTO = new NotificacionDTO();
            notificacionDTO.Comentario = DeComentarioAComentarioDTO(notificacion.Comentario);
            return notificacionDTO;
        }

        public ComentarioDTO DeComentarioAComentarioDTO(Comentario comentario)
        {
            ComentarioDTO comentarioDTO = new ComentarioDTO(comentario.Titulo, comentario.Estado, DeUsuarioAUsuarioDTO(comentario.UsuarioCreador), DeUsuarioAUsuarioDTO(comentario.UsuarioResolvedor), comentario.FechaResolucion, comentario.Descripcion, DeTareaATareaDTO(comentario.Tarea));
            return comentarioDTO;
        }

        public PapeleraDTO DePapeleraAPapeleraDTO(Papelera papelera)
        {
            PapeleraDTO papeleradto = new PapeleraDTO();
            foreach (var panel in papelera.Paneles)
            {
                papeleradto.Paneles.Add(DePanelAPanelDTO(panel));
            }
            return papeleradto;
        }
        
        public PanelDTO DePanelAPanelDTO(Panel? panel)
        {
            PanelDTO panelDTO = new PanelDTO();
            panelDTO.Nombre = panel.Nombre;
            panelDTO.Descripcion = panel.Descripcion;
            foreach (var tarea in panel.Tareas)
            {
                panelDTO.Tareas.Add(DeTareaATareaDTO(tarea));
            }
            return panelDTO;
        }

        public TareaDTO DeTareaATareaDTO(Tarea tarea)
        {
            TareaDTO tareaDTO = new TareaDTO(tarea.Titulo, tarea.FechaExpiracion, tarea.Descripcion, (int)tarea.Prioridad, DePanelAPanelDTO(tarea.PanelActual), DePanelAPanelDTO(tarea.PanelOriginal), tarea.EsfuerzoEstimado);
            tareaDTO.EsfuerzoInvertido = tarea.EsfuerzoInvertido;
            
            foreach (var comentario in tarea.Comentarios)
            {
                tareaDTO.Comentarios.Add(DeComentarioAComentarioDTO(comentario));
            }
            
            return tareaDTO;
        }
        public string? ValidarYGuardarUsuario(UsuarioDTO usuario)
        {
            try
            {
                
                CrearUsuario(usuario);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}