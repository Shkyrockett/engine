using Engine.Geometry;
using Engine.Imaging;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorMap
        : ICollection<GraphicItem>
    {
        /// <summary>
        /// 
        /// </summary>
        private List<GraphicItem> shapes;

        /// <summary>
        /// 
        /// </summary>
        public VectorMap()
            : this(new List<GraphicItem>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapes"></param>
        public VectorMap(List<GraphicItem> shapes)
        {
            this.shapes = shapes;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<GraphicItem> Shapes
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
        public List<GraphicItem> this[Rectangle2D area]
        {
            get
            {
                List<GraphicItem> list = new List<GraphicItem>();
                foreach (GraphicItem shape in Shapes)
                {
                    if (shape.Bounds.IntersectsWith(area) || shape.Bounds.Contains(area))
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
        public List<GraphicItem> this[Point2D point]
        {
            get
            {
                List<GraphicItem> list = new List<GraphicItem>();
                foreach (GraphicItem shape in Shapes)
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
        public void Add(GraphicItem item)
        {
            shapes.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public void Add(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            shapes.Add(new GraphicItem(item, style, metadata));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public void Add(Shape item, ShapeStyle style = null, Metadata metadata = null)
        {
            if (style == null) style = ShapeStyle.DefaultStyle;
            shapes.Add(new GraphicItem(item, style, metadata));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(GraphicItem item)
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
        public bool Contains(GraphicItem item)
        {
            return shapes.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(GraphicItem[] array, int arrayIndex)
        {
            shapes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GraphicItem> GetEnumerator()
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
