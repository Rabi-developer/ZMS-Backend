using Microsoft.AspNetCore.Http;

namespace IMS.Business.DTOs.Requests;

public class BranchSettingReq
{
    public Guid BranchId { get; set; }
    public Guid CurrentSession { get; set; }
    public string AdmissionNoPrefix { get; set; }
    //Report Header
    public IFormFile RptHeaderLogo { get; set; }
    public string RptHeaderBranchName { get; set; }
    public string RptHeaderBranchAddress { get; set; }
    public string RptHeaderBranchPhone { get; set; }
    public string RptHeaderBranchEmail { get; set; }
    //Report Footer
    public string RptFooterDetail { get; set; }
}