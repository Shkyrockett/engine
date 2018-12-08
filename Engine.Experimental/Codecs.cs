// <copyright file="Codecs.cs" company="Shkyrockett" >
//     Copyright © 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.InteropServices;

namespace Engine.Experimental
{
    // Strongly typed string representing the name of a Codec.
    // Open ended to allow extensibility while giving the discoverable feel of an enum for common values.
    // Based off of https://referencesource.microsoft.com/#mscorlib/system/security/cryptography/HashAlgorithmName.cs,4b6821f6a0780784
    // Idea from: https://youtu.be/MuID_T2I7ds?t=6421
    // https://apisof.net/catalog/System.Runtime.InteropServices.OSPlatform
    // https://github.com/dotnet/corefx/blob/master/src/System.Runtime.InteropServices.RuntimeInformation/src/System/Runtime/InteropServices/RuntimeInformation/OSPlatform.cs

    /// <summary>
    /// The Codecs struct.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Codecs
        : IEquatable<Codecs>
    {
        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "Midi"
        /// </summary>
        public static Codecs MiDi => new Codecs("Midi");

        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "Wav"
        /// </summary>
        public static Codecs Wav => new Codecs("Wav");

        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "MP3"
        /// </summary>
        public static Codecs MP3 => new Codecs("MP3");

        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "Jpeg"
        /// </summary>
        public static Codecs Jpeg => new Codecs("Jpeg");

        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "Gif"
        /// </summary>
        public static Codecs Gif => new Codecs("Gif");

        /// <summary>
        /// Gets a <see cref="Codecs" /> representing "Png"
        /// </summary>
        public static Codecs Png => new Codecs("Png");

        /// <summary>
        /// Initializes a new instance of the <see cref="Codecs"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        private Codecs(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Empty String is not an acceptable codec name.", nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Codecs left, Codecs right) => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Codecs left, Codecs right) => !(left == right);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => Name ?? string.Empty;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj) => obj is Codecs && Equals((Codecs)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(Codecs other) =>
            // NOTE: intentionally ordinal and case sensitive, matches CNG.
            Name == other.Name;

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode() => Name == null ? 0 : Name.GetHashCode();
    }
}
