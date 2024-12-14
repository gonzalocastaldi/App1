using Domain;

namespace DTOs;

public class AgregarEpicaDto
{
    public Equipo EquipoSeleccionado { get; set; }
    public Epica Epica { get; set; }
    public Panel PanelSeleccionado { get; set; }
    public Usuario UsuarioLogueado { get; set; }
}