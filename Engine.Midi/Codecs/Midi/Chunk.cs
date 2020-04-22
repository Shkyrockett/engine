// <copyright file="Chunk.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// File Chunk class.
    /// </summary>
    public class Chunk
    {
        #region Constants
        /// <summary>
        /// List of supported Chunk types.
        /// </summary>
        public static readonly List<string> ChunkIds = new List<string>
        {
            "RIFF",
            "MThd",
            "MTrk"
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Chunk()
            : this(string.Empty, 0, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="length">The length.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Chunk(string id, int length)
            : this(id, length, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="length">The length.</param>
        /// <param name="subStream">The sub stream.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Chunk(string id, int length, Stream subStream)
        {
            (Id, Length, SubStream) = (id, length, subStream);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the chunk ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Chunk Length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the chunk sub stream.
        /// </summary>
        internal Stream SubStream { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Read a file from a reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Chunk Read(BinaryReaderExtended reader)
        {
            var chunk = new Chunk(reader.ReadASCIIString(4), reader.ReadNetworkInt32());
            Validate(chunk);
            if (chunk.Length > reader.BaseStream.Length - reader.BaseStream.Position)
            {
                throw new ArgumentOutOfRangeException(nameof(chunk.Length), "Chunk length larger than remaining stream length.");
            }

            // Isolate a chunk of the stream as a sub-stream to try to prevent over-reading. 
            chunk.SubStream = new SubStream(reader.BaseStream, reader.Position, chunk.Length);
            return chunk;
        }

        /// <summary>
        /// Validate a chunk.
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        internal static bool Validate(Chunk chunk)
        {
            if (string.IsNullOrWhiteSpace(chunk.Id))
            {
                throw new InvalidDataException("Chunk length not set.");
            }

            if (chunk.Length < 1)
            {
                throw new InvalidDataException("Chunk length not set.");
            }

            return true;
        }
        #endregion
    }
}
