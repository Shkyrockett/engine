// <copyright file="MidiBinaryWriter.cs" company="Shkyrockett">
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.Math;

namespace Engine.File
{
    /// <summary>
    /// Experimental Write Variable Length Integer Methods.
    /// </summary>
    public class BinaryWriterObsolete
        : BinaryWriter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriterExtended"/> class based on the
        /// supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriterObsolete(Stream output)
            : base(output)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWriterExtended"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriterObsolete(Stream output, Encoding encoding)
            : base(output, encoding)
        { }
        #endregion Constructors

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
        #endregion Properties

        #region Methods
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
            if (input < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            }
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("value", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            // Allocate a value buffer with room for bit shifting.  
            ulong value = input;

            var len = input != 0 ? (int)Ceiling(Log(input + 1, 0x80)) : 1;

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

            if (len != index)
            {
                Debug.Assert(len == index, "Length = " + len + " index = " + index);
            }

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
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">The stream is closed.</exception>
        [Obsolete]
        public void WriteVarLen0(uint input)
        {
            if (input < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            }
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
                if ((buffer & 0x80) == 0)
                {
                    break;
                }

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
        [Obsolete]
        public void WriteVarLen1(uint input)
        {
            if (input < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            }
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
                if ((buffer & 0x80) == 0)
                {
                    break;
                }

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
        [Obsolete]
        public void WriteVarLen2(uint input)
        {
            if (input < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(input), input.ToString(), "Cannot write a negative Var Int.");
            }
            //if (input > MidiMaxDeltaTime) throw new ArgumentOutOfRangeException("input", input.ToString(), "Maximum allowed Var Int is 0x0FFFFFFF.");

            var value = input;
            ulong buffer = value & 0x7f;

            while ((value >>= 7) != 0)
            {
                buffer <<= 8;
                buffer |= 0x80;
                buffer += value & 0x7f;
            }

            while (true)
            {
                Write((byte)(buffer & 0xFF));
                if ((buffer & 0x80) == 0)
                {
                    break;
                }

                buffer >>= 8;
            }
        }
        #endregion Methods
    }
}
