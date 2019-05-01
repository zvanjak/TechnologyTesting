using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTestProject.Model
{
    public class PipingModel
    {
        private int _ID;
        private string _name;
        private List<PipeLine> _lines = new List<PipeLine>();

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

        public virtual List<PipeLine> Lines
        {
            get { return _lines; }
        }
    }
}
