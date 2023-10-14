using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class DashboardsController : Controller
    {

        private readonly IDepartment _department;
        private readonly IHospital _hospital;
        private readonly HospitalDbContext _context;

        public DashboardsController(IDepartment department,IHospital hospital,HospitalDbContext context)
        {
                _department = department;
            _hospital = hospital;
            _context = context;
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(InDepartmentDTO department , IFormFile file)
        {
            if (ModelState.IsValid && file != null)
            {
                // Upload the image to Azure Blob Storage and get the URI
                await _department.GetFile(file, department);

                // Save the product to the database
                await _department.CreateDepartment(department);

                return RedirectToAction("ViewAllProducts", "Products");
            }

            return View(department);
        }



        [HttpGet]
        public IActionResult AddHospital()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHospital(OutHospitalDTO hospital)
        {
            await _hospital.Create(hospital);

            return View(hospital);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDepartment(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(int id, OutDepartmentDTO updateDepartmentDTO, IFormFile? file)
        {
            if (file != null)
            {
                await _department.GetFile2(file, updateDepartmentDTO);

            }

            await _department.UpdateDepartment(id, updateDepartmentDTO);
            return RedirectToAction("UpdateDepartment", "Dashboards");
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _department.GetDepartments();

                if (departments == null || departments.Count == 0)
                {
                    ViewData["ErrorMessage"] = "No departments found.";
                    return View();
                }

                return View(departments);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred while retrieving departments: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                // Handle case where department is not found
                return NotFound();
            }

            return View(department);
        }

        
        [HttpPost, ActionName("DeleteDepartment")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                // Handle case where department is not found
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetDepartments");
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _department.GetDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


    }
}
