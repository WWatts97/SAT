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

        [Display(Name = "Status")]
        public int Ssid { get; set; }

        [Display(Name = "Status")]
        public virtual StudentStatus Ss { get; set; }
    }

    public class StudentStatusesMetadata
    {
        public int SSID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Status")]
        public string Ssname { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Status Description")]
        public string Ssdescription { get; set; }
    }

    public class ScheduledClassesMetadata
    {
        public int ScheduledClassId { get; set; }

        [Display(Name ="Course")]
        public int CourseId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [StringLength(40)]
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [Display(Name = "Class Status")]
        public int Scsid { get; set; }

        [Display(Name = "Class Status")]
        public virtual ScheduledClassStatus? Scs { get; set; }
    }

    public class EnrollmentsMetadata
    {
        public int EnrollmentId { get; set; }

        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Scheduled Class")]
        public int ScheduledClassId { get; set; }

        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Scheduled Class")]
        public virtual ScheduledClass ScheduledClass { get; set; }
    }

    public class ScheduledClassStatusesMetadata
    {
        public int SCSID { get; set; }

        [StringLength(50)]
        [Display(Name = "Class Status")]
        public string Scsname { get; set; }

    }

    public class CoursesMetadata
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [Display(Name ="Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; }

        [StringLength(250)]
        public string Curriculum { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }
    }

}
