using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<DtoAppointment.ReadDTO> BookAppointmentAsync(DtoAppointment.CreateDTO dto);

        Task<bool> CancelAppointmentAsync(int appointmentId);

        Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsByDayAsync(int doctorId, DateTime date);
        Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsByIDAsync(int doctorId);

        Task<IEnumerable<DtoAppointment.ReadDTO>> GetAllAsync();
        Task<IEnumerable<DtoAppointment.ReadDTO>> GetPatientHistoryAsync(int patientId);
        Task<bool> UpdateStatusAsync(int id, ClsEnums.AppointmentStatus status);

        Task<DtoAppointment.ReadDTO> GetByIdAsync(int id);


    }
}