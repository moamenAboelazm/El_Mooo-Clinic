using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DtoDepartment.ReadDTO>> GetAllAsync();
        Task<DtoDepartment.ReadDTO> GetByIdAsync(int id);
        Task<DtoDepartment.ReadDTO> CreateAsync(DtoDepartment.CreateDTO dto);
        Task<bool> UpdateAsync(DtoDepartment.UpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}