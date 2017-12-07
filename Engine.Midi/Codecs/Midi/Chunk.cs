// <copyright file="Chunk.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine.File
{
    /// <summary>
    /// File Chunk class.
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// List of supported Chunk types.
        /// </summary>
        public static List<string> ChunkIds = new List<string>
        {
            "RIFF",
            "MThd",
            "MTrk"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        public Chunk()
        {
            Id = string.Empty;
            Length = 0;
            SubStream = null;
        }

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

        /// <summary>
        /// Read a file from a reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Chunk Read(BinaryReaderExtended reader)
        {
            var chunk = new Chunk
            {
                Id = reader.ReadASCIIBytes(4),
                Length = reader.ReadNetworkInt32()
            };

            Validate(chunk);

            if (chunk.Length > reader.BaseStream.Length - reader.BaseStream.Position)
                throw new ArgumentOutOfRangeException("Chunk length larger than remaining stream length.");

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
            var value = false;

            if (string.IsNullOrWhiteSpace(chunk.Id))
            {
                throw new InvalidDataException("Chunk length not set.");
                // return value;
            }

            if (chunk.Length < 1)
            {
                throw new InvalidDataException("Chunk length not set.");
                // return value;
            }

            value = true;
            return value;
        }
    }
}
