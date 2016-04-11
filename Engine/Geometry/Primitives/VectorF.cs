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
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a vector in 2D coordinate space (float precision floating-point coordinates).
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName("VectorF")]
    public class VectorF
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="VectorF"/>.
        /// </summary>
        public static readonly VectorF AUnit = new VectorF(1, 1);

        /// <summary>
        /// An Empty <see cref="VectorF"/>.
        /// </summary>
        public static readonly VectorF Empty = new VectorF();
        #endregion

        #region Private Fields
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private float x;

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        private float y;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <remarks></remarks>
        public VectorF()
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
        public VectorF(float valueX, float valueY)
        {
            x = valueX;
            y = valueY;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        /// <remarks></remarks>
        public VectorF(double valueX, double valueY)
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
        public VectorF(float value1X, float value1Y, float value2X, float value2Y)
        {
            VectorF Temp = new PointF(value1X, value1Y).Delta(new PointF(value2X, value2Y)).Unit();
            x = Temp.x;
            y = Temp.y;
        }

        /// <summary>
        /// Create a new Vector2D
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <remarks></remarks>
        public VectorF(PointF value1, PointF value2)
        {
            VectorF Temp = value1.Delta(value2).Unit();
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
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VectorF"/> is empty.
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
        public static VectorF operator *(VectorF value, double multiplyer)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator *(double multiplyer, VectorF value)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator *(VectorF value, float multiplyer)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator *(float multiplyer, VectorF value)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator *(VectorF value, int multiplyer)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator *(int multiplyer, VectorF value)
        {
            return new VectorF(value.X * multiplyer, value.Y * multiplyer);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="Value">The Vector2D</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>A Vector2D Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator /(VectorF Value, float divisor)
        {
            return new VectorF(Value.x / divisor, Value.y / divisor);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF operator +(VectorF value1, Point value2)
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
        public static PointF operator +(PointF value1, VectorF value2)
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
        public static PointF operator +(VectorF value1, PointF value2)
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
        public static VectorF operator +(VectorF value1, VectorF value2)
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
        public static PointF operator -(VectorF value1, PointF value2)
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
        public static PointF operator -(PointF value1, VectorF value2)
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
        public static VectorF operator -(VectorF value1, VectorF value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(VectorF left, VectorF right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(VectorF left, VectorF right)
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
        public static implicit operator VectorF(PointF value)
        {
            return new VectorF(value.X, value.Y);
        }

        /// <summary>
        /// PointF to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator PointF(VectorF value)
        {
            return new PointF((int)value.x, (int)value.y);
        }

        /// <summary>
        /// Vector2D to Point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator VectorF(Point value)
        {
            return new VectorF(value.X, value.Y);
        }

        /// <summary>
        /// Point to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator Point(VectorF value)
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
        public static bool Compare(ref VectorF value1, ref VectorF value2)
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
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector ToVector()
        {
            return Vector.Truncate(this);
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
