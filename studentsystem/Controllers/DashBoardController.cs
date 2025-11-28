using Microsoft.AspNetCore.Mvc;
using studentsystem.Data;
using studentsystem.Repository.Interface;

namespace studentsystem.Controllers
{
    public class DashboardController : Controller

    {
        private readonly IUnitOfWork _unitofWork;
        public DashboardController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public async Task<IActionResult> Index()

        {
            ViewBag.TotalStudents = await _unitofWork.StudentRepository.GetAllAsync();
            ViewBag.TotalCourses = await _unitofWork.CourseRepository.GetAllAsync();
            ViewBag.TotalEnrollments = await _unitofWork.EnrollmentRepository.GetAllAsync();
            ViewBag.PopularCourses = await _unitofWork.EnrollmentRepository.GetPopularCoursesAsync();
            ViewBag.CourseAvgGrades = await _unitofWork.EnrollmentRepository.GetCourseAvgGradesAsync();
            ViewBag.GradeDistribution = await _unitofWork.EnrollmentRepository.GetGradeDistributionAsync();
            ViewBag.TopStudents = await _unitofWork.EnrollmentRepository.GetTopStudentsAsync();
            ViewBag.RecentStudents = await _unitofWork.StudentRepository.GetRecentAsync();
            ViewBag.AvgStudentsPerCourse =
            (ViewBag.TotalCourses == 0 ? 0 :
            (double)ViewBag.TotalEnrollments / ViewBag.TotalCourses);            
            DateOnly fromDates = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
            return View();

        }
    }
}