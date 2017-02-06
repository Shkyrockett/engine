// <copyright file="Physics.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class PhysicsMath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double AverageVelocity(List<double> velocity, double time)
            => velocity.Sum() / time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double DistanceTraveled(Acceleration acceleration, double time)
            => acceleration.Value * time * time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="averageSpeed"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double DistanceTraveled(double averageSpeed, double time)
            => averageSpeed * time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double FreeFallVelocity(ITime time)
            => EarthGravity.Value * time.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static double Frequency(double period)
            => 1d / period;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static double Period(double frequency)
            => 1d / frequency;
    }
}
