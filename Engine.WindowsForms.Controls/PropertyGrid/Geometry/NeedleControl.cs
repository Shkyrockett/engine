// <copyright file="NeedleControl.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The needle control class.
    /// </summary>
    public partial class NeedleControl
        : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueCommittedEventHandler(object sender, ValueChangedEventArgs e);

        /// <summary>
        /// The value changed event of the <see cref="ValueChangedEventHandler"/>.
        /// </summary>
        [Category("Value")]
        [Description("This event is raised if the value changes.")]
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// The value committed event of the <see cref="ValueCommittedEventHandler"/>.
        /// </summary>
        [Category("Value")]
        [Description("This event is raised if the value is committed.")]
        public event ValueCommittedEventHandler ValueCommitted;

        /// <summary>
        /// The angle.
        /// </summary>
        private double angle;

        /// <summary>
        /// The selecting.
        /// </summary>
        private bool selecting;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeedleControl"/> class.
        /// </summary>
        public NeedleControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            // To minimize the flicking
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // Enable transparent BackColor
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            // Redraw the control after its size is changed
            ResizeRedraw = true;

            InitializeComponent();

            SetDoubleBuffered(this);
        }

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        private bool IsTransparent
            => BackColor == Color.Transparent;

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        [Category("Value")]
        [DefaultValue(0)]
        [Bindable(true)]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(value));
            }
        }

        /// <summary>
        /// The needle control mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void NeedleControl_MouseDown(object sender, MouseEventArgs e) => selecting = true;

        /// <summary>
        /// The needle control mouse move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void NeedleControl_MouseMove(object sender, MouseEventArgs e)
        {
            var center = Center(Bounds.Size);

            if (selecting)
            {
                Angle = Operations.Angle(e.X, e.Y, center.X, center.Y);
                Invalidate(true);
            }
        }

        /// <summary>
        /// The needle control mouse up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void NeedleControl_MouseUp(object sender, MouseEventArgs e)
        {
            var center = Center(Bounds.Size);

            if (selecting)
            {
                Angle = Operations.Angle(e.X, e.Y, center.X, center.Y);
                selecting = false;
                Invalidate(true);
                ValueCommitted?.Invoke(this, new ValueChangedEventArgs(Angle));
            }
        }

        /// <summary>
        /// The needle control resize.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void NeedleControl_Resize(object sender, EventArgs e)
        {
            var sqr = ToSquare(new Rectangle(Point.Empty, Bounds.Size));
            using var g = new GraphicsPath();
            g?.AddEllipse(sqr);
            Region = new Region(g);
        }

        /// <summary>
        /// Raises the paint background event.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (e is null) return;
            if (!IsTransparent)
            {
                base.OnPaintBackground(e);
            }

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            var sqr = ToSquare(ClientRectangle);
            FillBack(e.Graphics, sqr);
        }

        /// <summary>
        /// Raises the paint event.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (e is null) return;
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            var sqr = ToSquare(ClientRectangle);
            FillBack(e.Graphics, sqr);
            sqr.Inflate(new SizeF(-1f, -1f));
            DrawBorder(e.Graphics, sqr);
            DrawTicks(e.Graphics, sqr);
            DrawNeedle(e.Graphics, sqr, Angle);
        }

        /// <summary>
        /// Fill the back.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        private static void FillBack(Graphics g, RectangleF rect)
        {
            var faceBrush = SystemBrushes.ButtonFace;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(Brushes.Transparent, g.VisibleClipBounds);
            g.CompositingMode = CompositingMode.SourceOver;
            g.FillEllipse(faceBrush, rect);
        }

        /// <summary>
        /// The draw border.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        private static void DrawBorder(Graphics g, RectangleF rect)
        {
            using var highlightPen = new Pen(SystemBrushes.ButtonHighlight)
            {
                Alignment = PenAlignment.Inset,
                Width = 3
            };
            {
                g.DrawArc(highlightPen, rect, (float)-45d, (float)-180d);
            }
            using var shaddowPen = new Pen(SystemBrushes.ButtonShadow)
            {
                Alignment = PenAlignment.Inset,
                Width = 3
            };
            g.DrawArc(shaddowPen, rect, (float)-45d, (float)180d);
        }

        /// <summary>
        /// The draw ticks.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        private static void DrawTicks(Graphics g, RectangleF rect)
        {
            using var pen = new Pen(Brushes.Black)
            {
                Alignment = PenAlignment.Center,
                Width = 1
            };
            var center = Center(rect);
            var radiusOutside = rect.Width * 0.5f;
            var radiusInside = 0.9f * radiusOutside;

            for (double i = 0; i < 2d * PI; i += 15d.DegreesToRadians())
            {
                var pointInside = new PointF(
                    (float)(center.X + (radiusInside * Cos(i))),
                    (float)(center.Y + (radiusInside * Sin(i))));
                var pointOutside = new PointF(
                    (float)(center.X + (radiusOutside * Cos(i))),
                    (float)(center.Y + (radiusOutside * Sin(i))));
                g.DrawLine(pen, pointInside, pointOutside);
            }
        }

        /// <summary>
        /// The draw needle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="angle">The angle.</param>
        private static void DrawNeedle(Graphics g, RectangleF rect, double angle)
        {
            using var pen = new Pen(Brushes.Red)
            {
                Alignment = PenAlignment.Center,
                Width = 2
            };
            var center = Center(rect);
            var radius = rect.Width * 0.5f;
            var point = new PointF(
                (float)(center.X + (radius * Cos(angle))),
                (float)(center.Y + (radius * Sin(angle))));
            g.DrawLine(pen, center, point);
        }

        /// <summary>
        /// The to square.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="RectangleF"/>.</returns>
        private static RectangleF ToSquare(RectangleF rect)
        {
            var smallest = rect.Height <= rect.Width ? rect.Height : rect.Width;
            return new RectangleF(
                rect.X + ((rect.Width - smallest) / 2f),
                rect.Y + ((rect.Height - smallest) / 2f),
                smallest,
                smallest);
        }

        /// <summary>
        /// Method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks><para>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Center(RectangleF rectangle) => new PointF(
            rectangle.Left + ((rectangle.Right - rectangle.Left) * 0.5f),
            rectangle.Top + ((rectangle.Bottom - rectangle.Top) * 0.5f)
        );

        /// <summary>
        /// Method to find the center point of a rectangle.
        /// </summary>
        /// <param name="size">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks><para>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Center(Size size) => new PointF(
            size.Width * 0.5f,
            size.Height * 0.5f
        );

        /// <summary>
        /// The inflate.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="Size"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(Size size, int amount)
            => new Size(size.Width + amount, size.Height + amount);
    }
}
