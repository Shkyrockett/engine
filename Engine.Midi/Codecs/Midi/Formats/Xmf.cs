// <copyright file="Riff.cs" company="Shkyrockett">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// Xmf midi file format.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Xmf
        : IMediaContainer
    {
        #region Constants
        /// <summary>
        /// The registered codec
        /// </summary>
        public static readonly bool CodecRegistered = RegisterMediaCodecs();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Xmf" /> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Xmf()
            : this(string.Empty, new List<IMediaContainer>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Xmf"/> class.
        /// </summary>
        /// <param name="contentID">The content identifier.</param>
        /// <param name="contents">The contents.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Xmf(string contentID, List<IMediaContainer> contents)
            : this("Xmf??", 0, contentID, contents)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Xmf" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataSize">Size of the data.</param>
        /// <param name="contentID">The content identifier.</param>
        /// <param name="contents">The contents.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Xmf(string id, uint dataSize, string contentID, List<IMediaContainer> contents)
        {
            (ID, DataSize, ContentID, Contents) = (id, dataSize, contentID, contents);
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="IMediaContainer"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IMediaContainer"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IMediaContainer this[int index]
        {
            get { return Contents[index]; }
            set { Contents[index] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Header ID for an Xmf file.
        /// </summary>
        public string ID { get; } // = "Xmf??";

        /// <summary>
        /// Gets or sets the size of the data.
        /// </summary>
        public uint DataSize { get; set; }

        /// <summary>
        /// Gets or sets the content ID.
        /// </summary>
        public string ContentID { get; set; }

        /// <summary>
        /// Gets or sets the contents of the XMF file.
        /// </summary>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<IMediaContainer> Contents { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Registers the media extensions.
        /// </summary>
        public static bool RegisterMediaCodecs()
        {
            if (!MediaFile.RegisteredTypes.Contains(typeof(Xmf))) MediaFile.RegisteredTypes.Add(typeof(Xmf));
            if (!MediaFile.RegisteredExtensions.ContainsKey(".XMF")) MediaFile.RegisteredExtensions.Add(".XMF", s => Load(s));
            return true;
        }

        /// <summary>
        /// Loads an Xmf file from a filename.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static Xmf Load(Stream stream)
        {
            _ = stream;
            return new Xmf();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Xmf File";
        #endregion
    }
}
