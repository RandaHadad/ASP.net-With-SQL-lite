using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class DepartmentController : Controller
    {
        //Lab2
        //DepartmentMoc db = new DepartmentMoc();
  
        IDepartment db;
        ICourse course;
        public DepartmentController(IDepartment _db,ICourse _course)
        {
            db = _db;
            course = _course;
        }
        public IActionResult Index()
        {
            IEnumerable<Department> depts = db.GetAllDept();
            return View(depts);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int id)
        {
            Department department = db.GetById(id);
            return View(department);
        }
        public IActionResult Courses(int id)
        {
            Department depart = db.GetById(id);
            IEnumerable<Course> lstcourse = course.GetDeptCourses(id);
            IEnumerable<Course> lstnotcourse = course.GetAllCourses().Except(lstcourse);
            CourseDepartment cd = new CourseDepartment()
            {
                CourseInDept = lstcourse,
                CourseNotInDept = lstnotcourse,
                department = depart
            };
            return View(cd);
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Create(department);
                return RedirectToAction("Index");

            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult Update(Department department, int id)
        {
            if (ModelState.IsValid)
            {

                db.GetById(id).Name = department.Name;
                db.GetById(id).Id = department.Id;
                db.Update(department, id);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Department department = db.GetById(id);
            db.Delete(department);
            return RedirectToAction("Index");
        }

        public IActionResult updateDept(int deptId , int[] InCourse,int[] NotInCourse)
        {
     
            foreach(var i in InCourse)
            {
                course.removeDeptCourse(deptId, i);
            }
            foreach (var i in NotInCourse)
            {
                course.addDeptCourse(deptId, i);
            }
            return RedirectToAction("Index");
        }
    }
}
