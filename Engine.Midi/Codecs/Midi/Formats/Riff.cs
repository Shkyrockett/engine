// <copyright file="Riff.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// Midi formatted Rich Interchange formatted file media container.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class Riff
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
    /// Initializes a new instance of the <see cref="Riff"/> class.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Riff()
        : this(string.Empty, [])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Riff"/> class.
    /// </summary>
    /// <param name="contentID">The content identifier.</param>
    /// <param name="contents">The contents.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Riff(string contentID, List<IMediaContainer> contents)
        : this("RIFF", 0, contentID, contents)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Riff"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="dataSize">Size of the data.</param>
    /// <param name="contentID">The content identifier.</param>
    /// <param name="contents">The contents.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Riff(string id, uint dataSize, string contentID, List<IMediaContainer> contents)
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
    /// Gets the Header ID of a Rich Interchange formatted file.
    /// </summary>
    public string ID { get; } // = "RIFF";

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
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<IMediaContainer> Contents { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Registers the media extensions.
    /// </summary>
    public static bool RegisterMediaCodecs()
    {
        if (!MediaFile.RegisteredTypes.Contains(typeof(Riff))) MediaFile.RegisteredTypes.Add(typeof(Riff));
        if (!MediaFile.RegisteredExtensions.ContainsKey(".RMID")) MediaFile.RegisteredExtensions.Add(".RMID", s => Load(s));
        return true;
    }

    /// <summary>
    /// Loads a RIFF file from a filename.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public static Riff Load(Stream stream)
    {
        _ = stream;
        return new Riff();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Riff File";
    #endregion
}
