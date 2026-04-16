using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task<DtoDoctorSchedule.ReadDTO> CreateAsync(DtoDoctorSchedule.CreateDTO dto);
        Task<IEnumerable<DtoDoctorSchedule.ReadDTO>> GetByDoctorIdAsync(int doctorId);
        Task<DtoDoctorSchedule.ReadDTO> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}