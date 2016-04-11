// <copyright file="BinaryWriterEx.cs" >
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

namespace Engine.File
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Overloads BinaryWriter to write extended primitive data types in specific encodings to a stream.
    /// </summary>
    public class BinaryWriterExtensions 
        : BinaryWriter
    {
        /// <summary>
        /// Initializes a new instance of the System.IO.BinaryWriterEx class based on the
        /// supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriterExtensions(Stream output)
            : base(output)
        { }

        /// <summary>
        /// Initializes a new instance of the System.IO.BinaryWriterEx class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriterExtensions(Stream output, Encoding encoding) :
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
        {
            get { return BaseStream.Length; }
        }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream
        /// position by two bytes.
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        public void WriteNetwork(short value)
        {
            Write(IPAddress.HostToNetworkOrder(value));
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the
        /// stream position by four bytes.
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        public void WriteNetwork(int value)
        {
            Write(IPAddress.HostToNetworkOrder(value));
        }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the
        /// stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        public void WriteNetwork(long value)
        {
            Write(IPAddress.HostToNetworkOrder(value));
        }

        /// <summary>
        /// Writes a variable-length unsigned integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The value to write</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        /// <remarks>This method uses an array</remarks>
        public void WriteVarLen(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("value", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("value", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            // Allocate a value buffer with room for bit shifting.  
            ulong value = input;

            int len = input != 0 ? (int)Math.Ceiling(Math.Log(input + 1, 0x80)) : 1;

            // In theory, you could have a very long VLV number which was quite large; however, in the standard MIDI file specification,
            // the maximum length of a VLV value is 5 bytes, and the number it represents can not be larger than 4 bytes.
            byte[] buffer = new byte[sizeof(uint) + 1];
            int index = 0;

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
                {
                    Write((byte)(buffer[index] | 0x80));
                }
                else
                {
                    Write(buffer[index]);
                }
            }
        }

        /// <summary>
        /// Writes a variable-length signed integer to the current stream and advances the
        /// stream position by the size of the integer in bytes.
        /// </summary>
        /// <param name="input">The Value to write.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        /// <remarks></remarks>
        public void WriteVarLen0(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("input", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            uint value = input;
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
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        /// <remarks></remarks>
        public void WriteVarLen1(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("value", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("value", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            uint value = input;
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
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        /// <remarks></remarks>
        public void WriteVarLen2(uint input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input.ToString(), "Cannot write a negative Var Int.");
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("input", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            uint value = input;
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
