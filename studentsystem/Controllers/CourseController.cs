using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitofWork;

        public CourseController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task<IActionResult> Index()
        {

            var courses = await _unitofWork.CourseRepository.GetAllAsync(); 
            return View(courses);

        }

        public async Task<IActionResult> Details(int id)
        {

            var courses = await _unitofWork.CourseRepository.GetByIdAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            var CourseCodeExists = await _unitofWork.CourseRepository.IsCourseCodeExist(course.CourseCode);

            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(course);
            }
            if (ModelState.IsValid)
            {
                _unitofWork.CourseRepository.Add(course);
                await _unitofWork.CourseRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(course);


        }


        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitofWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Course model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var CourseCodeExists = await _unitofWork.CourseRepository.IsCourseCodeExist(model.CourseCode, model.CourseId);
            if (CourseCodeExists)
            {
                ModelState.AddModelError("CourseCode", "CourseCode must be unique");
                return View(model);
            }
            _unitofWork.CourseRepository.Update(model);
            await _unitofWork.CourseRepository.Save();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var courses = await _unitofWork.CourseRepository.GetByIdAsync(id);
            if (courses == null) return NotFound();
            return View(courses);
        }



        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _unitofWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _unitofWork.CourseRepository.Remove(course);
            await _unitofWork.CourseRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
