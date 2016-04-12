using Engine.Imaging;
using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("RectangleF")]
    public class RectF
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly RectF Empty = new RectF();

        /// <summary>
        /// 
        /// </summary>
        private RectangleF rect;

        /// <summary>
        /// 
        /// </summary>
        public RectF()
            : this(0, 0, 0, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public RectF(Point location, Size size)
        {
            rect = new Rectangle(location, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectF(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        public SizeF Size
        {
            get { return rect.Size; }
            set { rect.Size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Height
        {
            get { return rect.Height; }
            set { rect.Height = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF Location
        {
            get { return rect.Location; }
            set { rect.Location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Bottom
        {
            get { return rect.Bottom; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Left
        {
            get { return rect.Left; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Right
        {
            get { return rect.Left; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get { return rect.IsEmpty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override RectangleF Bounds
        {
            get { return rect; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Render(Graphics g)
        {
            g.FillRectangles(Style.BackBrush, new RectangleF[] { rect });
            g.DrawRectangles(Style.ForePen, new RectangleF[] { rect });
        }
    }
}
