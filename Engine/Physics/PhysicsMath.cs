using System.Collections.Generic;
using System.Linq;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public static class PhysicsMath
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public static double Sum(this List<double> values)
        //{
        //    double value = 0;
        //    foreach (double item in values)
        //    {
        //        value += item;
        //    }

        //    return value;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public static double Average(this List<double> values)
        //{
        //    return values.Sum() / values.Count;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double AverageVelocity(List<double> velocity, double time) => velocity.Sum() / time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double DistanceTraveled(Acceleration acceleration, double time) => acceleration.Value * time * time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="averageSpeed"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double DistanceTraveled(double averageSpeed, double time) => averageSpeed * time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double FreeFallVelocity(ITime time) => Constants.EarthGravity.Value * time.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static double Frequency(double period) => 1d / period;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static double Period(double frequency) => 1d / frequency;
    }
}
