// <copyright file="ASCIIEncoding.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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

using System.Text;

namespace Engine.Midi
{
    /// <summary>
    /// PCL doesn't have an ASCII encoder, so here is one:
    /// http://stackoverflow.com/questions/4022281/asciiencoding-in-windows-phone-7
    /// </summary>
    public class ASCIIEncoding
        : Encoding
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="charCount"></param>
        /// <returns></returns>
        public override int GetMaxByteCount(int charCount)
            => charCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public override int GetMaxCharCount(int byteCount)
            => byteCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int GetByteCount(char[] chars, int index, int count)
            => count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public override byte[] GetBytes(char[] chars)
            => base.GetBytes(chars);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override int GetCharCount(byte[] bytes)
            => bytes.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="charIndex"></param>
        /// <param name="charCount"></param>
        /// <param name="bytes"></param>
        /// <param name="byteIndex"></param>
        /// <returns></returns>
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            for (var i = 0; i < charCount; i++)
                bytes[byteIndex + i] = (byte)chars[charIndex + i];
            return charCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int GetCharCount(byte[] bytes, int index, int count)
            => count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="byteIndex"></param>
        /// <param name="byteCount"></param>
        /// <param name="chars"></param>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            for (var i = 0; i < byteCount; i++)
                chars[charIndex + i] = (char)bytes[byteIndex + i];
            return byteCount;
        }
    }
}
