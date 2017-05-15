// <copyright file="TimeSignature.cs" company="Shkyrockett">
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

namespace Engine.Midi
{
    /// <summary>
    /// Time signature.
    /// </summary>
    /// <remarks>
    /// FF 58 04  NN DD CC BB
    /// </remarks>
    [ElementName(nameof(TimeSignature))]
    [DisplayName("Time Signature")]
    public class TimeSignature
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <param name="clocks"></param>
        /// <param name="beats"></param>
        /// <param name="status"></param>
        public TimeSignature(byte numerator, byte denominator, byte clocks, byte beats, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Numerator = numerator;
            Denominator = denominator;
            Clocks = clocks;
            Beats = beats;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Numerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Denominator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Clocks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Beats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static TimeSignature Read(MidiBinaryReader reader, EventStatus status)
            => new TimeSignature(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), status);
    }
}
