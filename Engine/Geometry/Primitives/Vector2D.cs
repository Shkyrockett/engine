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
using System.Text;
using System.Windows.Markup;
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
        private double x;

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private double y;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <remarks></remarks>
        public Vector2D()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        /// <remarks></remarks>
        public Vector2D(double valueX, double valueY)
        {
            x = (float)valueX;
            y = (float)valueY;
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
            x = Temp.x;
            y = Temp.y;
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
            x = Temp.x;
            y = Temp.y;
        }
        #endregion

        #region Properties
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double Y
        {
            get { return y; }
            set { y = value; }
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
                return x == 0f && y == 0f;
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
            return new Vector2D(value.X * multiplyer, value.Y * multiplyer);
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
            return new Vector2D(value.X * multiplyer, value.Y * multiplyer);
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
            return new Vector2D(Value.x / divisor, Value.y / divisor);
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
            return new Vector2D(Value / divisor.x, Value / divisor.y);
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
            return left.X == right.X && left.Y == right.Y;
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
            return new PointF((int)value.x, (int)value.y);
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
            return new Point((int)value.x, (int)value.y);
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
            return (value1.x == value2.x) && (value1.y == value2.y);
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
            comp.X == this.X &&
            comp.Y == this.Y &&
            comp.GetType().Equals(this.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^
                   Y.GetHashCode();
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
                                 x,
                                 y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Vector{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) + "}";
        }
        #endregion
    }
}
