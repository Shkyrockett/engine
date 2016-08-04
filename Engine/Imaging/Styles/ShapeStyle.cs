// <copyright file="ShapeStyle.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeStyle
        : IStyle, IDisposable
    {
        #region Public Implementations

        /// <summary>
        /// 
        /// </summary>
        public static readonly ShapeStyle DefaultStyle = new ShapeStyle(new Pen(Brushes.Black), new Pen(Brushes.White));

        #endregion

        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ShapeStyle()
            : this(Pens.Transparent, Pens.Transparent)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Brush forePen, Brush backPen)
            : this(new Pen(backPen), new Pen(forePen))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Pen forePen, Pen backPen)
        {
            BackPen = backPen;
            ForePen = forePen;
        }

        #endregion

        #region Destructors

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // Free any other managed objects here.
                ForePen.Dispose();
                BackPen.Dispose();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        ~ShapeStyle()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Pen ForePen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Pen BackPen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Brush BackBrush
        {
            get { return BackPen.Brush; }
            set { BackPen.Brush = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{nameof(ShapeStyle)}{{{nameof(ForePen)}={ForePen},{nameof(BackPen)}={BackPen}}}";

        #endregion
    }
}
