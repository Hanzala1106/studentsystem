using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        public EnrollmentsController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }



        public async Task<IActionResult> Index()
        {
            var enrollments = await _unitofWork.EnrollmentRepository.GetAllAsync();
            return View(enrollments);
        }

        public async Task<IActionResult> Create()
        {

            var students = await _unitofWork.StudentRepository.GetAllAsync();
            var courses = await _unitofWork.CourseRepository.GetAllAsync();
            ViewBag.StudentList = new SelectList(students, "StudentId", "FirstName");
            ViewBag.CourseList = new SelectList(courses, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enrollments model)

        {
            var DuplicateEnrollment = await _unitofWork.EnrollmentRepository
                        .IsDuplicateEnrollmentAsync(model.StudentId, model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }

            if (ModelState.IsValid)
            {
                ViewBag.StudentList = new SelectList(await _context.Students.ToListAsync(), "StudentId", "FirstName", model.StudentId);
                ViewBag.CourseList = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", model.CourseId);
                return View(model);
            }
            _context.Enrollments.Add(model);
            await _unitofWork.EnrollmentRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)

        {
            var enrollment = await _unitofWork.EnrollmentRepository.GetByIdAsync(id);
            var students = await _unitofWork.StudentRepository.GetAllAsync();
            var courses = await _unitofWork.CourseRepository.GetAllAsync();
            ViewBag.StudentList = new SelectList(students, "StudentId", "FirstName", enrollment.StudentId);
            ViewBag.CourseList = new SelectList(courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Enrollments model)
        {

            if (ModelState.IsValid)
            {
                return View(model);
            }
            var DuplicateEnrollment = await _unitofWork.EnrollmentRepository
                        .IsDuplicateEnrollmentAsync(model.StudentId, model.CourseId);
            if (DuplicateEnrollment)
            {
                ModelState.AddModelError("", "This student is already enrolled in the selected course");
            }
            _unitofWork.EnrollmentRepository.Update(model);
            await _unitofWork.EnrollmentRepository.Save();
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _unitofWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _unitofWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _unitofWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            _unitofWork.EnrollmentRepository.Remove(enrollment);
            await _unitofWork.EnrollmentRepository.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
