using Domain;

namespace DTOs.Test;

[TestClass]
public class AgregarComentarioDto
{
    [TestMethod]
    public void CrearAgregarComentarioDto()
    {
        UsuarioDTO usuarioLogueado = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        AgregarComentarioDTO agregarComentario = new AgregarComentarioDTO("panelSeleccionado","tareaSeleccionada","comentarioTitulo",usuarioLogueado, "equipo");
        Assert.AreEqual("comentarioTitulo", agregarComentario.ComentarioTitulo);
    }
}