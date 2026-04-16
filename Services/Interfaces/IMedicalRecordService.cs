using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<DtoMedicalRecord.ReadDTO> CreateRecordAsync(DtoMedicalRecord.CreateDTO dto);
        Task<bool> UpdateRecordAsync(DtoMedicalRecord.UpdateDTO dto);
        Task<DtoMedicalRecord.ReadDTO> GetRecordByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<DtoMedicalRecord.ReadDTO>> GetPatientMedicalHistoryAsync(int patientId);
    }
}