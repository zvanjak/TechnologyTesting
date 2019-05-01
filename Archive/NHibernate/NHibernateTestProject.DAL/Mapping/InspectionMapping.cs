using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernateTestProject.Model;

namespace NHibernateTestProject.Mapping
{
    public class InspectionMapping : ClassMap<Inspection>
    {
        public InspectionMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }
}
