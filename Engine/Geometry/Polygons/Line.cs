// <copyright file="Line.cs" company="Shkyrockett" >
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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 2D Line Structure
    /// </summary>
    /// <structure>Engine.Geometry.Line2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Line")]
    [XmlType(TypeName = "line", Namespace = "http://www.w3.org/2000/svg")]
    public class Line
        : Shape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        Point2D location;

        /// <summary>
        /// 
        /// </summary>
        Vector2D direction;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public Line()
            : this(Point2D.Empty, Vector2D.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <remarks></remarks>
        public Line(Point2D location, Vector2D direction)
            : base()
        {
            Location = location;
            Direction = direction;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Deconstruct(out double x, out double y,out double i, out double j)
        {
            x = this.Location.X;
            y = this.Location.Y;
            i = this.Direction.I;
            j = this.Direction.J;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement, SoapElement]
        public Point2D Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
                Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement, SoapElement]
        public Vector2D Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                OnPropertyChanged(nameof(Direction));
                update?.Invoke();
                Refresh();
            }
        }

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
        /// Creates a string representation of this <see cref="Line"/> struct based on the format string
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
            if (this == null) return $"{nameof(Line)}";
            return $"{nameof(Line)}={{{nameof(Location)}:{Location.ConvertToString(format, provider)},{nameof(Direction)}:{Direction.ConvertToString(format, provider)}}}";
        }

        #endregion
    }
}
