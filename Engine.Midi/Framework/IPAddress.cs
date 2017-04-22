// <copyright file="IPAddress.cs" company="Shkyrockett">
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
    /// Partial implementation of .Net framework <see cref="IPAddress"/> class, just for the NetworkOrder methods.
    /// </summary>
    internal class IPAddress
    {
        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        public static long HostToNetworkOrder(long host)
            => ((HostToNetworkOrder((int)host) & 0xffffffffL) << 0x20) | (HostToNetworkOrder((int)(host >> 0x20)) & 0xffffffffL);

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        public static int HostToNetworkOrder(int host)
            => (((HostToNetworkOrder((short)host) & 0xffff) << 0x10) | (HostToNetworkOrder((short)(host >> 0x10)) & 0xffff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        public static short HostToNetworkOrder(short host)
            => (short)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A long value, expressed in host byte order.</returns>
        public static long NetworkToHostOrder(long network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts an integer value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        public static int NetworkToHostOrder(int network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts a short value from network byte order to host byte order.
        /// </summary>
        /// <param name="network"> The number to convert, expressed in network byte order. </param>
        /// <returns> A short value, expressed in host byte order.</returns>
        public static short NetworkToHostOrder(short network)
            => HostToNetworkOrder(network);
    }
}
