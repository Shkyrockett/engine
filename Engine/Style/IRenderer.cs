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
        void Clear(IColor color);

        void DrawBitmap();

        void DrawLine(IStroke pen, double x1, double y1, double x2, double y2);

        void DrawLines(IStroke pen, IEnumerable<Point2D> points);

        void DrawPolygon(IStroke pen, IEnumerable<Point2D> points);

        void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        void DrawPath(IStroke pen);

        void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3);

        void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void DrawEllipse(IStroke pen, double x, double y, double width, double height);

        void DrawRectangle(IStroke pen, double x, double y, double width, double height);

        void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles);

        void FillPolygon(IFill brush, IEnumerable<Point2D> points);

        void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        void FillClosedCurve(IFill brush, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        void FillPath(IFill brush);

        void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3);

        void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        void FillRegion(IFill brush);

        void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void FillEllipse(IFill brush, double x, double y, double width, double height);

        void FillRectangle(IFill brush, double x, double y, double width, double height);

        void FillRectangles(IFill brush, IEnumerable<Rectangle2D> rectangles);

        void DrawString(string text, IFont font, IFill brush, double x, double y, double width, double height, StringFormat stringFormat);

        Size2D MeasureString(string text, IFont font, Size2D layoutArea, StringFormat stringFormat);

        Size2D MeasureCharacterRanges(string text, IFont font, Size2D layoutArea, StringFormat stringFormat);
    }
}
