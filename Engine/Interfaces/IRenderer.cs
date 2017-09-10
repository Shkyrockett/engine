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

        void DrawLine(IPen pen, double x1, double y1, double x2, double y2);

        void DrawLines(IPen pen, IEnumerable<Point2D> points);

        void DrawPolygon(IPen pen, IEnumerable<Point2D> points);

        void DrawCurve(IPen pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5d);

        void DrawClosedCurve(IPen pen, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        void DrawPath(IPen pen);

        void DrawQuadraticBezier(IPen pen, double x1, double y1, double x2, double y2, double x3, double y3);

        void DrawCubicBezier(IPen pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        void DrawArc(IPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void DrawPie(IPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void DrawEllipse(IPen pen, double x, double y, double width, double height);

        void DrawRectangle(IPen pen, double x, double y, double width, double height);

        void DrawRectangles(IPen pen, IEnumerable<Rectangle2D> rectangles);

        void FillClosedCurve(IBrush brush, IEnumerable<Point2D> points, double tension, FillMode fillmode);

        void FillRegion(IBrush brush);

        void FillArc(IBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void FillPie(IBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle);

        void FillEllipse(IBrush brush, double x, double y, double width, double height);

        void FillRectangle(IBrush brush, double x, double y, double width, double height);

        void FillRectangles(IBrush brush, IEnumerable<Rectangle2D> rectangles);

        void FillPolygon(IBrush brush, IEnumerable<Point2D> points);

        void DrawString(string text, IFont font, IBrush brush, double x, double y, double width, double height, StringFormat stringFormat);

        Size2D MeasureString(string text, IFont font, Size2D layoutArea, StringFormat stringFormat);

        Size2D MeasureCharacterRanges(string text, IFont font, Size2D layoutArea, StringFormat stringFormat);


    }
}
