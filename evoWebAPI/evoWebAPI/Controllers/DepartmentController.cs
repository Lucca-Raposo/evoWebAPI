using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using evoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using evoWebAPI.DTO;

namespace evoWebAPI.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        [Route("departments")]
        public async Task<IActionResult> Get(
            [FromServices] DataContext context)
        {
            var departments = await context.Departments.ToArrayAsync();

            return Ok(departments);
        }

        [HttpGet]
        [Route("departments/{id}")]
        public async Task<IActionResult> GetById(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var department = await context.Departments.FirstOrDefaultAsync(x => x.id == id);

            return Ok(department);
        }

        [HttpPost]
        [Route("departments")]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var department = new Department
            {
                name = departmentDTO.name,
                abbrev = departmentDTO.abbrev,
            };

            try
            {
                await context.Departments.AddAsync(department);
                await context.SaveChangesAsync();
                return Created(uri: $"/departments/{department.id}", department);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("departments/{id}")]
        public async Task<IActionResult> Put(
            [FromServices] DataContext context,
            [FromBody] EditDepartmentDTO departmentDTO,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var department = await context.Departments.FirstOrDefaultAsync(x => x.id == id);

            if (department == null)
            {
                return NotFound();
            }

            try
            {
                department.name = departmentDTO.name;
                department.abbrev = departmentDTO.abbrev;

                context.Departments.Update(department);
                await context.SaveChangesAsync();

                return Ok(department);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Route("departments/{id}")]
        public async Task<IActionResult> Delete(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var department = context.Departments.FirstOrDefault(x => x.id == id);

            if (department == null)
            {
                return BadRequest();
            }

            try
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return Ok(department);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
