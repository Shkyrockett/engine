// <copyright file="ScreenPoint.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The screen point class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Point))]
    public class ScreenPoint
        : Shape
    {
        #region Fields
        /// <summary>
        /// The point.
        /// </summary>
        private Point2D point;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenPoint"/> class.
        /// </summary>
        public ScreenPoint()
            : this(Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenPoint"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public ScreenPoint(Point2D point)
        {
            this.point = point;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenPoint"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public ScreenPoint(double x, double y)
            : this(new Point2D(x, y))
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void Deconstruct(out double x, out double y)
        {
            x = point.X;
            y = point.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        [DataMember, XmlAnyAttribute, SoapElement]
        public double X
        {
            get { return point.X; }
            set
            {
                point.X = value;
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        [DataMember, XmlAnyAttribute, SoapElement]
        public double Y
        {
            get { return point.Y; }
            set
            {
                point.Y = value;
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Point2D Point
        {
            get { return point; }
            set
            {
                point = value;
                OnPropertyChanged(nameof(Point));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => 0;

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => new Rectangle2D(point, point));

        /// <summary>
        /// Gets the area.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Area
            => 0;
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(ScreenPoint)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(ScreenPoint)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(ScreenPoint)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(ScreenPoint)} has been deserialized.");
        //}

        //#endregion

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => point;

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => this.point == point;

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="ScreenPoint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScreenPoint Clone()
            => new ScreenPoint(point.X, point.Y);

        /// <summary>
        /// Creates a string representation of this <see cref="ScreenPoint"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(ScreenPoint);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(ScreenPoint)}{{{nameof(X)}={X.ToString(format, provider)}{sep}{nameof(Y)}={Y.ToString(format, provider)}}}";
        }
    }
}
