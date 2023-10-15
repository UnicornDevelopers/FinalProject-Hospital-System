using Hospital_System.Auth.Models;
using Hospital_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Hospital_System.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {

        public HospitalDbContext(DbContextOptions options) : base(options)
        {


        }

        

        public DbSet<Hospital> Hospitals { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Nurse> Nurses { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Medicine> Medicines { get; set; }
		public DbSet<MedicalReport> MedicalReports { get; set; }




		//---------------------------------------------------

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Hospital>()
     .HasMany(a => a.Departments)
      .WithOne(b => b.Hospital)
        .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Department>()
           .HasOne(a => a.Hospital)
           .WithMany(d => d.Departments)
           .HasForeignKey(a => a.HospitalID)
           .OnDelete(DeleteBehavior.ClientSetNull);

         

            modelBuilder.Entity<Doctor>()
          .HasMany(a => a.Appointments)
           .WithOne(b => b.doctor)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
             .HasOne(a => a.doctor)
             .WithMany(d => d.Appointments)
             .HasForeignKey(a => a.DoctorId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Doctor>()
      .HasMany(a => a.medicalReports)
      .WithOne(b => b.doctor)
       .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Patient>()
      .HasMany(a => a.Appointments)
      .WithOne(b => b.patient);

            modelBuilder.Entity<Patient>()
   .HasMany(a => a.MedicalReports)
   .WithOne(b => b.patient);

            modelBuilder.Entity<Department>()
      .HasMany(a => a.Doctors)
      .WithOne(b => b.department);

            modelBuilder.Entity<Department>()
      .HasMany(a => a.Nurses)
      .WithOne(b => b.department);

            modelBuilder.Entity<Department>()
      .HasMany(a => a.Rooms)
      .WithOne(b => b.department);

            modelBuilder.Entity<MedicalReport>()
      .HasMany(a => a.Medicines)
      .WithOne(b => b.medicalReport);

            modelBuilder.Entity<Room>()
           .HasMany(a => a.Patients)
            .WithOne(b => b.Rooms);

            modelBuilder.Entity<MedicalReport>()
               .HasMany(a => a.Medicines)
              .WithOne(b => b.medicalReport);

            modelBuilder.Entity<MedicalReport>()
           .HasMany(a => a.Medicines)
         .WithOne(b => b.medicalReport);

			SeedRole(modelBuilder, "Admin");
			SeedRole(modelBuilder, "Receptionist");
			SeedRole(modelBuilder, "Doctor");
			SeedRole(modelBuilder, "Patient");
			SeedRole(modelBuilder, "Nurse");

		}
		private void SeedRole(ModelBuilder modelBuilder, string roleName)
		{
			var role = new IdentityRole
			{
				Id = roleName.ToLower(),
				Name = roleName,
				NormalizedName = roleName.ToUpper(),
				ConcurrencyStamp = Guid.Empty.ToString()
			};
			modelBuilder.Entity<IdentityRole>().HasData(role);
		}
	}
}