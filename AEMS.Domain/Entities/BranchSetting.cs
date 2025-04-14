using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities;

public class BranchSetting
{
    [Key]
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public Guid CurrentSession { get; set; }
    public string AdmissionNoPrefix { get; set; }
    //Report Header
    public Guid? RptHeaderLogoId { get; set; }
    public Attachment? RptHeaderLogo { get; set; }
    public string RptHeaderBranchName { get; set; }
    public string RptHeaderBranchAddress { get; set; }
    public string RptHeaderBranchPhone { get; set; }
    public string RptHeaderBranchEmail { get; set; }
    //Report Footer
    public string RptFooterDetail { get; set; }
}