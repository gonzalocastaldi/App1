using System.ComponentModel.DataAnnotations;
using Domain;

namespace DTOs;

public class UsuarioDTO
{
    public bool EsAdmin { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Contrasena { get; set; }
    
    public PapeleraDTO Papelera { get; set; }
    
    public List<NotificacionDTO> Notificaciones { get; set; }
    
    public UsuarioDTO(bool esAdmin, string nombre, string apellido, string email, DateTime fechaNacimiento, string contrasena){
        EsAdmin = esAdmin;
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        FechaNacimiento = fechaNacimiento;
        Contrasena = contrasena;
    }
    public UsuarioDTO(){}
    
    public Usuario AEntidad(){
        return new Usuario(EsAdmin, Nombre, Apellido, Email, FechaNacimiento, Contrasena);
    }
}
