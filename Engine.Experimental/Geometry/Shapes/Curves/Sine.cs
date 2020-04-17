// <copyright file="Sine.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The sine class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Sine))]
    public class Sine
        : Shape2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sine"/> class.
        /// </summary>
        public Sine()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sine"/> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public Sine(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Gets or sets the a.
        /// </summary>
        public Point2D A { get; set; }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        public Point2D B { get; set; }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>

        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.Sine(t, A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(Sine);
            }

            return $"{nameof(Sine)}{{{nameof(A)}={A},{nameof(B)}={B}}}";
        }
    }
}
