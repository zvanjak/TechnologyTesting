using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestProject.Model
{
    public class InspectionSite
    {
        private int _ID;
        private string _name;
        private string _owner;

        private List<Inspection> _listInspections = new List<Inspection>();
        private readonly PipingModel _pipingModel = new PipingModel();

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public virtual PipingModel PipingModel
        {
            get { return _pipingModel; }
        }

        public virtual List<Inspection> ListInspections
        {
            get { return _listInspections;  }
        }

        public virtual void InitializeData(string inDBFileName)
        {
            
        }

        public virtual void InitializeTestData()
        {
            Name = "Test system";
            Owner = "ACME Ltd.";

            //PipeLine l1 = new PipeLine();
            //l1.Name = "Line 1";
            //PipeElementPart e1 = new PipeElementPart() { Name = "EL1" };
            //PipeElementPart e2 = new PipeElementPart() { Name = "EL2" };
            //PipeElementPart e3 = new PipeElementPart() { Name = "EL3" };
            //l1._lineElements.Add(e1);
            //l1._lineElements.Add(e2);
            //l1._lineElements.Add(e3);

            //PipeLine l2 = new PipeLine();
            //l2.Name = "Line 2";
            //PipeElementPart f1 = new PipeElementPart() { Name = "EL1" };
            //PipeElementPart f2 = new PipeElementPart() { Name = "EL2" };
            //PipeElementPart f3 = new PipeElementPart() { Name = "EL3" };
            //l2._lineElements.Add(f1);
            //l2._lineElements.Add(f2);
            //l2._lineElements.Add(f3);

            //_pipingModel.Lines.Add(l1);
            //_pipingModel.Lines.Add(l2);
        }
    }
}
