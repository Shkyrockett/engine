// <copyright file="Vector.cs" >
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
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a vector in 2D coordinate space.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName("Vector")]
    public class Vector
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="Vector"/>.
        /// </summary>
        public static readonly Vector AUnit = new Vector(1, 1);

        /// <summary>
        /// An Empty <see cref="Vector"/>.
        /// </summary>
        public static readonly Vector Empty = new Vector();
        #endregion

        #region Private Fields
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private int x;

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private int y;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Vector()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integer"></param>
        public Vector(int integer)
        {
            unchecked
            {
                x = (short)MathExtensions.LOWORD(integer);
                y = (short)MathExtensions.HIWORD(integer);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        /// <remarks></remarks>
        public Vector(int valueX, int valueY)
        {
            x = valueX;
            y = valueY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="value1X"></param>
        /// <param name="value1Y"></param>
        /// <param name="value2X"></param>
        /// <param name="value2Y"></param>
        /// <remarks></remarks>
        public Vector(int value1X, int value1Y, int value2X, int value2Y)
        {
            Vector Temp = new Point(value1X, value1Y).Delta(new Point(value2X, value2Y)).Unit();
            x = Temp.x;
            y = Temp.y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <remarks></remarks>
        public Vector(Point value1, Point value2)
        {
            Vector Temp = value1.Delta(value2).Unit();
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
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return x == 0 && y == 0; }
        }

        /// <summary>
        /// Create a Random Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        [Browsable(false)]
        public static Vector Random
        {
            get
            {
                Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                return new Vector((2 * rnd.Next()) - 1, (2 * rnd.Next()) - 1);
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
        public static Vector operator *(Vector value, double multiplyer)
        {
            return new Vector((int)(value.X * multiplyer), (int)(value.Y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator *(double multiplyer, Vector value)
        {
            return new Vector((int)(value.X * multiplyer), (int)(value.Y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator *(Vector value, float multiplyer)
        {
            return new Vector((int)(value.X * multiplyer), (int)(value.Y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator *(float multiplyer, Vector value)
        {
            return new Vector((int)(value.X * multiplyer), (int)(value.Y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator *(Vector value, int multiplyer)
        {
            return new Vector(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator *(int multiplyer, Vector value)
        {
            return new Vector(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="Value">The Vector2D</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>A Vector2D Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector operator /(Vector Value, float divisor)
        {
            return new Vector((int)(Value.x / divisor), (int)(Value.y / divisor));
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector operator +(Vector value1, Point value2)
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
        public static Vector operator +(Vector value1, PointF value2)
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
        public static Vector operator +(Vector value1, Vector value2)
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
        public static Point operator -(Point value1, Vector value2)
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
        public static Vector operator -(Vector value1, Point value2)
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
        public static Vector operator -(Vector value1, Vector value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector left, Vector right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Vector left, Vector right)
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
        public static implicit operator Vector(Point value)
        {
            return new Vector(value.X, value.Y);
        }

        /// <summary>
        /// PointF to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator PointF(Vector value)
        {
            return new PointF(value.x, value.y);
        }

        /// <summary>
        /// Point to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator Point(Vector value)
        {
            return new Point(value.x, value.y);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Converts a PointF to a Point by performing a ceiling operation on
        /// all the coordinates.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector Ceiling(VectorF value)
        {
            return new Vector((int)Math.Ceiling(value.X), (int)Math.Ceiling(value.Y));
        }

        /// <summary>
        /// Converts a PointF to a Point by performing a truncate operation on
        /// all the coordinates.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector Truncate(VectorF value)
        {
            return new Vector((int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Converts a PointF to a Point by performing a round operation on
        /// all the coordinates.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector Round(VectorF value)
        {
            return new Vector((int)Math.Round(value.X), (int)Math.Round(value.Y));
        }

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool Compare(ref Vector value1, ref Vector value2)
        {
            return (value1.x == value2.x) && (value1.y == value2.y);
        }

        /// <summary>
        /// Specifies whether this <see cref="Vector"/> contains
        /// the same coordinates as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Point)) return false;
            Point comp = (Point)obj;
            return comp.X == X && comp.Y == Y;
        }

        /// <summary>
        /// Returns a hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return unchecked(x ^ y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) + "}";
        }
        #endregion
    }
}
