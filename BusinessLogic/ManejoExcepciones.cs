namespace BusinessLogic;

public static class ManejoExcepciones
{
    public static void ChequearCondiciones(bool condicion, string mensaje = "Invariant condition failed")
    {
        if (condicion)
        {
            return;
        }
        throw new ArgumentException(mensaje);
    }
    
    public static void ChequearPermisos(bool condicion, string mensaje = "Invariant condition failed")
    {
        if (condicion)
        {
            return;
        }

        throw new InvalidOperationException(mensaje);
    }
}