namespace IMS.Business.DTOs.Requests
{
    public class EmployeeRes
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string CNICNumber { get; set; }
        public string Email { get; set; }
        public string IndustryType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string EmploymentType { get; set; }
        public string Position { get; set; }
        public string? Description { get; set; }
        public string? MaritalStatus { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}