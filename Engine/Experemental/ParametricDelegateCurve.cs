// <copyright file="ParametricDelegateCurve.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// Parametric Delegate Curve.
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(ParametricDelegateCurve))]
    public class ParametricDelegateCurve
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public ParametricDelegateCurve(Func<double, Point2D> function)
        {
            Function = function;
            Location = new Point2D();
            Multiplyer = new Size2D();
            Precision = 0.1;
        }

        /// <summary>
        /// 
        /// </summary>
        public Func<double, Point2D> Function { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Multiplyer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolate(Function, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D Interpolate(Func<double, Point2D> function, double t)
            => function?.Invoke(t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(double precision)
        {
            var points = new List<Point2D>();
            for (double Index = (PI * -1); (Index < PI); Index = (Index + precision))
                points.Add(Interpolate(Index));

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(ParametricDelegateCurve);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4},{5}={6}}}", nameof(ParametricDelegateCurve), nameof(Location), Location, nameof(Multiplyer), Multiplyer, nameof(Precision), Precision);
        }
    }
}
