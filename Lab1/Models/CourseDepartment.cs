namespace Lab1.Models
{
    public class CourseDepartment
    {
        public IEnumerable<Course> CourseInDept { get; set; }
        public IEnumerable<Course> CourseNotInDept { get; set; }
        public Department department { get; set; }
    }
}
