// <copyright file="ASCIIEncoding.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// PCL doesn't have an ASCII encoder, so here is one:
// http://stackoverflow.com/questions/4022281/asciiencoding-in-windows-phone-7
// </references>

using System;
using System.Text;

namespace Engine.File
{
    /// <summary>
    /// PCL doesn't have an ASCII encoder, so here is one:
    /// http://stackoverflow.com/questions/4022281/asciiencoding-in-windows-phone-7
    /// </summary>
    public class ASCIIEncoding
        : Encoding
    {
        /// <summary>
        /// Get the max byte count.
        /// </summary>
        /// <param name="charCount">The charCount.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetMaxByteCount(int charCount) => charCount;

        /// <summary>
        /// Get the max char count.
        /// </summary>
        /// <param name="byteCount">The byteCount.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetMaxCharCount(int byteCount) => byteCount;

        /// <summary>
        /// Get the byte count.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetByteCount(char[] chars, int index, int count) => count;

        /// <summary>
        /// Get the bytes.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public override byte[] GetBytes(char[] chars) => base.GetBytes(chars);

        /// <summary>
        /// Get the char count.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetCharCount(byte[] bytes) => (bytes?.Length).Value;

        /// <summary>
        /// Get the bytes.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <param name="charIndex">The charIndex.</param>
        /// <param name="charCount">The charCount.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="byteIndex">The byteIndex.</param>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// chars
        /// or
        /// bytes
        /// </exception>
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            if (chars is null)
            {
                throw new ArgumentNullException(nameof(chars));
            }

            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            for (var i = 0; i < charCount; i++)
            {
                bytes[byteIndex + i] = (byte)chars[charIndex + i];
            }

            return charCount;
        }

        /// <summary>
        /// Get the char count.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetCharCount(byte[] bytes, int index, int count)
            => count;

        /// <summary>
        /// Get the chars.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="byteIndex">The byteIndex.</param>
        /// <param name="byteCount">The byteCount.</param>
        /// <param name="chars">The chars.</param>
        /// <param name="charIndex">The charIndex.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (chars is null)
            {
                throw new ArgumentNullException(nameof(chars));
            }

            for (var i = 0; i < byteCount; i++)
            {
                chars[charIndex + i] = (char)bytes[byteIndex + i];
            }

            return byteCount;
        }
    }
}
