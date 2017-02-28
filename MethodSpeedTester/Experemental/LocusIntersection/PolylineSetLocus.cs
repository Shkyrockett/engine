﻿// <copyright file="PolylineSetLocus.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PolylineSetLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        public PolylineSetLocus()
            : this(new List<List<Point2D>>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public PolylineSetLocus(IEnumerable<List<Point2D>> polylines)
        {
            Polylines = polylines as List<List<Point2D>>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public PolylineSetLocus(params IEnumerable<Point2D>[] polylines)
            : this(new List<List<Point2D>>(polylines as List<Point2D>[]))
        { }

        /// <summary>
        /// 
        /// </summary>
        public List<List<Point2D>> Polylines { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator PolylineSet(PolylineSetLocus locus)
            => new PolylineSet(locus.Polylines);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PolylineSetLocus Add(List<Point2D> point)
        {
            Polylines.Add(point);
            return this;
        }
    }
}
