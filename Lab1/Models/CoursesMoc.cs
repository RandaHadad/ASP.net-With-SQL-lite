using Lab1.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lab1.Models
{

    public interface ICourse
    {
        public IEnumerable<Course> GetDeptCourses(int id);
        public Course GetCourseById(int id);
        public void removeDeptCourse(int id, int courseID);
        public void addDeptCourse(int id, int courseID);
        public IEnumerable<Course> GetAllCourses();
    }
    public class CoursesMoc : ICourse
    {
        appDB dB;
        public CoursesMoc(appDB _db)
        {
            dB = _db;
        }
        public void addDeptCourse(int id, int courseID)
        {
            Department dept = dB.Departments.Include(a => a.Courses).Include(b=>b.Students).ThenInclude(c=>c.studentCourses).FirstOrDefault(a => a.Id == id);
            foreach(var i in dept.Students)
            {
                i.studentCourses.Add(
                new StudentCourse()
                {
                    CourseId = courseID,
                    StdId= i.Id,
                }) ;
            }
            dept.Courses.Add(dB.Courses.FirstOrDefault(b => b.id == courseID));
            dB.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return dB.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return dB.Courses.FirstOrDefault(a=>a.id== id);
        }

        public IEnumerable<Course> GetDeptCourses(int id)
        {
            return dB.Departments.Include(a => a.Courses).FirstOrDefault(a => a.Id == id).Courses;
        }

        public void removeDeptCourse(int id, int courseID)
        {
            Department dept = dB.Departments.Include(a => a.Courses).Include(b => b.Students).ThenInclude(c=>c.studentCourses).FirstOrDefault(a => a.Id == id);
            foreach (var i in dept.Students)
            {
               
                i.studentCourses.Remove(i.studentCourses.FirstOrDefault(a => a.CourseId == courseID));
            }
            dept.Courses.Remove(dB.Courses.FirstOrDefault(b => b.id == courseID));
            dB.SaveChanges();
        }
    }
}
