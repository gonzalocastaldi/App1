using Domain;

namespace DTOs.Test;

[TestClass]
public class UsuarioDTOTests
{
    [TestMethod]
    public void CrearUsuarioDTO()
    {
        UsuarioDTO user = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        Assert.AreEqual("Castaldi", user.Apellido);
    }
    
    [TestMethod]
    public void DeUsuarioDTOAUsuario()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        Usuario usuario = usuariodto.AEntidad();
        Assert.AreEqual("Gonzalo", usuario.Nombre);
    }
}