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
[TypeConverter(typeof(StructConverter<OrientedBoundingBox4D>))]
[DebuggerDisplay("{ToString()}")]
public struct OrientedBoundingBox4D
    : IEquatable<OrientedBoundingBox4D>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrientedBoundingBox4D"/> struct.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <param name="w">The w.</param>
    /// <param name="height">The height.</param>
    /// <param name="width">The width.</param>
    /// <param name="depth">The depth.</param>
    /// <param name="breadth">The breadth.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OrientedBoundingBox4D(double x, double y, double z, double w, double height, double width, double depth, double breadth)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
        Height = height;
        Width = width;
        Depth = depth;
        Breadth = breadth;
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
    /// Gets or sets the w.
    /// </summary>
    /// <value>
    /// The w.
    /// </value>
    public double W { get; set; }

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
    /// Gets or sets the breadth.
    /// </summary>
    /// <value>
    /// The breadth.
    /// </value>
    public double Breadth { get; set; }

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
    public static bool operator ==(OrientedBoundingBox4D left, OrientedBoundingBox4D right) => left.Equals(right);

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
    public static bool operator !=(OrientedBoundingBox4D left, OrientedBoundingBox4D right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is OrientedBoundingBox4D d && Equals(d);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals([AllowNull] OrientedBoundingBox4D other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W && Height == other.Height && Width == other.Width && Depth == other.Depth && Breadth == other.Breadth;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z, W, Height, Width, Depth, Breadth);
}
