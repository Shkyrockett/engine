// <copyright file="Line.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 2D Line Structure
    /// </summary>
    /// <structure>Engine.Geometry.Line2D</structure>
    /// <remarks></remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName("Line")]
    [XmlType(TypeName = "line", Namespace = "http://www.w3.org/2000/svg")]
    public class Line
        : Shape
    {
        #region Implementations

        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        /// <remarks></remarks>
        public static readonly Line Empty = new Line();

        #endregion

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
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Line((double x, double y, double i, double j) tuple)
            : this(tuple.x, tuple.y, tuple.i, tuple.j)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public Line(double x, double y, double i, double j)
            : this(new Point2D(x, y), new Vector2D(i, j))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <remarks></remarks>
        public Line(Point2D Point, double RadAngle)
            : this(Point.X, Point.Y, Cos(RadAngle), Sin(RadAngle))
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        /// <remarks></remarks>
        public Line(Point2D a, Point2D b)
            : this(a.X, a.Y, b.X - a.X, b.Y - a.Y)
        { }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Deconstruct(out double x, out double y, out double i, out double j)
        {
            x = Location.X;
            y = Location.Y;
            i = Direction.I;
            j = Direction.J;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D Location
        {
            get { return location; }
            set
            {
                location = value;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Vector2D Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                ClearCache();
                OnPropertyChanged(nameof(Direction));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Rectangle2D Bounds
            => new Rectangle2D(location, location + direction);

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Line)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Line)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Line)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Line)} has been deserialized.");
        //}

        //#endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolators.Linear(location, location + direction, t);

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
