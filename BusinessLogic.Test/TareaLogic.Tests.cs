using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic.Test;

[TestClass]
public class TareaLogic_Tests
{
    
    private RepositorioEquipo _repositorioEquipo;
    private RepositorioUsuario _repositorioUsuario;
    private ContextoSql _context;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();

    [TestInitialize]
    public void Setup()
    {
        _context = _contextFactory.CrearContextoDb();
        _repositorioEquipo = new RepositorioEquipo(_context);
        _repositorioUsuario = new RepositorioUsuario(_context);
    }


    [TestMethod]
    public void ModificarTituloDeTarea()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel panel = new Panel("nombre", "desc", "equipo1");
        _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
        PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
        TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
        _tareaLogic.ModificarTitulo("titulo", "nuevoTitulo");
        Assert.AreEqual("nuevoTitulo", tarea.Titulo);
    }
   
    [TestMethod]
    public void ModificarDescripcionDeTarea()
    {
        Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
        _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
        Panel panel = new Panel("nombre", "desc", "equipo1");
        _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
        Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
        PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
        _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
        TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
        _tareaLogic.ModificarDescripcion("titulo", "nuevaDesc");
        Assert.AreEqual("nuevaDesc", tarea.Descripcion);
    }
    
       [TestMethod]
       [ExpectedException(typeof(InvalidOperationException))]
       public void ModificatDescripcionDeTareaQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.ModificarDescripcion("tituloNoExiste", "nuevaDesc");
           Assert.AreEqual("nuevaDesc", tarea.Descripcion);
       }

       [TestMethod]
       public void ModificarTituloDeTareaQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           Assert.ThrowsException<InvalidOperationException>(() => _tareaLogic.ModificarTitulo("titulo2", "nuevoTitulo"));
       }

       [TestMethod]
       public void ModificarTituloDeTareaConTituloExistente()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           Tarea tarea2 = new Tarea("titulo2",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           _panelLogic.AgregarTarea("nombre", tarea2, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           Assert.ThrowsException<InvalidOperationException>(() => _tareaLogic.ModificarTitulo("titulo", "titulo2"));
       }

       [TestMethod]
       public void AgregarComentarioATarea()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","titulo", "coment1","comentario", usuarioCreador);
           Assert.AreEqual(1, tarea.Comentarios.Count);
       }

       [TestMethod]
       [ExpectedException(typeof(InvalidOperationException))]
       public void AgregarComentarioATareaConPanelQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombreNoExiste","titulo", "coment1","comentario", usuarioCreador);
       }

       [TestMethod]
       [ExpectedException(typeof(InvalidOperationException))]
       public void AgregarComentarioATareaQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","tituloNoExiste", "coment1","comentario", usuarioCreador);
       }

       [TestMethod]
       public void AgregarComentarioATarea_Cuando_YaExisteUnComentarioConEseTitulo()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre", "titulo", "coment1", "comentario", usuarioCreador);
           Assert.ThrowsException<InvalidOperationException>(() => _tareaLogic.AgregarComentario("equipo1","nombre", "titulo", "coment1", "comentario", usuarioCreador));
       }

       [TestMethod]
       public void ResolverComentarioEnTarea()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","titulo", "coment1","comentario", usuarioCreador);
           ComentarioLogic _comentarioLogic = new ComentarioLogic(_repositorioEquipo, _repositorioUsuario);
           UsuarioDTO usuarioCreadorDto = new UsuarioDTO(usuarioCreador.EsAdmin, usuarioCreador.Nombre, usuarioCreador.Apellido, usuarioCreador.Email, usuarioCreador.FechaNacimiento, usuarioCreador.Contrasena);
           AgregarComentarioDTO agregarComentarioDto = new AgregarComentarioDTO("nombre", "titulo", "coment1", usuarioCreadorDto, "equipo1");
           _comentarioLogic.ResolverComentario(agregarComentarioDto);
           Assert.IsTrue(tarea.Comentarios[0].Estado);
       }

       [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
       public void ResolverComentarioEnPanelQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","titulo", "coment1","comentario", usuarioCreador);
           ComentarioLogic _comentarioLogic = new ComentarioLogic(_repositorioEquipo, _repositorioUsuario);
           UsuarioDTO usuarioCreadorDto = new UsuarioDTO(usuarioCreador.EsAdmin, usuarioCreador.Nombre, usuarioCreador.Apellido, usuarioCreador.Email, usuarioCreador.FechaNacimiento, usuarioCreador.Contrasena);
              AgregarComentarioDTO agregarComentarioDto = new AgregarComentarioDTO("nombreNoExiste", "titulo", "coment1", usuarioCreadorDto, "equipo1");
           _comentarioLogic.ResolverComentario(agregarComentarioDto);
       }

       [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
       public void ResolverComentarioEnTareaQueNoExiste()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","titulo", "coment1","comentario", usuarioCreador);
           ComentarioLogic _comentarioLogic = new ComentarioLogic(_repositorioEquipo, _repositorioUsuario);
           UsuarioDTO usuarioCreadorDto = new UsuarioDTO(usuarioCreador.EsAdmin, usuarioCreador.Nombre, usuarioCreador.Apellido, usuarioCreador.Email, usuarioCreador.FechaNacimiento, usuarioCreador.Contrasena);
           AgregarComentarioDTO agregarComentarioDto = new AgregarComentarioDTO("nombre", "tituloNoExiste", "coment1", usuarioCreadorDto, "equipo1");
           _comentarioLogic.ResolverComentario(agregarComentarioDto);
       }

       [TestMethod]
       public void ValidarUsuarioResolvedorYFechaResolucionDeComentario()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo",  new DateTime(2025, 5, 3),"asdasd",2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.AgregarComentario("equipo1","nombre","titulo", "coment1","comentario", usuarioCreador);
           ComentarioLogic _comentarioLogic = new ComentarioLogic(_repositorioEquipo, _repositorioUsuario);
           UsuarioDTO usuarioCreadorDto = new UsuarioDTO(usuarioCreador.EsAdmin, usuarioCreador.Nombre, usuarioCreador.Apellido, usuarioCreador.Email, usuarioCreador.FechaNacimiento, usuarioCreador.Contrasena);
           AgregarComentarioDTO agregarComentarioDto = new AgregarComentarioDTO("nombre", "titulo", "coment1", usuarioCreadorDto, "equipo1");
           _comentarioLogic.ResolverComentario(agregarComentarioDto);
           Assert.IsTrue(tarea.Comentarios[0].UsuarioResolvedor == usuarioCreador && tarea.Comentarios[0].FechaResolucion.Date == DateTime.Today.Date);
       }
       
       [TestMethod]
       public void ModificarEsfuerzoInvertido_DeberiaModificarEsfuerzo()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo", new DateTime(2025, 5, 3), "asdasd", 2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.ModificarEsfuerzoInvertido("titulo", 5);
           Assert.AreEqual(5, tarea.EsfuerzoInvertido);
       }
       
       [TestMethod]
       [ExpectedException(typeof(InvalidOperationException))]
       public void ModificarEsfuerzoInvertido_TareaNoExiste_DeberiaLanzarExcepcion()
       {
           Usuario usuarioCreador = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
           EquipoLogic _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorioUsuario, _context);
           _equipoLogic.CrearEquipo("equipo1", 10, "descripcion", usuarioCreador);
           Panel panel = new Panel("nombre", "desc", "equipo1");
           _equipoLogic.AgregarPanel("equipo1", panel, usuarioCreador);
           Tarea tarea = new Tarea("titulo", new DateTime(2025, 5, 3), "asdasd", 2, panel, panel);
           PanelLogic _panelLogic = new PanelLogic(_repositorioEquipo, _repositorioUsuario);
           _panelLogic.AgregarTarea("nombre", tarea, "equipo1");
           TareaLogic _tareaLogic = new TareaLogic(_repositorioEquipo, _context);
           _tareaLogic.ModificarEsfuerzoInvertido("tituloNoExiste", 5);
       }
}