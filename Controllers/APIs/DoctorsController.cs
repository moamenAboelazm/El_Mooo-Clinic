using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound($"Doctor with ID {id} not found.");

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DtoDoctor.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _doctorService.CreateDoctorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] DtoDoctor.UpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _doctorService.UpdateDoctorAsync(dto);
            if (!success) return NotFound("Doctor not found or update failed.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _doctorService.DeleteDoctorAsync(id);
            if (!success) return NotFound("Doctor not found.");

            return Ok("Doctor deleted successfully.");
        }

        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var doctors = await _doctorService.GetDoctorsByNameAsync(name);
            return Ok(doctors);
        }

        [HttpGet("SearchByPhone")]
        public async Task<IActionResult> SearchByPhone([FromQuery] string phone)
        {
            var doctors = await _doctorService.GetDoctorsByPhoneNumberAsync(phone);
            return Ok(doctors);
        }

        [HttpGet("SearchByDepartment")]
        public async Task<IActionResult> SearchByDepartment([FromQuery] int departmentId)
        {
            var doctors = await _doctorService.GetDoctorsByDepartmentAsync(departmentId);
            return Ok(doctors);
        }
    }
}