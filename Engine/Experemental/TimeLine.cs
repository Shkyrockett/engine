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
        /// <param name="index"></param>
        /// <returns></returns>
        public List<Tuple<Delegate, List<object>>> this[double index]
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
        public double Tick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Tuple<double, double> Range { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<double, List<Tuple<Delegate, List<object>>>> Actions { get; set; }
    }
}
