using Microsoft.EntityFrameworkCore;
using MonolithicSPA.Models.DomainModels.PersonAggregate;
using MonolithicSPA.Models.DomainModels.ProductAggregate;
using MonolithicSPA.Models.DomainModels.ProductType;

namespace MonolithicSPA.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
        //اسم جداول جمع می باشد.=> مهم
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }


    }
}
