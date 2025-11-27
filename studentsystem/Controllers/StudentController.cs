using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentsystem.Data;
using studentsystem.Entites;
using studentsystem.Repository.Interface;

namespace studentsystem.Controllers
{
    public class StudentsController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        public StudentsController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _unitofWork.StudentRepository.GetAllAsync();
            return View(students);
        }
        public async Task<IActionResult> Details(int id)
        {
            var student = await _unitofWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Students student)
        {
            var EmailExists = await _unitofWork.StudentRepository.IsEmailExist(student.Email);

            if (EmailExists)
            {
                ModelState.AddModelError("Email", "Email   already in use");
                return View(student);
            }
            if (ModelState.IsValid)
            {
                _unitofWork.StudentRepository.Add(student);
                await _unitofWork.StudentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _unitofWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Students model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var EmailExists = await _unitofWork.StudentRepository.IsEmailExist(model.Email, model.StudentId);


            if (EmailExists)
            {
                ModelState.AddModelError("Email", "Email   already in use");
                return View(model);
            }
            _unitofWork.StudentRepository.Update(model);
            await _unitofWork.StudentRepository.Save();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var students = await _unitofWork.StudentRepository.GetByIdAsync(id);
            if (students == null) return NotFound();
            return View(students);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _unitofWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _unitofWork.StudentRepository.Remove(student);
            await _unitofWork.StudentRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
