using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<IEnumerable<DtoDoctor.ReadDTO>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.Doctors.FindAsync(d => true, new string[] { "Department" });

            return _mapper.Map<IEnumerable<DtoDoctor.ReadDTO>>(doctors).OrderBy(d => d.ID);
        }

        public async Task<DtoDoctor.ReadDTO> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id, new[] { "Schedules", "Department" });
            return _mapper.Map<DtoDoctor.ReadDTO>(doctor);
        }

        public async Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllDoctorsAsync();

            var doctors = await _unitOfWork.Doctors.FindAsync(d => d.Name.Contains(name),new string[] { "Department" } );

            return _mapper.Map<IEnumerable<DtoDoctor.ReadDTO>>(doctors);
        }

        public async Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return await GetAllDoctorsAsync();

            var doctors = await _unitOfWork.Doctors.FindAsync(d => d.PhoneNumber.Contains(phoneNumber), new string[] { "Department" });
            return _mapper.Map<IEnumerable<DtoDoctor.ReadDTO>>(doctors);
        }

        public async Task<IEnumerable<DtoDoctor.ReadDTO>> GetDoctorsByDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                return await GetAllDoctorsAsync();

            var doctors = await _unitOfWork.Doctors.FindAsync(d => d.DepartmentID == departmentId, new string[] { "Department" });
            return _mapper.Map<IEnumerable<DtoDoctor.ReadDTO>>(doctors);
        }

        public async Task<DtoDoctor.ReadDTO> CreateDoctorAsync(DtoDoctor.CreateDTO dto)
        {
            var doctor = _mapper.Map<ClsDoctor>(dto);

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                doctor.ProfilePicture = await _fileService.UploadFileAsync(dto.ImageFile, "Images/doctors");
            }
            else
            {
                if (dto.Gender == ClsEnums.Gender.Male) 
                {
                    doctor.ProfilePicture = "DefaultImageDoctorMale.jpg";
                }
                else
                {
                    doctor.ProfilePicture = "DefaultImageDoctorFemale.jpg";
                }
            }

            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            var doctorWithDept = await _unitOfWork.Doctors.GetByIdAsync(doctor.ID, new string[] { "Department" });

            return _mapper.Map<DtoDoctor.ReadDTO>(doctorWithDept);
        }

        public async Task<bool> UpdateDoctorAsync(DtoDoctor.UpdateDTO dto)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.ID);
            if (doctor == null) return false;

            if (dto.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(doctor.ProfilePicture) && !doctor.ProfilePicture.Contains("DefaultImage"))
                {
                    _fileService.DeleteFile(doctor.ProfilePicture);
                }

                doctor.ProfilePicture = await _fileService.UploadFileAsync(dto.ImageFile, "Images/Doctors");
            }

            _mapper.Map(dto, doctor);

            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null) return false;

            if (!string.IsNullOrEmpty(doctor.ProfilePicture) && !doctor.ProfilePicture.Contains("DefaultImage"))
            {
                _fileService.DeleteFile(doctor.ProfilePicture);
            }

            _unitOfWork.Doctors.Delete(doctor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsAsync(int doctorId)
        {
            var appointments = (await _unitOfWork.Appointments.FindAsync(a => a.DoctorID == doctorId))
                                .OrderByDescending(a => a.AppointmentDate);

            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments);
        }

    }
}