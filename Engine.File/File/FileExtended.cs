// <copyright file="FileEx.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.IO;
using System.Text;
using static System.Math;

namespace Engine.File
{
    /// <summary>
    /// Extended File processing class.
    /// </summary>
    public static class FileExtended
    {
        /// <summary>
        /// The maximum value of a Delta Time signature.
        /// </summary>
        public const uint MidiMaxDeltaTime = 0x0FFFFFFF;

        /// <summary>
        /// Most significant byte high bit overflow identifier.
        /// </summary>
        public const byte VarLenHighBit = 0x7F;

        /// <summary>
        /// Clear bits identifier.
        /// </summary>
        public const byte VarLenClearBits = 0x80; // 128

        /// <summary>
        /// Finds the number of bytes a variable length value would be written to.
        /// </summary>
        /// <param name="value">The value to find the byte length from.</param>
        /// <returns>An integer value indicating the number of bytes the value would take up when saved in variable length.</returns>
        // If the value is 0, return 1 byte. Otherwise calculate the number of bytes. 
        // The number of bytes to use is found by rounding up the 128th log of the value provided plus one. 
        public static int VarLenByteLength(uint value)
            => value != 0 ? (int)Ceiling(Log(value + 1, VarLenClearBits)) : 1;

        /// <summary>
        /// Extension method to read a string of specified <see cref="char"/>s length from a file stream
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="length">The number of <see cref="char"/>s to read</param>
        /// <returns>A string as read from the file stream.</returns>
        public static string ReadString(this Stream stream, int length)
        {
            var reader = new StreamReader(stream);
            var buffer = new char[length];
            reader.Read(buffer, 0, length);
            return new string(buffer);
        }

        /// <summary>
        /// Extension method to read a string of specified <see cref="char"/>s length from a file stream
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="length">The number of <see cref="char"/>s to read</param>
        /// <returns>A string as read from the file stream.</returns>
        public static string ReadString(this StreamReader reader, int length)
        {
            var buffer = new char[length];
            reader.Read(buffer, 0, length);
            return new string(buffer);
        }

        /// <summary>
        /// Extension method to read a string of specified <see cref="char"/>s length from a file stream
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="length">The number of <see cref="char"/>s to read</param>
        /// <returns>A string as read from the file stream.</returns>
        public static string ReadString(this BinaryReader reader, int length)
        {
            var buffer = new char[length];
            reader.Read(buffer, 0, length);
            return new string(buffer);
        }

        /// <summary>
        /// Extension method to read a string of specified <see cref="char"/>s length from a file stream
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="length">The number of <see cref="char"/>s to read</param>
        /// <returns>A string as read from the file stream.</returns>
        public static string ReadString16(this BinaryReader reader, int length)
        {
            var buffer = new byte[length];
            reader.Read(buffer, 0, length);
            return Encoding.Unicode.GetString(buffer);
        }

        /// <summary>
        /// Write the string.
        /// </summary>
        /// <param name="bw">The bw.</param>
        /// <param name="value">The value.</param>
        public static void WriteString(this BinaryWriter bw, string value)
            => bw.Write(Encoding.ASCII.GetBytes(value));
    }
}
