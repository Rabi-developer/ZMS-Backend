using IMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.Entities
{
    public class EmployeeManagement : GeneralBase
    {
       
        public Guid? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string? Department { get; set; }

        public string JobTitle { get; set; }

 
        public DateTime HireDate { get; set; }

       
        public string EmployeeType { get; set; }

       
        public decimal Salary { get; set; }

        public DateTime? ImportantDates { get; set; }

        public string WorkLocation { get; set; }

        public string Promotion { get; set; }

        public string Position { get; set; }


        // Navigation property to Employee entity
        
    }
}