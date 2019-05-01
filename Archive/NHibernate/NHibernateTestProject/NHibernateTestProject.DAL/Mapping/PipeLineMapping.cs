using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernateTestProject.Model;

namespace NHibernateTestProject.Mapping
{
    public class PipeLineMapping : ClassMap<PipeLine>
    {
        public PipeLineMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            HasMany(x => x.LineElements);
        }
    }

}
