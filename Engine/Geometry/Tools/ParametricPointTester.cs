// <copyright file="ParametricPointTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class ParametricPointTester
        : Shape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ParametricPointTester()
            : base()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intersecter"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        public ParametricPointTester(Func<double, double, Inclusion> intersecter, double minX, double minY, double maxX, double maxY, double stepX, double stepY)
            : base()
        {
            Intersecter = intersecter;
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
            StepX = stepX;
            StepY = stepY;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intersecter"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        public void Deconstruct(out Func<double, double, Inclusion> intersecter, out double minX, out double minY, out double maxX, out double maxY, out double stepX, out double stepY)
        {
            intersecter = Intersecter;
            minX = MinX;
            minY = MinY;
            maxX = MaxX;
            maxY = MaxY;
            stepX = StepX;
            stepY = StepY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Func<double, double, Inclusion> Intersecter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MinX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MinY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MaxX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double MaxY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double StepX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double StepY { get; set; }

        /// <summary>
        /// 
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

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> Grid()
            => Grid(MinX, MinY, MaxX, MaxY, StepX, StepY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public (List<Point2D>, List<Point2D>, List<Point2D>) Interactions()
        {
            var boundary = new List<Point2D>();
            var inside = new List<Point2D>();
            var outside = new List<Point2D>();
            foreach (Point2D point in Grid())
            {
                var value = Intersecter.Invoke(point.X, point.Y);
                switch (value)
                {
                    case Inclusion.Outside:
                        outside.Add(point);
                        break;
                    case Inclusion.Inside:
                        inside.Add(point);
                        break;
                    case Inclusion.Boundary:
                        boundary.Add(point);
                        break;
                }
            }
            
            return (boundary, inside, outside);
        }

        #endregion
    }
}
