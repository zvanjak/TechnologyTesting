namespace NHibernateTestProject.Model
{
    public abstract class Account
    {
        private int _ID;
        private string _name;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Account()
        {

        }
        public Account(string inName)
        {
            Name = inName;
        }
    }

    public abstract class AccountWithBalance : Account
    {
        private float _initialBalance = 0.0F;

        public virtual float InitialBalance
        {
            get { return _initialBalance; }
            set { _initialBalance = value; }
        }

        public AccountWithBalance()
        {

        }
        public AccountWithBalance(string inName)
            : base(inName)
        {
            _initialBalance = 0.0F;
        }
        public AccountWithBalance(string inName, float inBalance)
            : base(inName)
        {
            _initialBalance = inBalance;
        }
    }
}
