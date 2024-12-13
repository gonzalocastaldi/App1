using Domain;

namespace DTOs.Test;

[TestClass]
public class ComentarioDTOTests
{
    [TestMethod]
    public void CrearComentarioDTO()
    {
        TareaDTO tareaDto = new TareaDTO();
        UsuarioDTO usuario = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        ComentarioDTO comentario = new ComentarioDTO("Titulo", false, usuario, usuario, new DateTime(2000, 12, 12), "desc", tareaDto);
        Assert.AreEqual("desc", comentario.Descripcion);
    }

    [TestMethod]
    public void DeComentarioDTOAComentario()
    {
        TareaDTO tareaDto = new TareaDTO();
        UsuarioDTO usuario = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        ComentarioDTO comentario =
            new ComentarioDTO("Titulo", false, usuario, usuario, new DateTime(2000, 12, 12), "desc", tareaDto);
        Comentario comentarioEntidad = comentario.AEntidad();
        Assert.AreEqual("Titulo", comentarioEntidad.Titulo);
    }
}