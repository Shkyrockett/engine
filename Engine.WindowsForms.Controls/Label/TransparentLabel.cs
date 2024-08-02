// <copyright file="TransparentLabel.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.WindowsForms;

/// <summary>
/// The transparent label class.
/// </summary>
/// <remarks>
/// <para>http://stackoverflow.com/questions/1517179/c-overriding-onpaint-on-progressbar-not-working</para>
/// </remarks>
public partial class TransparentLabel
    : Label
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ff700543(v=vs.85).aspx
    /// </summary>
    private const int wS_EX_TRANSPARENT = 0x00000020;

    //private bool transparent;

    //public bool Transparent
    //{
    //    get { return transparent; }
    //    set
    //    {
    //        transparent = value;

    //    }
    //}

    /// <summary>
    /// Initializes a new instance of the <see cref="TransparentLabel"/> class.
    /// </summary>
    public TransparentLabel()
    {
        InitializeComponent();
        SetStyle(ControlStyles.Opaque, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
        UpdateStyles();
    }

    /// <summary>
    /// Gets the create params.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= wS_EX_TRANSPARENT;
            return cp;
        }
    }
}
