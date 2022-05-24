namespace Lab1.Models
{
    public interface IDepartment
    {
        public IEnumerable<Department> GetAllDept();
        public Department GetById(int id);
        public void Create(Department department);
        public void Delete(Department department);
        public void Update(Department newdept, int id);
        //public IEnumerable<Course> GetDeptCourses(int id);
        //public void removeDeptCourse(int id, int courseID);
        //public void addDeptCourse(int id, int courseID);
        //public IEnumerable<Course> GetAllCourses();
    }
}
