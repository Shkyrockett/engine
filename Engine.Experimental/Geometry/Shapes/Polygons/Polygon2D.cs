﻿namespace Engine;

///// <summary>
///// A complex polygon is represented by many contours (i.e. simple polygons).
///// </summary>
//public class Polygon2D
//{
//    /// <summary>
//    /// The contours.
//    /// </summary>
//    public List<PolygonContour2D> contours = new List<PolygonContour2D>();

//    /// <summary>
//    /// The bounds.
//    /// </summary>
//    public Rectangle2D bounds;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="Polygon2D"/> class.
//    /// </summary>
//    public Polygon2D()
//    {
//        contours = new List<PolygonContour2D>();
//    }

//    /// <summary>
//    /// Gets the vertices count.
//    /// </summary>
//    public int VerticesCount
//    {
//        get
//        {
//            var verticesCount = 0;
//            foreach (var c in contours)
//            {
//                verticesCount += c.Points.Count;
//            }

//            return verticesCount;
//        }
//    }

//    /// <summary>
//    /// Gets the bounds.
//    /// </summary>
//    public Rectangle2D Bounds
//    {
//        get
//        {
//            if (bounds is not null)
//            {
//                return bounds;
//            }

//            Rectangle2D bb = null;
//            foreach (var c in contours)
//            {
//                var cBB = c.Bounds;
//                bb = bb is null ? cBB : bb.Union(cBB);
//            }

//            bounds = bb;
//            return bounds;
//        }
//    }

//    /// <summary>
//    /// Add.
//    /// </summary>
//    /// <param name="c">The c.</param>
//    public void Add(PolygonContour2D c)
//        => contours.Add(c);

//    /// <summary>
//    /// Clone.
//    /// </summary>
//    /// <returns>The <see cref="Polygon2D"/>.</returns>
//    public Polygon2D Clone()
//    {
//        var poly = new Polygon2D();
//        foreach (var cont in contours)
//        {
//            var c = new PolygonContour2D();
//            foreach (var p in cont.Points)
//            {
//                c.Add(new Point2D(p));
//            }

//            poly.Add(c);

//        }
//        return poly;
//    }
//}
