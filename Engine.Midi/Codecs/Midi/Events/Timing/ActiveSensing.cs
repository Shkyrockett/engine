// <copyright file="ActiveSensing.cs" company="Shkyrockett">
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
    /// Active Sensing. This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nF 0E
    /// This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation.</para>
    /// </remarks>
    [ElementName(nameof(ActiveSensing))]
    public class ActiveSensing
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveSensing" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ActiveSensing(IEventStatus status)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="ActiveSensing" />.
        /// </returns>
        internal static new ActiveSensing Read(BinaryReaderExtended reader, IEventStatus status)
        {
            _ = reader;
            return new ActiveSensing(status);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Active Sensing";
    }
}
