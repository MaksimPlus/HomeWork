using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HomeWork.Models;
using System.Threading.Tasks;

namespace HomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        EmployeesContext db;
        public EmployeesController(EmployeesContext context)
        {
            db = context;
            if (!db.Employees.Any())
            {
                db.Employees.Add(new Employees { Name = "Maxim", Post = "Courier" });
                db.Employees.Add(new Employees { Name = "Alex", Post = "Scientist" });
                db.Employees.Add(new Employees { Name = "Julia", Post = "Postman" });
                db.Employees.Add(new Employees { Name = "Nikita", Post = "Seller" });
                db.Employees.Add(new Employees { Name = "Vladimir", Post = "Writer" });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> Get()
        {
            return await db.Employees.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> Get(int id)
        {
            Employees employees = await db.Employees.FirstOrDefaultAsync(x=>x.Id==id);
            if (employees == null)
            {
                return NotFound();
            }
            return new ObjectResult(employees);
            
        }
        [HttpPost]
        public async Task<ActionResult<Employees>> Post(Employees employees)
        {
            if(employees == null)
            {
                return BadRequest();
            }
            db.Employees.Add(employees);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<Employees>>Put(Employees employees)
        {
            if (employees == null)
            {
                return BadRequest();
            }
            if (!db.Employees.Any(x => x.Id == employees.Id))
            {
                return NotFound();
            }
            db.Update(employees);
            await db.SaveChangesAsync();
            return Ok(employees);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> Delete(int id)
        {
            Employees employees = db.Employees.FirstOrDefault(x => x.Id == id);
            if (employees == null)
            {
                return NotFound();
            }
            db.Employees.Remove(employees);
            await db.SaveChangesAsync();
            return Ok(employees);
        }
    }
}
