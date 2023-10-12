using Hospital_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {

        public HospitalDbContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            SeedRole(modelBuilder, "Admin", "create", "update", "delete");
            SeedRole(modelBuilder, "Receptionist", "create", "update", "delete");
            SeedRole(modelBuilder, "Doctor", "create", "update","delete");
            SeedRole(modelBuilder, "Patient", "create", "update", "delete");
            SeedRole(modelBuilder, "Nurse");





            //---------------------------------------------------
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

            //----------------------------------------------------------------------

        }

        private int id = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
             new IdentityRoleClaim<string>
             {
                 Id = id++,
                 RoleId = role.Id,
                 ClaimType = "permissions",
                 ClaimValue = permission
             }
            );
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
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

    }
}
