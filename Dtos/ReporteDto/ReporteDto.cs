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
    public int? LitrosCisterna { get; set; }
    public string? TipoCisterna { get; set; }
    public bool TieneTanque { get; set; }
    public string? TipoTanque { get; set; }
    public int FamiliasBeneficiadas { get; set; }
    public int? ApoyoAdicionalLitros { get; set; }
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
    public string? TrancaPropiciaNombre { get; set; }
    public string? TrancaLugar { get; set; }
    public TimeSpan? TrancaDuracion { get; set; }

    public bool TieneConflictos { get; set; }
    public string? ConflictosExplicacion { get; set; }

    public bool TieneFugas { get; set; }
    public string? FugaLugar { get; set; }
    public string? FugaTipo { get; set; }
}

public class ReporteSaludDto
{
    public bool TieneDiarrea { get; set; }
    public int CantidadCasosDiarrea { get; set; }
    public bool TieneVomitos { get; set; }
    public bool TieneDolorAbdominal { get; set; }
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
