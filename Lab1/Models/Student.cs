using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public class Student
    {
        public Student()
        {
            studentCourses = new HashSet<StudentCourse>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Remote("CheckEmail","Student",ErrorMessage ="Email is Already Exists", AdditionalFields ="Id")]
        public String Email { get; set; }


        /// <summary>
        /// Lab4
        /// </summary>
        [Required]
        public String Password { get; set; }

        [Compare("Password")]
        [NotMapped]
        public String CPassword { get; set; }

        [ForeignKey("department")]
        [RegularExpression("^[0-9]", ErrorMessage = "Id Must be a Number")]
        public int DeptNo { get; set; }

        public virtual Department? department { get; set; }

        public virtual ICollection<StudentCourse> studentCourses { get; set; }

    }
}