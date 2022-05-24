using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public class Course
    {
        public Course()
        {
            Departments = new HashSet<Department>();
            studentCourses = new HashSet<StudentCourse>();

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<StudentCourse> studentCourses { get; set; }

      

    }
}
