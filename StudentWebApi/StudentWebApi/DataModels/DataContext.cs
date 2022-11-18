using Microsoft.EntityFrameworkCore;
using StudentWebApi.DomainModels;

namespace StudentWebApi.DataModels
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }

        public DbSet<Student>? Students { get; set; }
        public DbSet<Gender>? Genders { get; set; }  
        public DbSet<Address>? Addresses { get; set; }
    }
}
