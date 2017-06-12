// <copyright file="Riff.cs" company="Shkyrockett">
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

using System.Collections.Generic;

namespace Engine.File
{
    /// <summary>
    /// Midi formatted Rich Interchange formatted file media container.
    /// </summary>
    [DisplayName("Riff File")]
    public class Riff
        : IMediaContainer
    {
        /// <summary>
        /// Gets the Header ID of a Rich Interchange formatted file.
        /// </summary>
        public string ID { get; } = "RIFF";

        /// <summary>
        /// Gets or sets the data size of the contents of the file.
        /// </summary>
        public uint DataSize { get; set; }

        /// <summary>
        /// Gets or sets the content ID for the file.
        /// </summary>
        public string ContentID { get; set; }

        /// <summary>
        /// Gets or sets the contents of the file.
        /// </summary>
        public List<IMediaContainer> Contents { get; set; }

        /// <summary>
        /// Loads a RIFF file from a filename. 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Riff Load(string filename)
            => new Riff();
    }
}
