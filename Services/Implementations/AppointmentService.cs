using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DtoAppointment.ReadDTO> BookAppointmentAsync(DtoAppointment.CreateDTO dto)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorID);
            var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientID);
            if (doctor == null) throw new Exception("Doctor not found.");
            if (patient == null) throw new Exception("Patient not found.");

            var day = dto.RequestedDate.DayOfWeek;
            var schedule = (await _unitOfWork.DoctorSchedules.FindAsync(s => s.DoctorID == dto.DoctorID && s.DayOfWeek == day)).FirstOrDefault();

            if (schedule == null || !schedule.IsAvailable)
                throw new Exception("Doctor is not available on this day.");

            var lastAppointment = (await _unitOfWork.Appointments.FindAsync(a =>
                a.DoctorID == dto.DoctorID &&
                a.AppointmentDate.Date == dto.RequestedDate.Date &&
                a.Status != ClsEnums.AppointmentStatus.Cancelled))
                .OrderByDescending(a => a.AppointmentDate).FirstOrDefault();

            DateTime nextSlot = lastAppointment == null ?
                  dto.RequestedDate.Date.Add(schedule.StartTime) 
                : lastAppointment.AppointmentDate.AddMinutes(lastAppointment.DurationInMinutes); 

            var expectedEndTime = nextSlot.TimeOfDay.Add(TimeSpan.FromMinutes(dto.DurationInMinutes));
            if (expectedEndTime > schedule.EndTime)
                throw new Exception("No sufficient time left in the schedule for this appointment today.");

            var appointment = _mapper.Map<ClsAppointments>(dto);
            appointment.AppointmentDate = nextSlot;
            appointment.DurationInMinutes = dto.DurationInMinutes; 
            appointment.Cost = doctor.ExaminationCost;
            appointment.Status = ClsEnums.AppointmentStatus.Booked;

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DtoAppointment.ReadDTO>(appointment);
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);
            if (appointment == null) return false;

            appointment.Status = ClsEnums.AppointmentStatus.Cancelled;
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetAllAsync()
        {
            var appointments = await _unitOfWork.Appointments.FindAsync(a => true, new string[] { "Patient", "Doctor" });
            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments.OrderByDescending(a => a.AppointmentDate));
        }

        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsByDayAsync(int doctorId, DateTime date)
        {
            var appointments = await _unitOfWork.Appointments.FindAsync(a => a.DoctorID == doctorId && a.AppointmentDate.Date == date.Date,new string[] { "Patient", "Doctor" });

            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments);
        }

        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetDoctorAppointmentsByIDAsync(int doctorId)
        {
            var appointments = await _unitOfWork.Appointments.FindAsync( a => a.DoctorID == doctorId,new string[] { "Patient", "Doctor" });

            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments);
        }

        public async Task<bool> UpdateStatusAsync(int id, ClsEnums.AppointmentStatus status)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return false;

            appointment.Status = status;

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        public async Task<IEnumerable<DtoAppointment.ReadDTO>> GetPatientHistoryAsync(int patientId)
        {
            var appointments = await _unitOfWork.Appointments.FindAsync(a => a.PatientID == patientId,new string[] { "Patient", "Doctor" });
            return _mapper.Map<IEnumerable<DtoAppointment.ReadDTO>>(appointments.OrderByDescending(a => a.AppointmentDate));
        }

        public async Task<DtoAppointment.ReadDTO> GetByIdAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id, new[] { "Patient", "Doctor" });

            if (appointment == null) return null;

            return _mapper.Map<DtoAppointment.ReadDTO>(appointment);
        }
    }
}