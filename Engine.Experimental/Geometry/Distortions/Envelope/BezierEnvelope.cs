using System;

namespace Engine
{
    // //!/usr/bin/env python
    // '''
    // Bezier Envelope extension for Inkscape
    // Copyright (C) 2009 Gerrit Karius
    //
    // This program is free software; you can redistribute it and/or
    // modify it under the terms of the GNU General Public License
    // as published by the Free Software Foundation; either version 2
    // of the License, or (at your option) any later version.
    //
    // This program is distributed in the hope that it will be useful,
    // but WITHOUT ANY WARRANTY; without even the implied warranty of
    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    // GNU General Public License for more details.
    //
    // You should have received a copy of the GNU General Public License
    // along with this program; if not, write to the Free Software
    // Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    //
    //
    // About the Bezier Envelope extension:
    //
    // This extension implements Bezier enveloping.
    // It takes an arbitrary path (the "letter") and a 4-sided path (the "envelope") as input.
    // The envelope must be 4 segments long. Unless the letter is to be rotated or flipped,
    // the envelope should begin at the upper left corner and be drawn clockwise.
    // The extension then attempts to squeeze the letter into the envelope
    // by rearranging all anchor and handle points of the letter's path.
    //
    // In order to do this, the bounding box of the letter is used.
    // All anchor and bezier handle points get new x and y coordinates between 0% and 100%
    // according to their place inside the bounding box.
    // The 4 sides of the envelope are then interpreted as deformed axes.
    // Points at 0% or 100% could be placed along these axes, but because most points
    // are somewhere inside the bounding box, some tweening of the axes must be done.
    //
    // The function mapPointsToMorph does the tweening.
    // Say, some point is at x=30%, y=40%.
    // For the tweening, the function tweenCubic first calculates a straight tween
    // of the y axis at the x percentage of 30%.
    // This tween axis now floats somewhere between the y axis keys at the x percentage,
    // but is not necessarily inside the envelope, because the x axes are not straight.
    // Now, the end points on the two x axes at 30% are calculated. The function match()
    // takes these points and calculates a "stretch" transform which maps the two anchor
    // points of the y axis tween to the two points on the x axes by rotating the tween and
    // stretching it along its endpoints. This transform is then applied to the handle points,
    // to get the entire tweened y axis to its x tweened position.
    // Last, the point at the y percentage 40% of this y axis tween is calculated.
    // That is the final point of the enveloped letter.
    //
    // Finally, after all of the letter's points have been recalculated in this manner,
    // the resulting path is taken and replaces the letter's original path.
    //
    // ToDo:
    // * Some points of the letter appear outside the envelope, apparently because the bounding box
    // calculated by simpletransform.py is only a rough estimate. -> Calculate the real bbox,
    // perhaps uSing other existing extensions, or py2geom.
    // * Currently, both letter and envelope must be paths to work.
    // -> Arbitrary other shapes like circles and rectangles should be interpreted as paths.
    // * It should be possible to select several letters, and squeeze them into one envelope as a group.
    // * It should be possible to insert a clone of the letter, instead of replacing it.
    // * Bug //241565 prevented the matrix parser constructors from working. This extension can
    // only be used with the fixed version of simpletransform.py. As a workaround, two matrix constructors
    // were copied into this file.
    // * This program was originally written in Java. Maybe for some code, Python shortcuts can be used.
    //
    // I hope the comments are not too verbose. Enjoy!
    //
    // ''';
    // import inkex, os, simplepath, cubicsuperpath, simpletransform,sys;
    // from ffgeom import *;

    /// <summary>
    /// The Bézier envelope class.
    /// </summary>
    internal class BezierEnvelope
    {
        ///// <summary>
        ///// The effect.
        ///// </summary>
        ///// <param name="shape">The shape.</param>
        ///// <param name="envelope">The envelope.</param>
        ///// <exception cref="Exception">Two paths must be selected. The 1st is the letter, the 2nd is the envelope and must have 4 sides.</exception>
        ///// <exception cref="Exception">No axes found on envelope.</exception>
        ///// <exception cref="Exception">The envelope path has less than 4 segments.</exception>
        //public static void Effect(Polycurve2D shape, CubicBezierQuad envelope)
        //{
        //    if (shape is null || envelope is null)
        //    {
        //        throw new Exception("Two paths must be selected. The 1st is the letter, the 2nd is the envelope and must have 4 sides.");
        //    }

        //    var axes = ExtractMorphAxes(shape);
        //    if (axes is null)
        //    {
        //        throw new Exception("No axes found on envelope.");
        //    }
        //    var axisCount = axes.Length;
        //    if (axisCount < 4)
        //    {
        //        throw new Exception("The envelope path has less than 4 segments.");
        //    }
        //    for (var i = 0; i < 4; i++)
        //    {
        //        if (axes[i] is null)
        //        {
        //            throw new Exception($"axes[{i}] is Null.");
        //        }
        //    }
        //    // morph the enveloped element according to the axes
        //    MorphElement(shape, envelope, axes);
        //}

