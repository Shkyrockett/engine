using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeLine
    {
        /// <summary>
        /// 
        /// </summary>
        private double tick;

        /// <summary>
        /// 
        /// </summary>
        private Tuple<double, double> range;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<double, List<Tuple<Delegate, List<object>>>> actions;

        /// <summary>
        /// 
        /// </summary>
        public TimeLine()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public double Tick
        {
            get { return tick; }
            set { tick = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Tuple<double, double> Range
        {
            get { return range; }
            set { range = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<double, List<Tuple<Delegate, List<object>>>> Actions
        {
            get { return actions; }
            set { actions = value; }
        }
    }
}
