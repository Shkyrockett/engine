using Engine;
using Engine.File;
using System.Collections.Generic;
using System.ComponentModel;

namespace EventEditorMidi
{
    /// <summary>
    /// The music files class.
    /// </summary>
    [ElementName(nameof(MediaFiles))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MediaFiles
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFiles"/> class.
        /// </summary>
        public MediaFiles()
            : this(new List<MediaFile>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFiles"/> class.
        /// </summary>
        /// <param name="media">The media.</param>
        public MediaFiles(params MediaFile[] media)
            : this(new List<MediaFile>(media))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFiles"/> class.
        /// </summary>
        /// <param name="media">The media.</param>
        public MediaFiles(List<MediaFile> media)
        {
            Media = media;
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="MediaFile" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="MediaFile" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public MediaFile this[int index]
        {
            get { return Media[index]; }
            set { Media[index] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the midi.
        /// </summary>
        /// <value>
        /// The midi.
        /// </value>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<MediaFile> Media { get; set; } = new List<MediaFile>();
        #endregion

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Media Files";
    }
}
