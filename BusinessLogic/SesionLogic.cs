using BusinessLogic;
using DataAccess;
using Domain;

public class SesionLogic
{

    public SesionLogic()
    {
    }

    public Usuario UsuarioLogueado {set; get;}

    public void IniciarSesion(Usuario cualquierUsuario)
    {
        UsuarioLogueado = cualquierUsuario;
    }

    public void CerrarSesion()
    {
        UsuarioLogueado = null;
    }
}