using El_Mooo_Clinic.DTOs;
using EL_Mooo_Clinic.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.Mvc
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var allAppointments = await _unitOfWork.Appointments.FindAsync(a => true);

            var completedAppointments = allAppointments.Where(a => a.Status == Models.ClsEnums.AppointmentStatus.Completed).ToList();

            var dashboardData = new DtoDashboard
            {
                TotalDoctors = (await _unitOfWork.Doctors.FindAsync(d => true)).Count(),
                TotalPatients = (await _unitOfWork.Patients.FindAsync(p => true)).Count(),

                TotalAppointments = allAppointments.Count(),
                CompletedVisits = completedAppointments.Count(),

                TotalRevenue = completedAppointments.Sum(a => a.Cost)
            };

            return View(dashboardData);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}