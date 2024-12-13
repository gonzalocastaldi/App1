using Domain;

namespace DTOs;

public class EquipoDTO
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int CantidadMaxima { get; set; }
    public List<UsuarioDTO> Usuarios = new List<UsuarioDTO>();
    public List<PanelDTO> Paneles = new List<PanelDTO>();
    
    public EquipoDTO(string nombre, int cantidadMaxima, string descripcion, UsuarioDTO usuario)
    {
        Nombre = nombre;
        CantidadMaxima = cantidadMaxima;
        Descripcion = descripcion;
        Usuarios.Add(usuario);
    }
    
    public EquipoDTO()
    {
    }
    
    public Equipo AEntidad()
    {
        var equipo = new Equipo(Nombre, CantidadMaxima, Descripcion, Usuarios[0].AEntidad());
        foreach (var usuario in Usuarios)
        {
            equipo.Usuarios.Add(usuario.AEntidad());
        }
        foreach (var panel in Paneles)
        {
            equipo.Paneles.Add(panel.AEntidad());
        }
        return equipo;
    }

}