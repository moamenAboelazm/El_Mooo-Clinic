using Microsoft.AspNetCore.Mvc;
using El_Mooo_Clinic.Services.Interfaces;
using System.Threading.Tasks;
using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Controllers.Mvc
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllAsync();

            return View(departments);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] DtoDepartment.CreateDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.CreateAsync(dto);

                TempData["success"] = "Department created successfully! 🎉";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, El_Mooo_Clinic.DTOs.DtoDepartment.UpdateDTO dto)
        {
            dto.ID = id;

            if (ModelState.IsValid)
            {
                await _departmentService.UpdateAsync(dto);

                TempData["success"] = "Department updated successfully! 🎉";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        //----------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.DeleteAsync(id);

            TempData["success"] = "Department deleted successfully! 🗑️";
            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------------

    }
}