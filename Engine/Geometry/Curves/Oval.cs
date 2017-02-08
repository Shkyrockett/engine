// <copyright file="Oval.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Oval))]
    [XmlType(TypeName = "oval")]
    public class Oval
        : Shape, IClosedShape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double h;

        /// <summary>
        /// 
        /// </summary>
        private double v;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Oval()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Oval(Point2D location, Size2D size)
        {
            x = location.X;
            y = location.Y;
            h = size.Width;
            v = size.Height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Point2D Location
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("x")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public Double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("y")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public Double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Size2D Size
        {
            get { return new Size2D(h, v); }
            set
            {
                h = value.Width;
                v = value.Height;
                OnPropertyChanged(nameof(Size));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("h")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public Double Width
        {
            get { return h; }
            set
            {
                h = value;
                OnPropertyChanged(nameof(Width));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("v")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public Double Height
        {
            get { return v; }
            set
            {
                v = value;
                OnPropertyChanged(nameof(Height));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public override Rectangle2D Bounds
            => new Rectangle2D(x, y, h, v);

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Oval"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Oval);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Oval)}{{{nameof(Location)}={Location}{sep}{nameof(Size)}={Size}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
