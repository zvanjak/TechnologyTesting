using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestProject.Model
{
    public class ExpenditureCategory
    {
        private int _id;
        private string _categoryName;
        private ExpenditureCategory _parent;
        private List<ExpenditureCategory> _listChildren = new List<ExpenditureCategory>();

        public virtual int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public virtual ExpenditureCategory ParentCategory
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public virtual List<ExpenditureCategory> ListChildren
        {
            get { return _listChildren; }
            set { _listChildren = value; }
        }

        public ExpenditureCategory()
        {

        }
        public ExpenditureCategory(string inName, ExpenditureCategory inParent)
        {
            Name = inName;
            ParentCategory = inParent;
        }

        public virtual void AddCategoryChildren(ExpenditureCategory inChild)
        {
            ListChildren.Add(inChild);
        }
    }
}
