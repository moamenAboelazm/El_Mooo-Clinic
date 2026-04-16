using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DtoDoctor.ReadDTO>> GetAllDoctorsAsync();
        Task<DtoDoctor.ReadDTO> GetDoctorByIdAsync(int id);

        Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByNameAsync(string name);
        Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByDepartmentAsync(int departmentId);

        Task<DtoDoctor.ReadDTO> CreateDoctorAsync(DtoDoctor.CreateDTO dto);
        Task<bool> UpdateDoctorAsync(DtoDoctor.UpdateDTO dto);
        Task<bool> DeleteDoctorAsync(int id);

        Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsAsync(int doctorId);
    }
}