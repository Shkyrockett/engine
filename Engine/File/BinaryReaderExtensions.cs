// <copyright file="BinaryReaderExtensions.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Engine.File
{
    /// <summary>
    /// Overloads BinaryReader to read extended primitive data types as binary values in specific encodings.
    /// </summary>
    public class BinaryReaderExtensions
        : BinaryReader
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReaderExtensions"/> class, based on the
        /// supplied stream, using System.Text.UTF8Encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        public BinaryReaderExtensions(Stream input) :
            base(input)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReaderExtensions"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        /// <exception cref="System.ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">encoding is null.</exception>
        public BinaryReaderExtensions(Stream input, Encoding encoding) :
            base(input, encoding)
        { }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="System.NotSupportedException">The stream does not support seeking.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public long Position
        {
            get { return BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        /// <summary>
        /// Gets the length in bytes of the current stream.
        /// </summary>
        /// <exception cref="System.NotSupportedException">A class derived from Stream does not support seeking.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public long Length => BaseStream.Length;

        #endregion
        #region Methods

        /// <summary>
        /// Reads a 2-byte unsigned 14-bit integer from the current stream using Big-endian
        /// encoding, and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public short ReadNetworkInt14()
        {
            // Save the first byte to add in later.
            short stack = ReadByte();

            // Add the second byte to the buffer.
            short buffer = ReadByte();

            // Shift the buffer seven bits to make room for the first byte.
            buffer <<= 7;

            // Bitwise OR back in the first byte and return the results.
            return buffer |= stack;
        }

        /// <summary>
        /// Reads a 2-byte unsigned 14-bit integer from the current stream using Big-endian
        /// encoding, and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public ushort ReadNetworkUInt14()
        {
            // Save the first byte to add.
            ushort First = ReadByte();

            // Add the second byte to the buffer.
            ushort buffer = ReadByte();

            // Shift the buffer to make room for the first byte.
            buffer <<= 7;

            // Or in and return the first byte.
            return buffer |= First;
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream using Big-endian
        /// encoding, and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        // Read a 16 bit integer and change it to Big-endian.
        public short ReadNetworkInt16() => IPAddress.NetworkToHostOrder(ReadInt16());

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public ushort ReadNetworkUInt16() => (ushort)IPAddress.NetworkToHostOrder(ReadUInt16());

        /// <summary>
        /// Reads a 3-byte signed integer from the current stream using big-endinan
        /// encoding, and advances the current position of the stream by three bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from this stream as a 3-byte signed integer.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public int ReadNetworkInt24()
        {
            int value = 0;
            for (int i = 0; i < 3; i++)
            {
                value <<= 8;
                value |= ReadByte();
            }

            return value;
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using big-endinan
        /// encoding, and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public int ReadNetworkInt32() => IPAddress.NetworkToHostOrder(ReadInt32());

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public uint ReadNetworkUInt32() => (uint)IPAddress.NetworkToHostOrder(ReadInt32());

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream using big-endinan
        /// encoding, and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns> An 8-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public long ReadNetworkInt64() => IPAddress.NetworkToHostOrder(ReadInt64());

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public ulong ReadNetworkUInt64() => (ulong)IPAddress.NetworkToHostOrder(ReadInt64());

        /// <summary>
        /// Utility function that can read a variable length integer from a binary stream
        /// </summary>
        /// <returns>The integer read</returns>
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
                if ((buffer & 0x80) == 0) return value;
            }

            throw new FormatException("Invalid Var Int");
        }

        /// <summary>
        /// Reads a variable length unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public uint ReadVarLen()
        {
            uint value = 0;
            byte buffer;

            if (((value = ReadByte()) & 0x80) != 0)
            {
                value &= 0x7F;
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
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public int ReadVarInt()
        {
            byte buffer = ReadByte();
            int value = buffer & 0x7F;

            for (int i = 0; i < 3; i++)
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
        /// Reads a variable length unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <remarks></remarks>
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
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <remarks></remarks>
        public static int ReadVarLen1x(BinaryReader file)
        {
            byte buffer = file.ReadByte();
            var value = (uint)(buffer & 0x7f);

            for (int i = 0; i < 3; i++)
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
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endinan
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <remarks></remarks>
        public static int ReadVarLen2x(BinaryReader file)
        {
            int value = 0;
            byte buffer;
            //// length of 4??
            for (int i = 0; i < Marshal.SizeOf(value); i++)
            {
                buffer = file.ReadByte();
                value <<= 7;
                value += buffer & 0x7F;
                if ((buffer & 0x80) == 0)
                    return value;
            }

            throw new FormatException("Invalid Var Int");
        }

        /// <summary>
        /// Reads an ASCII string of known byte length from the current stream.
        /// </summary>
        /// <param name="Bytes">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <remarks>This method needs to be omitted from Windows phone and X-Box development, because ASCII is not supported on those platforms.</remarks>
        public string ReadASCIIString(int Bytes)
        {
            var encoding = new ASCIIEncoding();
            var BBuffer = new byte[Bytes];
            Read(BBuffer, 0, Bytes);
            return encoding.GetString(BBuffer, 0, Bytes);
        }

        /// <summary>
        /// Reads an UTF8 string of known byte length from the current stream.
        /// </summary>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public string ReadUTF8String(int length)
        {
            Encoding encoding = Encoding.UTF8; // .ASCII
            var buffer = new byte[length];
            Read(buffer, 0, length);
            return encoding.GetString(buffer, 0, length);
        }

        /// <summary>
        /// Returns the next available byte and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available byte, or 0 if no more bytes are available or
        /// the stream does not support seeking.
        /// </returns>
        public byte PeekByte()
        {
            if (!BaseStream.CanSeek)
                return 0; // -1;

            byte temp = ReadByte();
            BaseStream.Position -= sizeof(byte);
            return temp;
        }

        /// <summary>
        /// Returns the next available Int16 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available Int16, or -1 if no more Int16s are available or
        /// the stream does not support seeking.
        /// </returns>
        public short PeekInt16()
        {
            if (!BaseStream.CanSeek)
                return -1;

            short temp = ReadInt16();
            BaseStream.Position -= sizeof(short);
            return temp;
        }

        /// <summary>
        /// Returns the next available NetworkInt16 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available NetworkInt16, or -1 if no more NetworkInt16s are available or
        /// the stream does not support seeking.
        /// </returns>
        public short PeekNetworkInt16()
        {
            if (!BaseStream.CanSeek)
                return -1;

            short temp = ReadNetworkInt16();
            BaseStream.Position -= sizeof(short);
            return temp;
        }

        /// <summary>
        /// Returns the next available Int32 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available Int32, or -1 if no more Int32s are available or
        /// the stream does not support seeking.
        /// </returns>
        public int PeekInt32()
        {
            if (!BaseStream.CanSeek)
                return -1;

            int temp = ReadInt32();
            BaseStream.Position -= sizeof(int);
            return temp;
        }

        /// <summary>
        /// Returns the next available NetworkInt32 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available NetworkInt32, or -1 if no more NetworkInt32s are available or
        /// the stream does not support seeking.
        /// </returns>
        public int PeekNetworkInt32()
        {
            if (!BaseStream.CanSeek)
                return -1;

            int temp = ReadNetworkInt32();
            BaseStream.Position -= sizeof(int);
            return temp;
        }

        /// <summary>
        /// Returns the next available Int64 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available Int64, or -1 if no more Int64s are available or
        /// the stream does not support seeking.
        /// </returns>
        public long PeekInt64()
        {
            if (!BaseStream.CanSeek)
                return -1;

            long temp = ReadInt64();
            BaseStream.Position -= sizeof(long);
            return temp;
        }

        /// <summary>
        /// Returns the next available NetworkInt64 and does not advance the byte or character
        /// position.
        /// </summary>
        /// <returns>
        /// The next available NetworkInt64, or -1 if no more NetworkInt64s are available or
        /// the stream does not support seeking.
        /// </returns>
        public long PeekNetworkInt64()
        {
            if (!BaseStream.CanSeek)
                return -1;

            long temp = ReadNetworkInt64();
            BaseStream.Position -= sizeof(long);
            return temp;
        }
        #endregion
    }
}
