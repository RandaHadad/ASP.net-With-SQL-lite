using Lab1.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Models
{
    public interface IStudent
    {
        public IEnumerable<Student> AllStudents();
        public  void Create(Student student);
        public  void Update(Student newstudent);
        public  void Delete(Student student);
    }
    public class StudentMoc:IStudent
    {
        private  List<Student> allStudents = new List<Student>();
        public  IEnumerable<Student> AllStudents()
        {
             return allStudents; 
        }
        public  void Create(Student student)
        {
            allStudents.Add(student);
        }
        public  void Update(Student newstudent)
        {
            Student oldstudent = allStudents.FirstOrDefault(a => a.Id == newstudent.Id);
        
            oldstudent.Name = newstudent.Name;
            oldstudent.Email = newstudent.Email;
            oldstudent.Password = newstudent.Password;
            oldstudent.CPassword = newstudent.Password;
        }
        public  void Delete(Student student)
        {
            allStudents.Remove(student);
        }

     
    }
    public class StudentDB : IStudent
    {
        appDB db; //Lab4  = new appDB(); 
        public StudentDB(appDB _db)
        {
            db = _db;
        }
        public IEnumerable<Student> AllStudents()
        {
            return db.Students.Include(a=>a.department).ThenInclude(d=>d.Courses).Include(b=>b.studentCourses).ThenInclude(c=>c.course).ToList();
        }

        public void Create(Student student)
        {
            Department dept = db.Departments.Include(a => a.Courses).FirstOrDefault(b => b.Id == student.DeptNo);
            foreach(var i in dept.Courses)
            {
                student.studentCourses.Add(new StudentCourse()
                {
                    CourseId = i.id,
                    StdId=student.Id
                });
            }
            db.Add(student);
            db.SaveChanges();
        }

        public void Delete(Student student)
        {
            student.studentCourses = new List<StudentCourse>();
            db.Students.Remove(student);
            db.SaveChanges();

        }

        public void Update(Student newstudent)
        {
            Student oldstudent = AllStudents().FirstOrDefault(a => a.Id == newstudent.Id);
            oldstudent.Name = newstudent.Name;
            oldstudent.Email = newstudent.Email;
            oldstudent.DeptNo = newstudent.DeptNo;
            oldstudent.Password = newstudent.Password;
            oldstudent.CPassword = newstudent.CPassword;
            db.SaveChanges();
        }
    }
}
