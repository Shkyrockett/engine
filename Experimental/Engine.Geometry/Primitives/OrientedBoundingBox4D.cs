using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{Engine.OrientedBoundingBox4D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<OrientedBoundingBox4D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct OrientedBoundingBox4D
        : IEquatable<OrientedBoundingBox4D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Breadth { get; set; }

        public static bool operator ==(OrientedBoundingBox4D left, OrientedBoundingBox4D right) => left.Equals(right);
        public static bool operator !=(OrientedBoundingBox4D left, OrientedBoundingBox4D right) => !(left == right);

        public override bool Equals(object obj) => obj is OrientedBoundingBox4D d && Equals(d);
        public bool Equals(OrientedBoundingBox4D other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W && Height == other.Height && Width == other.Width && Depth == other.Depth && Breadth == other.Breadth;
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W, Height, Width, Depth, Breadth);
    }
}
