using Microsoft.AspNetCore.Mvc;
using studentsystem.Repository.Interface;

namespace studentsystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalStudents = await _unitOfWork.StudentRepository.Count();
            ViewBag.TotalCourses = await _unitOfWork.CourseRepository.Count();
            ViewBag.TotalEnrollments = await _unitOfWork.EnrollmentRepository.GetTotalEnrollmentsAsync();

            ViewBag.AvgStudentsPerCourse =
                (ViewBag.TotalCourses == 0 ? 0 :
                (double)ViewBag.TotalEnrollments / ViewBag.TotalCourses);

            ViewBag.TopStudents = await _unitOfWork.EnrollmentRepository.GetTopStudentsAsync();

            DateOnly fromDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
            ViewBag.RecentStudents = await _unitOfWork.StudentRepository
                .GetAllAsync(s => s.EnrollmentDate >= fromDate, orderBy: s => s.OrderByDescending(x => x.EnrollmentDate), take: 5);

            ViewBag.PopularCourses = await _unitOfWork.EnrollmentRepository.GetPopularCoursesAsync();
            ViewBag.CourseAvgGrades = await _unitOfWork.EnrollmentRepository.GetCourseAvgGradesAsync();
            ViewBag.GradeDistribution = await _unitOfWork.EnrollmentRepository.GetGradeDistributionAsync();

            return View();
        }
    }
}
