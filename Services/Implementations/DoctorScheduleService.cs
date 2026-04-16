using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using EL_Mooo_Clinic.Repositories.Interfaces;
using El_Mooo_Clinic.Services.Interfaces;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DtoDoctorSchedule.ReadDTO> CreateAsync(DtoDoctorSchedule.CreateDTO dto)
        {
            var schedule = _mapper.Map<ClsDoctorSchedule>(dto);
            await _unitOfWork.DoctorSchedules.AddAsync(schedule);
            await _unitOfWork.CompleteAsync();

            var result = await _unitOfWork.DoctorSchedules.GetByIdAsync(schedule.ID, new[] { "Doctor" });
            return _mapper.Map<DtoDoctorSchedule.ReadDTO>(result);
        }

        public async Task<IEnumerable<DtoDoctorSchedule.ReadDTO>> GetByDoctorIdAsync(int doctorId)
        {
            var schedules = await _unitOfWork.DoctorSchedules.FindAsync(
                s => s.DoctorID == doctorId,
                new[] { "Doctor" }
            );
            return _mapper.Map<IEnumerable<DtoDoctorSchedule.ReadDTO>>(schedules);
        }

        public async Task<DtoDoctorSchedule.ReadDTO> GetByIdAsync(int id)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id, new[] { "Doctor" });
            if (schedule == null) return null;
            return _mapper.Map<DtoDoctorSchedule.ReadDTO>(schedule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _unitOfWork.DoctorSchedules.GetByIdAsync(id);
            if (schedule == null) return false;

            _unitOfWork.DoctorSchedules.Delete(schedule);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}