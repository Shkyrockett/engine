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

using System.IO;

namespace Engine.File
{
    /// <summary>
    /// The media file class.
    /// </summary>
    public class MediaFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        public MediaFile()
        {
            Media = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public MediaFile(IMediaContainer data)
        {
            Media = data;
        }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        public IMediaContainer Media { get; set; }

        /// <summary>
        /// Gets or sets the name of the Midi file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Load.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The <see cref="MediaFile"/>.</returns>
        public static MediaFile Load(string filename, Stream stream)
        {
            IMediaContainer media;

            // http://stackoverflow.com/questions/9033/hidden-features-of-c/12137#12137
            switch (Path.GetExtension(filename).ToUpperInvariant())
            {
                case ".KAR":
                case ".MID":
                case ".MIDI":
                case ".SMF":
                    media = Midi.Load(stream);
                    break;
                case ".RMID":
                    media = new Riff();
                    break;
                case ".XMF":
                    media = new Xmf();
                    break;
                default:
                    return null;
            }

            return new MediaFile
            {
                Media = media,
                FileName = filename
            };
        }
    }
}
