using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<IEnumerable<DtoPatient.ReadDTO>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.Patients.FindAsync(p => true,new string[] { "Appointments" });

            return _mapper.Map<IEnumerable<DtoPatient.ReadDTO>>(patients);
        }

        public async Task<DtoPatient.ReadDTO> GetPatientByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id, new string[] { "Appointments" });
            if (patient == null) return null;
            return _mapper.Map<DtoPatient.ReadDTO>(patient);
        }

        public async Task<IEnumerable<DtoPatient.ReadDTO>> GetPatientsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await GetAllPatientsAsync();

            var patients = await _unitOfWork.Patients.FindAsync( p => p.Name.Contains(name),new string[] { "Appointments" });

            return _mapper.Map<IEnumerable<DtoPatient.ReadDTO>>(patients);
        }

        public async Task<IEnumerable<DtoPatient.ReadDTO>> GetPatientsByPhNumberAsync(string PhNumber)
        {
            if (string.IsNullOrWhiteSpace(PhNumber)) return await GetAllPatientsAsync();

            var patients = await _unitOfWork.Patients.FindAsync(p => p.PhoneNumber.Contains(PhNumber),new string[] { "Appointments" });

            return _mapper.Map<IEnumerable<DtoPatient.ReadDTO>>(patients);
        }

        public async Task<DtoPatient.ReadDTO> CreatePatientAsync(DtoPatient.CreateDTO dto)
        {
            var patient = _mapper.Map<ClsPatient>(dto);

            if (dto.ImageFile != null)
            {
                patient.ProfilePicture = await _fileService.UploadFileAsync(dto.ImageFile, "Images/Patients");
            }

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DtoPatient.ReadDTO>(patient);
        }

        public async Task<bool> UpdatePatientAsync(DtoPatient.UpdateDTO dto)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(dto.ID);
            if (patient == null) return false;

            if (dto.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(patient.ProfilePicture) && !patient.ProfilePicture.Contains("DefaultImage"))
                {
                    _fileService.DeleteFile(patient.ProfilePicture);
                }

                patient.ProfilePicture = await _fileService.UploadFileAsync(dto.ImageFile, "Images/Patients");
            }

            _mapper.Map(dto, patient);

            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return false;

            if (!string.IsNullOrEmpty(patient.ProfilePicture) && !patient.ProfilePicture.Contains("DefaultImage"))
            {
                _fileService.DeleteFile(patient.ProfilePicture);
            }

            _unitOfWork.Patients.Delete(patient);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetPatientAppointmentsAsync(int patientId)
        {
            var appointments = (await _unitOfWork.Appointments.FindAsync(a => a.PatientID == patientId))
                                .OrderByDescending(a => a.AppointmentDate);

            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments);
        }

    }
}