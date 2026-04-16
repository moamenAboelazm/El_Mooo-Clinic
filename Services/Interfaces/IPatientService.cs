using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<DtoPatient.ReadDTO>> GetAllPatientsAsync();
        Task<DtoPatient.ReadDTO> GetPatientByIdAsync(int id);
        Task<IEnumerable<DtoPatient.ReadDTO>> GetPatientsByNameAsync(string searchTerm);
        Task<IEnumerable<DtoPatient.ReadDTO>> GetPatientsByPhNumberAsync(string searchTerm);
        Task<DtoPatient.ReadDTO> CreatePatientAsync(DtoPatient.CreateDTO dto);
        Task<bool> UpdatePatientAsync(DtoPatient.UpdateDTO dto);
        Task<bool> DeletePatientAsync(int id);

        Task<IEnumerable<DtoAppointment.ReadDTO>> GetPatientAppointmentsAsync(int patientId);
    }
}