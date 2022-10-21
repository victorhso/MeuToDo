using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTodo.Model
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
