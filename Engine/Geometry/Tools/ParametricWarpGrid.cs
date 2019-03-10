// <copyright file="ParametricPointTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The parametric warp grid class.
    /// </summary>
    [DataContract, Serializable]
    public class ParametricWarpGrid
        : Shape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricWarpGrid"/> class.
        /// </summary>
        public ParametricWarpGrid()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricWarpGrid"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="path">The path.</param>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        public ParametricWarpGrid(Func<Point2D, Point2D> filter, Rectangle2D path, double minX, double minY, double maxX, double maxY, double stepX, double stepY)
        {
            Filter = filter;
            Path = path;
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
            StepX = stepX;
            StepY = stepY;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="path">The path.</param>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        public void Deconstruct(out Func<Point2D, Point2D> filter, out Rectangle2D path, out double minX, out double minY, out double maxX, out double maxY, out double stepX, out double stepY)
        {
            filter = Filter;
            path = Path;
            minX = MinX;
            minY = MinY;
            maxX = MaxX;
            maxY = MaxY;
            stepX = StepX;
            stepY = StepY;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        [Browsable(true)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Func<Point2D, Point2D> Filter { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        [Browsable(true)]
        //[DataMember, XmlElement, SoapElement]
        public Rectangle2D Path { get; set; }

        /// <summary>
        /// Gets or sets the min x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MinX { get; set; }

        /// <summary>
        /// Gets or sets the min y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MinY { get; set; }

        /// <summary>
        /// Gets or sets the max x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MaxX { get; set; }

        /// <summary>
        /// Gets or sets the max y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MaxY { get; set; }

        /// <summary>
        /// Gets or sets the step x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double StepX { get; set; }

        /// <summary>
        /// Gets or sets the step y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double StepY { get; set; }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
        {
            get { return Rectangle2D.FromLTRB(MinX, MinY, MaxX, MaxY); }
            set
            {
                MinX = value.Left;
                MinY = value.Top;
                MaxX = value.Right;
                MaxY = value.Bottom;
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// The grid.
        /// </summary>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public List<Point2D> Grid()
            => Grid(MinX, MinY, MaxX, MaxY, StepX, StepY);

        /// <summary>
        /// The grid.
        /// </summary>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> Grid(double minX, double minY, double maxX, double maxY, double stepX, double stepY)
        {
            var width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            var height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<Point2D>(
                from x in Enumerable.Range(0, (int)width + 1)
                from y in Enumerable.Range(0, (int)height + 1)
                select new Point2D(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// The warp.
        /// </summary>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public List<Point2D> Warp()
        {
            var result = new List<Point2D>();
            foreach (Point2D point in Grid())
            {
                result.Add(Filter.Invoke(point));
            }

            return result;
        }
        #endregion Methods
    }
}