        ///// <summary>
        ///// The morph element.
        ///// </summary>
        ///// <param name="path">The path.</param>
        ///// <param name="envelopeElement">The envelopeElement.</param>
        ///// <param name="axes">The axes.</param>
        //public static void MorphElement(PolycurveContour2D path, CubicBezierQuad envelopeElement, Point2D[] axes)
        //{
        //    var morphedPath = MorphPath(path, envelopeElement, axes);
        //    //letterElement.set("d", simplepath.formatPath(morphedPath));
        //}

        ///// <summary>
        ///// Morphs a path into a new path, according to cubic curved bounding axes.
        ///// </summary>
        ///// <param name="path">The path.</param>
        ///// <param name="envelopeElement"></param>
        ///// <param name="axes">The axes.</param>
        ///// <returns>The <see cref="Polycurve2D"/>.</returns>
        //public static Polycurve2D MorphPath(PolycurveContour2D path, CubicBezierQuad envelopeElement, Point2D[] axes)
        //{
        //    var bounds = path.Bounds;
        //    var newPath = new Polycurve2D();
        //    var current = new Point2D(0.0, 0.0);
        //    var start = new Point2D(0.0, 0.0);

        //    foreach (var segmentType in path)
        //    {
        //        //var points = segmentType.;
        //        if (segmentType is PointSegment2D)
        //        {
        //            start = points[0];
        //        }
        //        segmentType = convertSegmentToCubic(current, segmentType, points, start);
        //        percentages = [0.0] * len(points);
        //        morphed = [0.0] * len(points);
        //        numPts = getNumPts(segmentType);
        //        normalizePoints(bounds, points, percentages, numPts);
        //        mapPointsToMorph(axes, percentages, morphed, numPts);
        //        addSegment(newPath, segmentType, morphed);
        //        if (len(points) >= 2)
        //        {
        //            current[0] = points[len(points) - 2];
        //            current[1] = points[len(points) - 1];
        //        }
        //    }
        //    return newPath;
        //}

        ///// <summary>
        ///// Get the num pts.
        ///// </summary>
        ///// <param name="curveSegment">The curveSegment.</param>
        ///// <returns>The <see cref="int"/>.</returns>
        //private static int GetNumPts(CurveSegment2D curveSegment)
        //{
        //    switch (curveSegment)
        //    {
        //        case PointSegment2D p:
        //            return 1;
        //        case LineCurveSegment2D l:
        //            return 1;
        //        case QuadraticBezierSegment2D q:
        //            return 2;
        //        case CubicBezierSegment2D c:
        //            return 3;
        //        case EllipticalArcSegment2D a:
        //            return 0;
        //        default:
        //            return -1;
        //    }
        //}

        ///// <summary>
        ///// add the segment.
        ///// </summary>
        ///// <param name="path">The path.</param>
        ///// <param name="segmentType">The segmentType.</param>
        ///// <param name="points">The points.</param>
        //private static void AddSegment(PolyBezier2D path, SegmentTypes segmentType, Point2D[] points)
        //{
        //    path.Add(segmentType, points);
        //}

        ///// <summary>
        ///// Converts visible path segments (Z,L,Q) into absolute cubic segments (C).
        ///// </summary>
        ///// <param name="current">The current.</param>
        ///// <returns>The <see cref="CurveSegment2D"/>.</returns>
        ///// <exception cref="Exception"></exception>
        //private static CurveSegment2D ConvertSegmentToCubic(CurveSegment2D current)
        //{
        //    switch (current)
        //    {
        //        case CubicBezierSegment2D c:
        //            return c;
        //        case LineCurveSegment2D l:
        //            return new CubicBezierSegment2D(
        //                l.Previous,
        //                Maths.Lerp(l.Start.Value.X, l.Start.Value.Y, l.End.Value.X, l.End.Value.Y, 1d / 3d),
        //                Maths.Lerp(l.Start.Value.X, l.Start.Value.Y, l.End.Value.X, l.End.Value.Y, 2d / 3d),
        //                l.End.Value);
        //        case QuadraticBezierSegment2D q:
        //            var cubic = Conversions.QuadraticBezierToCubicBezierList(q.Start.Value, q.Handle.Value, q.End.Value);
        //            return new CubicBezierSegment2D(q.Previous, cubic[1], cubic[2], q.End.Value);
        //        default:
        //            throw new Exception($"unsupported segment type: {current}");
        //    }
        //}

