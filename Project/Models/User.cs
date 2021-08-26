using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int ContaId { get; set; }

        public Conta Conta { get; set; }        
    }
}
