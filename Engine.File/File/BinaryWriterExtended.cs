// <copyright file="BinaryWriterExtended.cs" company="Shkyrockett">
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>
// <notes></notes>
// <references>
// </references>

using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Engine.File
{
    /// <summary>
    /// Overloads BinaryWriter to write extended primitive data types in specific encodings to a stream.
    /// </summary>
    public class BinaryWriterExtended
        : BinaryWriter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriterExtended"/> class based on the
        /// supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriterExtended(Stream output)
            : base(output)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriterExtended"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriterExtended(Stream output, Encoding encoding)
            : base(output, encoding)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        public long Position
        {
            get { return BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        public long Length
            => BaseStream.Length;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkUInt14(ushort value)
        {
            //var v = BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(value) : value;
            //byte[] buffer = BitConverter.GetBytes(v);
            //// I think there needs to be some shifting here.
            //Write(buffer, 0, 2);

            var uvalue = (ushort)value;
            Write((byte)(uvalue >> 7));
            Write((byte)(uvalue & 0x7F));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkInt14(short value)
        {
            //var v = BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(value) : value;
            //byte[] buffer = BitConverter.GetBytes(v);
            //// I think there needs to be some shifting here.
            //Write(buffer, 0, 2);

            var uvalue = (ushort)value;
            Write((byte)(uvalue >> 7));
            Write((byte)(uvalue & 0x7F));
        }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(ushort value)
            => WriteNetworkUInt16(value);

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkUInt16(ushort value)
        {
            //Write(IPAddress.HostToNetworkOrder((short)value));
            var uvalue = value;
            Write((byte)(uvalue >> 8));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(short value)
            => WriteNetworkInt16(value);

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkInt16(short value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (ushort)value;
            Write((byte)(uvalue >> 8));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>http://stackoverflow.com/questions/4918525/converting-int32-to-24-bit-signed-integer</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkUInt24(uint value)
        {
            //var v = BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(value) : value;
            //byte[] buffer = BitConverter.GetBytes(v);
            //Write(buffer, 0, 3);
            var uvalue = (uint)value;
            Write((byte)(uvalue >> 16));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>http://stackoverflow.com/questions/4918525/converting-int32-to-24-bit-signed-integer</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkInt24(int value)
        {
            //var v = BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(value) : value;
            //byte[] buffer = BitConverter.GetBytes(v);
            //Write(buffer, 0, 3);
            var uvalue = (uint)value;
            Write((byte)(uvalue >> 16));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the
        /// stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(uint value)
            => WriteNetworkUInt32(value);

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the
        /// stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkUInt32(uint value)
        {
            //Write(IPAddress.HostToNetworkOrder((int)value));
            var uvalue = value;
            Write((byte)(uvalue >> 24));
            Write((byte)((uvalue >> 16) & 0xFF));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the
        /// stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(int value)
            => WriteNetworkInt32(value);

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the
        /// stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkInt32(int value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (uint)value;
            Write((byte)(uvalue >> 24));
            Write((byte)((uvalue >> 16) & 0xFF));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the
        /// stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(ulong value)
            => WriteNetworkUInt64(value);

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the
        /// stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkUInt64(ulong value)
        {
            //Write(IPAddress.HostToNetworkOrder((long)value));
            var uvalue = value;
            Write((byte)(uvalue >> 56));
            Write((byte)((uvalue >> 48) & 0xFF));
            Write((byte)((uvalue >> 40) & 0xFF));
            Write((byte)((uvalue >> 32) & 0xFF));
            Write((byte)((uvalue >> 24) & 0xFF));
            Write((byte)((uvalue >> 16) & 0xFF));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the
        /// stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetwork(long value)
            => WriteNetworkInt64(value);

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the
        /// stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNetworkInt64(long value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (ulong)value;
            Write((byte)(uvalue >> 56));
            Write((byte)((uvalue >> 48) & 0xFF));
            Write((byte)((uvalue >> 40) & 0xFF));
            Write((byte)((uvalue >> 32) & 0xFF));
            Write((byte)((uvalue >> 24) & 0xFF));
            Write((byte)((uvalue >> 16) & 0xFF));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        ///  Writes a 32-bit integer in a compressed format.
        /// </summary>
        /// <param name="value">The 32-bit integer to be written.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write7BitEncodedUInt(uint value)
            => base.Write7BitEncodedInt((int)value);

        /// <summary>
        ///  Writes a 32-bit integer in a compressed format.
        /// </summary>
        /// <param name="value">The 32-bit integer to be written.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new void Write7BitEncodedInt(int value)
            => base.Write7BitEncodedInt(value);

        /// <summary>
        /// Writes a UTF8 character array to the current stream and advances the current position
        /// of the stream in accordance with the Encoding used and the specific characters
        /// being written to the stream.
        /// </summary>
        /// <param name="chars">A character array containing the data to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUTF8String(string chars)
        {
            Encoding encoding = new UTF8Encoding();
            byte[] buffer = encoding.GetBytes(chars);
            Write7BitEncodedInt(buffer.Length);
            Write(buffer);
        }

        /// <summary>
        /// Writes an ASCII character array to the current stream and advances the current position
        /// of the stream in accordance with the Encoding used and the specific characters
        /// being written to the stream.
        /// </summary>
        /// <param name="chars">A character array containing the data to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteASCIIString(string chars)
        {
            Encoding encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(chars);
            Write7BitEncodedInt(buffer.Length);
            Write(buffer);
        }

        #endregion
    }
}
