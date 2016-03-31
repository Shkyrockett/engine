using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/1551889/how-to-make-an-ownerdraw-trackbar-in-winforms
    /// </remarks>
    public partial class ColorTrackBarControl
        : TrackBar
    {
        /// <summary>
        /// 
        /// </summary>
        public ColorTrackBarControl()
            : base()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint
                , true);
            UpdateStyles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            //var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis;
            //TextRenderer.DrawText(e.Graphics, "Hello", Font, Bounds, Color.Black, flags);

            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, ClientRectangle);

            TrackBarRenderer.DrawHorizontalTicks(e.Graphics, ClientRectangle, TickFrequency, EdgeStyle.Etched);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle ThumbRect = new Rectangle(Point.Empty, TrackBarRenderer.GetBottomPointingThumbSize(e.Graphics, TrackBarThumbState.Normal));
            TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, ThumbRect, TrackBarThumbState.Normal);
            //var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis;
            //TextRenderer.DrawText(e.Graphics, "Hello", Font, Bounds, Color.Black, flags);
        }
    }
}
