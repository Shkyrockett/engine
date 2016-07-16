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
        public TimeLine()
            :this(new Dictionary<double, List<(Delegate, List<object>)>>())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        public TimeLine(Dictionary<double, List<(Delegate, List<object>)>> actions)
        {
            Actions = actions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<(Delegate, List<object>)> this[double index]
        {
            get { return Actions[index]; }
            set
            {
                if (Actions[index] == null)
                    Actions.Add(index, value);
                else
                    Actions[index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Tick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public (double X, double Y) Range { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<double, List<(Delegate, List<object>)>> Actions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeLine Update()
        {
            Time += Tick;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TimeLine Update(double value)
        {
            Time += value;
            return this;
        }
    }
}
