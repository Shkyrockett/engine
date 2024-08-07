﻿// <copyright file="Renderer.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

//using Engine.MathNotation;
using Engine.WindowsForms;

namespace Engine.Imaging;

/// <summary>
/// The renderer class.
/// </summary>
public static partial class Renderer
{
    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="g">The g.</param>
    /// <param name="renderer">The renderer.</param>
    /// <param name="style">The style.</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public static void Render(GraphicItem item, Graphics g, IRenderer renderer, IStyle style = null)
    {
        //g.DrawRectangles(Pens.Lime, new RectangleF[] { shape.Bounds.ToRectangleF() });

        var clipRect = g?.VisibleClipBounds.ToRectangle2D();

        //if (clipRect.Intersects(item.Shape.Bounds))
        switch (item?.Shape)
        {
            case ParametricDelegateCurve2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case ParametricPointTester t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case ParametricWarpGrid t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case AngleVisualizerTester t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case NodeRevealer t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Text2D t:
                t.Render(g, item, style as ShapeStyle);
                break;
            //case Coefficient t:
            //    t.Render(g, item, style as TextStyle);
            //    break;
            //case Expression t:
            //    t.Render(g, item, style as TextStyle);
            //    break;
            //case Radical t:
            //    t.Render(g, item, style as TextStyle);
            //    break;
            //case Term t:
            //    t.Render(g, item, style as TextStyle);
            //    break;
            //case Logarithm t:
            //    t.Render(g, item, style as TextStyle);
            //    break;
            case ScreenPoint2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Ray2D t:
                t.Render(g, renderer, item, clipRect, style as ShapeStyle);
                break;
            case Line2D t:
                t.Render(g, renderer, item, clipRect, style as ShapeStyle);
                break;
            case LineSegment2D t: // Line segment needs to be in front of Polyline because LineSegment is a subset of Polyline.
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Polyline2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case PolylineSet2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case PolygonContour2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Polygon2D t:
                t.Render(g, item, style as ShapeStyle);
                break;
            case PolycurveContour2D t:
                t.Render(g, item, style as ShapeStyle);
                break;
            case Polycurve2D t:
                t.Render(g, item, style as ShapeStyle);
                break;
            //case Oval t:
            //    t.Render(g, item, style as ShapeStyle);
            //    break;
            case Rectangle2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case CircularArc2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case EllipticalArc2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Circle2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case Ellipse2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case BezierSegment2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case CubicBezier2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case QuadraticBezier2D t:
                t.Render(g, renderer, item, style as ShapeStyle);
                break;
            case null:
                throw new NullReferenceException($"{nameof(item)} is null.");
            default:
                throw new InvalidCastException($"Unknown {nameof(item)}.");
        }
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="shape">The shape.</param>
    /// <param name="g">The g.</param>
    /// <param name="item">The item.</param>
    /// <param name="style">The style.</param>
    public static void Render(this Text2D shape, Graphics g, GraphicItem item, ShapeStyle style = null)
    {
        var itemStyle = style ?? (ShapeStyle)item?.Style;
        var layoutRectangle = shape?.Bounds?.ToRectangleF();
        if (layoutRectangle is not null)
        {
            using var font = shape.Font.ToFont();
            g?.DrawString(shape.Text, font, itemStyle.ForeBrush, layoutRectangle.Value);
        }
    }
}
