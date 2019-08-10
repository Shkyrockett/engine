using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The ellipse node flag struct.
    /// </summary>
    public struct EllipseNodeFlag
        : INodeItem, IEquatable<EllipseNodeFlag>
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
        public static bool operator ==(EllipseNodeFlag left, EllipseNodeFlag right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(EllipseNodeFlag left, EllipseNodeFlag right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is EllipseNodeFlag flag && Equals(flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EllipseNodeFlag other) => Elements == other.Elements;

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
        public string ToString(string format, IFormatProvider formatProvider) => $"{nameof(EllipseNodeFlag)}";
    }
}
