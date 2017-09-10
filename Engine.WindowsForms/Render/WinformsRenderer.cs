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

        public void DrawLine(IPen pen, double x1, double y1, double x2, double y2)
            => Graphics.DrawLine(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2);

        public void DrawLines(IPen pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawPolygon(IPen pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawCurve(IPen pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5) => throw new NotImplementedException();

        public void DrawClosedCurve(IPen pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        public void DrawPath(IPen pen) => throw new NotImplementedException();

        public void DrawQuadraticBezier(IPen pen, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            Graphics.DrawBezier(pen.ToPen(), (float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
        }

        public void DrawCubicBezier(IPen pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
            => Graphics.DrawBezier(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);

        public void DrawArc(IPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void DrawPie(IPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void DrawEllipse(IPen pen, double x, double y, double width, double height)
            => Graphics.DrawEllipse(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        public void DrawRectangle(IPen pen, double x, double y, double width, double height)
            => Graphics.DrawRectangle(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        public void FillRectangle(IBrush brush, double x, double y, double width, double height)
            => Graphics.FillRectangle(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        public void DrawRectangles(IPen pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        public void FillClosedCurve(IBrush pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        public void FillRegion(IBrush pen) => throw new NotImplementedException();

        public void FillArc(IBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        public void FillPie(IBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.FillPie(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        public void FillEllipse(IBrush brush, double x, double y, double width, double height)
            => Graphics.FillEllipse(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        public void FillRectangles(IBrush pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        public void FillPolygon(IBrush pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        public void DrawString(string text, IFont font, IBrush brush, double x, double y, double width, double height, StringFormat stringFormat) => throw new NotImplementedException();

        public Size2D MeasureString(string text, IFont font, Size2D layoutArea, StringFormat stringFormat) => throw new NotImplementedException();

        public Size2D MeasureCharacterRanges(string text, IFont font, Size2D layoutArea, StringFormat stringFormat) => throw new NotImplementedException();
    }
}
