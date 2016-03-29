// <copyright file="VectorF.cs" company="Shkyrockett">
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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a 2D Vector.
    /// </summary>
    public class VectorF
    {
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
        /// Create an Empty Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        public static Vector Empty
        {
            get { return new Vector(); }
        }

        /// <summary>
        /// Create a Random Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        public static Vector Random
        {
            get
            {
                Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                return new Vector((2 * rnd.Next()) - 1, (2 * rnd.Next()) - 1);
            }
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        public static Vector AUnit
        {
            get { return new Vector(1, 1); }
        }

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Unit(VectorF value)
        {
            return VectorF.Scale(value, (1 / Math.Sqrt(((value.x * value.x) + (value.y * value.y)))));
        }

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
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public double DotProduct(PointF value)
        {
            return ((x * value.X) + (y * value.Y));
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public double DotProduct(VectorF value)
        {
            return ((x * value.X) + (y * value.Y));
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="VectorA">First Point</param>
        /// <param name="VectorB">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        public static double DotProduct(VectorF VectorA, VectorF VectorB)
        {
            return VectorA.DotProduct(VectorB);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="VectorA">First Point</param>
        /// <param name="VectorB">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        public static double DotProduct(PointF VectorA, VectorF VectorB)
        {
            return VectorA.DotProduct(VectorB);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="VectorA">First Point</param>
        /// <param name="VectorB">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        public static double DotProduct(VectorF VectorA, PointF VectorB)
        {
            return VectorA.DotProduct(VectorB);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="VectorA">First Point</param>
        /// <param name="VectorB">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public static double DotProduct(PointF VectorA, PointF VectorB)
        {
            return ((VectorA.X * VectorB.X) + (VectorA.Y * VectorB.Y));
        }

        /// <summary>
        /// Cross Product of a corner
        /// </summary>
        /// <param name="value"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        public double CrossProduct(VectorF value)
        {
            return (x * value.Y) - (y * value.X);
        }

        /// <summary>
        /// Cross Product a Perpendicular dot product of two vectors.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>
        /// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        /// </returns>
        /// <remarks>Graphics Gems IV, page 139.</remarks>
        public static double CrossProduct(VectorF value1, VectorF value2)
        {
            return value1.CrossProduct(value2);
        }

        /// <summary>
        /// Cross Product a Perpendicular dot product of two vectors.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>
        /// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        /// </returns>
        /// <remarks>Graphics Gems IV, page 139.</remarks>
        public static double CrossProduct(PointF value1, PointF value2)
        {
            return value1.CrossProduct(value2);
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public VectorF Invert()
        {
            return new VectorF((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Invert(VectorF value)
        {
            return value.Invert();
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Invert(PointF value)
        {
            return new VectorF((1 / value.X), (1 / value.Y));
        }

        /// <summary>
        /// Modulus of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public double Modulus()
        {
            return Math.Pow((x * x) + (y * y), 0.5F);
        }

        /// <summary>
        /// Modulus of a Vector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Modulus(VectorF value)
        {
            return value.Modulus();
        }

        /// <summary>
        /// Perpendicular of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks>To get the perpendicular in two dimensions use X = -Y, Y = X</remarks>
        public VectorF Perpendicular()
        {
            return new VectorF(y * -1, x);
        }

        /// <summary>
        /// Perpendicular of a Vector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular in two dimensions use X = -Y, Y = X</remarks>
        public static VectorF Perpendicular(VectorF value)
        {
            return value.Perpendicular();
        }

        /// <summary>
        /// Perpendicular of a Vector
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular in two dimensions use X = -Y, Y = X</remarks>
        public static VectorF Perpendicular(PointF value)
        {
            return new VectorF(value.Y * -1, value.X);
        }

        /// <summary>
        /// Multiply Vector
        /// </summary>
        /// <param name="value">Second Vector</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public VectorF Multiply(VectorF value)
        {
            return new VectorF(x * value.X, y * value.Y);
        }

        /// <summary>
        /// Multiply Vector
        /// </summary>
        /// <param name="value">Second Vector</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public VectorF Multiply(PointF value)
        {
            return new VectorF(x * value.X, y * value.Y);
        }

        /// <summary>
        /// Multiply Point
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Multiply(PointF value1, PointF value2)
        {
            return new VectorF((value1.X * value2.X), (value1.Y * value2.Y));
        }

        /// <summary>
        /// Multiply Vector
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Multiply(VectorF value1, VectorF value2)
        {
            return value1.Multiply(value2);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public VectorF Scale(int multiplyer)
        {
            return new VectorF((x * multiplyer), (y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="multiplyer">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF Scale(VectorF value, double multiplyer)
        {
            return new VectorF((int)(value.x * multiplyer), (int)(value.y * multiplyer));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplyer">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF Scale(double multiplyer, VectorF value)
        {
            return new VectorF((value.x * multiplyer), (value.y * multiplyer));
        }

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
        /// Divide Point
        /// </summary>
        /// <param name="Value1">First Point</param>
        /// <param name="Value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Divide(VectorF Value1, VectorF Value2)
        {
            return Value1.DotProduct(Value2.Invert());
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="Value">The Vector2D</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF operator /(VectorF Value, VectorF divisor)
        {
            return new VectorF(Value.x / divisor.x, Value.y / divisor.y);
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
        /// Add Vector2D
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Add(VectorF value1, PointF value2)
        {
            return value1.Add(value2);
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Add(VectorF value1, VectorF value2)
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

        /// <summary>
        /// Length of a Vector
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(ref VectorF value1, ref VectorF value2)
        {
            return VectorF.Modulus(value1 - value2);
        }

        /// <summary>
        /// Finds the length of a 2D vector
        /// </summary>
        /// <param name="Value"> Point</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public double Length(VectorF Value)
        {
            return (double)(Math.Sqrt(Math.Pow(Value.x - x, 2) + Math.Pow(Value.y - y, 2)));
        }

        /// <summary>
        /// Finds the Length between two points
        /// </summary>
        /// <param name="Value1">First Point</param>
        /// <param name="Value2">Second Point</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public static double Length(VectorF Value1, VectorF Value2)
        {
            return (double)(Math.Sqrt(Math.Pow(Value2.x - Value1.x, 2) + Math.Pow(Value2.y - Value1.y, 2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            temp.Append(x.ToString());
            temp.Append(", ");
            temp.Append(y.ToString());
            return temp.ToString();
        }
    }
}
