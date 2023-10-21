using Hospital_System.Auth.Models;
using Hospital_System.Auth.Models.Interface;
using Hospital_System.Auth.Models.Services;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;




            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddSession();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<HospitalDbContext>
            (opions => opions.UseSqlServer(connString));

            // Add services to the container.

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages();



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<HospitalDbContext>();


            // failed trials - accessing path
            // new for cookies auth
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.AccessDeniedPath = "/auth/index";
            });

            builder.Services.AddAuthentication();

            builder.Services.AddAuthorization();

builder.Services.AddTransient<IDepartment, DepartmentService>();
builder.Services.AddTransient<IRoom, RoomService>();
builder.Services.AddTransient<IAppointment, AppointmentService>();
builder.Services.AddTransient<IMedicalReport, MedicalReportService>();
builder.Services.AddTransient<IMedicine, MedicineService>();
builder.Services.AddTransient<IEmail, EmailServices>();
builder.Services.AddTransient<IUser, UserServices>();
builder.Services.AddTransient<IHospital, HospitalService>();
builder.Services.AddTransient<INurse, NurseService>();
builder.Services.AddTransient<IDoctor, DoctorService>();
builder.Services.AddTransient<IPatient, PatientService>();
builder.Services.AddTransient<IAppointmentSlot, AppointmentSlotService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



