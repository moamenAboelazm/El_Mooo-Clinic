using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace El_Mooo_Clinic.Controllers.APIs
{
    [Route("api/[controller]")] 
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound($"Department with ID {id} does not exist.");

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoDepartment.CreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _departmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoDepartment.UpdateDTO dto)
        {
            var success = await _departmentService.UpdateAsync(dto);
            if (!success) return NotFound("Department not found or update failed.");

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _departmentService.DeleteAsync(id);
            if (!success) return NotFound("Department not found.");

            return Ok("Department deleted successfully.");
        }

    }
}