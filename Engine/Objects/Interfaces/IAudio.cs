using Engine.Objects;
using System;
using System.Collections.Generic;

namespace Engine.Audio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAudio
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> Lyrics { get; set; }
    }
}
