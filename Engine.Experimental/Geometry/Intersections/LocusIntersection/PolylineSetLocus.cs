// <copyright file="PolylineSetLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The polyline set locus class.
    /// </summary>
    /// <seealso cref="Locus" />
    /// <remarks>
    /// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
    /// </remarks>
    public class PolylineSetLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSetLocus" /> class.
        /// </summary>
        public PolylineSetLocus()
            : this(new List<List<Point2D>>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSetLocus" /> class.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        public PolylineSetLocus(IEnumerable<List<Point2D>> polylines)
        {
            Polylines = polylines as List<List<Point2D>>;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSetLocus" /> class.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        public PolylineSetLocus(params IEnumerable<Point2D>[] polylines)
            : this(new List<List<Point2D>>(polylines as List<Point2D>[]))
        { }

        /// <summary>
        /// Gets or sets the polylines.
        /// </summary>
        /// <value>
        /// The polylines.
        /// </value>
        public List<List<Point2D>> Polylines { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PolylineSetLocus"/> to <see cref="PolylineSet2D"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PolylineSet2D(PolylineSetLocus locus) => new PolylineSet2D(locus?.Polylines);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="PolylineSetLocus" />.
        /// </returns>
        public PolylineSetLocus Add(List<Point2D> point)
        {
            Polylines.Add(point);
            return this;
        }

        /// <summary>
        /// Converts to polylineset.
        /// </summary>
        /// <returns></returns>
        public PolylineSet2D ToPolylineSet2D() => new PolylineSet2D(Polylines);
    }
}
