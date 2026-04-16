using AutoMapper;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.DTOs;

namespace El_Mooo_Clinic.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DtoDepartment.CreateDTO, ClsDepartment>();

            CreateMap<DtoDepartment.UpdateDTO, ClsDepartment>();

            CreateMap<ClsDepartment, DtoDepartment.ReadDTO>();
            
            CreateMap<ClsAppointments, DtoAppointment>();

            //----------------------------------------------------------------------------

            CreateMap<DtoDoctor.CreateDTO, ClsDoctor>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.ProfilePicture)
                        ? src.ProfilePicture
                        : (src.Gender == ClsEnums.Gender.Male ? "DefaultImageDoctorMale.jpg" : "DefaultImageDoctorFemale.jpg")
                ));

            CreateMap<DtoDoctor.UpdateDTO, ClsDoctor>()
                .ForMember(dest => dest.ProfilePicture, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.ProfilePicture));
                    opt.MapFrom(src => src.ProfilePicture);
                });

            CreateMap<ClsDoctor, DtoDoctor.ReadDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules));
                
            //----------------------------------------------------------------------------

            CreateMap<DtoPatient.CreateDTO, ClsPatient>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.ProfilePicture)
                        ? src.ProfilePicture
                        : (src.Gender == ClsEnums.Gender.Male ? "DefaultImagePatientMale.png" : "DefaultImagePatientFemale.png")
                ));

            CreateMap<DtoPatient.UpdateDTO, ClsPatient>()
                .ForMember(dest => dest.ProfilePicture, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.ProfilePicture));
                    opt.MapFrom(src => src.ProfilePicture);
                });

            CreateMap<ClsPatient, DtoPatient.ReadDTO>()
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.ToString()));
            //----------------------------------------------------------------------------

            CreateMap<DtoAppointment.CreateDTO, ClsAppointments>()
                .ForMember(dest => dest.AppointmentDate, opt => opt.Ignore())
                .ForMember(dest => dest.Cost, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<DtoAppointment.UpdateDTO, ClsAppointments>();

            CreateMap<ClsAppointments, DtoAppointment.ReadDTO>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name));
                CreateMap<DtoMedicalRecord.CreateDTO, ClsMedicalRecords>();

            //----------------------------------------------------------------------------

            CreateMap<DtoMedicalRecord.UpdateDTO, ClsMedicalRecords>();
            CreateMap<DtoMedicalRecord.CreateDTO, ClsMedicalRecords>();

            CreateMap<ClsMedicalRecords, DtoMedicalRecord.ReadDTO>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Doctor.Name))
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.Appointment.AppointmentDate));

            //----------------------------------------------------------------------------

            CreateMap<ClsDoctorSchedule, DtoDoctorSchedule.ReadDTO>()
                .ForMember(dest => dest.DayName, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name));

            CreateMap<DtoDoctorSchedule.CreateDTO, ClsDoctorSchedule>();

        }
    }
}