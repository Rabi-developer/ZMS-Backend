namespace IMS.Business.DTOs.Requests
{
    public class EmployeeManagementReq
    {
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeeType { get; set; }
        public decimal Salary { get; set; }
        public DateTime? ImportantDates { get; set; }
        public string WorkLocation { get; set; }
        public string Promotion { get; set; }
        public string Position { get; set; }
    }
}
