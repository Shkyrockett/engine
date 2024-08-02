// <copyright file="NeedleControl.Designer.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms;

namespace Engine;

/// <summary>
/// 
/// </summary>
partial class NeedleControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// 
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
                return cp;
        }
    }

    /// <summary>
    /// Set an arbitrary control to double-buffer.
    /// </summary>
    /// <param name="control">The control to set as double buffered.</param>
    /// <remarks>
    /// Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
    /// </remarks>
    private static void SetDoubleBuffered(Control control)
    {
        if (SystemInformation.TerminalServerSession) return;
        System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        aProp.SetValue(control, true, null);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // NeedleControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.Transparent;
        this.DoubleBuffered = true;
        this.Name = "NeedleControl";
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NeedleControl_MouseDown);
        this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NeedleControl_MouseMove);
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NeedleControl_MouseUp);
        this.Resize += new System.EventHandler(this.NeedleControl_Resize);
        this.ResumeLayout(false);

    }

    #endregion
}
