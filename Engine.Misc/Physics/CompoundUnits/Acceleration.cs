// <copyright file="Acceleration.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct Acceleration
        : IAcceleration
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="velocityChange"></param>
        /// <param name="timeInterval"></param>
        public Acceleration(IVelocity velocityChange, ITime timeInterval)
        {
            VelocityChange = velocityChange;
            TimeInterval = timeInterval;
        }

        /// <summary>
        ///
        /// </summary>
        public IVelocity VelocityChange { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ITime TimeInterval { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value
            => VelocityChange.Value / TimeInterval.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Acceleration";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"∆{Value}/∆{VelocityChange.Abreviation}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} {Abreviation}";
    }
}
