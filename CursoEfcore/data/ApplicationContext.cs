using System;
using CursoEfcore.data.Configuration;
using CursoEfcore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CursoEfcore.data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger =  LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Pedido> Pedidos {get; set;}
        public DbSet<Produto> produtos {get;set;}

        public DbSet<Cliente>   Clientes { get; set; }
        protected   override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder
            .UseLoggerFactory(_logger)
            .EnableSensitiveDataLogging()
            .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true"
            , p=> p.EnableRetryOnFailure(//conexão resiliente, podemos pedir pra conectar novamente em caso de falhas, 
                                        //por padrao ele tenta se conectar por 6 vezes até completar 1 minuto
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null)

                    .MigrationsHistoryTable("tab_migracoes"));//alterando o nome padrao da tabela de migração

        }

         protected   override void  OnModelCreating(ModelBuilder builder){
             //builder.ApplyConfiguration(new ClienteConfiguration());
             builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
         }



    }
}