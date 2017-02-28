// <copyright file="DistortionService.cs" >
//    Copyright (c) 2017 Ben Morris. All rights reserved.
// </copyright>
// <author id="benmorris44">Ben Morris</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/benmorris44/EnvelopeDistortion </remarks>

using System.Collections.Generic;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class DistortionService
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IDistortion distortion;

        /// <summary>
        /// 
        /// </summary>
        private readonly PathContour source;

        /// <summary>
        /// 
        /// </summary>
        private readonly float flatness;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of a Distortion Service
        /// </summary>
        /// <param name="distortion">The distortion to be performed</param>
        /// <param name="source">The graphics path to be distorted</param>
        /// <param name="flatness">The precision of the flattening operation (smaller = more points = slower)</param>
        public DistortionService(IDistortion distortion, PathContour source, float flatness = .2f)
        {
            this.distortion = distortion;
            this.source = source;
            this.flatness = flatness;
            //this.source.Flatten(null, flatness);
        }

        #endregion

        /// <summary>
        /// returns a newly created graphics path with points distorted
        /// </summary>
        /// <returns>The distorted Graphics Path</returns>
        public PathContour ApplyDistortion()
        {
            //var it = new GraphicsPathIterator(source);
            //it.Rewind();
            //var Gp = new GeometryPath(FillMode.Winding);
            //var ReturnPath = new GeometryPath(FillMode.Winding);

            //for (var i = 0; i < it.SubpathCount; i++)
            //{
            //    it.NextSubpath(Gp, out bool result);

            //    InjectPrecisionPoints(Gp);

            //    ReturnPath.AddPolygon(Gp.PathPoints.Select(p => distortion.Distort(source, p)).ToArray());
            //    Gp.Reset();
            //}

            //it.Dispose();
            //Gp.Dispose();

            return null;// ReturnPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gp"></param>
        private void InjectPrecisionPoints(PathContour gp)
        {
            var InsertDictionary = new Dictionary<int, Point2D[]>();
            //inject points on vertical and horizontal runs to increase precision
            //for (var j = 0; j < gp.PointCount; j++)
            //{
            //    Point2D CurrentPoint;
            //    Point2D NextPoint;
            //    if (j != gp.PointCount - 1)
            //    {
            //        CurrentPoint = gp.PathPoints[j];
            //        NextPoint = gp.PathPoints[j + 1];
            //    }
            //    else
            //    {
            //        CurrentPoint = gp.PathPoints[j];
            //        NextPoint = gp.PathPoints[0];
            //    }
            //    if (Math.Abs(CurrentPoint.X - NextPoint.X) < .001 && Math.Abs(CurrentPoint.Y - NextPoint.Y) > flatness)
            //    {
            //        var Distance = CurrentPoint.Y - NextPoint.Y;
            //        var Items = Enumerable.Range(1, Convert.ToInt32(Math.Floor(Math.Abs(Distance) / flatness)))
            //                               .Select(p => new Point2D(CurrentPoint.X, Distance < 0 ? (CurrentPoint.Y + (flatness * p)) : (CurrentPoint.Y - (flatness * p))))
            //                               .ToArray();
            //        InsertDictionary.Add(j + 1, Items);
            //    }
            //    if (Math.Abs(CurrentPoint.Y - NextPoint.Y) < .001 && Math.Abs(CurrentPoint.X - NextPoint.X) > flatness)
            //    {
            //        var Distance = CurrentPoint.X - NextPoint.X;
            //        var Items = Enumerable.Range(1, Convert.ToInt32(Math.Floor(Math.Abs(Distance) / flatness)))
            //                               .Select(p => new Point2D(Distance < 0 ? (CurrentPoint.X + (flatness * p)) : (CurrentPoint.X - (flatness * p)), CurrentPoint.Y))
            //                               .ToArray();

            //        InsertDictionary.Add(j + 1, Items);
            //    }
            //}

            //if (InsertDictionary.Count > 0)
            //{
            //    var PointArray = gp.PathPoints.ToList();
            //    InsertDictionary.OrderByDescending(p => p.Key).ToList().ForEach(p => PointArray.InsertRange(p.Key, p.Value));

            //    gp.Reset();
            //    gp.AddPolygon(PointArray.ToArray());

            //    InsertDictionary.Clear();
            //}
        }

        /// <summary>
        /// A debugging method - will return the corresponding distorted point for a given source
        /// </summary>
        /// <param name="point">The source point</param>
        /// <returns>The distorted point location</returns>
        public Point2D DistortPoint(Point2D point)
            => distortion.Distort(source, point);
    }
}
