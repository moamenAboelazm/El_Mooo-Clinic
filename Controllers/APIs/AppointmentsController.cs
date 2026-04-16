using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Implementations;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("Book")]
        public async Task<IActionResult> Book(DtoAppointment.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _appointmentService.BookAppointmentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var success = await _appointmentService.CancelAppointmentAsync(id);
            if (!success) return NotFound("Appointment not found.");

            return Ok("Appointment cancelled successfully.");
        }

        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorID(int doctorId) 
        {
           var appointments = await _appointmentService.GetDoctorAppointmentsByIDAsync(doctorId);

            if (appointments == null || !appointments.Any())
                return NotFound($"There is no Appointments for doctor with id {doctorId}.");

            return Ok(appointments);
        }

        [HttpGet("Doctor/{doctorId}/{date}")]
        public async Task<IActionResult> GetAppointmentsByDoctorIDandDate(int doctorId, DateTime date)
        {
            var appointments = await _appointmentService.GetDoctorAppointmentsByDayAsync(doctorId, date);
            return Ok(appointments);
        }

        [HttpGet("Patient/{patientId}/History")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            var history = await _appointmentService.GetPatientHistoryAsync(patientId);
            return Ok(history);
        }

    }
}