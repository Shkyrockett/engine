using System;
using System.Collections.Generic;

namespace Engine.Audio
{
    /// <summary>
    /// 
    /// </summary>
    public class Midi
        : IAudio
    {
        /// <summary>
        /// 
        /// </summary>
        public Midi()
        {
            DisplayName = "Midi";
            Name = "Midi";
            Filename = "Midi.mid";
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