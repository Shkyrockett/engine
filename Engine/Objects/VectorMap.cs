using Engine.Geometry;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorMap
        : ICollection<Shape>
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
        public int Count
        {
            get { return shapes.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<Shape> this[Rectangle2D area]
        {
            get
            {
                List<Shape> list = new List<Shape>();
                foreach (Shape shape in Shapes)
                {
                    if (shape.Bounds.IntersectsWith(area)|| shape.Bounds.Contains(area))
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
        public List<Shape> this[Point2D point]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(Shape item)
        {
            shapes.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Shape item)
        {
            return shapes.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            shapes.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Shape item)
        {
            return shapes.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Shape[] array, int arrayIndex)
        {
            shapes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Shape> GetEnumerator()
        {
            return shapes.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return shapes.GetEnumerator();
        }
    }
}
