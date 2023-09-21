using CoreApp.DTOs;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Mappings
{
    public class PersonenMap : ClassMap<Personen>
    {
        public PersonenMap()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.Adresse);
            Map(x => x.Email);
            Map(x => x.Name);
        }
    }
}
