using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Courses = new List<Course>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}
