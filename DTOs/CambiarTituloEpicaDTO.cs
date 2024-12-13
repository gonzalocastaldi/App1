namespace DTOs;

public class CambiarTituloEpicaDTO
{
    public string nombreEquipo { get; set; }
    public string nombrePanel { get; set; }
    public string nuevoTitulo { get; set; }
    public string tituloOriginal { get; set; }
    
    public CambiarTituloEpicaDTO(string nombreEquipo, string nombrePanel, string nuevoTitulo, string tituloOriginal)
    {
        this.nombreEquipo = nombreEquipo;
        this.nombrePanel = nombrePanel;
        this.nuevoTitulo = nuevoTitulo;
        this.tituloOriginal = tituloOriginal;
    }
}