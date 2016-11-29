// <copyright file="DistanceFreeFall.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct DistanceFreeFall
        : ILength
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="time"></param>
        public DistanceFreeFall(IAcceleration acceleration, ITime time)
        {
            Acceleration = acceleration;
            Time = time;
        }

        /// <summary>
        ///
        /// </summary>
        public IAcceleration Acceleration { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value
            => Acceleration.Value * Time.Value * Time.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Instantaneous Speed";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"{Value}{Acceleration.Abreviation}²";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} {Acceleration.Abreviation}{Time.Abreviation}²";
    }
}
