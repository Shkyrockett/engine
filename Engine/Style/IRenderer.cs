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
    /// https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Graphics.cs,f75b9da086111fe9,references
    /// </summary>
    public interface IRenderer
        : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        void Clear(IColor color);

        /// <summary>
        /// 
        /// </summary>
        void DrawBitmap();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        void DrawLine(IStroke pen, double x1, double y1, double x2, double y2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        void DrawLines(IStroke pen, IEnumerable<Point2D> points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        void DrawPolygon(IStroke pen, IEnumerable<Point2D> points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfSegments"></param>
        /// <param name="tension"></param>
        void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <param name="fillmode"></param>
        void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        void DrawPath(IStroke pen);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void DrawEllipse(IStroke pen, double x, double y, double width, double height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void DrawRectangle(IStroke pen, double x, double y, double width, double height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rectangles"></param>
        void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="points"></param>
        void FillPolygon(IFill brush, IEnumerable<Point2D> points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="points"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfSegments"></param>
        /// <param name="tension"></param>
        void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <param name="fillmode"></param>
        void FillClosedCurve(IFill brush, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        void FillPath(IFill brush);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        void FillRegion(IFill brush);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void FillEllipse(IFill brush, double x, double y, double width, double height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void FillRectangle(IFill brush, double x, double y, double width, double height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="rectangles"></param>
        void FillRectangles(IFill brush, IEnumerable<Rectangle2D> rectangles);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stringFormat"></param>
        void DrawString(string text, RenderFont font, IFill brush, double x, double y, double width, double height, TextFormat stringFormat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="layoutArea"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="layoutArea"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        Size2D MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat);
    }
}
