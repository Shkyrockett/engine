// <copyright file="RoundRectangle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(RoundRectangle))]
    public class RoundRectangle
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private double radius;

        /// <summary>
        /// 
        /// </summary>
        private Rectangle2D bounds;

        /// <summary>
        /// 
        /// </summary>
        [NonSerialized]
        private GraphicsPath path;

        /// <summary>
        /// 
        /// </summary>
        public RoundRectangle()
        {
            radius = 0;
            bounds = new Rectangle2D();
            path = InterpolatePath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="radius"></param>
        public RoundRectangle(Rectangle2D bounds, double radius)
        {
            this.radius = radius;
            this.bounds = bounds;
            path = InterpolatePath();
        }

        /// <summary>
        /// 
        /// </summary>
        public new Rectangle2D Bounds
        {
            get { return bounds; }
            set
            {
                bounds = value;
                path = InterpolatePath();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                path = InterpolatePath();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Handles
        {
            get
            {
                return new List<Point2D> {
                    bounds.Location,
                    new Point2D(bounds.Right,bounds.Top),
                    new Point2D(bounds.Right, bounds.Bottom),
                    new Point2D(bounds.Left, bounds.Bottom),
                    new Point2D(bounds.Left+radius,bounds.Top)
                };
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GraphicsPath InterpolatePath()
        {
            return GetGraphicsPath(bounds, radius);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static GraphicsPath GetGraphicsPath(Rectangle2D bounds, double radius)
        {
            //  Start the Path object.
            GraphicsPath GfxPath = new GraphicsPath();
            if (!bounds.IsEmpty)
            {
                if (radius > 0)
                {
                    double diameter = radius * 2;
                    //  prepare the curves.
                    GfxPath.AddArc((float)(bounds.X + (bounds.Width - diameter)), (float)bounds.Y, (float)diameter,(float) diameter, 270, 90);
                    GfxPath.AddArc((float)(bounds.X + (bounds.Width - diameter)),(float) (bounds.Y + (bounds.Height - diameter)), (float)diameter,(float) diameter, 0, 90);
                    GfxPath.AddArc((float)bounds.X,(float) (bounds.Y + (bounds.Height - diameter)),(float) diameter, (float)diameter, 90, 90);
                    GfxPath.AddArc((float)bounds.X,(float) bounds.Y,(float) diameter, (float)diameter, 180, 90);
                    //  Close the path.
                    GfxPath.CloseFigure();
                }
                else
                {
                    GfxPath.AddRectangle(bounds.ToRectangleF());
                }
            }
            return GfxPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "RoundRectangle";
            return string.Format("{0}{{L={1},S={2},R={3}}}", "RoundRectangle", bounds.Location.ToString(), bounds.Size.ToString(), radius.ToString());
        }
    }
}
