using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernateTestProject.Model;

namespace NHibernateTestProject.Mapping
{
    public class PipingModelMapping : ClassMap<PipingModel>
    {
        public PipingModelMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            HasMany(x => x.Lines);
        }
    }

}
