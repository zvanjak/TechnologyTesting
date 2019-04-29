using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateTestProject.Model
{
    public class InspectionPlan
    {
        private int _ID;

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private DateTime _plannedDate;
        private List<PipeElementMeasurementGridDefinition> _listPlannedMeasurements;
    }
}
