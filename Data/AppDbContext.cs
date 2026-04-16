using El_Mooo_Clinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace El_Mooo_Clinic.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        DbSet<ClsDoctor> Doctors { get; set; }
        DbSet<ClsPatient> Patients  { get; set; }
        DbSet<ClsDepartment> Departments  { get; set; }
        DbSet<ClsAppointments> Appointments  { get; set; }
        DbSet<ClsMedicalRecords> MedicalRecords { get; set; }
        DbSet<ClsDoctorSchedule> DoctorSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClsAppointments>()
                .HasOne(a => a.MedicalRecord)
                .WithOne(m => m.Appointment)
                .HasForeignKey<ClsMedicalRecords>(m => m.AppointmentID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ClsAppointments>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ClsAppointments>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
