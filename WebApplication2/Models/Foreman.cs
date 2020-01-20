using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class Foreman
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Workplace> Workplaces { get; set; }
    }
}