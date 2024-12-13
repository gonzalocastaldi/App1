using System.Runtime.CompilerServices;
using DataAccess;
using Domain;
using DTOs;

namespace BusinessLogic.Test;

[TestClass]
public class UsuarioLogicTests
{
    private RepositorioUsuario _repositorio;
    private ContextoSql _context;
    private readonly ContextoEnMemoria _contextFactory = new ContextoEnMemoria();
    private RepositorioEquipo _repositorioEquipo;
    private EquipoLogic _equipoLogic;
    private UsuarioLogic usuarioLogic;

    [TestInitialize]
    public void Setup()
    {
        _context = _contextFactory.CrearContextoDb();
        _repositorio = new RepositorioUsuario(_context);
        _equipoLogic = new EquipoLogic(_repositorioEquipo, _repositorio, _context);
    }

    [TestMethod]
    public void PruebaCrearUsuario()
    {
        UsuarioDTO usuario = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuario);
        Assert.IsTrue(_repositorio.ObtenerTodosLosUsuarios().Any(u => u.Email == "gonzalo@gmail.com"));
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void PruebaCrearUsuarioRepetido()
    {
        UsuarioDTO usuario = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuario);
        UsuarioDTO segundoUsuario = new UsuarioDTO(false, "Gonzalo", "Ramos", "gonzalo@gmail.com", new DateTime(2000, 8, 2), "123456Aa=");
        usuarioLogic.CrearUsuario(segundoUsuario);
    }

    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PruebaCrearUsuarioConDatoErroneo()
    {
        UsuarioDTO usuario = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2026, 5, 3), "123456Aa");
        usuarioLogic.CrearUsuario(usuario);
    }

    [TestMethod]
    public void ModificarNombreUsuario_CuandoEsAdmin()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        UsuarioDTO segundoUsuariodto = new UsuarioDTO(true, "Juan", "Gonzalez", "gonzalez@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(segundoUsuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        Usuario usuarioModificador = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalez@gmail.com");
        usuarioLogic.ModificarNombre(usuarioModificador, usuario, "Martin");
        Assert.AreEqual(usuario.Nombre, "Martin");
    }

    [TestMethod]
    public void ModificarNombreUsuario_CuandoEsElPropioUsuario()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        usuarioLogic.ModificarNombre(usuario, usuario, "Ivan");
        Assert.AreEqual(usuario.Nombre, "Ivan");
    }

    [TestMethod]
    public void ModificarApellidoUsuario_CuandoEsElPropioUsuario()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        usuarioLogic.ModificarApellido(usuario, usuario, "Martinez");
        Assert.AreEqual(usuario.Apellido, "Martinez");
    }

    [TestMethod]
    public void ModificarApellidoUsuarioCuandoEsAdmin()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario admin = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        UsuarioDTO segundoUsuariodto = new UsuarioDTO(false, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(segundoUsuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "joaquin@gmail.com");
        usuarioLogic.ModificarApellido(admin, usuario, "Martinez");
        Assert.AreEqual(usuario.Apellido, "Martinez");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarApellidoUsuarioCuandoNoEsAdmin()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario admin = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        UsuarioDTO segundoUsuariodto = new UsuarioDTO(false, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(segundoUsuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "joaquin@gmail.com");
        usuarioLogic.ModificarApellido(admin, usuario, "Martinez");
        Assert.AreEqual(usuario.Apellido, "Martinez");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarApellidoUsuarioNoExistente()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario admin = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        Usuario usuario = new Usuario();
        usuarioLogic.ModificarApellido(admin, usuario, "Martinez");
        Assert.AreEqual(usuario.Apellido, "Martinez");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarNombreUsuario_CuandoNoDeberia()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        UsuarioDTO segundoUsuariodto = new UsuarioDTO(false, "Juan", "Gonzalez", "gonzalez@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(segundoUsuariodto);
        Usuario usuario2 = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalez@gmail.com");
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        usuarioLogic.ModificarNombre(usuario2, usuario, "Ivan");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarNombreAUsuarioQueNoExiste()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.CrearUsuario(usuariodto);
        Usuario usuario = _repositorio.ObtenerTodosLosUsuarios().Find(u => u.Email == "gonzalo@gmail.com");
        Usuario usuarioNoExistente = new Usuario();
        usuarioLogic.ModificarNombre(usuarioNoExistente, usuario, "Ivan");
    }

    [TestMethod]
    public void ModificarContrasena_CuandoEsAdmin()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioAdmin = new Usuario(true, "Juan", "Gonzalez", "gonzalez@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioAdmin);
        usuarioLogic.ReiniciarContraseña(usuarioAdmin, usuario);
        Assert.AreNotEqual(usuario.Contrasena, "123456Aa=2");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarContrasena_CuandoNoEsAdmin()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioAdmin = new Usuario(false, "Juan", "Gonzalez", "gonzalez@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioAdmin);
        usuarioLogic.ReiniciarContraseña(usuarioAdmin, usuario);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarContrasena_CuandoEsElPropioUsuario()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.ReiniciarContraseña(usuario, usuario);
    }

    [TestMethod]
    public void ModificarEmail_CuandoEsAdmin()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioAdmin = new Usuario(true, "Martin", "Rondon", "martin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioAdmin);
        usuarioLogic.ModificarEmail(usuario, usuarioAdmin, "gcastaldi@gmail.com");
        Assert.AreEqual(usuario.Email, "gcastaldi@gmail.com");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarEmail_CuandoNoEsAdmin()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioNoAdmin = new Usuario(false, "Martin", "Rondon", "martin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioNoAdmin);
        usuarioLogic.ModificarEmail(usuario, usuarioNoAdmin, "gcastaldi@gmail.com");
    }

    [TestMethod]
    public void ModificarEmail_CuandoEsElPropioUsuario()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        usuarioLogic.ModificarEmail(usuario, usuario, "gcastaldi@gmail.com");
        Assert.AreEqual(usuario.Email, "gcastaldi@gmail.com");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ModificarEmailDejandoloNulo()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        usuarioLogic.ModificarEmail(usuario, usuario, "");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ModificarEmailConEmailExistente()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuario2 = new Usuario(false, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario2);
        usuarioLogic.ModificarEmail(usuario, usuario, "joaquin@gmail.com");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void EliminarUsuarioSiendoAdmin()
    {
        Usuario usuarioModificador = new Usuario(true, "Gonzalo", "Castaldi", "gonalo@gmail.com", new DateTime(2000, 12, 12), "Admin1234_");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificador);
        Usuario usuarioModificado = new Usuario(false, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificado);
        usuarioLogic.EliminarUsuario(usuarioModificado, usuarioModificador);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void EliminarUsuarioSinSerAdmin()
    {
        Usuario usuarioModificado = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificado);
        Usuario usuarioModificador = new Usuario(false, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificador);
        usuarioLogic.EliminarUsuario(usuarioModificado, usuarioModificador);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void EliminarUsuarioNoExistente()
    {
        Usuario usuarioModificado = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificado);
        Usuario usuarioModificador = new Usuario(true, "Joaquin", "Mauri", "joaquin@gmail.com", new DateTime(2002, 9, 5), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioModificador);
        Usuario usuario = new Usuario();
        usuarioLogic.EliminarUsuario(usuario, usuarioModificador);
    }

    [TestMethod]
    public void IniciarSesionUsuarioExistente()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "Admin1234_");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioSesion = usuarioLogic.IniciarSesion(usuario);
        Assert.AreEqual(usuario.Email, usuarioSesion.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void IniciarSesionUsuarioNoExistente()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.IniciarSesion(usuario);
    }

    [TestMethod]
    public void PruebaListaUsuarios()
    {
        Usuario usuario = new Usuario(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        List<Usuario> usuarios = usuarioLogic.ListarUsuarios(usuario);
        CollectionAssert.AreEquivalent(_repositorio.ObtenerTodosLosUsuarios(), usuarios);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ModificarEmailSinArroba()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioAdmin = new Usuario(true, "Martin", "Rondon", "martin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioAdmin);
        usuarioLogic.ModificarEmail(usuario, usuarioAdmin, "gcastaldigmail.com");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ModificarEmailSinPuntoCom()
    {
        Usuario usuario = new Usuario(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuario);
        Usuario usuarioAdmin = new Usuario(true, "Martin", "Rondon", "martin@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        _repositorio.ObtenerTodosLosUsuarios().Add(usuarioAdmin);
        usuarioLogic.ModificarEmail(usuario, usuarioAdmin, "gcastaldi@gmail");
    }

    [TestMethod]
    public void TestValidarYGuardarUsuario()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2004, 5, 3), "123456Aa=");
        usuarioLogic.ValidarYGuardarUsuario(usuariodto);
        Assert.IsTrue(_repositorio.ObtenerTodosLosUsuarios().Any(u => u.Email == "gonzalo@gmail.com"));
    }
}