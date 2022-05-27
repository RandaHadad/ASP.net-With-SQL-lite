using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Controllers
{
    public class StudentController : Controller
    {
        IStudent db;
        IDepartment depts;
        public StudentController(IStudent _db, IDepartment _depts)
        {
            db = _db;
            depts = _depts;
        }

        public IActionResult Index(String serchedname)
        {
            IEnumerable<Student> std;
            if (serchedname == String.Empty || serchedname== null)
            {
                std = db.AllStudents();

            }
            else

            {
                std = db.AllStudents().Where(a => a.Name.ToLower().Contains(serchedname.ToLower()));

            }

            return View(std);
        }

        // HTTP GET VERSION

        public IActionResult Create()
        {

            SelectList dept = new SelectList(depts.GetAllDept(), "Id", "Name");
            ViewBag.depts = dept;
            return View();
        }
        public IActionResult Update(int id)
        {
            Student student = db.AllStudents().Where(s => s.Id == id).FirstOrDefault();

            SelectList dept = new SelectList(depts.GetAllDept(), "Id", "Name");
            ViewBag.depts = dept;
            return View(student);
        }
        public IActionResult Courses(int id)
        {
            Student student = db.AllStudents().FirstOrDefault(a => a.Id == id);
            return View(student.studentCourses);
        }
        public IActionResult Delete(int id)
        {
            Student student = db.AllStudents().Where(s => s.Id == id).FirstOrDefault();
            db.Delete(student);
            return RedirectToAction("Index");
        }
        public IActionResult CheckEmail(string Email,int Id)
        {
            Student student = db.AllStudents().FirstOrDefault(a => a.Email == Email);
            if (student == null|| student.Id==Id)
            {
                return Json(true);
            }
            return Json(false);
        }
        public IActionResult Detials(int id)
        {
            Student student = db.AllStudents().Where(s => s.Id == id).FirstOrDefault();
            return View(student);
        }

        // HTTP POST VERSION  
   
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Create(student);
                return RedirectToAction("Index");

            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Content($"{message}");
            }

        }


        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Update(student);
                return RedirectToAction("Index");
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Content($"nothing{message}");
            }
        }

        [HttpPost]
        public IActionResult Courses(int stdid ,int[] degree, int[] cid) { 
           
                db.addStudentDegree(stdid, degree, cid);
                return RedirectToAction("Index");
            
        }


    }
}
