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
using Engine.WindowsForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine.Imaging
{
    /// <summary>
    /// The shape style class.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ShapeStyle
        : IStyle, IDisposable
    {
        #region Public Implementations
        /// <summary>
        /// The default style (readonly). Value: new ShapeStyle(Brushes.Black, new Pen(Brushes.White)).
        /// </summary>
        public static readonly ShapeStyle DefaultStyle = new ShapeStyle(Brushes.Black, new Pen(Brushes.White));
        #endregion Public Implementations

        #region Private Fields
        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed = false;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeStyle"/> class.
        /// </summary>
        public ShapeStyle()
            : this(Brushes.Transparent, Pens.Transparent)
        {
            LineStyle.PropertyChanged += PropertyChanged_Event;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeStyle"/> class.
        /// </summary>
        /// <param name="forePen">The forePen.</param>
        /// <param name="backPen">The backPen.</param>
        public ShapeStyle(Brush forePen, Brush backPen)
            : this(backPen, new Pen(forePen))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeStyle"/> class.
        /// </summary>
        /// <param name="forePen">The forePen.</param>
        /// <param name="backPen">The backPen.</param>
        public ShapeStyle(Brush forePen, Pen backPen)
        {
            BackPen = backPen;
            ForePen = new Pen(forePen);
        }
        #endregion Constructors

        /// <summary>
        /// The property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The property changed event arguments.</param>
        private void PropertyChanged_Event(object sender, PropertyChangedEventArgs e)
            => BuildPen();

        #region Destructors
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

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
        /// Finalizes an instance of the <see cref="ShapeStyle"/> class.
        /// </summary>
        ~ShapeStyle()
        {
            Dispose(false);
        }
        #endregion Destructors

        #region Properties
        /// <summary>
        /// Gets or sets the line style.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Stroke LineStyle { get; set; }

        /// <summary>
        /// Gets the fore pen.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Pen ForePen { get; private set; }

        /// <summary>
        /// Gets the stroke.
        /// </summary>
        public IStroke Stroke
        {
            get
            {
                switch (ForePen.Brush)
                {
                    case SolidBrush b:
                        return new Stroke(new SolidFill(b.Color.ToRGBA()));
                    case HatchBrush h:
                        return new Stroke(new SolidFill(h.ForegroundColor.ToRGBA()));
                    case LinearGradientBrush l:
                    case PathGradientBrush p:
                    case TextureBrush t:
                    default:
                        return new Stroke(new SolidFill(Colors.Transparent));
                }

            }
        }

        /// <summary>
        /// Gets the fill.
        /// </summary>
        public IFill Fill
            => new SolidFill(BackPen.Color.ToRGBA());

        /// <summary>
        /// Gets or sets the fore brush.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Brush ForeBrush
        {
            get { return ForePen.Brush; }
            set { ForePen.Brush = value; }
        }

        /// <summary>
        /// Gets or sets the back pen.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Pen BackPen { get; set; }

        /// <summary>
        /// Gets or sets the back brush.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [NotifyParentProperty(true)]
        public Brush BackBrush
        {
            get { return BackPen.Brush; }
            set { BackPen.Brush = value; }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Build the pen.
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
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{nameof(ShapeStyle)}{{{nameof(ForePen)}={ForePen},{nameof(BackPen)}={BackPen}}}";
        #endregion Methods
    }
}
