
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public class StudentCourse
    {
        [ForeignKey("student")]
        public int StdId { get; set; }
        public Student student { get; set; }


        [ForeignKey("course")]
        public int CourseId { get; set; }
        public Course course { get; set; }

        public int? Degree { get; set; }
    }
}
