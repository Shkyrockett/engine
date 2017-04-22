// <copyright file="MidiBinaryReader.cs" company="Shkyrockett">
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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Engine.Midi
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class MidiBinaryReader
        : BinaryReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiBinaryReader"/> class, based on the
        /// supplied stream, using System.Text.UTF8Encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <exception cref="ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        public MidiBinaryReader(Stream input)
            : base(input)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiBinaryReader"/> class based on the
        /// supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="input">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        /// <exception cref="ArgumentException">
        /// The stream does not support reading, the stream is null, or the stream is
        /// already closed.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">encoding is null.</exception>
        public MidiBinaryReader(Stream input, Encoding encoding)
            : base(input, encoding)
        { }

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
        public short ReadNetworkInt16()
            => IPAddress.NetworkToHostOrder(ReadInt16());

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public ushort ReadNetworkUInt16()
            => (ushort)IPAddress.NetworkToHostOrder(ReadUInt16());

        /// <summary>
        /// Reads a 3-byte signed integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by three bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from this stream as a 3-byte signed integer.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public int ReadNetworkInt24()
        {
            var value = 0;
            for (var i = 0; i < 3; i++)
            {
                value <<= 8;
                value |= ReadByte();
            }

            return value;
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public int ReadNetworkInt32()
            => IPAddress.NetworkToHostOrder(ReadInt32());

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public uint ReadNetworkUInt32()
            => (uint)IPAddress.NetworkToHostOrder(ReadInt32());

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns> An 8-byte signed integer read from the current stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public long ReadNetworkInt64()
            => IPAddress.NetworkToHostOrder(ReadInt64());

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte unsigned integer read from this stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public ulong ReadNetworkUInt64()
            => (ulong)IPAddress.NetworkToHostOrder(ReadInt64());

        /// <summary>
        /// Reads in a 32-bit integer in compressed format.
        /// </summary>
        /// <returns>A 32-bit integer in compressed format.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="FormatException">The stream is corrupted.</exception>
        public new int Read7BitEncodedInt()
            => base.Read7BitEncodedInt();

        /// <summary>
        /// Reads the variable byte integer length from the current stream and that number of
        /// bytes into a byte array and advances the current position by that number of bytes.
        /// </summary>
        /// <returns>
        /// A byte array containing data read from the underlying stream. This might be less
        /// than the number of bytes requested if the end of the stream is reached.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The number of decoded characters to read is greater than count. This can happen if
        /// a Unicode decoder returns fallback characters or a surrogate pair.
        /// </exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Length encountered is negative.</exception>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        public byte[] ReadVariableLengthBytes()
        {
            var length = Read7BitEncodedInt();
            return ReadBytes(length);
        }

        /// <summary>
        /// Reads an ASCII string of variable byte integer length from the current stream.
        /// </summary>
        /// <param name="Bytes">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public string ReadASCIIString()
        {
            var length = Read7BitEncodedInt(); // ReadVarInt();
            return ReadASCIIBytes(length);
        }

        /// <summary>
        /// Reads an ASCII string of known byte length from the current stream.
        /// </summary>
        /// <param name="Bytes">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public string ReadASCIIBytes(int Bytes)
        {
            var encoding = new ASCIIEncoding();
            var BBuffer = new byte[Bytes];
            Read(BBuffer, 0, Bytes);
            return encoding.GetString(BBuffer, 0, Bytes);
        }

        /// <summary>
        /// Reads an UTF8 string of variable byte integer length from the current stream.
        /// </summary>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public string ReadUTF8String()
        {
            var length = Read7BitEncodedInt(); // ReadVarInt();
            return ReadASCIIBytes(length);
        }

        /// <summary>
        /// Reads an UTF8 string of known byte length from the current stream.
        /// </summary>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns>The string being read.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        public string ReadUTF8Bytes(int length)
        {
            Encoding encoding = Encoding.UTF8;
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

            var temp = ReadByte();
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

            var temp = ReadInt16();
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

            var temp = ReadNetworkInt16();
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

            var temp = ReadInt32();
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

            var temp = ReadNetworkInt32();
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

            var temp = ReadInt64();
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

            var temp = ReadNetworkInt64();
            BaseStream.Position -= sizeof(long);
            return temp;
        }
    }

    /// <summary>
    /// Experimental Read Variable Length Integer Methods.
    /// </summary>
    internal partial class MidiBinaryReader
    {
        /// <summary>
        /// Utility function that can read a variable length integer from a binary stream
        /// </summary>
        /// <returns>The integer read</returns>
        //[Obsolete]
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
        /// Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
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
        /// (Alternate)Reads a variable length unsigned integer from the current stream using big-endian
        /// encoding, and advances the current position by the number of bytes read.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream as variable length.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="IOException">An I/O error has occurred.</exception>
        [Obsolete]
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
        /// <remarks></remarks>
        [Obsolete]
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
        /// <remarks></remarks>
        [Obsolete]
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
        /// <exception cref="System.IO.EndOfStreamException">The end of the stream has been reached.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream has been closed.</exception>
        /// <exception cref="System.IO.IOException">An I/O error has occurred.</exception>
        /// <remarks></remarks>
        [Obsolete]
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
                    return value;
            }

            throw new FormatException("Invalid Var Int");
        }
    }
}
