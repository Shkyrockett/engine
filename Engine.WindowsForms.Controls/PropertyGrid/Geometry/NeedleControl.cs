// <copyright file="NeedleControl.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public partial class NeedleControl
        : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public class ValueChangedEventArgs
            : EventArgs
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            public ValueChangedEventArgs(double value)
            {
                Value = value;
            }

            /// <summary>
            /// 
            /// </summary>
            public double Value { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueChangedDelegate(object sender, ValueChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ValueCommittedDelegate(object sender, ValueChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        [Category("Value")]
        [Description("This event is raised if the value changes.")]
        public event ValueChangedDelegate ValueChanged;

        /// <summary>
        /// 
        /// </summary>
        [Category("Value")]
        [Description("This event is raised if the value is committed.")]
        public event ValueCommittedDelegate ValueCommitted;

        /// <summary>
        /// 
        /// </summary>
        private double angle;

        /// <summary>
        /// 
        /// </summary>
        private bool selecting;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        private bool IsTransparent
            => BackColor == Color.Transparent;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl_MouseDown(object sender, MouseEventArgs e) => selecting = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl_MouseMove(object sender, MouseEventArgs e)
        {
            var center = Center(Bounds.Size);

            if (selecting)
            {
                Angle = Maths.Angle(e.X, e.Y, center.X, center.Y);
                Invalidate(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl_MouseUp(object sender, MouseEventArgs e)
        {
            var center = Center(Bounds.Size);

            if (selecting)
            {
                Angle = Maths.Angle(e.X, e.Y, center.X, center.Y);
                selecting = false;
                Invalidate(true);
                ValueCommitted?.Invoke(this, new ValueChangedEventArgs(Angle));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NeedleControl_Resize(object sender, EventArgs e)
        {
            var sqr = ToSquare(new Rectangle(Point.Empty, Bounds.Size));
            var g = new GraphicsPath();
            g.AddEllipse(sqr);
            Region = new Region(g);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!IsTransparent) base.OnPaintBackground(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            var sqr = ToSquare(ClientRectangle);
            FillBack(e.Graphics, sqr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
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
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private static void FillBack(Graphics g, RectangleF rect)
        {
            var faceBrush = SystemBrushes.ButtonFace;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(Brushes.Transparent, g.VisibleClipBounds);
            g.CompositingMode = CompositingMode.SourceOver;
            g.FillEllipse(faceBrush, rect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private static void DrawBorder(Graphics g, RectangleF rect)
        {
            var highlightPen = new Pen(SystemBrushes.ButtonHighlight)
            {
                Alignment = PenAlignment.Inset,
                Width = 3
            };
            var shaddowPen = new Pen(SystemBrushes.ButtonShadow)
            {
                Alignment = PenAlignment.Inset,
                Width = 3
            };
            g.DrawArc(highlightPen, rect, (float)-45d, (float)-180d);
            g.DrawArc(shaddowPen, rect, (float)-45d, (float)180d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private static void DrawTicks(Graphics g, RectangleF rect)
        {
            var pen = new Pen(Brushes.Black)
            {
                Alignment = PenAlignment.Center,
                Width = 1
            };
            var center = Center(rect);
            var radiusOutside = rect.Width / 2;
            var radiusInside = 0.9f * radiusOutside;

            for (double i = 0; i < 2d * PI; i += 15d.ToRadians())
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
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="angle"></param>
        private static void DrawNeedle(Graphics g, RectangleF rect, double angle)
        {
            var pen = new Pen(Brushes.Red)
            {
                Alignment = PenAlignment.Center,
                Width = 2
            };
            var center = Center(rect);
            var radius = rect.Width / 2;
            var point = new PointF(
                (float)(center.X + (radius * Cos(angle))),
                (float)(center.Y + (radius * Sin(angle))));
            g.DrawLine(pen, center, point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
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
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Center(RectangleF rectangle) => new PointF(
            rectangle.Left + (rectangle.Right - rectangle.Left) * 0.5f,
            rectangle.Top + (rectangle.Bottom - rectangle.Top) * 0.5f
        );

        /// <summary>
        /// Method to find the center point of a rectangle.
        /// </summary>
        /// <param name="size">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Center(Size size) => new PointF(
            size.Width * 0.5f,
            size.Height * 0.5f
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(Size size, int amount)
            => new Size(size.Width + amount, size.Height + amount);
    }
}
