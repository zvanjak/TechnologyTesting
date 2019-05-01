using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernateTestProject.Model;

namespace NHibernateTestProject.DAL.Mapping
{
    public class PipeElementPartMapping : ClassMap<PipeElementPart>
    {
        public PipeElementPartMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }
}
