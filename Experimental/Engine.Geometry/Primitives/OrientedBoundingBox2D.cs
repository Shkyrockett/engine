using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{Engine.OrientedBoundingBox2D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<OrientedBoundingBox2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct OrientedBoundingBox2D
        : IEquatable<OrientedBoundingBox2D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public static bool operator ==(OrientedBoundingBox2D left, OrientedBoundingBox2D right) => left.Equals(right);
        public static bool operator !=(OrientedBoundingBox2D left, OrientedBoundingBox2D right) => !(left == right);

        public override bool Equals(object obj) => obj is OrientedBoundingBox2D d && Equals(d);
        public bool Equals(OrientedBoundingBox2D other) => X == other.X && Y == other.Y && Height == other.Height && Width == other.Width;
        public override int GetHashCode() => HashCode.Combine(X, Y, Height, Width);
    }
}
