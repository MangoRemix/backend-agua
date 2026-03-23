using backend_agua.Dtos.Common;
using backend_agua.Models;

namespace backend_agua.Dtos.Usuario;

public class UsuarioFilterDto : PaginationParams
{
    public Guid? ParroquiaId { get; set; }
    public Guid? ComunaId { get; set; }
    public Guid? ComunidadId { get; set; }
    public StatusUsuario? Status { get; set; }
    public UsuarioRol? Rol { get; set; }
}
