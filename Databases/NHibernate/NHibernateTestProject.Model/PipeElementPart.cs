using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTestProject.Model
{
    public class PipeElementPart
    {
        private int _ID;
        private string _name;
        private double x, y, z;

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

        //Object _refToBasicPipeElement;

        public PipeElementPart()
        {
        }
        public PipeElementPart(string inName)
        {
            Name = inName;
        }
    }
}
