using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace El_Mooo_Clinic.Controllers.Mvc
{
    [Authorize(Roles = "Receptionist")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public PatientController(IPatientService patientService, IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentSearch"] = searchString;
            IEnumerable<DtoPatient.ReadDTO> patients;

            if (string.IsNullOrWhiteSpace(searchString))
            {
                patients = await _patientService.GetAllPatientsAsync();
            }
            else
            {
                if (searchString.Any(char.IsLetter))
                {
                    patients = await _patientService.GetPatientsByNameAsync(searchString);
                }
                else
                {
                    patients = await _patientService.GetPatientsByPhNumberAsync(searchString);
                }
            }
            var sortedPatients = patients.OrderBy(p => p.ID);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_PatientTablePartial", sortedPatients);
            }

            return View(sortedPatients);
        }
       
        //----------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] El_Mooo_Clinic.DTOs.DtoPatient.CreateDTO dto, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/patients");
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    dto.ProfilePicture = fileName;
                }

                await _patientService.CreatePatientAsync(dto);
                TempData["success"] = "Patient registered successfully! 🤒";
                return RedirectToAction(nameof(Index)); 
            }

            return View(dto);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            var updateDto = new El_Mooo_Clinic.DTOs.DtoPatient.UpdateDTO
            {
                ID = patient.ID,
                Name = patient.Name,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                ProfilePicture = patient.ProfilePicture
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, El_Mooo_Clinic.DTOs.DtoPatient.UpdateDTO dto, IFormFile? ImageFile)
        {
            dto.ID = id;

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/patients");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    dto.ProfilePicture = fileName;
                }

                await _patientService.UpdatePatientAsync(dto);
                TempData["success"] = "Patient info updated successfully! ✏️";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _patientService.DeletePatientAsync(id);
            TempData["success"] = "Patient deleted successfully! 🗑️";
            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            var appointments = await _appointmentService.GetPatientHistoryAsync(id);
            ViewBag.PatientAppointments = appointments;

            return View(patient);
        }
    }
}