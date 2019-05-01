using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTestProject.Model
{
    public class PipeLine
    {
        private int _ID;
        private string _name;
        private List<PipeElementPart> _lineElements = new List<PipeElementPart>();

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

        public virtual List<PipeElementPart> LineElements
        {
            get { return _lineElements; }
        }

        public PipeLine()
        {
        }

        public PipeLine(string inName)
        {
            Name = inName;
        }
    }
}
