
namespace NHibernateTestProject.Mapping
{
    using FluentNHibernate.Mapping;
    using Model;

    public class AccountMapping : ClassMap<Account>
    {
        public AccountMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }

    //http://www.codeproject.com/Articles/232034/Inheritance-mapping-strategies-in-Fluent-Nhibernat
    //ja sma bezveze uzela table per concrete type
    public class AccountWithBalanceMapping : SubclassMap<AccountWithBalance>
    {
        public AccountWithBalanceMapping()
        {
            Map(x => x.InitialBalance);
        }
    }
}
