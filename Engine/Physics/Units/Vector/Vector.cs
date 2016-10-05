// <copyright file="Vector.cs" >
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
        public double Value => Magnitude * Direction;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Vector";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{"?"}{"?"}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} K";
    }
}
