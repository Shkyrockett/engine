using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class RoundRectangle
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private float radius;

        /// <summary>
        /// 
        /// </summary>
        private RectangleF bounds;

        /// <summary>
        /// 
        /// </summary>
        private GraphicsPath path;

        /// <summary>
        /// 
        /// </summary>
        public RoundRectangle()
        {
            radius = 0;
            bounds = new RectangleF();
            path = InterpolatePath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="radius"></param>
        public RoundRectangle(RectangleF bounds, float radius)
        {
            this.radius = radius;
            this.bounds = bounds;
            path = InterpolatePath();
        }

        /// <summary>
        /// 
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
                path = InterpolatePath();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                path = InterpolatePath();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public  List<PointF> Handles
        {
            get
            {
                return new List<PointF> {
                    bounds.Location,
                    new PointF(bounds.Right,bounds.Top),
                    new PointF(bounds.Right, bounds.Bottom),
                    new PointF(bounds.Left, bounds.Bottom),
                    new PointF((float)(bounds.Left+radius),bounds.Top)
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
        /// <history>
        /// Shkyrockett[Alma Jenks] 29/December/2005Created
        /// </history>
        public static GraphicsPath GetGraphicsPath(RectangleF bounds, float radius)
        {
            //  Start the Path object.
            GraphicsPath GfxPath = new GraphicsPath();
            if (!bounds.IsEmpty)
            {
                if (radius > 0)
                {
                    float diameter = radius * 2;
                    //  prepare the curves.
                    GfxPath.AddArc((bounds.X + (bounds.Width - diameter)), bounds.Y, diameter, diameter, 270, 90);
                    GfxPath.AddArc((bounds.X + (bounds.Width - diameter)), (bounds.Y + (bounds.Height - diameter)), diameter, diameter, 0, 90);
                    GfxPath.AddArc(bounds.X, (bounds.Y + (bounds.Height - diameter)), diameter, diameter, 90, 90);
                    GfxPath.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                    //  Close the path.
                    GfxPath.CloseFigure();
                }
                else
                {
                    GfxPath.AddRectangle(bounds);
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
