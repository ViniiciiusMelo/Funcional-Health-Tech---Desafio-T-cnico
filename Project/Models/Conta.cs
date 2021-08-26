using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Conta
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public double Saldo { get; set; } = 100;

        public int UserId { get; set; }

        public User User { get; set; }        
    }
}
