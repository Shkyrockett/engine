using System.Collections.Generic;

namespace Engine._Preview
{
    /// <summary>
    /// A complex polygon is represented by many contours (i.e. simple polygons).
    /// </summary>
    public class Polygon2D
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Contour> contours = new List<Contour>();

        /// <summary>
        /// 
        /// </summary>
        public Rectangle2D bounds;

        /// <summary>
        /// 
        /// </summary>
        public Polygon2D()
            => contours = new List<Contour>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int VerticesCount
        {
            get
            {
                int verticesCount = 0;
                foreach (var c in contours)
                    verticesCount += c.Points.Count;

                return verticesCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle2D Bounds
        {
            get
            {
                if (bounds != null)
                    return bounds;

                Rectangle2D bb = null;
                foreach (var c in contours)
                {
                    var cBB = c.Bounds;
                    if (bb == null)
                        bb = cBB;
                    else
                        bb = bb.Union(cBB);
                }

                bounds = bb;
                return bounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void Add(Contour c)
            => contours.Add(c);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Polygon Clone()
        {
            Polygon poly = new Polygon();
            foreach (var cont in contours)
            {
                var c = new Contour();
                foreach (var p in cont.Points)
                    c.Add(p.Clone());

                poly.Add(c);

            }
            return poly;
        }
    }
}
