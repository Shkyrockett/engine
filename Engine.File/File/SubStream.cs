﻿// <copyright file="SubStream.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// http://daisy-trac.cvsdude.com/urakawa-sdk/browser/trunk/csharp/core/data/SubStream.cs
// Also look into:
// http://stackoverflow.com/questions/6949441/how-to-expose-a-sub-section-of-my-stream-to-a-user
// https://social.msdn.microsoft.com/Forums/vstudio/en-US/c409b63b-37df-40ca-9322-458ffe06ea48/how-to-access-part-of-a-filestream-or-memorystream?forum=netfxbcl
// https://www.codeproject.com/Articles/13061/StreamMuxer
// http://stackoverflow.com/questions/30494541/passing-subsequence-of-a-stream-without-copying-its-content-into-a-new-instance?noredirect=1&lq=1
// </references>

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// A <see cref="Stream"/> that implements reading a sub-chunk of a source <see cref="Stream"/>.
/// The source <see cref="Stream"/> must support seeking
/// </summary>
public class SubStream
    : Stream
{
    #region Fields
    /// <summary>
    /// The base stream (readonly).
    /// </summary>
    private readonly Stream baseStream;

    /// <summary>
    /// The start position.
    /// </summary>
    private readonly long startPosition;

    /// <summary>
    /// The length.
    /// </summary>
    private readonly long length;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Constructor setting the source <see cref="Stream"/> as well as the start position and count
    /// of the sub-chunk specifying the <see cref="SubStream"/>
    /// </summary>
    /// <param name="source">The source <see cref="Stream"/></param>
    /// <param name="start">The start position of the sub-chunk</param>
    /// <param name="length">The count of the sub-chunk</param>
    public SubStream(Stream source, long start, long length)
    {
        baseStream = source ?? throw new ArgumentNullException(nameof(source), "The source stream can not be null");

        if (!source.CanRead)
        {
            throw new ArgumentException("Can't read base stream", nameof(source));
        }

        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start), "The start position of a SubStream can not be negative");
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "The count of a SubStream can not be negative");
        }

        if (start + length > source.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(source), "The sub-chunk exceeds beyond the end of the source stream");
        }

        startPosition = start;
        this.length = length;
        Position = 0;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets the base stream.
    /// </summary>
    /// <value>
    /// The base stream.
    /// </value>
    public Stream BaseStream => baseStream;

    /// <summary>
    /// Gets a value indicating whether the current stream supports reading. 
    /// </summary>
    public override bool CanRead => baseStream?.CanRead ?? throw new ObjectDisposedException(GetType().Name);

    /// <summary>
    /// Gets a value indicating whether the current stream supports seeking. 
    /// </summary>
    public override bool CanSeek => baseStream?.CanSeek ?? throw new ObjectDisposedException(GetType().Name);

    /// <summary>
    /// Gets a value indicating whether the current stream supports writing - always returns <see langword="false"/>
    /// </summary>
    public override bool CanWrite => /*CheckDisposed();*/false;

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
            return baseStream?.Position - startPosition ?? throw new ObjectDisposedException(GetType().Name);
        }
        set
        {
            CheckDisposed();
            var newPos = value < 0 ? 0 : value > length ? length : value;
            baseStream.Position = startPosition + newPos;
        }
    }
    #endregion Properties

    #region Methods
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
        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer), "The read buffer is null");
        }

        if (offset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "The offset is negative");
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "The count is negative");
        }

        if (offset + count > buffer.Length)
        {
            throw new ArgumentException("The buffer is too small", nameof(buffer));
        }

        CheckDisposed();

        if (count <= 0)
        {
            return 0;
        }
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
    public override void SetLength(long value) => throw new NotSupportedException("A SubStream is read-only so it's count can not be set");

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
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException("A SubStream is read-only so it can not be written to");

    /// <summary>
    /// Closes the <see cref="SubStream"/> and it's underlying source <see cref="Stream"/>
    /// </summary>
    public override void Close()
    {
        //base.Close();
        //baseStream.Close();
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <param name="disposing">The disposing.</param>
    protected override void Dispose(bool disposing)
    {
        //base.Dispose(disposing);
        //if (disposing && baseStream is not null)
        //{
        //    try
        //    {
        //        baseStream.Dispose();
        //    }
        //    catch
        //    {
        //    }

        //    baseStream = null;
        //}
    }

    /// <summary>
    /// Check the disposed.
    /// </summary>
    /// <exception cref="ObjectDisposedException"></exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void CheckDisposed()
    {
        if (baseStream is null)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
    #endregion Methods
}
