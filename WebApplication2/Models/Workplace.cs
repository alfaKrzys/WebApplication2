using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Workplace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ForemanId { get; set; }
        public virtual Foreman Foreman { get; set; }
    }
}
