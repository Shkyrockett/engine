﻿// <copyright file="BulgeDistortion.cs" >
//    Copyright (c) 2017 Ben Morris. All rights reserved.
// </copyright>
// <author id="benmorris44">Ben Morris</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/benmorris44/EnvelopeDistortion </remarks>

using System;
using System.Collections.Generic;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class BulgeDistortion
        : DistortionBase, IDistortion
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private GeometryPath distortionPath;

        /// <summary>
        /// 
        /// </summary>
        private List<List<Point2D>> distortionPoints;

        /// <summary>
        /// 
        /// </summary>
        private Rectangle2D distortionBounds;

        /// <summary>
        /// 
        /// </summary>
        private Rectangle2D sourceBounds;

        /// <summary>
        /// 
        /// </summary>
        private Point2D upperLeft;

        /// <summary>
        /// 
        /// </summary>
        private Point2D upperRight;

        /// <summary>
        /// 
        /// </summary>
        private Point2D lowerLeft;

        /// <summary>
        /// 
        /// </summary>
        private Point2D lowerRight;

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<float, Point2D[]> boundCache = new Dictionary<float, Point2D[]>();

        #endregion

        #region Constructors

        public BulgeDistortion()
            : base()
        { }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point2D Distort(GeometryPath source, Point2D point)
        {
            if (distortionPath == null)
            {
                BuildDistortion(source);
            }

            var ScaledPoint = point;

            GetBoundedPoints(out Point2D UpperBoundPoint, out Point2D LowerBoundPoint, point);
            var Y = UpperBoundPoint.Y + (((ScaledPoint.Y - sourceBounds.Top) / sourceBounds.Height) * Math.Abs(UpperBoundPoint.Y - LowerBoundPoint.Y));

            return new Point2D(ScaledPoint.X, Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperBoundPoint"></param>
        /// <param name="lowerBoundPoint"></param>
        /// <param name="source"></param>
        private void GetBoundedPoints(out Point2D upperBoundPoint, out Point2D lowerBoundPoint, Point2D source)
        {
            upperBoundPoint = new Point2D();
            lowerBoundPoint = new Point2D();

            //if (boundCache.ContainsKey(source.X))
            //{
            //    upperBoundPoint = boundCache[source.X][0];
            //    lowerBoundPoint = boundCache[source.X][1];
            //    return;
            //}

            //var Path = new GeometryPath();
            //var UpperX = source.X * (sourceBounds.Width / (upperRight.X - upperLeft.X));
            //var LowerX = source.X * (sourceBounds.Width / (lowerRight.X - lowerLeft.X));
            //Path.AddPolygon(new Point2D[]{
            //    new Point2D(distortionBounds.Left,distortionBounds.Bottom),
            //    new Point2D(distortionBounds.Left, distortionBounds.Top),
            //    new Point2D(UpperX,  distortionBounds.Top),
            //    new Point2D(LowerX, distortionBounds.Bottom),
            //});
            //Path.CloseFigure();
            ////var ClippingPath = ClipperUtility.ConvertToClipperPolygons(Path);
            //Path.Dispose();

            ////var ClippedPath = ClipperUtility.Clip(ClippingPath, distortionPoints);
            //if (Math.Abs(source.X - sourceBounds.Left) < .1 || Math.Abs(source.X - sourceBounds.Right) < .1)
            //{
            //    upperBoundPoint = new Point2D(sourceBounds.Left, sourceBounds.Top);
            //    lowerBoundPoint = new Point2D(sourceBounds.Left, sourceBounds.Bottom);
            //}
            //else
            //{
            //    var Points = ClippedPath.PathPoints;
            //    var QuickBounded = Points.Where(p => Math.Abs(p.X - LowerX) < .01);
            //    if (QuickBounded.Any())
            //    {
            //        upperBoundPoint = Points.Where(p => Math.Abs(p.X - LowerX) < .01).OrderBy(p => p.Y).First();
            //        lowerBoundPoint = Points.Where(p => Math.Abs(p.X - LowerX) < .01).OrderByDescending(p => p.Y).First();
            //        boundCache.Add(source.X, new Point2D[] { upperBoundPoint, lowerBoundPoint });
            //    }
            //    else
            //    {
            //        var RightMostPoints = Points.OrderByDescending(p => p.X).Take(2).ToList();
            //        upperBoundPoint = RightMostPoints.OrderBy(p => p.Y).First();
            //        lowerBoundPoint = RightMostPoints.OrderByDescending(p => p.Y).First();
            //    }
            //    ClippedPath.Dispose();
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        private void BuildDistortion(GeometryPath source)
        {
            //sourceBounds = source.GetBounds();

            //distortionPath = new GraphicsPath(source.FillMode);

            //lowerLeft = new Point2D(sourceBounds.Left, sourceBounds.Bottom);
            //lowerRight = new Point2D(sourceBounds.Right, sourceBounds.Bottom);
            //upperLeft = new Point2D(sourceBounds.Left, sourceBounds.Top);
            //upperRight = new Point2D(sourceBounds.Right, sourceBounds.Top);

            //distortionPath.AddLine(lowerLeft, upperLeft);

            //distortionPath.AddBezier(
            //    upperLeft,
            //    new Point2D(sourceBounds.Left, sourceBounds.Top + ((sourceBounds.Height * (float)Intensity)) * -1),
            //    new Point2D(sourceBounds.Right, sourceBounds.Top + ((sourceBounds.Height * (float)Intensity)) * -1),
            //    upperRight);

            //distortionPath.AddLine(upperRight, lowerRight);

            //distortionPath.AddBezier(
            //    lowerRight,
            //    new Point2D(sourceBounds.Right, sourceBounds.Bottom + (sourceBounds.Height * (float)Intensity)),
            //    new Point2D(sourceBounds.Left, sourceBounds.Bottom + (sourceBounds.Height * (float)Intensity)),
            //    lowerLeft);

            //distortionPath.Flatten();
            //distortionPoints = ClipperUtility.ConvertToClipperPolygons(distortionPath);
            //distortionBounds = distortionPath.GetBounds();
        }
    }
}
