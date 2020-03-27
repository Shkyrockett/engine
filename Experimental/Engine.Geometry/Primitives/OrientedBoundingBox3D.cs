using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{Engine.OrientedBoundingBox3D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<OrientedBoundingBox3D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct OrientedBoundingBox3D
        : IEquatable<OrientedBoundingBox3D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }

        public static bool operator ==(OrientedBoundingBox3D left, OrientedBoundingBox3D right) => left.Equals(right);
        public static bool operator !=(OrientedBoundingBox3D left, OrientedBoundingBox3D right) => !(left == right);

        public override bool Equals(object obj) => obj is OrientedBoundingBox3D d && Equals(d);
        public bool Equals(OrientedBoundingBox3D other) => X == other.X && Y == other.Y && Z == other.Z && Height == other.Height && Width == other.Width && Depth == other.Depth;
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, Height, Width, Depth);
    }
}
