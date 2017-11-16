using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.WindowsForms
{
    /// <summary>
    /// 
    /// </summary>
    public class WinformsRenderer
        : IRenderer
    {
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        public WinformsRenderer(Graphics graphics)
        {
            Graphics = graphics;
        }

        /// <summary>
        /// 
        /// </summary>
        Graphics Graphics { get; set; }

        #region IDisposable Support

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void Clear(IColor color) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        public void DrawBitmap() => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void DrawLine(IStroke pen, double x1, double y1, double x2, double y2)
            => Graphics.DrawLine(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        public void DrawLines(IStroke pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        public void DrawPolygon(IStroke pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfSegments"></param>
        /// <param name="tension"></param>
        public void DrawCurve(IStroke pen, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <param name="fillmode"></param>
        public void DrawClosedCurve(IStroke pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        public void DrawPath(IStroke pen) => throw new NotImplementedException();

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
        public void DrawQuadraticBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            Graphics.DrawBezier(pen.ToPen(), (float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
        }

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
        public void DrawCubicBezier(IStroke pen, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
            => Graphics.DrawBezier(pen.ToPen(), (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);

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
        public void DrawArc(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

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
        public void DrawPie(IStroke pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.DrawArc(pen.ToPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawEllipse(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawEllipse(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawRectangle(IStroke pen, double x, double y, double width, double height)
            => Graphics.DrawRectangle(pen.ToPen(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void FillRectangle(IFill brush, double x, double y, double width, double height)
            => Graphics.FillRectangle(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rectangles"></param>
        public void DrawRectangles(IStroke pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="tension"></param>
        /// <param name="fillmode"></param>
        public void FillClosedCurve(IFill pen, IEnumerable<Point2D> points, double tension, FillMode fillmode) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        public void FillRegion(IFill pen) => throw new NotImplementedException();

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
        public void FillArc(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
            Graphics.FillPath(brush.ToBrush(), path);
        }

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
        public void FillPie(IFill brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
            => Graphics.FillPie(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="points"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfSegments"></param>
        /// <param name="tension"></param>
        public void FillCurve(IFill brush, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        public void FillPath(IFill brush) => throw new NotImplementedException();

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
        public void FillQuadraticBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var path = new GraphicsPath();
            (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            path.AddBezier((float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
            Graphics.FillPath(brush.ToBrush(), path);
        }

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
        public void FillCubicBezier(IFill brush, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            var path = new GraphicsPath();
            path.AddBezier((float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
            Graphics.FillPath(brush.ToBrush(), path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void FillEllipse(IFill brush, double x, double y, double width, double height)
            => Graphics.FillEllipse(brush.ToBrush(), (float)x, (float)y, (float)width, (float)height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="rectangles"></param>
        public void FillRectangles(IFill pen, IEnumerable<Rectangle2D> rectangles) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        public void FillPolygon(IFill pen, IEnumerable<Point2D> points) => throw new NotImplementedException();

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
        public void DrawString(string text, RenderFont font, IFill brush, double x, double y, double width, double height, TextFormat stringFormat) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="layoutArea"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="layoutArea"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public Size2D MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat) => throw new NotImplementedException();
    }
}
