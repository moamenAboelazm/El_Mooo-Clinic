using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DtoMedicalRecord.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _medicalRecordService.CreateRecordAsync(dto);
                return CreatedAtAction(nameof(GetByAppointmentId), new { appointmentId = result.AppointmentID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoMedicalRecord.UpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _medicalRecordService.UpdateRecordAsync(dto);
            if (!success) return NotFound("Medical record not found or update failed.");

            return NoContent();
        }

        [HttpGet("Appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(int appointmentId)
        {
            var record = await _medicalRecordService.GetRecordByAppointmentIdAsync(appointmentId);
            if (record == null) return NotFound($"No medical record found for Appointment ID {appointmentId}.");

            return Ok(record);
        }

        [HttpGet("Patient/{patientId}")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            var history = await _medicalRecordService.GetPatientMedicalHistoryAsync(patientId);
            return Ok(history);
        }
    }
}