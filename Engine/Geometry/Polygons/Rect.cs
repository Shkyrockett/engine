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
    [DisplayName("Rectangle")]
    public class Rect
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Rect Empty = new Rect();

        /// <summary>
        /// 
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// 
        /// </summary>
        public Rect()
            : this(0, 0, 0, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Rect(Point location, Size size)
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
        public Rect(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        public Size Size
        {
            get { return rect.Size; }
            set { rect.Size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get { return rect.Height; }
            set { rect.Height = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point Location
        {
            get { return rect.Location; }
            set { rect.Location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Bottom
        {
            get { return rect.Bottom; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Left
        {
            get { return rect.Left; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Right
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
            g.FillRectangle(Style.BackBrush, rect);
            g.DrawRectangle(Style.ForePen, rect);
        }
    }
}
