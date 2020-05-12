// <copyright file="MidiContinue.cs" company="Shkyrockett">
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

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// MidiContinue. Continue at the point the sequence was Stopped.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nF 0B
    /// MidiContinue at the point the sequence was Stopped.</para>
    /// </remarks>
    [ElementName(nameof(MidiContinue))]
    public class MidiContinue
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiContinue" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MidiContinue(IEventStatus status)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="MidiContinue" />.
        /// </returns>
        internal static new MidiContinue Read(BinaryReaderExtended reader, IEventStatus status)
        {
            _ = reader;
            return new MidiContinue(status);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "MidiContinue";
    }
}
