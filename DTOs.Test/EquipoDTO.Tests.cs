using Domain;

namespace DTOs.Test;

[TestClass]
public class EquipoDTO_Tests
{
    [TestMethod]
    public void CrearEquipoDTO()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(false, "Gonzalo", "Castaldi", "gonzalo@gmail.com", new DateTime(2000, 5, 5), "123456Aa_");
        EquipoDTO equipo = new EquipoDTO("equipo1", 2, "desc", usuariodto);
        Assert.AreEqual("equipo1", equipo.Nombre);
    }

    [TestMethod]
    public void DeEquipoDTOAEquipo()
    {
        UsuarioDTO usuariodto = new UsuarioDTO(true, "Gonzalo", "Castaldi", "gonzalo@gmail.com",
            new DateTime(2000, 5, 5), "123456Aa_");
        EquipoDTO equipo = new EquipoDTO("equipo1", 2, "desc", usuariodto);
        Equipo equipoEntidad = equipo.AEntidad();
        Assert.AreEqual("equipo1", equipoEntidad.Nombre);
    }
}