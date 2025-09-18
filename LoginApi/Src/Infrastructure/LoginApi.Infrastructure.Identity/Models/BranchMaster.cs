using System.ComponentModel.DataAnnotations;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models;

public class BranchMaster : AuditableBaseEntity
{
    [Required] public string VerticalId { get; set; }

    [Required] public string CorporateCode { get; set; }

    [Required] public string CorporateName { get; set; }

    [Required] public string ROCode { get; set; }

    [Required] public string ROName { get; set; }

    [Required] public string BranchCode { get; set; }

    [Required] public string BranchName { get; set; }

    public string FACode { get; set; }
    public string RBILicNo { get; set; }

    public string BranchHeadsId { get; set; }

    [Required] public string Place { get; set; }

    [Required] public string Street { get; set; }

    [Required] public string State { get; set; }

    [Required] public string City { get; set; }

    [Required] public string Pin { get; set; }

    [Required] public string Email { get; set; }

    public string Zone { get; set; }

    [Required] public string Phone { get; set; }

    public string Fax { get; set; }
    public string Mobile { get; set; }
    public string Website { get; set; }

    public string ServiceTaxNo { get; set; }
    public string CIN_No { get; set; }
    public string PAN { get; set; }
    public string GSTIN { get; set; }
    public string ISDNo { get; set; }
    public new int Status { get; set; }
}