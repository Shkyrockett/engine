using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The point node flag struct.
    /// </summary>
    public struct PointNodeFlag
        : INodeItem, IEquatable<PointNodeFlag>
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public int Elements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(PointNodeFlag left, PointNodeFlag right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(PointNodeFlag left, PointNodeFlag right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is PointNodeFlag flag && Equals(flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PointNodeFlag other) => Elements == other.Elements;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => 1573927372 + Elements.GetHashCode();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => $"{nameof(PointNodeFlag)}";
    }
}
