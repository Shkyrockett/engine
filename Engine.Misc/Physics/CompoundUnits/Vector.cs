// <copyright file="Vector.cs" company="Shkyrockett" >
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
    public struct Vector
        : IVector
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="direction"></param>
        public Vector(double magnitude, double direction)
        {
            Magnitude = magnitude;
            Direction = direction;
        }

        /// <summary>
        ///
        /// </summary>
        public double Magnitude { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Direction { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value
            => Magnitude * Direction;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Vector";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"|{Value}|";
    }
}
