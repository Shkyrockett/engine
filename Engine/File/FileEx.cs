// <copyright file="FileEx.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Math;

namespace Engine.File
{
    /// <summary>
    /// Extended File processing class.
    /// </summary>
    public static class FileEx
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
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="value"></param>
        public static void WriteString(this BinaryWriter bw, string value)
        {
            bw.Write(Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        public static long HostToNetworkOrder(long host)
            => ((HostToNetworkOrder((int)host) & 0xffffffffL) << 0x20) | (HostToNetworkOrder((int)(host >> 0x20)) & 0xffffffffL);

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        public static int HostToNetworkOrder(int host)
            => (((HostToNetworkOrder((short)host) & 0xffff) << 0x10) | (HostToNetworkOrder((short)(host >> 0x10)) & 0xffff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        public static short HostToNetworkOrder(short host)
            => (short)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A long value, expressed in host byte order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static long NetworkToHostOrder(long network)
            => HostToNetworkOrder(network);

        /// <summary>Converts an integer value from network byte order to host byte order.</summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static int NetworkToHostOrder(int network)
            => HostToNetworkOrder(network);

        /// <summary>Converts a short value from network byte order to host byte order.</summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A short value, expressed in host byte order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static short NetworkToHostOrder(short network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        public static ulong HostToNetworkOrder(ulong host)
            => (ulong)(((HostToNetworkOrder((uint)host) & 0xffffffffL) << 0x20) | (HostToNetworkOrder((uint)(host >> 0x20)) & 0xffffffffL));

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        public static uint HostToNetworkOrder(uint host)
            => (ushort)(((HostToNetworkOrder((ushort)host) & 0xffff) << 0x10) | (HostToNetworkOrder((ushort)(host >> 0x10)) & 0xffff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        public static ushort HostToNetworkOrder(ushort host)
            => (ushort)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A long value, expressed in host byte order.</returns>
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static ulong NetworkToHostOrder(ulong network)
            => HostToNetworkOrder(network);

        /// <summary>Converts an integer value from network byte order to host byte order.</summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static uint NetworkToHostOrder(uint network)
            => HostToNetworkOrder(network);

        /// <summary>Converts a short value from network byte order to host byte order.</summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A short value, expressed in host byte order.</returns>
        [TargetedPatchingOptOut("Performance critical to in-line this type of method across NGen image boundaries")]
        public static ushort NetworkToHostOrder(ushort network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int HighWord(int n)
            => (n >> 16) & 0xffff;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int LowWord(int n)
            => n & 0xffff;
    }
}
