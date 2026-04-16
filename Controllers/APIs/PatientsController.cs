using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound($"Patient with ID {id} not found.");

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DtoPatient.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var patient = await _patientService.CreatePatientAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = patient.ID }, patient);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] DtoPatient.UpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _patientService.UpdatePatientAsync(dto);
            if (!success) return NotFound("Patient not found or update failed.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _patientService.DeletePatientAsync(id);
            if (!success) return NotFound("Patient not found.");

            return Ok("Patient deleted successfully.");
        }
        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var patients = await _patientService.GetPatientsByNameAsync(name);
            return Ok(patients);
        }

        [HttpGet("SearchByPhone")]
        public async Task<IActionResult> SearchByPhone([FromQuery] string phone)
        {
            var patients = await _patientService.GetPatientsByPhNumberAsync(phone);
            return Ok(patients);
        }

        [HttpGet("{id}/WithAppointments")]
        public async Task<IActionResult> GetPatientWithAppointments(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound($"Patient with ID {id} not found.");

            var appointments = await _patientService.GetPatientAppointmentsAsync(id);
            return Ok(appointments);
        }
    }
}