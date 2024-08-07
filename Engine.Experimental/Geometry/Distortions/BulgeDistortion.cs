﻿// <copyright file="BulgeDistortion.cs" >
// Copyright © 2017 Ben Morris. All rights reserved.
// </copyright>
// <author id="benmorris44">Ben Morris</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/benmorris44/EnvelopeDistortion </remarks>

using static System.Math;

namespace Engine;

/// <summary>
/// The bulge distortion class.
/// </summary>
public class BulgeDistortion
    : DistortionBase, IDistortion
{
    #region Fields
    /// <summary>
    /// The distortion path.
    /// </summary>
    public PolycurveContour2D distortionPath;

    /// <summary>
    /// The distortion bounds.
    /// </summary>
    private Rectangle2D distortionBounds;

    /// <summary>
    /// The source bounds.
    /// </summary>
    private Rectangle2D sourceBounds;

    /// <summary>
    /// The upper left.
    /// </summary>
    private Point2D upperLeft;

    /// <summary>
    /// The upper right.
    /// </summary>
    private Point2D upperRight;

    /// <summary>
    /// The lower left.
    /// </summary>
    private Point2D lowerLeft;

    /// <summary>
    /// The lower right.
    /// </summary>
    private Point2D lowerRight;

    /// <summary>
    /// The bound cache (readonly). Value: new Dictionary&lt;double, Point2D[]&gt;().
    /// </summary>
    private readonly Dictionary<double, Point2D[]> boundCache = [];
    #endregion Fields

    /// <summary>
    /// The distort.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public Point2D Distort(PolycurveContour2D source, Point2D point)
    {
        if (distortionPath is null)
        {
            BuildDistortion(source);
        }

        var ScaledPoint = point;

        GetBoundedPoints(out var UpperBoundPoint, out var LowerBoundPoint, point);
        var Y = UpperBoundPoint.Y + ((ScaledPoint.Y - sourceBounds.Top) / sourceBounds.Height * Math.Abs(UpperBoundPoint.Y - LowerBoundPoint.Y));

        return new Point2D(ScaledPoint.X, Y);
    }

    /// <summary>
    /// Get the bounded points.
    /// </summary>
    /// <param name="upperBoundPoint">The upperBoundPoint.</param>
    /// <param name="lowerBoundPoint">The lowerBoundPoint.</param>
    /// <param name="source">The source.</param>
    private void GetBoundedPoints(out Point2D upperBoundPoint, out Point2D lowerBoundPoint, Point2D source)
    {
        upperBoundPoint = new Point2D();
        lowerBoundPoint = new Point2D();

        if (boundCache.ContainsKey(source.X))
        {
            upperBoundPoint = boundCache[source.X][0];
            lowerBoundPoint = boundCache[source.X][1];
            return;
        }

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
        //var ClippingPath = ClipperUtility.ConvertToClipperPolygons(Path);
        //Path.Dispose();

        //var ClippedPath = ClipperUtility.Clip(ClippingPath, distortionPoints);
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
    /// Build the distortion.
    /// </summary>
    /// <param name="source">The source.</param>
    private void BuildDistortion(PolycurveContour2D source)
    {
        sourceBounds = source.Bounds();

        lowerLeft = new Point2D(sourceBounds.Left, sourceBounds.Bottom);
        lowerRight = new Point2D(sourceBounds.Right, sourceBounds.Bottom);
        upperLeft = new Point2D(sourceBounds.Left, sourceBounds.Top);
        upperRight = new Point2D(sourceBounds.Right, sourceBounds.Top);

        distortionPath = new PolycurveContour2D(lowerLeft);// source.FillMode);
        distortionPath.AddLineSegment(upperLeft);

        distortionPath.AddCubicBezier(
            new Point2D(sourceBounds.Left, sourceBounds.Top + (sourceBounds.Height * (float)Intensity * -1)),
            new Point2D(sourceBounds.Right, sourceBounds.Top + (sourceBounds.Height * (float)Intensity * -1)),
            upperRight);

        distortionPath.AddLineSegment(lowerRight);

        distortionPath.AddCubicBezier(
            new Point2D(sourceBounds.Right, sourceBounds.Bottom + (sourceBounds.Height * (float)Intensity)),
            new Point2D(sourceBounds.Left, sourceBounds.Bottom + (sourceBounds.Height * (float)Intensity)),
            lowerLeft);

        //distortionPath.Flatten();
        //distortionPoints = ClipperUtility.ConvertToClipperPolygons(distortionPath);
        distortionBounds = distortionPath.Bounds();
    }

    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="graphics"></param>
    ///// <param name="width"></param>
    ///// <param name="height"></param>
    ///// <param name="phrase"></param>
    ///// <remarks> http://stackoverflow.com/a/9019432 </remarks>
    //internal static void DrawPhrase(this Contour graphics, int width, int height, string phrase)
    //{
    //    graphics.FillRectangle(Brushes.White, 0, 0, width, height);

    //    var gp = new Contour();
    //    gp.AddString(phrase, FontFamily.GenericMonospace, (int)FontStyle.Bold, 33f, new Point(0, 0), StringFormat.GenericTypographic);

    //    using (var gpp = Deform(gp, width, height))
    //    {
    //        var bounds = gpp.GetBounds();
    //        var matrix = new Matrix();
    //        var x = (width - bounds.Width) / 2 - bounds.Left;
    //        var y = (height - bounds.Height) / 2 - bounds.Top;
    //        matrix.Translate(x, y);
    //        gpp.Transform(matrix);
    //        graphics.FillPath(Brushes.Black, gpp);
    //    }

    //    graphics.Flush();
    //}

    /// <summary>
    /// The deform.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>The <see cref="PolygonContour2D"/>.</returns>
    /// <remarks> <para>http://stackoverflow.com/a/9019432</para> </remarks>
    internal static PolygonContour2D Deform(PolygonContour2D path, int width, int height)
    {
        var rng = new Random();
        const int WarpFactor = 4;
        var xAmp = WarpFactor * width / 300d;
        var yAmp = WarpFactor * height / 50d;
        var xFreq = Tau / width;
        var yFreq = Tau / height;
        var deformed = new Point2D[path.Count];
        var xSeed = rng.NextDouble() * Tau;
        var ySeed = rng.NextDouble() * Tau;
        var i = 0;
        foreach (var original in path.Points)
        {
            var val = (xFreq * original.X) + (yFreq * original.Y);
            var xOffset = (int)(xAmp * Math.Sin(val + xSeed));
            var yOffset = (int)(yAmp * Math.Sin(val + ySeed));
            deformed[i++] = new Point2D(original.X + xOffset, original.Y + yOffset);
        }

        return new PolygonContour2D(deformed);
    }
}
