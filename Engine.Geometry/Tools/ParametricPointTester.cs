// <copyright file="ParametricPointTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The parametric point tester class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class ParametricPointTester
        : Shape2D
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricPointTester"/> class.
        /// </summary>
        public ParametricPointTester()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricPointTester"/> class.
        /// </summary>
        /// <param name="intersector">The intersector.</param>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        public ParametricPointTester(Func<double, double, Inclusions> intersector, double minX, double minY, double maxX, double maxY, double stepX, double stepY)
        {
            Intersector = intersector;
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
        /// The deconstructor.
        /// </summary>
        /// <param name="intersector">The intersector.</param>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        public void Deconstruct(out Func<double, double, Inclusions> intersector, out double minX, out double minY, out double maxX, out double maxY, out double stepX, out double stepY)
        {
            intersector = Intersector;
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
        /// Gets or sets the intersector.
        /// </summary>
        [Browsable(true)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Func<double, double, Inclusions> Intersector { get; set; }

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
        /// <returns>The <see cref="List{T}"/>.</returns>
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
        /// <returns>The <see cref="List{T}"/>.</returns>
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
        /// The interactions.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        public (List<Point2D>, List<Point2D>, List<Point2D>) Interactions()
        {
            var boundary = new List<Point2D>();
            var inside = new List<Point2D>();
            var outside = new List<Point2D>();
            foreach (var point in Grid())
            {
                var value = Intersector.Invoke(point.X, point.Y);
                switch (value)
                {
                    case Inclusions.Outside:
                        outside.Add(point);
                        break;
                    case Inclusions.Inside:
                        inside.Add(point);
                        break;
                    case Inclusions.Boundary:
                        boundary.Add(point);
                        break;
                }
            }

            return (boundary, inside, outside);
        }
        #endregion Methods
    }
}
