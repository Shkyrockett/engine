// <copyright file="Physics.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// The physics math class.
    /// </summary>
    public static partial class PhysicsMath
    {
        /// <summary>
        /// Add the velocities.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double AddVelocities(double v1, double v2, double c = 299790000d) => v1 + (v2 / (1d + (v1 * v2 / c * c)));

        /// <summary>
        /// The average velocity.
        /// </summary>
        /// <param name="velocity">The velocity.</param>
        /// <param name="time">The time.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double AverageVelocity(List<double> velocity, double time) => velocity.Sum() / time;

        /// <summary>
        /// The distance traveled.
        /// </summary>
        /// <param name="acceleration">The acceleration.</param>
        /// <param name="time">The time.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double DistanceTraveled(Acceleration acceleration, double time) => acceleration.Value * time * time;

        /// <summary>
        /// The distance traveled.
        /// </summary>
        /// <param name="averageSpeed">The averageSpeed.</param>
        /// <param name="time">The time.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double DistanceTraveled(double averageSpeed, double time) => averageSpeed * time;

        /// <summary>
        /// The free fall velocity.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double FreeFallVelocity(ITime time) => EarthGravity.Value * (time?.Value).Value;

        /// <summary>
        /// The frequency.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double Frequency(double period) => 1d / period;

        /// <summary>
        /// The period.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double Period(double frequency) => 1d / frequency;
    }
}
