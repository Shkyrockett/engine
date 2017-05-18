// <copyright file="MidiBinaryWriter.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Engine.Midi
{
    /// <summary>
    /// Overloads BinaryWriter to write extended primitive data types in specific encodings to a stream.
    /// </summary>
    public partial class MidiBinaryWriter
        : BinaryWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiBinaryWriter"/> class based on the
        /// supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public MidiBinaryWriter(Stream output)
            : base(output)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiBinaryWriter"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public MidiBinaryWriter(Stream output, Encoding encoding) :
            base(output, encoding)
        { }

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

        /// <summary>
        ///  Writes a 32-bit integer in a compressed format.
        /// </summary>
        /// <param name="value">The 32-bit integer to be written.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        public new void Write7BitEncodedInt(int value)
        {
            base.Write7BitEncodedInt(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>http://stackoverflow.com/questions/4918525/converting-int32-to-24-bit-signed-integer</remarks>
        public void WriteNetworkInt14(int value)
        {
            var v = BitConverter.IsLittleEndian ? IPAddress.HostToNetworkOrder(value) : value;
            byte[] buffer = BitConverter.GetBytes(v);
            // I think there needs to be some shifting here.
            Write(buffer, 0, 2);
        }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        public void WriteNetwork(short value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (ushort)value;
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
        public void WriteNetwork(ushort value)
        {
            //Write(IPAddress.HostToNetworkOrder((short)value));
            var uvalue = value;
            Write((byte)(uvalue >> 8));
            Write((byte)(uvalue & 0xFF));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>http://stackoverflow.com/questions/4918525/converting-int32-to-24-bit-signed-integer</remarks>
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
        public void WriteNetwork(int value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (uint)value;
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
        public void WriteNetwork(uint value)
        {
            //Write(IPAddress.HostToNetworkOrder((int)value));
            var uvalue = value;
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
        public void WriteNetwork(long value)
        {
            //Write(IPAddress.HostToNetworkOrder(value));
            var uvalue = (ulong)value;
            Write((byte)(uvalue >> 56));
            Write((byte)(uvalue >> 48) & 0xFF);
            Write((byte)(uvalue >> 40) & 0xFF);
            Write((byte)(uvalue >> 32) & 0xFF);
            Write((byte)(uvalue >> 24) & 0xFF);
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
        public void WriteNetwork(ulong value)
        {
            //Write(IPAddress.HostToNetworkOrder((long)value));
            var uvalue = value;
            Write((byte)(uvalue >> 56));
            Write((byte)(uvalue >> 48) & 0xFF);
            Write((byte)(uvalue >> 40) & 0xFF);
            Write((byte)(uvalue >> 32) & 0xFF);
            Write((byte)(uvalue >> 24) & 0xFF);
            Write((byte)((uvalue >> 16) & 0xFF));
            Write((byte)((uvalue >> 8) & 0xFF));
            Write((byte)(uvalue & 0xFF));
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
        public void WriteASCIIString(string chars)
        {
            Encoding encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(chars);
            Write7BitEncodedInt(buffer.Length);
            Write(buffer);
        }

        /// <summary>
        /// Writes a UTF8 character array to the current stream and advances the current position
        /// of the stream in accordance with the Encoding used and the specific characters
        /// being written to the stream.
        /// </summary>
        /// <param name="chars">A character array containing the data to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        public void WriteUTF8String(string chars)
        {
            Encoding encoding = new UTF8Encoding();
            byte[] buffer = encoding.GetBytes(chars);
            Write7BitEncodedInt(buffer.Length);
            Write(buffer);
        }
    }

    /// <summary>
    /// Experimental Write Variable Length Integer Methods.
    /// </summary>
    public partial class MidiBinaryWriter
    {
        /// <summary>
        /// Writes a variable-length unsigned integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The value to write</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        /// <remarks>This method uses an array</remarks>
        [Obsolete]
        public void WriteVarLen(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("value", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("value", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            // Allocate a value buffer with room for bit shifting.  
            ulong value = input;

            var len = input != 0 ? (int)Math.Ceiling(Math.Log(input + 1, 0x80)) : 1;

            // In theory, you could have a very long VLV number which was quite large; however, in the standard MIDI file specification,
            // the maximum length of a VLV value is 5 bytes, and the number it represents can not be larger than 4 bytes.
            var buffer = new byte[sizeof(uint) + 1];
            var index = 0;

            // Setup the values in VLV
            do
            {
                buffer[index++] = (byte)(value & 0x7F);
                value >>= 7;
            }
            while (value > 0);

            if (len != index) Debug.Assert(len == index, "Length = " + len + " index = " + index);

            // Write to file.
            while (index > 0)
            {
                index--;
                if (index > 0)
                    Write((byte)(buffer[index] | 0x80));
                else
                    Write(buffer[index]);
            }
        }

        /// <summary>
        /// Writes a variable-length signed integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The Value to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        /// <remarks></remarks>
        [Obsolete]
        public void WriteVarLen0(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("input", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            var value = input;
            ulong buffer = value & 0x7F;

            // Setup the buffer. 
            while ((value >>= 7) != 0)
            {
                buffer <<= 8;
                buffer |= (value & 0x7F) | 0x80;
            }

            // Write to the file.
            while (true)
            {
                // Write the buffer as a truncated byte to the file
                Write((byte)buffer);
                // Shift to get the next byte. 
                if ((buffer & 0x80) == 0) break;
                buffer >>= 8;
            }
        }

        /// <summary>
        /// Writes a variable-length signed integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The Value to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        /// <remarks></remarks>
        [Obsolete]
        public void WriteVarLen1(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("value", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("value", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            var value = (uint)input;
            ulong buffer = value & 0x7f;

            while ((value >>= 7) > 0)
            {
                buffer <<= 8;
                buffer |= 0x80;
                buffer += value & 0x7f;
            }

            while (true)
            {
                Write((byte)(buffer & 0xFF));
                if ((buffer & 0x80) == 0) break;
                buffer >>= 8;
            }
        }

        /// <summary>
        /// Writes a variable-length signed integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The Value to write.</param>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        /// <remarks></remarks>
        [Obsolete]
        public void WriteVarLen2(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("input", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            var value = input;
            ulong buffer = value & 0x7f;

            while ((value >>= 7) != 0)
            {
                buffer <<= 8;
                buffer |= 0x80;
                buffer += (value & 0x7f);
            }

            while (true)
            {
                Write((byte)(buffer & 0xFF));
                if ((buffer & 0x80) == 0) break;
                buffer >>= 8;
            }
        }
    }
}
