using Microsoft.EntityFrameworkCore;

namespace HomeWork.Models
{
    public class EmployeesContext:DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
