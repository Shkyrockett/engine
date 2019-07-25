// <copyright file="BinaryReaderExtensions.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Engine.File
{
    /// <summary>
    /// Experimental Read Variable Length Integer Methods.
    /// </summary>
    public class BinaryReaderObsolete
        : BinaryReader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReaderExtended"/> class, based on the
        /// supplied stream, using System.Text.UTF8Encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <exception cref="ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        public BinaryReaderObsolete(Stream input)
            : base(input)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReaderExtended"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        /// <exception cref="ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        /// <exception cref="ArgumentNullException">encoding is null.</exception>
        public BinaryReaderObsolete(Stream input, Encoding encoding)
            : base(input, encoding)
        { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public long Position
        {
            get { return BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        /// <summary>
        /// Gets the length in bytes of the current stream.
        /// </summary>
        /// <exception cref="NotSupportedException">A class derived from Stream does not support seeking.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public long Length
            => BaseStream.Length;
        #endregion Properties

        #region Methods
        /// <summary>
        /// Read the variable length.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int ReadVariableLength(BinaryReaderExtended reader)
        {
            var value = 0;
            int next;
            do
            {
                next = reader.ReadByte();
                value <<= 7;
                value |= next & 0x7F;
            } while ((next & 0x80) == 0x80);
            return value;
        }

        /// <summary>
        /// Utility function that can read a variable length integer from a binary stream
        /// </summary>
        /// <returns>The integer read</returns>
        //[Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ReadVarInt32()
        {
            uint value = 0;
            byte buffer;
            //// length of 4??
            for (uint i = 0; i < sizeof(uint); i++)
            {
                buffer = ReadByte();
                value <<= 7;
                value += buffer & (uint)0x7F;
                if ((buffer & 0x80) == 0)
                {
                    return value;
                }
            }

            throw new FormatException("Invalid Var Int");
        }

        /// <summary>
        /// Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ReadVarLen()
        {
            uint value;
            if (((value = ReadByte()) & 0x80) != 0)
            {
                value &= 0x7F;
                byte buffer;
                do
                {
                    buffer = ReadByte();
                    value = (uint)((value << 7) + (buffer & 0x7F));
                }

                while ((buffer & 0x80) != 0);
            }

            return value;
        }

        /// <summary>
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReadVarInt()
        {
            var buffer = ReadByte();
            var value = buffer & 0x7F;

            for (var i = 0; i < 3; i++)
            {
                if ((buffer & 0x80) != 0)
                {
                    buffer = ReadByte();
                    value = (value << 7) + (buffer & 0x7F);
                }
                else
                {
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadVarLenx(BinaryReader file)
        {
            int value;
            byte buffer;

            if (((value = file.ReadByte()) & 0x80) != 0)
            {
                value &= 0x7f;
                do
                {
                    buffer = file.ReadByte();
                    value = (value << 7) + (buffer & 0x7f);
                }
                while ((buffer & 0x80) != 0);
            }

            return value;
        }

        /// <summary>
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadVarLen1x(BinaryReader file)
        {
            var buffer = file.ReadByte();
            var value = (uint)(buffer & 0x7f);

            for (var i = 0; i < 3; i++)
            {
                if ((buffer & 0x80) != 0)
                {
                    buffer = file.ReadByte();
                    value = (uint)((value << 7) + (buffer & 0x7f));
                }
                else
                {
                    break;
                }
            }

            return (int)value;
        }

        /// <summary>
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadVarLen2x(BinaryReader file)
        {
            var value = 0;
            byte buffer;
            //// length of 4??
            for (var i = 0; i < Marshal.SizeOf(value); i++)
            {
                buffer = file.ReadByte();
                value <<= 7;
                value += buffer & 0x7F;
                if ((buffer & 0x80) == 0)
                {
                    return value;
                }
            }

            throw new FormatException("Invalid Var Int");
        }
        #endregion Methods
    }
}
