using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernateTestProject.Model;

namespace NHibernateTestProject.Mapping
{
    public class InspectionSiteMapping : ClassMap<InspectionSite>
    {
        public InspectionSiteMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Owner);
            References(x => x.PipingModel);
            HasMany(x => x.ListInspections);
        }
    }

}
