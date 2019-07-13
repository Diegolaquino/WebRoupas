using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRoupas.Models
{
    /*
     Essa class representa o banco de dados     
    */
    public class BancoDeDados : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public BancoDeDados(DbContextOptions<BancoDeDados> options) : base(options) { }

        // Esse Dbset representa a tabela da class no banco de dados, ou seja, para cada tabela, haverá um Dbset que deve ser criado
        //aqui.
        public DbSet<Funcionario> Funcionarios { get; set; }
    }

}
