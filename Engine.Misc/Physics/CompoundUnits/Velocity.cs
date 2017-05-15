// <copyright file="Velocity.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    public class Velocity
        : IVelocity
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public Velocity(ISpeed speed, IDirection direction)
        {
            Direction = direction;
            Acceleration = speed;
        }

        /// <summary>
        ///
        /// </summary>
        public ISpeed Acceleration { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IDirection Direction { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value
            => Acceleration.Value * Direction.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Velocity";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"{Acceleration.Abreviation} {Direction.Abreviation}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Abreviation}";
    }
}
