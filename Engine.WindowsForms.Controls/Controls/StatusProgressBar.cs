// <copyright file="StatusProgressBar.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.WindowsForms
{
    /// <summary>
    /// The status progress bar class.
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/1517179/c-overriding-onpaint-on-progressbar-not-working
    /// </remarks>
    public partial class StatusProgressBar
        : ProgressBar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusProgressBar"/> class.
        /// </summary>
        public StatusProgressBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        /// <summary>
        /// The wm paint (const). Value: 0x0F.
        /// </summary>
        private const int wmPaint = 0x0F;

        /// <summary>
        /// The wnd proc.
        /// </summary>
        /// <param name="m">The m.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case wmPaint:
                    using (var graphics = Graphics.FromHwnd(Handle))
                    {
                        var textSize = graphics.MeasureString(Text, Font);
                        using var textBrush = new SolidBrush(ForeColor);
                        graphics.DrawString(Text, Font, textBrush, (Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));
                    }

                    break;
            }
        }

        /// <summary>
        /// Raises the paint event.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = ClientRectangle;
            var g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-3, -3);
            if (Value > 0)
            {
                var clip = new Rectangle(rect.X, rect.Y, (int)Math.Round((float)Value / Maximum * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // assumes this.Maximum == 100
            var text = Value.ToString() + '%';

            using var f = new Font(FontFamily.GenericMonospace, 10);
            var strLen = g.MeasureString(text, f);
            var location = new Point((int)((rect.Width / 2) - (strLen.Width / 2)), (int)((rect.Height / 2) - (strLen.Height / 2)));
            g.DrawString(text, f, Brushes.Black, location);
        }
    }
}
