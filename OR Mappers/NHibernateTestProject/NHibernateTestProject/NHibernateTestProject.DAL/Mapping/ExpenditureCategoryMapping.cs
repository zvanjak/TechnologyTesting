using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestProject.Model
{
    using FluentNHibernate.Mapping;

    public class ExpenditureCategoryMapping : ClassMap<ExpenditureCategory>
    {
        public ExpenditureCategoryMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            References(x => x.ParentCategory);
            HasMany(x => x.ListChildren);
        }
    }
}
