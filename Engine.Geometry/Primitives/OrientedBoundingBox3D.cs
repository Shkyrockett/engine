using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine;

/// <summary>
/// 
/// </summary>
/// <seealso cref="System.IEquatable{T}" />
[DataContract, Serializable]
[TypeConverter(typeof(StructConverter<OrientedBoundingBox3D>))]
[DebuggerDisplay("{ToString()}")]
public struct OrientedBoundingBox3D
    : IEquatable<OrientedBoundingBox3D>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrientedBoundingBox3D"/> struct.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <param name="height">The height.</param>
    /// <param name="width">The width.</param>
    /// <param name="depth">The depth.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OrientedBoundingBox3D(double x, double y, double z, double height, double width, double depth)
    {
        X = x;
        Y = y;
        Z = z;
        Height = height;
        Width = width;
        Depth = depth;
    }

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    /// <value>
    /// The x.
    /// </value>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the z.
    /// </summary>
    /// <value>
    /// The z.
    /// </value>
    public double Z { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    public double Width { get; set; }

    /// <summary>
    /// Gets or sets the depth.
    /// </summary>
    /// <value>
    /// The depth.
    /// </value>
    public double Depth { get; set; }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(OrientedBoundingBox3D left, OrientedBoundingBox3D right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(OrientedBoundingBox3D left, OrientedBoundingBox3D right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is OrientedBoundingBox3D d && Equals(d);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals([AllowNull] OrientedBoundingBox3D other) => X == other.X && Y == other.Y && Z == other.Z && Height == other.Height && Width == other.Width && Depth == other.Depth;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z, Height, Width, Depth);
}
