using Engine.Imaging;
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
            => Graphics.Clear(color.ToColor());

        /// <summary>
        /// The draw bitmap.
        /// </summary>
        public void DrawBitmap()
            => throw new NotImplementedException();

        /// <summary>
        /// The draw line.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public void DrawLine(IStroke stroke, double x1, double y1, double x2, double y2)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        /// <summary>
        /// The draw lines.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawLines(IStroke stroke, IEnumerable<Point2D> points)
        {
            var pointFs = (points as List<Point2D>)!!.ConvertAll(new Converter<Point2D, PointF>(WinformsTypeExtensions.ToPointF)).ToArray();
            using var pen = stroke.ToPen();
            Graphics.DrawLines(pen, pointFs);
        }

        /// <summary>
        /// The draw polygon.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(IStroke stroke, IEnumerable<Point2D> points)
        {
            var pointFs = (points as List<Point2D>)!!.ConvertAll(new Converter<Point2D, PointF>(WinformsTypeExtensions.ToPointF)).ToArray();
            using var pen = stroke.ToPen();
            Graphics.DrawPolygon(pen, pointFs);
        }

        /// <summary>
        /// Fill the polygon.
        /// </summary>
        /// <param name="fill">The pen.</param>
        /// <param name="points">The points.</param>
        public void FillPolygon(IFill fill, IEnumerable<Point2D> points)
        {
            var pointFs = (points as List<Point2D>)!!.ConvertAll(new Converter<Point2D, PointF>(WinformsTypeExtensions.ToPointF)).ToArray();
            using var brush = fill.ToBrush();
            Graphics.FillPolygon(brush, pointFs);
        }

        /// <summary>
        /// The draw curve.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        public void DrawCurve(IStroke stroke, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5)
        {
            var pointFs = (points as List<Point2D>)!!.ConvertAll(new Converter<Point2D, PointF>(WinformsTypeExtensions.ToPointF)).ToArray();
            using var pen = stroke.ToPen();
            Graphics.DrawCurve(pen, pointFs);
        }

        /// <summary>
        /// Fill the curve.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="points">The points.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfSegments">The numberOfSegments.</param>
        /// <param name="tension">The tension.</param>
        public void FillCurve(IFill fill, IEnumerable<Point2D> points, double offset, int numberOfSegments, double tension = 0.5)
        {
            var pointFs = (points as List<Point2D>)!!.ConvertAll(new Converter<Point2D, PointF>(WinformsTypeExtensions.ToPointF)).ToArray();
            using var path = new GraphicsPath();
            path.AddCurve(pointFs);
            using var brush = fill.ToBrush();
            Graphics.FillPath(brush, path);
        }

        /// <summary>
        /// The draw closed curve.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fill-mode.</param>
        public void DrawClosedCurve(IStroke stroke, IEnumerable<Point2D> points, double tension, FillMode fillmode)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw path.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        public void DrawPath(IStroke stroke)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw quadratic bezier.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        public void DrawQuadraticBezier(IStroke stroke, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            (var aX, var aY, var bX, var bY, var cX, var cY, var dX, var dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            using var pen = stroke.ToPen();
            Graphics.DrawBezier(pen, (float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
        }

        /// <summary>
        /// Fill the quadratic bezier.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        public void FillQuadraticBezier(IFill fill, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            using var path = new GraphicsPath();
            (var aX, var aY, var bX, var bY, var cX, var cY, var dX, var dY) = Conversions.QuadraticBezierToCubicBezierTuple(x1, y1, x2, y2, x3, y3);
            path.AddBezier((float)aX, (float)aY, (float)bX, (float)bY, (float)cX, (float)cY, (float)dX, (float)dY);
            using var brush = fill.ToBrush();
            Graphics.FillPath(brush, path);
        }

        /// <summary>
        /// The draw cubic bezier.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        public void DrawCubicBezier(IStroke stroke, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawBezier(pen, (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
        }

        /// <summary>
        /// Fill the cubic bezier.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        public void FillCubicBezier(IFill fill, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            using var path = new GraphicsPath();
            path.AddBezier((float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
            using var brush = fill.ToBrush();
            Graphics.FillPath(brush, path);
        }

        /// <summary>
        /// The draw arc.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public void DrawArc(IStroke stroke, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
        }

        /// <summary>
        /// Fill the arc.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public void FillArc(IFill fill, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            using var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
            using var brush = fill.ToBrush();
            Graphics.FillPath(brush, path);
        }

        /// <summary>
        /// The draw arc.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="angle">The angle.</param>
        public void DrawArc(IStroke stroke, double x, double y, double width, double height, double startAngle, double sweepAngle, double angle)
        {
            var center = new PointF((float)((0.5d * width) + x), (float)((0.5d * height) + y));
            var mat = new Matrix();
            mat.RotateAt((float)angle.ToDegrees(), center);
            Graphics.Transform = mat;
            using var pen = stroke.ToPen();
            Graphics.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle.ToDegrees(), (float)sweepAngle.ToDegrees());
            Graphics.ResetTransform();
        }

        /// <summary>
        /// Fill the arc.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="angle">The angle.</param>
        public void FillArc(IFill fill, double x, double y, double width, double height, double startAngle, double sweepAngle, double angle)
        {
            var center = new PointF((float)((0.5d * width) + x), (float)((0.5d * height) + y));
            var mat = new Matrix();
            mat.RotateAt((float)angle.ToDegrees(), center);
            Graphics.Transform = mat;
            using var path = new GraphicsPath();
            path.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle.ToDegrees(), (float)sweepAngle.ToDegrees());
            using var brush = fill.ToBrush();
            Graphics.FillPath(brush, path);
            Graphics.ResetTransform();
        }

        /// <summary>
        /// The draw pie.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public void DrawPie(IStroke stroke, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
        }

        /// <summary>
        /// Fill the pie.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public void FillPie(IFill fill, double x, double y, double width, double height, double startAngle, double sweepAngle)
        {
            using var brush = fill.ToBrush();
            Graphics.FillPie(brush, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
        }

        /// <summary>
        /// The draw ellipse.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void DrawEllipse(IStroke stroke, double x, double y, double width, double height)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawEllipse(pen, (float)x, (float)y, (float)width, (float)height);
        }

        /// <summary>
        /// Fill the ellipse.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void FillEllipse(IFill fill, double x, double y, double width, double height)
        {
            using var brush = fill.ToBrush();
            Graphics.FillEllipse(brush, (float)x, (float)y, (float)width, (float)height);
        }

        /// <summary>
        /// The draw ellipse.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="angle">The angle.</param>
        public void DrawEllipse(IStroke stroke, double x, double y, double width, double height, double angle)
        {
            var mat = new Matrix();
            var center = new PointF((float)((0.5d * width) + x), (float)((0.5d * height) + y));
            mat.RotateAt((float)angle.ToDegrees(), center);
            Graphics.Transform = mat;
            using var pen = stroke.ToPen();
            Graphics.DrawEllipse(pen, (float)x, (float)y, (float)width, (float)height);
            Graphics.ResetTransform();
        }

        /// <summary>
        /// Fill the ellipse.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="angle">The angle.</param>
        public void FillEllipse(IFill fill, double x, double y, double width, double height, double angle)
        {
            var mat = new Matrix();
            var center = new PointF((float)((0.5d * width) + x), (float)((0.5d * height) + y));
            mat.RotateAt((float)angle.ToDegrees(), center);
            Graphics.Transform = mat;
            using var brush = fill.ToBrush();
            Graphics.FillEllipse(brush, (float)x, (float)y, (float)width, (float)height);
            Graphics.ResetTransform();
        }

        /// <summary>
        /// The draw rectangle.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void DrawRectangle(IStroke stroke, double x, double y, double width, double height)
        {
            using var pen = stroke.ToPen();
            Graphics.DrawRectangle(pen, (float)x, (float)y, (float)width, (float)height);
        }

        /// <summary>
        /// Fill the rectangle.
        /// </summary>
        /// <param name="fill">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void FillRectangle(IFill fill, double x, double y, double width, double height)
        {
            using var brush = fill.ToBrush();
            Graphics.FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);
        }

        /// <summary>
        /// The draw rectangles.
        /// </summary>
        /// <param name="stroke">The pen.</param>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(IStroke stroke, IEnumerable<Rectangle2D> rectangles)
        {
            var rectangleFs = (rectangles as List<Rectangle2D>)!!.ConvertAll(new Converter<Rectangle2D, RectangleF>(WinformsTypeExtensions.ToRectangleF)).ToArray();
            using var pen = stroke.ToPen();
            Graphics.DrawRectangles(pen, rectangleFs);
        }

        /// <summary>
        /// Fill the rectangles.
        /// </summary>
        /// <param name="fill">The pen.</param>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(IFill fill, IEnumerable<Rectangle2D> rectangles)
        {
            var rectangleFs = (rectangles as List<Rectangle2D>)!!.ConvertAll(new Converter<Rectangle2D, RectangleF>(WinformsTypeExtensions.ToRectangleF)).ToArray();
            using var brush = fill.ToBrush();
            Graphics.FillRectangles(brush, rectangleFs);
        }

        /// <summary>
        /// Fill the closed curve.
        /// </summary>
        /// <param name="fill">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="fillmode">The fill-mode.</param>
        public void FillClosedCurve(IFill fill, IEnumerable<Point2D> points, double tension, FillMode fillmode)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the region.
        /// </summary>
        /// <param name="fill">The pen.</param>
        public void FillRegion(IFill fill)
            => throw new NotImplementedException();

        /// <summary>
        /// Fill the path.
        /// </summary>
        /// <param name="fill">The brush.</param>
        public void FillPath(IFill fill)
            => throw new NotImplementedException();

        /// <summary>
        /// The draw string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        public void DrawString(string text, RenderFont font, IFill brush, double x, double y, TextFormat stringFormat)
        {
            using var font1 = font.ToFont();
            using var brush1 = brush.ToBrush();
            using var format = stringFormat.ToStringFormat();
            Graphics.DrawString(text, font1, brush1, (float)x, (float)y, format);
        }

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
        {
            using var font1 = font.ToFont();
            using var brush1 = brush.ToBrush();
            using var format = stringFormat.ToStringFormat();
            Graphics.DrawString(text, font1, brush1, new RectangleF((float)x, (float)y, (float)width, (float)height), format);
        }

        /// <summary>
        /// The measure string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public Size2D MeasureString(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat)
        {
            using var font1 = font.ToFont();
            using var stringFormat1 = stringFormat.ToStringFormat();
            return Graphics.MeasureString(text, font1, layoutArea.ToSizeF(), stringFormat1).ToSize2D();
        }

        /// <summary>
        /// The measure character ranges.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public object MeasureCharacterRanges(string text, RenderFont font, Rectangle2D layoutArea, TextFormat stringFormat)
        {
            using var font1 = font.ToFont();
            using var stringFormat1 = stringFormat.ToStringFormat();
            return Graphics.MeasureCharacterRanges(text, font1, layoutArea.ToRectangleF(), stringFormat1);
        }

        /// <summary>
        /// The measure character ranges.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="layoutArea">The layoutArea.</param>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public object MeasureCharacterRanges(string text, RenderFont font, Size2D layoutArea, TextFormat stringFormat)
        {
            using var font1 = font.ToFont();
            using var stringFormat1 = stringFormat.ToStringFormat();
            return Graphics.MeasureCharacterRanges(text, font1, new RectangleF(0, 0, (float)layoutArea.Width, (float)layoutArea.Height), stringFormat1);
        }
    }
}
