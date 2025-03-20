using IMS.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.DTOs.Responses
{
    public class AttendanceRes
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public bool? Uniform { get; set; }
        public bool? Late { get; set; }


    }
    public class AttendanceListRes
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public bool? Uniform { get; set; }
        public bool? Late { get; set; }
        public string FullName { get; set; }
        public string AdmissionNo { get; set; }
        public string MobilePhone { get; set; }
    }

    public class AttendanceReportRes
    {
        public Guid StudentId { get; set; }
        public Guid SectionId { get; set; }
        public int Present { get; set; }  // Number of days present
        public int Leave { get; set; }    // Number of days on leave
        public int Absent { get; set; }   // Number of days absent
        public int Late { get; set; }     // Number of days late
        public int Uniform { get; set; }
    }
}


