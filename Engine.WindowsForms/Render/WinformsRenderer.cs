using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.WindowsForms
{
    public class WinformsRenderer
        : IRenderer
    {
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        public WinformsRenderer(Graphics graphics)
        {
            Graphics = graphics;
        }

        Graphics Graphics { get; set; }

        #region IDisposable Support

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
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        public void Clear(IColor color) => throw new NotImplementedException();

        public void DrawBitmap() => throw new NotImplementedException();

        public void DrawLine(IStroke pen, double x1, double y1, double x2, double y2)
            => Graphics.DrawLine(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2);

        public void DrawLines(IStroke pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawPolygon(IStroke pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5) => throw new NotImplementedException();

        public void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        public void DrawPath(IStroke pen) => throw new NotImplementedException();

        public void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            Graphics.DrawBezier(pen.ToPen(), (float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
        }

        public void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
            => Graphics.DrawBezier(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);

        public void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void DrawEllipse(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawEllipse(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        public void DrawRectangle(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawRectangle(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        public void FillRectangle(IFill brush, double x, double y, double width, double height)
            => Graphics.FillRectangle(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        public void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        public void FillClosedCurve(IFill pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        public void FillRegion(IFill pen) => throw new NotImplementedException();

        public void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        public void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.FillPie(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5) => throw new NotImplementedException();

        public void FillPath(IFill brush) => throw new NotImplementedException();

        public void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var path = new GraphicsPath();
            (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            path.AddBezier((float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        public void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            var path = new GraphicsPath();
            path.AddBezier((float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        public void FillEllipse(IFill brush, double x, double y, double width, double height)
            => Graphics.FillEllipse(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        public void FillRectangles(IFill pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        public void FillPolygon(IFill pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawString(string text, RenderFont font, IFill brush, double x, double y, double width, double height, TextFormat stringFormat) => throw new NotImplementedException();

        public Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat) => throw new NotImplementedException();

        public Size2D MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat) => throw new NotImplementedException();
    }
}
