﻿// <copyright file="Renderer.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.MathNotation;
using Engine.Objects;
using Engine.Winforms;
using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    ///
    /// </summary>
    public static partial class Renderer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(GraphicItem item, Graphics g, IStyle style = null)
        {
            //g.DrawRectangles(Pens.Lime, new RectangleF[] { shape.Bounds.ToRectangleF() });

            switch (item?.Item)
            {
                case ParametricDelegateCurve t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case ParametricPointTester t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case AngleVisualizerTester t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Text2D t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Coefficient t:
                    t.Render(g, item, style as TextStyle);
                    break;
                case Expression t:
                    t.Render(g, item, style as TextStyle);
                    break;
                case Radical t:
                    t.Render(g, item, style as TextStyle);
                    break;
                case Term t:
                    t.Render(g, item, style as TextStyle);
                    break;
                case Logarithm t:
                    t.Render(g, item, style as TextStyle);
                    break;
                case LineSegment t: // Line segment needs to be in front of Polyline because LineSegment is a subset of Polyline.
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Polyline t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case PolylineSet t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Polygon t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case PolygonSet t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Oval t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Rectangle2D t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case CircularArc t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case EllipticalArc t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Circle t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case Ellipse t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case CubicBezier t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case QuadraticBezier t:
                    t.Render(g, item, style as ShapeStyle);
                    break;
                case null:
                    throw new NullReferenceException($"{nameof(item)} is null.");
                default:
                    throw new InvalidCastException($"Unknown {nameof(item)}.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this Text2D shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item.Style;
            g.DrawString(shape.Text, shape.Font, itemStyle.ForeBrush, shape.Bounds.ToRectangleF());
        }
    }
}