using Engine.Geometry;
using System;
using System.ComponentModel;

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
        public virtual Rectangle2D Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool HitTest(Point2D point)
        {
            throw new NotImplementedException();
        }
    }
}
