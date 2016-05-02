using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TimeLine
    {
        private double tick;

        private Tuple<double, double> range;

        private Dictionary<double, List<Tuple<Delegate, List<object>>>> actions;

        public TimeLine()
        {

        }

        public List<Tuple<Delegate, List<object>>> this[double index]
        {
            get { return actions[index]; }
            set
            {
                if (actions[index] == null)
                {
                    actions.Add(index, value);
                }
                else
                {
                    actions[index] = value;
                }
            }
        }

        public double Tick
        {
            get { return tick; }
            set { tick = value; }
        }

        public Tuple<double, double> Range
        {
            get { return range; }
            set { range = value; }
        }

        public Dictionary<double, List<Tuple<Delegate, List<object>>>> Actions
        {
            get { return actions; }
            set { actions = value; }
        }
    }
}
