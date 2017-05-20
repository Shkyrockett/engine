// <copyright file="MediaFile.cs" company="Shkyrockett">
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

using System.IO;

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    public class MediaFile
    {
        /// <summary>
        /// 
        /// </summary>
        public MediaFile()
        {
            Media = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public MediaFile(IMediaContainer data)
        {
            Media = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMediaContainer Media { get; set; }

        /// <summary>
        /// Gets or sets the name of the Midi file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MediaFile Load(string filename, Stream stream)
        {
            IMediaContainer media = null;

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

            return new MediaFile()
            {
                Media = media,
                FileName = filename
            };
        }
    }
}
