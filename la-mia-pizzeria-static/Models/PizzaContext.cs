using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        private string SqlString = "Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=db_pizzeria";


        public DbSet<Pizza>? Pizza { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Ingredient>? Ingredient  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlString);
        }

    }

}
