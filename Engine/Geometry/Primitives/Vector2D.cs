// <copyright file="VectorF.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a vector in 2D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName("Vector2D")]
    [TypeConverter(typeof(Vector2DConverter))]
    public class Vector2D
         : IFormattable
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D AUnit = new Vector2D(1, 1);

        /// <summary>
        /// An Empty <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D Empty = new Vector2D();
        #endregion

        #region Private Fields
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private double i;

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private double j;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <remarks></remarks>
        public Vector2D()
        {
            i = 0;
            j = 0;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        /// <remarks></remarks>
        public Vector2D(double valueX, double valueY)
        {
            i = (float)valueX;
            j = (float)valueY;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="value1X"></param>
        /// <param name="value1Y"></param>
        /// <param name="value2X"></param>
        /// <param name="value2Y"></param>
        /// <remarks></remarks>
        public Vector2D(double value1X, double value1Y, double value2X, double value2Y)
        {
            Vector2D Temp = new Point2D(value1X, value1Y).Delta(new Point2D(value2X, value2Y)).Unit();
            i = Temp.i;
            j = Temp.j;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <remarks></remarks>
        public Vector2D(Point2D value1, Point2D value2)
        {
            Vector2D Temp = value1.Delta(value2).Unit();
            i = Temp.i;
            j = Temp.j;
        }
        #endregion

        #region Properties
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double I
        {
            get { return i; }
            set { i = value; }
        }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double J
        {
            get { return j; }
            set { j = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return i == 0f && j == 0f;
            }
        }

        /// <summary>
        /// Create a Random Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        [Browsable(false)]
        public static Vector2D Random
        {
            get
            {
                Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                return new Vector2D((2 * rnd.Next()) - 1, (2 * rnd.Next()) - 1);
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(Vector2D value, double multiplyer)
        {
            return new Vector2D(value.I * multiplyer, value.J * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(double multiplyer, Vector2D value)
        {
            return new Vector2D(value.I * multiplyer, value.J * multiplyer);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="Value">The Vector2D</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(Vector2D Value, double divisor)
        {
            return new Vector2D(Value.i / divisor, Value.j / divisor);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="Value">The Vector2D</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(double Value, Vector2D divisor)
        {
            return new Vector2D(Value / divisor.i, Value / divisor.j);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Vector2D value1, double value2)
        {
            return value1.Add(value2);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator +(Vector2D value1, Point2D value2)
        {
            return value1.Add(value2);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Vector2D value1, Vector2D value2)
        {
            return value1.Add(value2);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Vector2D value1, double value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator -(Vector2D value1, Point2D value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Vector2D value1, Vector2D value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return left.I == right.I && left.J == right.J;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Vector2D to PointF
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(PointF value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// Vector2D to Point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(Point value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// Vector2D to Point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(Point2D value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// PointF to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator PointF(Vector2D value)
        {
            return new PointF((int)value.i, (int)value.j);
        }

        /// <summary>
        /// Point to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator Point(Vector2D value)
        {
            return new Point((int)value.i, (int)value.j);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool Compare(ref Vector2D value1, ref Vector2D value2)
        {
            return (value1.i == value2.i) && (value1.j == value2.j);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PointF)) return false;
            PointF comp = (PointF)obj;
            return
            comp.X == this.I &&
            comp.Y == this.J &&
            comp.GetType().Equals(this.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return I.GetHashCode() ^
                   J.GetHashCode();
        }

        /// <summary>
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Vector data </param>
        /// </summary>
        public static Vector2D Parse(string source)
        {
            TokenizerHelper th = new TokenizerHelper(source, CultureInfo.InvariantCulture);

            Vector2D value;

            String firstToken = th.NextTokenRequired();

            value = new Vector2D(
                Convert.ToDouble(firstToken, CultureInfo.InvariantCulture),
                Convert.ToDouble(th.NextTokenRequired(), CultureInfo.InvariantCulture));

            // There should be no more tokens in this string.
            th.LastTokenRequired();

            return value;
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // Helper to get the numeric list separator for a given culture.
            string separator = NumberFormatInfo.InvariantInfo.NumberGroupSeparator;
            return String.Format(provider,
                                 "{1:" + format + "}{0}{2:" + format + "}",
                                 separator,
                                 i,
                                 j);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Vector2D";
            return "Vector{X=" + I.ToString(CultureInfo.CurrentCulture) + ",Y=" + J.ToString(CultureInfo.CurrentCulture) + "}";
        }
        #endregion
    }
}
