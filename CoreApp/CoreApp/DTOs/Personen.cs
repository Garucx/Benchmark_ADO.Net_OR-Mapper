using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.DTOs
{
    public class Personen
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Adresse  { get; set; }
        public virtual string Email { get; set; }
    }
}
