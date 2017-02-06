// <copyright file="Direct2DForm.Designer.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    partial class Direct2DForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        private Engine.Winforms.Direct2D.Direct2DCanvas direct2DCanvas1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.direct2DCanvas1 = new Engine.Winforms.Direct2D.Direct2DCanvas();
            this.SuspendLayout();
            // 
            // direct2DCanvas1
            // 
            this.direct2DCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.direct2DCanvas1.Location = new System.Drawing.Point(12, 12);
            this.direct2DCanvas1.Name = "direct2DCanvas1";
            this.direct2DCanvas1.Size = new System.Drawing.Size(260, 237);
            this.direct2DCanvas1.TabIndex = 0;
            // 
            // Direct2DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.direct2DCanvas1);
            this.Name = "Direct2DForm";
            this.Text = "Direct2DForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}