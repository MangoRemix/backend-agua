using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class ReporteDto
{
    public Guid Id { get; set; }
    public Guid ComunidadId { get; set; }
    public Guid UsuarioId { get; set; }
    public string? UsuarioNombre { get; set; }
    
    // Información Territorial
    public string? ParroquiaNombre { get; set; }
    public string? ParroquiaLiderNombre { get; set; }
    public string? ParroquiaLiderCedula { get; set; }
    public string? ParroquiaLiderTlf { get; set; }

    public string? ComunaNombre { get; set; }
    public string? ComunaLiderNombre { get; set; }
    public string? ComunaLiderCedula { get; set; }
    public string? ComunaLiderTlf { get; set; }

    public string? ComunidadNombre { get; set; }
    public string? ComunidadLiderNombre { get; set; }
    public string? ComunidadLiderCedula { get; set; }
    public string? ComunidadLiderTlf { get; set; }

    public DateTime FechaCreacion { get; set; }
    public string Estatus { get; set; } = string.Empty;
    public bool IsLeido { get; set; }
    public double SubmissionRemainingSeconds { get; set; }

    public ReporteSuministroDto Suministro { get; set; } = new();
    public ReporteIncidenciasDto Incidencias { get; set; } = new();
    public ReporteSaludDto Salud { get; set; } = new();
    public ReporteParticipacionDto Participacion { get; set; } = new();
}

public class ReporteSuministroDto
{
    public bool LlegaPorTuberia { get; set; }
    public TimeSpan? HorasSuministro { get; set; }
    public string? Caudal { get; set; }
    public bool RecibeCisterna { get; set; }
    public List<CisternaDto> Cisternas { get; set; } = new();
    public bool TieneTanque { get; set; }
    public List<TanqueDto> Tanques { get; set; } = new();
    public int FamiliasBeneficiadas { get; set; }
    public int? ApoyoAdicionalLitros { get; set; }
}

public class CisternaDto
{
    public Guid Id { get; set; }
    public int Litros { get; set; }
    public string Tipo { get; set; } = string.Empty;
}

public class TanqueDto
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
}

public class ReporteIncidenciasDto
{
    public bool TieneVentaIlegal { get; set; }
    public string? ChoferNombreApellido { get; set; }
    public string? ChoferCedula { get; set; }
    public string? VehiculoMarcaModelo { get; set; }
    public string? VehiculoPlaca { get; set; }
    public string? VehiculoColor { get; set; }

    public bool TieneTrancas { get; set; }
    public List<OutputTrancaDto> Trancas { get; set; } = new();

    public bool TieneConflictos { get; set; }
    public List<OutputConflictoDto> Conflictos { get; set; } = new();

    public bool TieneFugas { get; set; }
    public List<OutputFugaDto> Fugas { get; set; } = new();
}

public class OutputTrancaDto
{
    public Guid Id { get; set; }
    public string? PropiciaNombre { get; set; }
    public string? Lugar { get; set; }
    public TimeSpan? Duracion { get; set; }
}

public class OutputConflictoDto
{
    public Guid Id { get; set; }
    public string? Explicacion { get; set; }
}

public class OutputFugaDto
{
    public Guid Id { get; set; }
    public string? Lugar { get; set; }
    public string? Tipo { get; set; }
}

public class ReporteSaludDto
{
    public bool TieneDiarrea { get; set; }
    public int CantidadCasosDiarrea { get; set; }
    public bool TieneVomitos { get; set; }
    public int CantidadCasosVomitos { get; set; }
    public bool TieneDolorAbdominal { get; set; }
    public int CantidadCasosDolorAbdominal { get; set; }
    public List<PersonaAfectadaDto> PersonasAfectadas { get; set; } = new();
}

public class ReporteParticipacionDto
{
    public bool TienePartido { get; set; }
    public string? PartidoNombre { get; set; }

    public bool TieneAlcaldia { get; set; }
    public string? DetalleAlcaldia { get; set; }

    public bool TieneGobernacion { get; set; }
    public string? DetalleGobernacion { get; set; }

    public bool TieneInstitucionNacional { get; set; }
    public string? DetalleInstitucionNacional { get; set; }
}
