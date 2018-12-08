// <copyright file="Cosine.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// The cosine class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("Cosine Curve")]
    public class Cosine
        : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cosine"/> class.
        /// </summary>
        public Cosine()
            :this(Point2D.Empty,Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cosine"/> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public Cosine(Point2D a, Point2D b)
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
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.Cosine(A.X, A.Y, B.X, B.Y, t));

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this is null)
            {
                return nameof(Cosine);
            }

            return $"{nameof(Cosine)}{{{nameof(A)}={A},{nameof(B)}={B}}}";
        }
    }
}
