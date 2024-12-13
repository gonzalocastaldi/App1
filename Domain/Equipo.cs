namespace Domain;

public class Equipo
{
    public int Id { get; set; }
    private string _nombre;
    private DateTime _fechaCreacion = DateTime.Today;
    private string _descripcion;
    private int _cantidadMaxima;
    private int _cantidadActualUsuarios = 1;
    private List<Usuario> _usuarios = new();
    private List<Panel> _paneles = new();

    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El nombre del equipo no puede ser vacío");
            }

            _nombre = value;
        }
    }

    public List<Usuario> Usuarios
    {
        get => _usuarios;
    }

    public DateTime FechaCreacion
    {
        get => _fechaCreacion;
    }

    public string Descripcion
    {
        get => _descripcion;
        set => _descripcion = value;
    }

    public int CantidadMaxima
    {
        get => _cantidadMaxima;
        set
        {
            ValidarCantidadUsuarios(value);
            _cantidadMaxima = value;
        }
    }

    public int CantidadActualUsuarios
    {
        get => _cantidadActualUsuarios;
    }

    public List<Panel> Paneles
    {
        get => _paneles;
    }

    public Equipo(Usuario usuario)
    {
        ValidarQueCreadorSeaAdmin(usuario);
    }
    
    public Equipo(string nombre, int cantidadMaxima, string descripcion, Usuario usuario)
    {
        ValidarCantidadUsuarios(cantidadMaxima);
        Nombre = nombre;
        CantidadMaxima = cantidadMaxima;
        Descripcion = descripcion;
        ValidarQueCreadorSeaAdmin(usuario);
        Usuarios.Add(usuario);
    }

    public Equipo()
    {
    }

    public void AgregarUsuario(Usuario usuario)
    {
        _usuarios.Add(usuario);
        _cantidadActualUsuarios = _usuarios.Count;
    }
    
    public void AgregarPanel(Panel panel)
    {
        _paneles.Add(panel);
    }

    public void CambiarNombre(String nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }
        Nombre = nombre;
    }
    
    public void CambiarDescripcion(String descripcion)
    {
        Descripcion = descripcion;
    }
    
    public void CambiarCantidadMaximaDeUsuarios(int cantidadMaxima)
    {
        if(cantidadMaxima < 0)
        {
            throw new ArgumentException("La cantidad máxima no puede ser negativa");
        }
        else
        { 
            CantidadMaxima = cantidadMaxima;   
        }
    }

    public void EliminarPanel(string nombrePanel)
    {
        var panel = _paneles.FirstOrDefault(p => p.Nombre == nombrePanel);
        panel?.EliminarTodasLasTareas();
        if (panel != null) _paneles.Remove(panel);
    }

    public void ValidarCantidadUsuarios(int cantidadMaxima)
    {
        if (cantidadMaxima <= 0 || cantidadMaxima > 500)
        {
            throw new ArgumentException("La cantidad máxima tiene que ser un número entre 1 y 500");
        }
    }

    public void ValidarQueCreadorSeaAdmin(Usuario usuario)
    {
        if (!usuario.EsAdmin)
        {
            throw new ArgumentException("No tiene permisos para crear un equipo.");
        }
    }
}