        /// <summary>
        /// Normalizes the points of a path segment, so that they are expressed as percentage coordinates relative to the bounding box axes of the total shape.
        /// </summary>
        /// <param name="bounds">The bounding box of the shape.</param>
        /// <param name="points">The points of the segment.</param>
        /// <returns>The returned points in normalized percentage form.</returns>
        internal static Point2D[] NormalizePoints(Rectangle2D bounds, Point2D[] points)
        {
            var (xmin, ymin, xdiff, ydiff) = bounds;
            var percentages = new Point2D[points.Length];
            var i = 0;
            foreach (var point in points)
            {
                percentages[i++] = ((point.X - xmin) / xdiff, (point.Y - ymin) / ydiff);
            }
            return percentages;
        }

        ///// <summary>
        ///// Extracts 4 axes from a path. It is assumed that the path starts with a move, followed by 4 cubic paths. The extraction reverses the last 2 axes, so that they run in parallel with the first 2.
        ///// </summary>
        ///// <param name="path">The path that is formed by the axes.</param>
        ///// <returns>The definition points of the 4 cubic path axes as float arrays, bundled in another array.</returns>
        //private static Point2D[] ExtractMorphAxes(Polycurve2D path)
        //{
        //    var points = new Point2D[4];
        //    var current = new Point2D(0.0, 0.0);
        //    var start = new Point2D(0.0, 0.0);
        //    // the curved axis definitions go in here
        //    var axes = new Point2D[2];
        //    var i = 0;

        //    for (cmd, params in path)
        //    {
        //        points = params;
        //        cmd = convertSegmentToCubic(current, cmd, points, start);

        //        if (cmd == "M")
        //        {
        //            current[0] = points[0];
        //            current[1] = points[1];
        //            start[0] = points[0];
        //            start[1] = points[1];
        //        }

        //        else if (cmd == "C")
        //        {
        //            // 1st cubic becomes x axis 0
        //            // 2nd cubic becomes y axis 1
        //            // 3rd cubic becomes x axis 2 and is reversed
        //            // 4th cubic becomes y axis 3 and is reversed
        //            if (i % 2 == 0)
        //            {
        //                index = i;
        //            }
        //            else
        //            {
        //                index = 4 - i;
        //            }
        //            if (i < 2)
        //            {
        //                // axes 1 and 2
        //                axes[index] = [current[0], current[1], points[0], points[1], points[2], points[3], points[4], points[5]];
        //            }
        //            else if (i < 4)
        //            {
        //                // axes 3 and 4
        //                axes[index] = [points[4], points[5], points[2], points[3], points[0], points[1], current[0], current[1]];
        //            }
        //            else
        //            {
        //                // more than 4 axes - hopefully it was an unnecessary trailing Z
        //                { };
        //            }
        //            current[0] = points[4];
        //            current[1] = points[5];
        //            i = i + 1;
        //        }
        //        else if (cmd == "Z")
        //        {
        //            // do nothing
        //            { };
        //        }
        //        else
        //        {
        //            throw new Exception($"Unsupported segment type: {cmd}");
        //        }
        //    }

        //    return axes;
        //}

        ///// <summary>
        ///// Projects points in percentage coordinates into a morphed coordinate system that is framed by 2 x cubic curves (along the x axis) and 2 y cubic curves (along the y axis).
        ///// </summary>
        ///// <param name="axes">The x and y axes of the envelope.</param>
        ///// <param name="percentage">The current segment of the letter in normalized percentage form.</param>
        ///// <param name="morphed">The array to hold the returned morphed path.</param>
        ///// <param name="numPts">The number of points to be transformed.</param>
        //private static void MapPointsToMorph(Point2D[] axes, double[] percentage, Point2D[] morphed, int numPts)
        //{
        //    // rename the axes for legibility
        //    var yCubic0 = axes[0].Y;
        //    var yCubic1 = axes[1].Y;
        //    var xCubic0 = axes[0].X;
        //    var xCubic1 = axes[1].X;
        //    // morph each point
        //    for (var i = 0; i < numPts; i++)
        //    {
        //        var x = i * 2;
        //        var y = i * 2 + 1;
        //        // tween between the morphed y axes according to the x percentage
        //        var tweenedY = TweenCubic(yCubic0, yCubic1, percentage[x]);
        //        // get 2 points on the morphed x axes
        //        var xSpot0 = PointOnCubic(xCubic0, percentage[x]);
        //        var xSpot1 = PointOnCubic(xCubic1, percentage[x]);
        //        // create a transform that stretches the y axis tween between these 2 points
        //        var yAnchor0 = [tweenedY[0], tweenedY[1]];
        //        var yAnchor1 = [tweenedY[6], tweenedY[7]];
        //        var xTransform = Match(yAnchor0, yAnchor1, xSpot0, xSpot1);
        //        // map the y axis tween to the 2 points by applying the stretch transform
        //        for (var j = 0; j < 4; j++)
        //        {
        //            var x2 = j * 2;
        //            var y2 = j * 2 + 1;
        //            var pointOnY = [tweenedY[x2], tweenedY[y2]];
        //            simpletransform.applyTransformToPoint(xTransform, pointOnY);
        //            tweenedY[x2] = pointOnY[0];
        //            tweenedY[y2] = pointOnY[1];
        //        }
        //        // get the point on the tweened and transformed y axis according to the y percentage
        //        var morphedPoint = PointOnCubic(tweenedY, percentage[y]);
        //        morphed[i].X = morphedPoint.X;
        //        morphed[i].Y = morphedPoint.Y;
        //    }
        //}

