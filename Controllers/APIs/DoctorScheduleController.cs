using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSchedulesController : ControllerBase
    {
        private readonly IDoctorScheduleService _scheduleService;

        public DoctorSchedulesController(IDoctorScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DtoDoctorSchedule.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _scheduleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
        }

        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(int doctorId)
        {
            var schedules = await _scheduleService.GetByDoctorIdAsync(doctorId);
            if (!schedules.Any()) return NotFound($"There is no Work Schedule for doctor with ID {doctorId}");

            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null) return NotFound();

            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _scheduleService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}