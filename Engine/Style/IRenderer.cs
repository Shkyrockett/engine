// <copyright file="IRenderer.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The IRenderer interface.
    /// </summary>
    /// <remarks>
    /// https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Graphics.cs,f75b9da086111fe9,references
    /// </remarks>
    public interface IRenderer
        : IDisposable
    {
        /// <summary>
        /// Clear.
        /// </summary>
        /// <param name="color">The color.</param>
        void Clear(IColor color);

        /// <summary>
        /// The draw bitmap.
        /// </summary>
        void DrawBitmap();

        /// <summary>
        /// The draw line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        void DrawLine(IStroke pen, double x1, double y1, double x2, double y2);

        /// <summary>
        /// The draw lines.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        void DrawLines(IStroke pen, IEnumerable<Point2D> points);

        /// <summary>
        /// The draw polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        void DrawPolygon(IStroke pen, IEnumerable<Point2D> points);

        /// <summary>
        /// The draw curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        /// <summary>
        /// The draw closed curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fillmode.</param>
        void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        /// <summary>
        /// The draw path.
        /// </summary>
        /// <param name="pen">The pen.</param>
        void DrawPath(IStroke pen);

        /// <summary>
        /// The draw quadratic bezier.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3);

        /// <summary>
        /// The draw cubic bezier.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        /// <summary>
        /// The draw arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// The draw pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// The draw ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void DrawEllipse(IStroke pen, double x, double y, double width, double height);

        /// <summary>
        /// The draw rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void DrawRectangle(IStroke pen, double x, double y, double width, double height);

        /// <summary>
        /// The draw rectangles.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangles">The rectangles.</param>
        void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles);

        /// <summary>
        /// Fill the polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        void FillPolygon(IFill brush, IEnumerable<Point2D> points);

        /// <summary>
        /// Fill the curve.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        /// <summary>
        /// Fill the closed curve.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fillmode.</param>
        void FillClosedCurve(IFill brush, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        /// <summary>
        /// Fill the path.
        /// </summary>
        /// <param name="brush">The brush.</param>
        void FillPath(IFill brush);

        /// <summary>
        /// Fill the quadratic bezier.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3);

        /// <summary>
        /// Fill the cubic bezier.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        /// <summary>
        /// Fill the region.
        /// </summary>
        /// <param name="brush">The brush.</param>
        void FillRegion(IFill brush);

        /// <summary>
        /// Fill the arc.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// Fill the pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// Fill the ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void FillEllipse(IFill brush, double x, double y, double width, double height);

        /// <summary>
        /// Fill the rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void FillRectangle(IFill brush, double x, double y, double width, double height);

        /// <summary>
        /// Fill the rectangles.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangles">The rectangles.</param>
        void FillRectangles(IFill brush, IEnumerable<Rectangle2D> rectangles);

        /// <summary>
        /// The draw string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        void DrawString(string text, RenderFont font, IFill brush, double x, double y, double width, double height, TextFormat stringFormat);

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat);

        /// <summary>
        /// The measure character ranges.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        Size2D MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat);
    }
}
