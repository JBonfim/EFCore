using System;
using System.Linq;
using CursoEfcore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEfcore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new data.ApplicationContext();

            var isExist = db.Database.GetPendingMigrations().Any();
            if(isExist){
                db.Database.Migrate();
            }
            

            Console.WriteLine("Hello World!");
        }

         private static void RemovendoRegistros(){
              using var db = new data.ApplicationContext();
              var cliente = db.Clientes.Find(2);
             // var cliente3 = new { id = 3};
              //db.Clientes.Remove(cliente);
              //db.Remove(cliente);
              db.Entry(cliente).State = EntityState.Deleted;

              db.SaveChanges();


         }

         private static void AtualizandoDados(){
              using var db = new data.ApplicationContext();
            // var cliente = db.Clientes.Find(1);

            var cliente = new Cliente{
                Id = 1
            };

             var clienteDesconectado = new {
                 Nome = "Cliente Desconectado",
                 Telefone = "7979797979"
             };

             db.Attach(cliente);//Infoma pra o entity pra que o objeto começe a ser rastreado.
             db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

             //db.Clientes.Update(cliente);
             db.SaveChanges();


         }

         private static void ConsultaDadosPedidoCarregamento(){
             using var db = new data.ApplicationContext();
            
             var pedidos = db.Pedidos
              .Include(p=> p.Itens)
               .ThenInclude(p=> p.Produto)
                .ToList();

         }

        private static void ConsultaDados(){
             using var db = new data.ApplicationContext();
            // var consulta = (from c in db.Clientes where c.Id > 0 select c).ToList();
             var consultaPorMetodo = db.Clientes.Where(x=>x.Id > 0)
             .OrderBy(p=> p.Id)
             .ToList();
             //var consultaPorMetodo = db.Clientes.AsNoTracking().Where(x=>x.Id > 0).ToList(); Informa ao Entity que não rastrei
             //esses objetos em memoria, forçando que as buscas va sempre no banco de dados

             foreach (var cliente in consultaPorMetodo)
             {
                 //db.Clientes.Find(item.Id); // comsulta se o objeto esta em memoria ou não, caso esteja ele não vai buscar na base de dados
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
             }
        }

        private static void InserirDadosEmMassa(){
            var produto = new Produto{
                Descricao = "Produto teste"
            };
            var cliente = new Cliente{
                Nome = "Cliente teste"
            };

            var lisCliente = new[]{
                new Cliente{
                Nome = "Cliente teste"
                },
                new Cliente{
                    Nome = "Cliente teste 2"
                }
            };

            using var db = new data.ApplicationContext();

            db.AddRange(produto,cliente);
            //db.Clientes.AddRange(lisCliente);
            //db.Set<Cliente>().AddRange(lisCliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"total registros: {registros}" );


        }

         private static void InserirDados(){
            var produto = new Produto{
                Descricao = "Produto teste"
            };

            using var db = new data.ApplicationContext();

            db.produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"total registros: {registros}" );


        }
    }
}
