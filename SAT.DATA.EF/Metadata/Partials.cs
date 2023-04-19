using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Update;
using SAT.DATA.EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace SAT.DATA.EF.Models
{
    [ModelMetadataType(typeof(CoursesMetadata))]
    public partial class Course { }

    [ModelMetadataType(typeof(EnrollmentsMetadata))]
    public partial class Enrollment { }

    [ModelMetadataType(typeof(ScheduledClassesMetadata))]
    public partial class ScheduledClass { }

    [ModelMetadataType(typeof(ScheduledClassStatusesMetadata))]
    public partial class ScheduledClassStatus { }

    [ModelMetadataType(typeof(StudentsMetadata))]
    public partial class Student { }

    [ModelMetadataType(typeof(StudentStatusesMetadata))]
    public partial class StudentStatus { }
}
