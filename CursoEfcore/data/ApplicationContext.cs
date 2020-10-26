using CursoEfcore.data.Configuration;
using CursoEfcore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEfcore.data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos {get; set;}
        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true");

        }

         protected   override void  OnModelCreating(ModelBuilder builder){
             //builder.ApplyConfiguration(new ClienteConfiguration());
             builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
         }



    }
}