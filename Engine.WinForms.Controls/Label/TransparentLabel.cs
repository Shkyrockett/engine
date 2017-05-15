// <copyright file="TransparentLabel.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Windows.Forms;

namespace Engine.WindowsForms
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/1517179/c-overriding-onpaint-on-progressbar-not-working
    /// </remarks>
    public partial class TransparentLabel
        : Label
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ff700543(v=vs.85).aspx
        /// </summary>
        private const int WS_EX_TRANSPARENT = 0x00000020;

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
        /// 
        /// </summary>
        public TransparentLabel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            UpdateStyles();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }
    }
}
