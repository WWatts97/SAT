using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF
{
    public class StudentsMetadata
    {
        public int StudentId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string Major {  get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

    }

    public class StudentStatusesMetadata
    {
        public int SSID { get; set; }

        public string SSName { get; set; }

        [Required]
        public string SSDecription { get; set; }
    }

    public class ScheduledClassesMetadata
    {
        public int ScheduledClassId { get; set; }

        public int CourseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string InstructorName { get; set; }

        public string Location { get; set; }

        public int SCSID { get; set; }
    }

    public class EnrollmentsMetadata
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int ScheduledClassId { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }

    public class ScheduledClassStatusesMetadata
    {
        public int SCSID { get; set; }

        public string SCSName { get; set; }

    }

    public class CoursesMetadata
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string CourseDescription { get; set; }

        [Required]
        public int CreditHours { get; set; }

        public string Curriculum { get; set; }

        public string Notes { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

}
