// <copyright file="PreservedFilter.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The preserving filter class.
/// </summary>
public abstract class PreservingFilter
    : IFilter
{
    #region Properties
    /// <summary>
    /// Gets or sets the tolerance.
    /// </summary>
    public double Tolerence { get; set; } = 1;

    /// <summary>
    /// Gets or sets the sample distance.
    /// </summary>
    public double SampleDistance { get; set; } = 8;
    #endregion Properties

    /// <summary>
    /// Process a <see cref="Point2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public virtual Point2D Process(Point2D point) => point;

    /// <summary>
    /// Process a <see cref="List{T}"/> structure with a distortion filter.
    /// </summary>
    /// <param name="contour">The contour.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    public List<Point2D> Process(Span<Point2D> contour)
    {
        //if (contour is null) return contour.ToArray();
        var results = new HashSet<Point2D>();
        foreach (var point in contour)
        {
            results.Add(Process(point));
        }
        return [.. results];
    }

    /// <summary>
    /// Process a <see cref="Type"/> structure with a distortion filter.
    /// </summary>
    /// <param name="shape">The shape.</param>
    /// <returns>The <see cref="Type"/>.</returns>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="T"></typeparam>
    public T Process<S, T>(S shape)
        where S : Shape2D
        where T : Shape2D
    {
        switch (shape)
        {
            case ScreenPoint2D t:
                return new ScreenPoint2D(Process(t.Point)) as T;
            case Line2D t:
                return Process(t) as T;
            case Ray2D t:
                return Process(t) as T;
            case LineSegment2D t:
                return Process(t) as T;
            case QuadraticBezier2D t:
                return Process(t) as T;
            case CubicBezier2D t:
                return Process(t) as T;
            case PointSet2D t:
                return Process(t) as T;
            case Triangle2D t:
                return Process(t) as T;
            case Polygon2D t:
                return Process(t) as T;
            case PolygonContour2D t:
                return Process(t) as T;
            case PolylineSet2D t:
                return Process(t) as T;
            case Polyline2D t:
                return Process(t) as T;
            case PolycurveContour2D t:
                return Process(t) as T;
            case Rectangle2D t:
                return Process(t) as T;
            default:
                break;
        }

        // Shape not supported.
        return shape as T;
    }

    /// <summary>
    /// Process a <see cref="Line2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="Line2D"/>.</returns>
    public Line2D Process(Line2D line)
    {
        if (line is null) return line;
        var location = Process(line.Location);
        var result = new Line2D(location, Process(line.Location + line.Direction) - location);
        return result;
    }

    /// <summary>
    /// Process a <see cref="Ray2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="Ray2D"/>.</returns>
    public Ray2D Process(Ray2D line)
    {
        if (line is null) return line;
        var location = Process(line.Location);
        var result = new Ray2D(location, Process(line.Location + line.Direction) - location);
        return result;
    }

    /// <summary>
    /// Process a <see cref="LineSegment2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="LineSegment2D"/>.</returns>
    public LineSegment2D Process(LineSegment2D line)
    {
        if (line is null) return line;
        var result = new LineSegment2D(Process(line.A), Process(line.B));
        return result;
    }

    /// <summary>
    /// Process a <see cref="QuadraticBezier2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="bezier">The Bézier.</param>
    /// <returns>The <see cref="QuadraticBezier2D"/>.</returns>
    public QuadraticBezier2D Process(QuadraticBezier2D bezier)
    {
        if (bezier is null) return bezier;
        var result = new QuadraticBezier2D(Process(bezier.A), Process(bezier.B), Process(bezier.C));
        return result;
    }

    /// <summary>
    /// Process a <see cref="CubicBezier2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="bezier">The Bézier.</param>
    /// <returns>The <see cref="CubicBezier2D"/>.</returns>
    public CubicBezier2D Process(CubicBezier2D bezier)
    {
        if (bezier is null) return bezier;
        var result = new CubicBezier2D(Process(bezier.A), Process(bezier.B), Process(bezier.C), Process(bezier.D));
        return result;
    }

    /// <summary>
    /// Process a <see cref="PointSet2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <returns>The <see cref="PointSet2D"/>.</returns>
    public PointSet2D Process(PointSet2D points)
    {
        if (points is null) return points;
        var results = new PointSet2D();
        foreach (var point in points)
        {
            results.Add(Process(point));
        }
        return results;
    }

    /// <summary>
    /// Process a <see cref="Triangle2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="triangle">The triangle.</param>
    /// <returns>The <see cref="Triangle2D"/>.</returns>
    public Triangle2D Process(Triangle2D triangle)
    {
        if (triangle is null) return triangle;
        return new Triangle2D(Process(triangle.A), Process(triangle.B), Process(triangle.C));
    }

    /// <summary>
    /// Process a <see cref="Rectangle2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <returns>The <see cref="PolygonContour2D"/>.</returns>
    public PolygonContour2D Process(Rectangle2D rect)
    {
        if (rect is null) return null;
        return [Process(rect.TopLeft), Process(rect.TopRight), Process(rect.BottomRight), Process(rect.BottomLeft)];
    }

    /// <summary>
    /// Process a <see cref="Polygon2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="polygon">The polygon.</param>
    /// <returns>The <see cref="Polygon2D"/>.</returns>
    public Polygon2D Process(Polygon2D polygon)
    {
        if (polygon is null) return polygon;
        var result = new Polygon2D();
        foreach (var contour in polygon)
        {
            result.Add(Process(contour));
        }
        return result;
    }

    /// <summary>
    /// Process a <see cref="PolygonContour2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="contour">The contour.</param>
    /// <returns>The <see cref="PolygonContour2D"/>.</returns>
    public PolygonContour2D Process(PolygonContour2D contour)
    {
        if (contour is null) return contour;
        var results = new PolygonContour2D();
        foreach (var point in contour)
        {
            results.Add(Process(point));
        }
        return results;
    }

    /// <summary>
    /// Process a <see cref="PolylineSet2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="polylines">The polylines.</param>
    /// <returns>The <see cref="PolylineSet2D"/>.</returns>
    public PolylineSet2D Process(PolylineSet2D polylines)
    {
        if (polylines is null) return polylines;
        var result = new PolylineSet2D();
        foreach (var contour in polylines)
        {
            result.Add(Process(contour));
        }
        return result;
    }

    /// <summary>
    /// Process a <see cref="Polyline2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="contour">The contour.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public Polyline2D Process(Polyline2D contour)
    {
        if (contour is null) return contour;
        var results = new Polyline2D();
        foreach (var point in contour)
        {
            results.Add(Process(point));
        }
        return results;
    }

    /// <summary>
    /// Process a <see cref="PolycurveContour2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="contour">The contour.</param>
    /// <returns>The <see cref="PolycurveContour2D"/>.</returns>
    public PolycurveContour2D Process(PolycurveContour2D contour)
    {
        if (contour is null) return contour;
        var result = new PolycurveContour2D(Process(contour.Items[0].Head));
        foreach (var item in contour)
        {
            switch (item)
            {
                case PointSegment2D _:
                    break;
                case LineCurveSegment2D t:
                    result.AddLineSegment(Process(t.Tail));
                    break;
                case ArcSegment2D t:
                    result.AddArc(t.RX, t.RY, t.Angle, t.LargeArc, t.Sweep, Process(t.Tail));
                    break;
                //case CardinalSegment2D t:
                //    result.AddCardinalCurve(Process(t.CentralPoints.ToArray()));
                //    break;
                case QuadraticBezierSegment2D t:
                    result.AddQuadraticBezier(Process(t.Handle), Process(t.Tail));
                    break;
                case CubicBezierSegment2D t:
                    result.AddCubicBezier(Process(t.Handle1), Process(t.Handle2), Process(t.Tail));
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    /// <summary>
    /// Process an <see cref="Envelope2D"/> structure with a distortion filter.
    /// </summary>
    /// <param name="envelope">The contour.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public Envelope2D Process(Envelope2D envelope)
    {
        var results = new Envelope2D
        {
            ControlPointTopLeft = new CubicControlPoint2D(
                Process(envelope.ControlPointTopLeft.Point),
                Process(envelope.ControlPointTopLeft.AnchorAGlobal),
                Process(envelope.ControlPointTopLeft.AnchorBGlobal), true),
            ControlPointTopRight = new CubicControlPoint2D(
                Process(envelope.ControlPointTopRight.Point),
                Process(envelope.ControlPointTopRight.AnchorAGlobal),
                Process(envelope.ControlPointTopRight.AnchorBGlobal), true),
            ControlPointBottomRight = new CubicControlPoint2D(
                Process(envelope.ControlPointBottomRight.Point),
                Process(envelope.ControlPointBottomRight.AnchorAGlobal),
                Process(envelope.ControlPointBottomRight.AnchorBGlobal), true),
            ControlPointBottomLeft = new CubicControlPoint2D(
                Process(envelope.ControlPointBottomLeft.Point),
                Process(envelope.ControlPointBottomLeft.AnchorAGlobal),
                Process(envelope.ControlPointBottomLeft.AnchorBGlobal), true)
        };

        return results;
    }
}
