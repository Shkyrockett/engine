// <copyright file="ParametricPointTester.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ParametricWarpGrid
        : Shape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ParametricWarpGrid()
            : base()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="path"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        public ParametricWarpGrid(Func<Rectangle2D, Point2D, Point2D> filter, Rectangle2D path, double minX, double minY, double maxX, double maxY, double stepX, double stepY)
            : base()
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

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="path"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        public void Deconstruct(out Func<Rectangle2D, Point2D, Point2D> filter, Rectangle2D path, out double minX, out double minY, out double maxX, out double maxY, out double stepX, out double stepY)
        {
            filter = this.Filter;
            path = this.Path;
            minX = this.MinX;
            minY = this.MinY;
            maxX = this.MaxX;
            maxY = this.MaxY;
            stepX = this.StepX;
            stepY = this.StepY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [XmlIgnore, SoapIgnore]
        public Func<Rectangle2D, Point2D, Point2D> Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [XmlElement, SoapElement]
        public Rectangle2D Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double MinX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double MinY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double MaxX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double MaxY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double StepX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double StepY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
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
            double width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            double height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<Point2D>(
                from x in Enumerable.Range(0, (int)width + 1)
                from y in Enumerable.Range(0, (int)height + 1)
                select new Point2D(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> Warp()
        {
            var result = new List<Point2D>();
            foreach (Point2D point in Grid())
            {
                result.Add(Filter.Invoke(Path.Bounds, point));
            }

            return result;
        }

        #endregion
    }
}