        /// <summary>
        /// Calculates the point on a cubic bezier curve at the given percentage.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        internal static Point2D PointOnCubic(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            var _1t = 1 - t;
            var _1t2 = _1t * _1t;
            var _1t3 = _1t2 * _1t;

            return new Point2D(
                (a.X * _1t3) + (3d * b.X * _1t2 * t) + (3d * c.X * _1t * t2) + (d.X * t3),
                (a.Y * _1t3) + (3d * b.Y * _1t2 * t) + (3d * c.Y * _1t * t2) + (d.Y * t3));
        }

        /// <summary>
        /// Tweens 2 Bézier curves in a straightforward way,
        /// i.e. each of the points on the curve is tweened along a straight line
        /// between the respective point on key1 and key2.
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <param name="key2">The key2.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        internal static Point2D[] TweenCubic(Point2D[] key1, Point2D[] key2, double percentage)
        {
            var tween = new Point2D[key1.Length];
            for (var i = 0; i < key1.Length; i++)
            {
                tween[i] = key1[i] + (percentage * (key2[i] - key1[i]));
            }
            return tween;
        }

        ///// <summary>
        ///// Calculates a transform that matches 2 points to 2 anchors
        ///// by rotating and scaling (up or down) along the axis that is formed by
        ///// a line between the two points.
        ///// </summary>
        ///// <param name="p1">The p1.</param>
        ///// <param name="p2">The p2.</param>
        ///// <param name="a1">The a1.</param>
        ///// <param name="a2">The a2.</param>
        //private static void Match(Point2D p1, Point2D p2, Point2D a1, Point2D a2)
        //{
        //    // distances
        //    var dp = (X: p2.X - p1.X, Y: p2.Y - p1.Y);
        //    var da = (X: a2.X - a1.X, Y: a2.Y - a1.Y);
        //    // angles
        //    var angle_p = Math.Atan2(dp.X, dp.Y);
        //    var angle_a = Math.Atan2(da.X, da.Y);
        //    // radians
        //    var rp = Math.Sqrt(dp.X * dp.X + dp.Y * dp.Y);
        //    var ra = Math.Sqrt(da.X * da.X + da.Y * da.Y);
        //    // scale
        //    var scale = ra / rp;
        //    // transforms in the order they are applied
        //    var t1 = simpletransform.parseTransform("translate(%f,%f)" % (-p1.X, -p1.Y));
        //    // t2 = simpletransform.parseTransform( "rotate(%f)"%(-angle_p) )
        //    // t3 = simpletransform.parseTransform( "scale(%f,%f)"%(scale,scale) )
        //    // t4 = simpletransform.parseTransform( "rotate(%f)"%angle_a )
        //    var t2 = RotateTransform(-angle_p);
        //    var t3 = scaleTransform(scale, scale);
        //    var t4 = RotateTransform(angle_a);
        //    var t5 = simpletransform.parseTransform("translate(%f,%f)" % (a1.X, a1.Y));
        //    // transforms in the order they are multiplied
        //    var t = t5;
        //    t = simpletransform.composeTransform(t, t4);
        //    t = simpletransform.composeTransform(t, t3);
        //    t = simpletransform.composeTransform(t, t2);
        //    t = simpletransform.composeTransform(t, t1);
        //    // return the combined transform
        //    return t;
        //}

        /// <summary>
        /// The rotate transform.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        internal static ((double x, double y, double z) x, (double x, double y, double z) y) RotateTransform(double a) => ((Math.Cos(a), -Math.Sin(a), 0), (Math.Sin(a), Math.Cos(a), 0));

        /// <summary>
        /// The scale transform.
        /// </summary>
        /// <param name="sx">The sx.</param>
        /// <param name="sy">The sy.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        internal static ((double x, double y, double z) x, (double x, double y, double z) y) ScaleTransform(double sx, double sy) => ((sx, 0, 0), (0, sy, 0));
    }
}
