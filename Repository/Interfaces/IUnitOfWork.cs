using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Repository.Interfaces;

namespace EL_Mooo_Clinic.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ClsDoctor> Doctors { get; }
        IGenericRepository<ClsPatient> Patients { get; }
        IGenericRepository<ClsDepartment> Departments { get; }
        IGenericRepository<ClsAppointments> Appointments { get; }
        IGenericRepository<ClsMedicalRecords> MedicalRecords { get; }
        IGenericRepository<ClsDoctorSchedule> DoctorSchedules { get; } 

        Task<int> CompleteAsync();
    }
}