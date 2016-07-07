﻿using System.ComponentModel;
using System.Windows.Forms;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/2397860/c-sharp-winforms-smart-textbox-control-to-auto-format-path-length-based-on-tex
    /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/f452fd0d-a4f1-49fa-ac3a-da614540cbf1/creating-a-label-that-displays-a-path-with-middleellipsis?forum=csharpgeneral
    /// </remarks>
    public partial class PathLabel
        : TransparentLabel
    {
        /// <summary>
        /// 
        /// </summary>
        public PathLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.PathEllipsis;
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, flags);
        }
    }
}