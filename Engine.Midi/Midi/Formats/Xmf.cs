﻿// <copyright file="Riff.cs" company="Shkyrockett">
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

using System.Collections.Generic;

namespace Engine.Midi
{
    /// <summary>
    /// Xmf midi file format.
    /// </summary>
    [DisplayName("Xmf File")]
    public class Xmf
        : IMediaContainer
    {
        /// <summary>
        /// Gets the Header ID for an Xmf file.
        /// </summary>
        public string ID { get; } = "Xmf??";

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public uint DataSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ContentID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IMediaContainer> Contents { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Xmf Load(string filename)
            => new Xmf();
    }
}
