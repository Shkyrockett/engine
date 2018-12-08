using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.WindowsForms
{
    /// <summary>
    /// The winforms renderer class.
    /// </summary>
    public class WinformsRenderer
        : IRenderer
    {
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinformsRenderer"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public WinformsRenderer(Graphics graphics)
        {
            Graphics = graphics;
        }

        /// <summary>
        /// Gets or sets the graphics.
        /// </summary>
        private Graphics Graphics { get; set; }

        #region IDisposable Support
        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below. set large fields to null.
                Graphics.Dispose();
                disposedValue = true;
            }
        }

        /// <summary>
        /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        /// </summary>
        ~WinformsRenderer()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // ToDo: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support

        /// <summary>
        /// Clear.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(IColor color)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw bitmap.
        /// </summary>
        public void DrawBitmap()
            => throw new NotImplementedException();

        /// <summary>
        /// The draw line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public void DrawLine(IStroke pen, double x1, double y1, double x2, double y2)
            => Graphics.DrawLine(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2);

        /// <summary>
        /// The draw lines.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawLines(IStroke pen, IEnumerable<Point2D> points)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(IStroke pen, IEnumerable<Point2D> points)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        public void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw closed curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fillmode.</param>
        public void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw path.
        /// </summary>
        /// <param name="pen">The pen.</param>
        public void DrawPath(IStroke pen)
            => throw new NotImplementedException();

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
        public void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            (var aX, var aY, var bX, var bY, var cX, var cY, var dX, var dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            Graphics.DrawBezier(pen.ToPen(), (float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
        }

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
        public void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
            => Graphics.DrawBezier(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);

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
        public void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

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
        public void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        /// <summary>
        /// The draw ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void DrawEllipse(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawEllipse(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// The draw rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void DrawRectangle(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawRectangle(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// Fill the rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void FillRectangle(IFill brush, double x, double y, double width, double height)
            => Graphics.FillRectangle(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// The draw rectangles.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the closed curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fillmode.</param>
        public void FillClosedCurve(IFill pen, IEnumerable<Point2D> points, double tension, FillMode fillmode)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the region.
        /// </summary>
        /// <param name="pen">The pen.</param>
        public void FillRegion(IFill pen)
            => throw new NotImplementedException();

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
        public void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
            Graphics.FillPath(brush.ToBrush(), path);
        }

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
        public void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.FillPie(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        /// <summary>
        /// Fill the curve.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        public void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the path.
        /// </summary>
        /// <param name="brush">The brush.</param>
        public void FillPath(IFill brush)
            => throw new NotImplementedException();

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
        public void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var path = new GraphicsPath();
            (var aX, var aY, var bX, var bY, var cX, var cY, var dX, var dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            path.AddBezier((float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
            Graphics.FillPath(brush.ToBrush(), path);
        }

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
        public void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            var path = new GraphicsPath();
            path.AddBezier((float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        /// <summary>
        /// Fill the ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void FillEllipse(IFill brush, double x, double y, double width, double height)
            => Graphics.FillEllipse(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// Fill the rectangles.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(IFill pen, IEnumerable<Rectangle2D> rectangles)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void FillPolygon(IFill pen, IEnumerable<Point2D> points)
            => throw new NotImplementedException();

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
        public void DrawString(string text, RenderFont font, IFill brush, double x, double y, double width, double height, TextFormat stringFormat)
            => throw new NotImplementedException();

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat)
            => throw new NotImplementedException();

        /// <summary>
        /// The measure character ranges.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public Size2D MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat)
            => throw new NotImplementedException();
    }
}
