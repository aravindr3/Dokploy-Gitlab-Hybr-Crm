using System.ComponentModel.DataAnnotations;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models;

public class BranchUser : AuditableBaseEntity
{
    [Required] public string BranchId { get; set; }

    [Required] public string UserId { get; set; }
}