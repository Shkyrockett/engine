﻿// <copyright file="SubStream.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// http://urakawa.sourceforge.net/
// </references>

using System;
using System.IO;

namespace Engine.Midi
{
    /// <summary>
    /// A <see cref="Stream"/> that implements reading a sub-chunk of a source <see cref="Stream"/>.
    /// The source <see cref="Stream"/> must support seeking
    /// </summary>
    public class SubStream
        : Stream
    {
        /// <summary>
        /// 
        /// </summary>
        private Stream baseStream;

        /// <summary>
        /// 
        /// </summary>
        private long startPosition;

        /// <summary>
        /// 
        /// </summary>
        private long length;

        /// <summary>
        /// Constructor setting the source <see cref="Stream"/> as well as the start position and count
        /// of the sub-chunk specifying the <see cref="SubStream"/>
        /// </summary>
        /// <param name="source">The source <see cref="Stream"/></param>
        /// <param name="start">The start position of the sub-chunk</param>
        /// <param name="length">The count of the sub-chunk</param>
        public SubStream(Stream source, long start, long length)
        {
            baseStream = source ?? throw new ArgumentNullException("The source stream can not be null");

            if (!source.CanRead)
                throw new ArgumentException("Can't read base stream");
            if (start < 0)
                throw new ArgumentOutOfRangeException("The start position of a SubStream can not be negative");
            if (length < 0)
                throw new ArgumentOutOfRangeException("The count of a SubStream can not be negative");
            if (start + length > source.Length)
                throw new ArgumentOutOfRangeException("The sub-chunk exceeds beyond the end of the source stream");

            startPosition = start;
            this.length = length;
            Position = 0;
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading. 
        /// </summary>
        public override bool CanRead
        {
            get
            {
                CheckDisposed();
                return baseStream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking. 
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                CheckDisposed();
                return baseStream.CanSeek;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing - always returns <c>false</c>
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                CheckDisposed();
                return false;
            }
        }

        /// <summary>
        /// Does nothing since a <see cref="SubStream"/> is read-only
        /// </summary>
        public override void Flush()
        {
            // Does nothing since there is no writing
            // CheckDisposed(); 
            // baseStream.Flush();
        }

        /// <summary>
        /// Gets the count in bytes of the <see cref="SubStream"/>
        /// </summary>
        public override long Length
        {
            get
            {
                CheckDisposed();
                return length;
            }
        }

        /// <summary>
        /// Gets or sets the position within the <see cref="SubStream"/>
        /// </summary>
        public override long Position
        {
            get
            {
                CheckDisposed();
                return baseStream.Position - startPosition;
            }
            set
            {
                CheckDisposed();
                var newPos = value;
                if (newPos < 0) newPos = 0;
                if (newPos >= Length) newPos = Length;
                baseStream.Position = startPosition + newPos;
            }
        }

        /// <summary>
        /// Reads a sequence of bytes from the current <see cref="SubStream"/> and 
        /// advances the position within the stream by the number of bytes read
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, 
        /// the buffer contains the specified byte array with the values between 
        /// <c><paramref name="offset"/></c> and <c>(<paramref name="offset"/> + <paramref name="count"/> - 1)</c> 
        /// replaced by the bytes read from the current <see cref="SubStream"/>.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="buffer"/> 
        /// at which to begin storing the data read from the current <see cref="SubStream"/>.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read from the current <see cref="SubStream"/>.</param>
        /// <returns>The number of <see cref="byte"/>s read</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("The read buffer is null");
            if (offset < 0)
                throw new ArgumentOutOfRangeException("The offset is negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException("The count is negative");
            if (offset + count > buffer.Length)
                throw new ArgumentException("The buffer is too small");

            CheckDisposed();

            if (count <= 0)
                return 0;
            else if (count >= Length - Position)
            {
                count = (int)(Length - Position);
            }

            return baseStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Sets the <see cref="Position"/> within the current stream.
        /// </summary>
        /// <param name="offset">
        /// A byte <paramref name="offset"/> relative to the origin parameter.
        /// </param>
        /// <param name="origin">
        /// A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.
        /// </param>
        /// <returns>The new <see cref="Position"/> within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            var newPos = Position;
            switch (origin)
            {
                case SeekOrigin.Current:
                    newPos += offset;
                    break;
                case SeekOrigin.Begin:
                    newPos = offset;
                    break;
                case SeekOrigin.End:
                    newPos = Length + offset;
                    break;
            }

            Position = newPos;
            return Position;
        }

        /// <summary>
        /// Sets the <see cref="Length"/> of the <see cref="SubStream"/>.
        /// Since a <see cref="SubStream"/> is read-only, 
        /// calling this method will throw an <see cref="NotSupportedException"/>
        /// </summary>
        /// <param name="value">The new <see cref="Length"/> </param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("A SubStream is read-only so it's count can not be set");
        }

        /// <summary>
        /// Since a <see cref="SubStream"/> is read only, 
        /// calling this method will throw an <see cref="NotSupportedException"/>
        /// </summary>
        /// <param name="buffer">
        /// An array of <see cref="byte"/>s. 
        /// This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="buffer"/> at which to begin copying <see cref="byte"/>s 
        /// to the current stream.
        /// </param>
        /// <param name="count">The number of <see cref="byte"/>s to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("A SubStream is read-only so it can not be written to");
        }

        ///// <summary>
        ///// Closes the <see cref="SubStream"/> and it's underlying source <see cref="Stream"/>
        ///// </summary>
        //public override void Close()
        //{
        //	base.Close();
        //	baseStream.Close();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && baseStream != null)
            {
                try
                {
                    baseStream.Dispose();
                }
                catch
                {
                }

                baseStream = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckDisposed()
        {
            if (baseStream == null) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
