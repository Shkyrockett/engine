// <copyright file="Maths.Computer.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Maths
    {
        /// <summary>
        /// Swap left and right values so the left object has the value of the right object and visa-versa.
        /// </summary>
        /// <param name="a">The left value.</param>
        /// <param name="b">The right value.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            var swap = a;
            a = b;
            b = swap;
        }

        /// <summary>
        /// The high nibble.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte HighNibble(this byte n)
            => (byte)(n >> 0x4);

        /// <summary>
        /// The low nibble.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte LowNibble(this byte n)
            => (byte)(n & 0x0F);

        /// <summary>
        /// The join nibbles.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="l">The l.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte JoinNibbles(byte h, byte l)
        {
            byte value = 0;
            value = (byte)((value & 0xF0) | (l & 0x0F));
            value = (byte)((value & 0x0F) | (h << 4));
            return value;
        }

        /// <summary>
        /// The high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int HighWord(this int n)
            => (n >> 0x10) & 0xffff;

        /// <summary>
        /// The signed high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedHighWord(this int n)
            => (short)((n >> 0x10) & 0xffff);

        /// <summary>
        /// The signed high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedHighWord(this IntPtr n)
            => SignedHighWord((int)((long)n));

        /// <summary>
        /// The low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LowWord(this int n)
            => n & 0xffff;

        /// <summary>
        /// The signed low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedLowWord(this int n)
            => (short)(n & 0xffff);

        /// <summary>
        /// The signed low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedLowWord(this IntPtr n)
            => SignedLowWord((int)((long)n));

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong HostToNetworkOrder(ulong host)
            => ((ulong)((HostToNetworkOrder((uint)host) & 0xffffffffL) << 0x20) | (ulong)(HostToNetworkOrder((uint)(host >> 0x20)) & 0xffffffffL));

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long HostToNetworkOrder(long host)
            => ((HostToNetworkOrder((int)host) & 0xffffffffL) << 0x20) | (HostToNetworkOrder((int)(host >> 0x20)) & 0xffffffffL);

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint HostToNetworkOrder(uint host)
            => ((uint)((HostToNetworkOrder((ushort)host) & 0xffff) << 0x10) | (uint)(HostToNetworkOrder((ushort)(host >> 0x10)) & 0xffff));

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int HostToNetworkOrder(int host)
            => (((HostToNetworkOrder((short)host) & 0xffff) << 0x10) | (HostToNetworkOrder((short)(host >> 0x10)) & 0xffff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort HostToNetworkOrder(ushort host)
            => (ushort)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short HostToNetworkOrder(short host)
            => (short)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A long value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong NetworkToHostOrder(ulong network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts a long value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>A long value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long NetworkToHostOrder(long network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts an integer value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint NetworkToHostOrder(uint network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts an integer value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order. </param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NetworkToHostOrder(int network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts a short value from network byte order to host byte order.
        /// </summary>
        /// <param name="network"> The number to convert, expressed in network byte order. </param>
        /// <returns> A short value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort NetworkToHostOrder(ushort network)
            => HostToNetworkOrder(network);

        /// <summary>
        /// Converts a short value from network byte order to host byte order.
        /// </summary>
        /// <param name="network"> The number to convert, expressed in network byte order. </param>
        /// <returns> A short value, expressed in host byte order.</returns>
        /// <remarks>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short NetworkToHostOrder(short network)
            => HostToNetworkOrder(network);
    }
}
