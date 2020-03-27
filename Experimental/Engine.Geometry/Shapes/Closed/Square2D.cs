using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.Square2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Square2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Square2D
        : IClosedShape, IEquatable<Square2D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Side { get; set; }

        public static bool operator ==(Square2D left, Square2D right) => left.Equals(right);
        public static bool operator !=(Square2D left, Square2D right) => !(left == right);

        public override bool Equals(object obj) => obj is Square2D d && Equals(d);
        public bool Equals(Square2D other) => X == other.X && Y == other.Y && Side == other.Side;
        public override int GetHashCode() => HashCode.Combine(X, Y, Side);
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
