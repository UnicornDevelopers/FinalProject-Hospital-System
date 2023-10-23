using Hospital_System.Auth.Models;
using Hospital_System.Models;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Hospital_System.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {

        public HospitalDbContext(DbContextOptions options) : base(options)
        {


        }


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<AppointmentSlot> AppointmentSlots { get; set; }
        public DbSet<MedicineMedicalReport> MedicineMedicalReports { get; set; }



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

            modelBuilder.Entity<MedicineMedicalReport>()
       .HasKey(c => new { c.MedicalReportID, c.MedicineID });



            modelBuilder.Entity<Hospital>().HasData(
              new Hospital { Id = 1, HospitalName = "Test", Address = "Test", ContactNumber = "079999999" }
            );

            //modelBuilder.Entity<Department>().HasData(
            //  new Department { Id = 1, DepartmentName = "Test", HospitalID = 1 }

            //);

            modelBuilder.Entity<Department>().HasData(
           new Department
           {
               Id = 1,
               DepartmentName = "Cardiology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/h2.png",
               Description = "Specializing in heart care and treatment."
           },
           new Department
           {
               Id = 2,
               DepartmentName = "Orthopedics",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Bone.png",
               Description = "Dealing with bone and joint-related issues."
           },
           new Department
           {
               Id = 3,
               DepartmentName = "Nephrology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Nephrology.png",
               Description = "Focused on kidney-related diseases and care."
           },
           new Department
           {
               Id = 4,
               DepartmentName = "Neurology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/brain.png",
               Description = "Specializing in brain and nervous system care."
           },
           new Department
           {
               Id = 5,
               DepartmentName = "Ophthalmology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/optics.png",
               Description = "Focused on eye and vision care."
           },
           new Department
           {
               Id = 6,
               DepartmentName = "Hepatology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Liver.png",
               Description = "Dealing with liver and digestive system issues."
           },
           new Department
           {
               Id = 7,
               DepartmentName = "Gastroenterology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Intestines.png",
               Description = "Specializing in intestinal care."
           },
           new Department
           {
               Id = 8,
               DepartmentName = "Pulmonology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/lung.png",
               Description = "Focused on lung and respiratory care."
           },
           new Department
           {
               Id = 9,
               DepartmentName = "Obstetrics",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Pediatrics.png",
               Description = "Specializing in maternity care and prenatal services."
           },
           new Department
           {
               Id = 10,
               DepartmentName = "Obstetrics and Gynecology",
               HospitalID = 1,
               Image = "https://storageaccbookimages.blob.core.windows.net/images/Obstetrics.png",
               Description = "Dedicated to women's health and maternity care."
           }
       );
            modelBuilder.Entity<Medicine>().HasData(
            new Medicine { Id = 1, MedicineName = "Ibuprofen", Portion = "200mg" },
            new Medicine { Id = 2, MedicineName = "Paracetamol", Portion = "500mg" },
            new Medicine { Id = 3, MedicineName = "Amoxicillin", Portion = "250mg" },
            new Medicine { Id = 4, MedicineName = "Aspirin", Portion = "325mg" },
            new Medicine { Id = 5, MedicineName = "Cetirizine", Portion = "10mg" },
            new Medicine { Id = 6, MedicineName = "Diclofenac", Portion = "50mg" },
            new Medicine { Id = 7, MedicineName = "Erythromycin", Portion = "500mg" },
            new Medicine { Id = 8, MedicineName = "Furosemide", Portion = "40mg" }
                );







            SeedRole(modelBuilder, "Nurse");
            SeedRole(modelBuilder, "Receptionist");
            SeedRole(modelBuilder, "Users");
            SeedRole(modelBuilder, "Patient");
            SeedRole(modelBuilder, "Doctor");
            SeedRole(modelBuilder, "Admin");


            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gamil.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "A12345@"),
            };
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "admin",
                UserId = "1"
            });


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