namespace DataAccess;
using Microsoft.EntityFrameworkCore;

public class ContextoEnMemoria
{
    public ContextoSql CrearContextoDb()
    {
        var opcionesConstruccion = new DbContextOptionsBuilder<ContextoSql>();
        opcionesConstruccion.UseInMemoryDatabase("TestDB");
        return new ContextoSql(opcionesConstruccion.Options);
    }
}