using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTestProject.Model
{
    enum InspectionStatus
    {
        PLANNED,
        IN_PROGRESS,
        DONE
    };

    public class Inspection
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

        public Inspection()
        {
            
        }

        DateTime _inspectionDate;
        InspectionPlan _inspectionPlan;
        InspectionData _inspectionData;

        InspectionStatus _inspectionStatus;
    }
}
