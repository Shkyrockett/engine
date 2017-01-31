// <copyright file="ShapeStyle.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ShapeStyle
        : IStyle, IDisposable
    {
        #region Public Implementations

        /// <summary>
        /// 
        /// </summary>
        public static readonly ShapeStyle DefaultStyle = new ShapeStyle(Brushes.Black, new Pen(Brushes.White));

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
            : this(Brushes.Transparent, Pens.Transparent)
            => LineStyle.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged_Event);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Brush forePen, Brush backPen)
            : this(backPen, new Pen(forePen))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forePen"></param>
        /// <param name="backPen"></param>
        public ShapeStyle(Brush forePen, Pen backPen)
        {
            BackPen = backPen;
            ForePen = new Pen(forePen);
        }

        #endregion

        private void PropertyChanged_Event(Object sender, PropertyChangedEventArgs e)
            => BuildPen();

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
        [NotifyParentProperty(true)]
        public LineStyle LineStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [NotifyParentProperty(true)]
        public Pen ForePen { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [NotifyParentProperty(true)]
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [NotifyParentProperty(true)]
        public Pen BackPen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [NotifyParentProperty(true)]
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
        public void BuildPen()
        {
            ForePen.Brush = null;
            ForePen.Dispose();
            ForePen = new Pen(ForeBrush)
            {
                Alignment = LineStyle.Alignment,
                DashStyle = LineStyle.Dashstyle.DashStyle,
                DashPattern = LineStyle.Dashstyle.DashPattern,
                DashOffset = LineStyle.Dashstyle.DashOffset,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(ShapeStyle)}{{{nameof(ForePen)}={ForePen},{nameof(BackPen)}={BackPen}}}";

        #endregion
    }
}
