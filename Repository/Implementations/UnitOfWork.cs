using El_Mooo_Clinic.Data;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Repository.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;

namespace EL_Mooo_Clinic.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<ClsDoctor> Doctors { get; private set; }
        public IGenericRepository<ClsPatient> Patients { get; private set; }
        public IGenericRepository<ClsDepartment> Departments { get; private set; }
        public IGenericRepository<ClsAppointments> Appointments { get; private set; }
        public IGenericRepository<ClsMedicalRecords> MedicalRecords { get; private set; }
        public IGenericRepository<ClsDoctorSchedule> DoctorSchedules { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Doctors = new GenericRepository<ClsDoctor>(_context);
            Patients = new GenericRepository<ClsPatient>(_context);
            Departments = new GenericRepository<ClsDepartment>(_context);
            Appointments = new GenericRepository<ClsAppointments>(_context);
            MedicalRecords = new GenericRepository<ClsMedicalRecords>(_context);
            DoctorSchedules = new GenericRepository<ClsDoctorSchedule>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}