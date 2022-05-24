using Lab1.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Models
{
    public class DepartmentMoc: IDepartment
    {
        private  List<Department> allDepartment = new List<Department>() { new Department() { Id=1,Name="CS" } , new Department() { Id = 2, Name = "IT" }, new Department() { Id = 3, Name = "IS" } };
        public IEnumerable<Department> GetAllDept()
        {
            return allDepartment; 
        }
        public Department GetById(int id )
        {
            return allDepartment.FirstOrDefault(a => a.Id == id);
        }
        public  void Create(Department student)
        {
            allDepartment.Add(student);
        }
        public void Update(Department newdept, int id )
        {
            Department olddept = allDepartment.FirstOrDefault(a => a.Id == id);
            olddept.Id = newdept.Id;
            olddept.Name = newdept.Name;
        }
        public  void Delete(Department student)
        {
            allDepartment.Remove(student);
        }

        public IEnumerable<Course> GetDeptCourses(int id)
        {
            throw new NotImplementedException();
        }

        public void removeDeptCourse(int id, int courseID)
        {
            throw new NotImplementedException();
        }

        public void addDeptCourse(int id, int courseID)
        {
            throw new NotImplementedException();
        }
    }
    public class DepartmentDB : IDepartment
    {
        appDB dB;
        public DepartmentDB(appDB _db)
        {
            dB = _db;
        }
        public void Create(Department department)
        {
            dB.Add(department);
            dB.SaveChanges();
            
        }

        public void Delete(Department department)
        {
            foreach(var i in department.Students)
            {
                dB.Students.Remove(i);
            }
            dB.Remove(department);
            dB.SaveChanges();
        }

        public IEnumerable<Department> GetAllDept()
        {
            //dB.Departments.FirstOrDefault(a => a.Id == 1).Courses.Add(dB.Courses.FirstOrDefault(b => b.id == 1));
            //dB.SaveChanges();
            return dB.Departments.Include(a => a.Courses).Include(b=>b.Students).ToList();    
        }
        public Department GetById(int id)
        {
            return GetAllDept().FirstOrDefault(a => a.Id == id);
        }

        public void Update(Department newdept, int id)
        {
            Department dept = GetById(id);
            dept.Name = newdept.Name;
            dB.SaveChanges();
        }
    }
}
