// <copyright file="Figure.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Figure
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Figure(Point2D start)
        {
            Items.Add(new FigurePoint(start));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FigureItem this[int index]
                => Items[index];

        /// <summary>
        /// 
        /// </summary>
        [TypeConverter(typeof(ListConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<FigureItem> Items { get; } = new List<FigureItem>();

        /// <summary>
        /// Gets a listing of all end nodes from the Figure.
        /// </summary>
        public List<Point2D> Nodes
            => Items.Select(item => item.End).ToList();

        /// <summary>
        /// 
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
            => Boundings.Figure(this);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddLineSegment(Point2D end)
        {
            var segment = new FigureLineSegment(Items[Items.Count - 1], end);
            Items.Add(segment);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new FigureArc(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new FigureQuadraticBezier(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new FigureCubicBezier(Items[Items.Count - 1], handle1, handle2, end);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public Figure AddCardinalCurve(List<Point2D> nodes)
        {
            var cubic = new FigureCardinal(Items[Items.Count - 1], nodes);
            Items.Add(cubic);
            return this;
        }
    }
}
