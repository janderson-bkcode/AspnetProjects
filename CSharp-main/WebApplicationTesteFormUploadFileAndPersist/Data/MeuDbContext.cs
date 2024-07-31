using Microsoft.EntityFrameworkCore;
using WebApplicationTesteFormUploadFileAndPersist.Models;

namespace WebApplicationTesteFormUploadFileAndPersist.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {

        }
        public MeuDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MeuDbContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
        //Aula 08.04 CRUD  passo 2
        //Trazendo a model para o contexto do banco e que seja entendida como uma tabela para ser mapeada
        public DbSet<DadosExcel> PixTransactionConciliation { get; set; }
    }
}
