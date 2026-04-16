using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DtoMedicalRecord.ReadDTO> CreateRecordAsync(DtoMedicalRecord.CreateDTO dto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentID, new[] { "Patient" });
            if (appointment == null) throw new Exception("Appointment not found!");

            if (appointment.Patient != null)
            {
                appointment.Patient.NumberOfVisits += 1;
            }

            var record = _mapper.Map<ClsMedicalRecords>(dto);
            await _unitOfWork.MedicalRecords.AddAsync(record);

            appointment.Status = ClsEnums.AppointmentStatus.Completed;

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DtoMedicalRecord.ReadDTO>(record);
        }

        public async Task<DtoMedicalRecord.ReadDTO> GetRecordByAppointmentIdAsync(int appointmentId)
        {
            var records = await _unitOfWork.MedicalRecords.FindAsync(r => r.AppointmentID == appointmentId,new string[] { "Appointment.Patient", "Appointment.Doctor" } );

            var record = records.FirstOrDefault();
            if (record == null) return null;

            return _mapper.Map<DtoMedicalRecord.ReadDTO>(record);
        }

        public async Task<IEnumerable<DtoMedicalRecord.ReadDTO>> GetPatientMedicalHistoryAsync(int patientId)
        {
            var records = await _unitOfWork.MedicalRecords.FindAsync(r => r.Appointment.PatientID == patientId,new string[] { "Appointment.Patient", "Appointment.Doctor" });

            return _mapper.Map<IEnumerable<DtoMedicalRecord.ReadDTO>>(records.OrderByDescending(r => r.AppointmentDate));
        }

        public async Task<bool> UpdateRecordAsync(DtoMedicalRecord.UpdateDTO dto)
        {
            var record = await _unitOfWork.MedicalRecords.GetByIdAsync(dto.ID);
            if (record == null) return false;

            _mapper.Map(dto, record);
            _unitOfWork.MedicalRecords.Update(record);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}