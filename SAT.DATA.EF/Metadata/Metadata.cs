using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF.Models
{
    public class StudentsMetadata
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string Major {  get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [StringLength(10)]
        [Display(Name = "Zip")]
        public string ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(13)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(60)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Image")]
        public string PhotoUrl { get; set; }
    }

    public class StudentStatusesMetadata
    {
        public int SSID { get; set; }

        [Required]
        [StringLength(30)]
        public string SSName { get; set; }

        [Required]
        [StringLength(250)]
        public string SSDecription { get; set; }
    }

    public class ScheduledClassesMetadata
    {
        public int ScheduledClassId { get; set; }

        public int CourseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(40)]
        public string InstructorName { get; set; }

        [StringLength(20)]
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

        [StringLength(50)]
        public string SCSName { get; set; }

    }

    public class CoursesMetadata
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string CourseDescription { get; set; }

        [Required]
        public int CreditHours { get; set; }

        [StringLength(250)]
        public string Curriculum { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

}
