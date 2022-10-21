using MeuTodo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTodo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");;
        }
    }
}
