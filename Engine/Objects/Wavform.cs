
using System;
using System.Collections.Generic;

namespace Engine.Audio
{
    /// <summary>
    /// 
    /// </summary>
    public class Wavform
        : IAudio
    {
        /// <summary>
        /// 
        /// </summary>
        public Wavform()
        {
            DisplayName = "Wavform";
            Name = "Wavform";
            Filename = "Wavform.wav";
            TimeSyncPoints = new Dictionary<int, DateTimeOffset>();
            Lyrics = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Lyrics { get; set; }
    }
}