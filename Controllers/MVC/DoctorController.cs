using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Implementations;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace El_Mooo_Clinic.Controllers.Mvc
{
    [Authorize(Roles = "Receptionist")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        public DoctorController(IDoctorService doctorService, IAppointmentService appointmentService, IDepartmentService departmentService , IDoctorScheduleService doctorScheduleService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _doctorScheduleService = doctorScheduleService;
            _appointmentService = appointmentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentSearch"] = searchString;

            var allDoctors = await _doctorService.GetAllDoctorsAsync();
            IEnumerable<El_Mooo_Clinic.DTOs.DtoDoctor.ReadDTO> doctorsToShow;

            if (User.IsInRole("Doctor"))
            {
                doctorsToShow = allDoctors.Where(d => d.Email == User.Identity.Name);
            }
            else
            {
                doctorsToShow = allDoctors;
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();
                doctorsToShow = doctorsToShow.Where(d =>
                    (d.Name != null && d.Name.ToLower().Contains(searchString)) ||
                    (d.DepartmentName != null && d.DepartmentName.ToLower().Contains(searchString)) ||
                    (d.PhoneNumber != null && d.PhoneNumber.Contains(searchString))
                );
            }

            var result = doctorsToShow.OrderBy(d => d.ID);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DoctorTablePartial", result);
            }

            return View(result);
        }
        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] El_Mooo_Clinic.DTOs.DtoDoctor.CreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _doctorService.CreateDoctorAsync(dto);
                TempData["success"] = "Doctor registered successfully! 🎉";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            var updateDto = new El_Mooo_Clinic.DTOs.DtoDoctor.UpdateDTO
            {
                ID = doctor.ID,
                Name = doctor.Name,
                DepartmentID = doctor.DepartmentID,
                ExaminationCost = doctor.ExaminationCost,
                PhoneNumber = doctor.PhoneNumber,
                ProfilePicture = doctor.ProfilePicture,
                BirthDate = doctor.BirthDate,
                Gender = doctor.Gender
            };

            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "ID", "Name", doctor.DepartmentID);

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, El_Mooo_Clinic.DTOs.DtoDoctor.UpdateDTO dto, IFormFile? ImageFile)
        {
            dto.ID = id;

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/doctors");

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

                await _doctorService.UpdateDoctorAsync(dto);
                TempData["success"] = "Doctor updated successfully! ✏️";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(await _departmentService.GetAllAsync(), "ID", "Name", dto.DepartmentID);
            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            TempData["success"] = "Doctor deleted successfully! 🗑️";
            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id); 
            if (doctor == null) return NotFound();

            var appointments = await _appointmentService.GetDoctorAppointmentsByIDAsync(id);
            ViewBag.DoctorAppointments = appointments;

            return View(doctor);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> ManageSchedule(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSchedule(DtoDoctorSchedule.CreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _doctorScheduleService.CreateAsync(dto);
                TempData["success"] = "New slot added successfully! 📅";
            }
            else
            {
                TempData["error"] = "Please check the time inputs.";
            }
            return RedirectToAction(nameof(ManageSchedule), new { id = dto.DoctorID });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchedule(int scheduleId, int doctorId)
        {
            await _doctorScheduleService.DeleteAsync(scheduleId);
            TempData["success"] = "Slot removed! 🗑️";
            return RedirectToAction(nameof(ManageSchedule), new { id = doctorId });
        } 
        
        //----------------------------------------------------------------------------

    }
}