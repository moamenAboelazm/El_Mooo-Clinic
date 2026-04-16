using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace El_Mooo_Clinic.Controllers.Mvc
{
    [Authorize(Roles = "Receptionist,Doctor")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMedicalRecordService _medicalRecordService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService , IMedicalRecordService medicalRecordService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentSearch"] = searchString;

            var appointments = await _appointmentService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();
                appointments = appointments.Where(a =>
                    a.PatientName.ToLower().Contains(searchString) ||
                    a.DoctorName.ToLower().Contains(searchString) ||
                    a.Status.ToLower().Contains(searchString)
                );
            }

            var sortedAppointments = appointments.OrderByDescending(a => a.AppointmentDate);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_AppointmentTablePartial", sortedAppointments);
            }

            return View(sortedAppointments);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var patients = await _patientService.GetAllPatientsAsync();

            ViewBag.Doctors = new SelectList(doctors, "ID", "Name");
            ViewBag.Patients = new SelectList(patients, "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] DtoAppointment.CreateDTO dto)
        {
            try
            {
                await _appointmentService.BookAppointmentAsync(dto);
                TempData["success"] = "Appointment booked successfully! 📅";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"⚠️ {ex.Message}";

                var doctors = await _doctorService.GetAllDoctorsAsync();
                var patients = await _patientService.GetAllPatientsAsync();
                ViewBag.Patients = new SelectList(await _patientService.GetAllPatientsAsync(), "ID", "Name");
                ViewBag.Doctors = new SelectList(await _doctorService.GetAllDoctorsAsync(), "ID", "Name");

                return View(dto);
            }
        }

        //----------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);
            if (result)
                TempData["success"] = "Appointment Cancelled! 🚫";
            else
                TempData["error"] = "Could not cancel the appointment.";

            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var medicalRecord = await _medicalRecordService.GetRecordByAppointmentIdAsync(id);
            ViewBag.MedicalRecord = medicalRecord;

            return View(appointment);
        }

        //----------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMedicalRecord(DtoMedicalRecord.CreateDTO dto)
        {
            await _medicalRecordService.CreateRecordAsync(dto);
            TempData["success"] = "Medical Record saved and Appointment marked as Completed! 📝";
            return RedirectToAction(nameof(Details), new { id = dto.AppointmentID });
        }

        //----------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMedicalRecord(DtoMedicalRecord.UpdateDTO dto, int appointmentId)
        {
            await _medicalRecordService.UpdateRecordAsync(dto);
            TempData["success"] = "Medical Record updated successfully! 🔄";
            return RedirectToAction(nameof(Details), new { id = appointmentId });
        }

        //----------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, ClsEnums.AppointmentStatus status)
        {
            var success = await _appointmentService.UpdateStatusAsync(id, status);
            if (success)
                TempData["success"] = $"Status updated to {status}! ✅";
            else
                TempData["error"] = "Failed to update status.";

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}