// <copyright file="GraphicsObject.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class GraphicsObject
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual double Perimeter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Rectangle2D Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual Point2D Interpolate(double t)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual List<Point2D> InterpolatePoints(int count)
        {
            return new List<Point2D>(
                from i in Enumerable.Range(0, count)
                select Interpolate((1d / count) * i));

            //List<Point2D> points = new List<Point2D>();
            //for (int i = 0; i <= count; i++)
            //{
            //    points.Add(Interpolate((1d / count) * i));
            //}
            //return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool HitTest(Point2D point)
        {
            return false;
        }
    }
}
