using Engine.Geometry;
using System.Collections.Generic;
using System.Drawing;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorMap
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Shape> shapes;

        /// <summary>
        /// 
        /// </summary>
        public VectorMap()
            : this(new List<Shape>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapes"></param>
        public VectorMap(List<Shape> shapes)
        {
            this.shapes = shapes;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Shape> Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<Shape> this[Rectangle area]
        {
            get
            {
                List<Shape> list = new List<Shape>();
                foreach (Shape shape in Shapes)
                {
                    if (shape.Bounds.IntersectsWith(area))
                    {
                        list.Add(shape);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public List<Shape> this[Point point]
        {
            get
            {
                List<Shape> list = new List<Shape>();
                foreach (Shape shape in Shapes)
                {
                    if (shape.Bounds.Contains(point))
                    {
                        if (shape.HitTest(point))
                        {
                            list.Add(shape);
                        }
                    }
                }

                return list;
            }
        }
    }
}
