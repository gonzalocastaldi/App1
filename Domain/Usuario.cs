namespace Domain;

public class Usuario
{
    public int Id { get; set; }
    private bool _esAdmin;
    private string _nombre;
    private string _apellido;
    private string _email;
    private DateTime _fechaNacimiento;
    private string _contrasena;
    public Papelera Papelera { get; set; }
    private List<Notificacion> _notificaciones = new List<Notificacion>();

    public Usuario(bool esAdmin, string nombre, string apellido, string email, DateTime fechaNacimiento, string contrasena)
    {
        ValidarContrasena(contrasena);
        ValidarFechaNacimiento(fechaNacimiento);
        ValidarEmail(email);
        _esAdmin = esAdmin;
        _nombre = nombre;
        _apellido = apellido;
        _email = email;
        _fechaNacimiento = fechaNacimiento;
        _contrasena = contrasena;
        Papelera = new Papelera();
    }

    public Usuario()
    {
    }

    public List<Notificacion> Notificaciones
    {
        get => _notificaciones;
        set => _notificaciones = value;
    }
    public string Nombre
    {
        get => _nombre;
        set => _nombre = value;
    }
    
    public string Apellido
    {
        get => _apellido;
        set => _apellido = value;
    }
    
    public string NombreCompleto => $"{_nombre} {_apellido}";

    public string Email
    {
        get => _email;
        set => _email = value;

    }

    public DateTime FechaNacimiento
    {
        get => _fechaNacimiento;
        set => _fechaNacimiento = value;

    }

    public string Contrasena
    {
        get => _contrasena;
        set
        {
            ValidarContrasena(value);
            _contrasena = value;
        }
    }

    public bool EsAdmin
    {
        get => _esAdmin;
        set => _esAdmin = value;
    }

    public IEnumerable<Equipo>? Equipos { get; set; }

    private void ValidarContrasena(string contrasena)
    {
        if (contrasena == null || contrasena.Length < 8)
        {
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");
        }

        if (!contrasena.Any(char.IsUpper))
        {
            throw new ArgumentException("La contraseña debe tener al menos una letra mayuscula");
        }

        if (!contrasena.Any(char.IsLower))
        {
            throw new ArgumentException("La contraseña debe tener al menos una letra minuscula");
        }

        if (!contrasena.Any(char.IsDigit))
        {
            throw new ArgumentException("La contraseña debe tener al menos un número");
        }
        
        if (!contrasena.Any(c => (c >= 33 && c <= 47) || (c >= 58 && c <= 64) || (c >= 91 && c <= 96) || (c >= 123 && c <= 126)))
        {
            throw new ArgumentException("La contraseña debe tener al menos un caracter especial");
        }
    }

    private void ValidarFechaNacimiento(DateTime? fechaNacimiento)
    {
        if (!fechaNacimiento.HasValue)
        {
            throw new ArgumentException("Debe ingresar una fecha de nacimiento.");
        }
        else if (fechaNacimiento.Value < new DateTime(1900,1,1) || fechaNacimiento.Value > DateTime.Now)
        {
            throw new ArgumentException("Debe ingresar una fecha de nacimiento correcta.");
        }
    }
    
    private void ValidarEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Debe ingresar un email.");
        }
        if (!email.Contains("@") || !email.Contains(".com"))
        {
            throw new ArgumentException("El email debe contener '@' y '.com' para ser válido.");
        }
    }
    
}