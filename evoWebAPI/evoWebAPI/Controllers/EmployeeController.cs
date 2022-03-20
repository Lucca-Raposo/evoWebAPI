using evoWebAPI.DTO;
using evoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evoWebAPI.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        public EmployeeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> Get(
            [FromServices] DataContext context)
        {
            var employees = await context.Employees.ToArrayAsync();

            return Ok(employees);
        }

        [HttpGet]
        [Route("employees/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(
        [FromServices] DataContext context,
        [FromRoute] int departmentId)
        {
            var employees = await context.Employees.Where<Employee>(x => x.departmentId == departmentId).ToArrayAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }


        [HttpPost]
        [Route("employees")]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromForm] CreateEmployeeDTO employeeDTO, IFormFile picture)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var department = await context.Departments.FirstOrDefaultAsync(x => x.id == employeeDTO.departmentId);

            if (department == null)
            {
                return NotFound("O Departamento informado não existe");
            }

            try
            {
                using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\Images\\" + picture.FileName))
                {
                    await picture.CopyToAsync(filestream);
                    filestream.Flush();
                }
            }
            catch (Exception ex)
            {
                NotFound(ex);
            }

            var employee = new Employee
            {
                name = employeeDTO.name,
                RG = employeeDTO.RG,
                picture = "http://127.0.0.1:8887/" + picture.FileName,
                pictureName = picture.FileName,
                departmentId = employeeDTO.departmentId,
                department = department
            };

            try
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return Created(uri: $"employees/{employee.id}", employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("employees/{id}")]
        public async Task<IActionResult> Put(
            [FromServices] DataContext context,
            [FromForm] EditEmployeeDTO employeeDTO, IFormFile? picture,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = await context.Employees.FirstOrDefaultAsync(y => y.id == id);
            var department = await context.Departments.FirstOrDefaultAsync(x => x.id == employeeDTO.departmentId);

            if (employee == null)
            {
                return NotFound("Funcionario não encontrado");
            }
            if (department == null)
            {
                return NotFound("Departamento não encontrado");
            }

            if (employee.pictureName != null && picture != null)
            {
                System.IO.File.Delete(_environment.ContentRootPath + "\\Images\\" + employee.pictureName);
                try
                {
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\Images\\" + picture.FileName))
                    {
                        await picture.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                    employee.picture = "http://127.0.0.1:8887/" + picture.FileName;
                    employee.pictureName = picture.FileName;
                }
                catch (Exception ex)
                {
                    NotFound(ex);
                }
            }


            try
            {
                employee.name = employeeDTO.name;
                employee.RG = employeeDTO.RG;
                employee.departmentId = employeeDTO.departmentId;
                employee.department = department;

                context.Employees.Update(employee);
                await context.SaveChangesAsync();

                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpDelete]
        [Route("employees/{id}")]
        public async Task<IActionResult> Delete(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var employee = context.Employees.FirstOrDefault(x => x.id == id);

            if (employee?.pictureName != null) {
                System.IO.File.Delete(_environment.ContentRootPath + "\\Images\\" + employee?.pictureName);
            }


            if (employee == null)
            {
                return BadRequest();
            }

            try
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                return Ok(employee);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
