namespace IMS.Business.DTOs.Requests
{
    public class ProjectTargetReq
    {
        public Guid? Id { get; set; } 
        public string TargetPeriod { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime TargetEndDate { get; set; }
        public decimal TargetValue { get; set; }
        public string Purpose { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectManager { get; set; }
        public string FinancialHealth { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public string? StepsToComplete { get; set; }
        public string? Attachments { get; set; } // Optional
        public Guid EmployeeId { get; set; }
        public string EmployeeType { get; set; }
        public DateTime? DueDate { get; set; } // Optional
        public string ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}