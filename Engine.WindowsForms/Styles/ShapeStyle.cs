// <copyright file="ShapeStyle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.Colorspace;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Drawing;
//using System.Xml.Serialization;

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
        {
            LineStyle.PropertyChanged += new PropertyChangedEventHandler(PropertyChanged_Event);
        }

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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Stroke LineStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Pen ForePen { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IStroke Stroke
        {
            get
            {
                switch (ForePen.Brush)
                {
                    case SolidBrush b:
                        return new Stroke(new SolidFill(new ARGB(b.Color.ToArgb())));
                    case HatchBrush h:
                        return new Stroke(new SolidFill(new ARGB(h.ForegroundColor.ToArgb())));
                    case LinearGradientBrush l:
                    case PathGradientBrush p:
                    case TextureBrush t:
                    default:
                        return new Stroke(new SolidFill(new ARGB(Color.Transparent.ToArgb())));
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IFill Fill
            => new SolidFill(new ARGB(BackPen.Color.ToArgb()));

        /// <summary>
        /// 
        /// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Pen BackPen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
                //Alignment = LineStyle.Alignment,
                //DashStyle = LineStyle.Dashstyle.DashStyle,
                //DashPattern = LineStyle.Dashstyle.DashPattern,
                //DashOffset = LineStyle.Dashstyle.DashOffset,
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
