// <copyright file="Direct2DForm.cs" company="Shkyrockett" >
// Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Editor;

/// <summary>
/// 
/// </summary>
public partial class Direct2DForm
    : Form
{
    /// <summary>
    /// 
    /// </summary>
    public Direct2DForm()
    {
        InitializeComponent();
        //this.DoubleBuffered = true;
    }

    //protected override CreateParams CreateParams
    //{
    //    get
    //    {
    //        CreateParams cp = base.CreateParams;
    //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
    //        return cp;
    //    }
    //}
}
