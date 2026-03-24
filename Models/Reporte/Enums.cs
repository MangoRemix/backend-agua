namespace backend_agua.Models;

public enum CaudalAgua
{
    Fuerte,
    Debil
}

public enum TipoCisterna
{
    Litros30000 = 30000,
    Litros10000 = 10000
}

public enum TipoTanque
{
    Litros5000 = 5000,
    Litros10000 = 10000
}

public enum EstatusReporte
{
    Incompleto,
    Completado
}

public enum CondicionSalud
{
    Diarrea,
    Vomitos,
    DolorAbdominal,
    Todos
}

public enum PartidoPolitico
{
    PSUV,
    SomosVenezuela,
    Podemos,
    PPT
}
