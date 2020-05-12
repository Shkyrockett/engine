// <copyright file="MediaFile.cs" company="Shkyrockett">
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
    /// The media file class.
    /// </summary>
    public class MediaFile
    {
        #region Constants
        /// <summary>
        /// The registered extensions
        /// </summary>
        public static readonly Dictionary<string, Func<Stream, IMediaContainer>> RegisteredExtensions = new Dictionary<string, Func<Stream, IMediaContainer>>();

        /// <summary>
        /// The registered types
        /// </summary>
        public static readonly HashSet<Type> RegisteredTypes = new HashSet<Type>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile" /> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MediaFile()
            : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile" /> class.
        /// </summary>
        /// <param name="media">The data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MediaFile(IMediaContainer media)
            : this(media, string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile" /> class.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <param name="filename">The filename.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MediaFile(IMediaContainer media, string filename)
        {
            Media = media;
            FileName = filename;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public IMediaContainer Media { get; set; }

        /// <summary>
        /// Gets or sets the name of the Midi file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        #endregion

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool RegisterExtensions<T>(T type, params string[] extensions)
        //    where T : IMediaContainer
        //{
        //    foreach (var extension in extensions)
        //    {
        //        if (!RegisteredExtensions.ContainsKey(extension)) RegisteredExtensions.Add(extension, (s) => type.Load<T>(s));
        //    }

        //    return true;
        //}

        #region Methods
        /// <summary>
        /// Loads the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The {ext} file format is not supported.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MediaFile Load(string filename)
        {
            var ext = Path.GetExtension(filename).ToUpperInvariant();
            if (!RegisteredExtensions.ContainsKey(ext))
            {
                throw new Exception($"The {ext} file format is not supported.");
            }

            using var stream = System.IO.File.OpenRead(filename);
            var media = RegisteredExtensions[ext]?.Invoke(stream);
            return new MediaFile(media, filename);
        }
        #endregion
    }
}
