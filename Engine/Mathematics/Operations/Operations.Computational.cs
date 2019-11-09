// <copyright file="Operations.Computational.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    public static partial class Operations
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
        /// https://blogs.msdn.microsoft.com/mazhou/2017/12/12/c-7-series-part-7-ref-returns/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ElementAt<T>(ref T[] array, int position)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return ref array[position];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T SelectHeigher<T>(ref T a, ref T b) where T : IComparable => ref a.CompareTo(b) >= 0 ? ref a : ref b;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T SelectLower<T>(ref T a, ref T b) where T : IComparable => ref a.CompareTo(b) <= 0 ? ref a : ref b;

        /// <summary>
        /// The high nibble.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte HighNibble(this byte n) => (byte)(n >> 0x4);

        /// <summary>
        /// The low nibble.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte LowNibble(this byte n) => (byte)(n & 0x0F);

        /// <summary>
        /// Get the Hight and Low nibbles as a <see cref="ValueTuple{T1, T2}"/>.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte High, byte Low) Nibbles(this byte n) => ((byte)(n >> 0x4), (byte)(n & 0x0F));

        /// <summary>
        /// The join nibbles.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte JoinNibbles((byte High, byte Low) tuple)
        {
            byte value = 0;
            value = (byte)((value & 0xF0) | (tuple.Low & 0x0F));
            value = (byte)((value & 0x0F) | (tuple.High << 4));
            return value;
        }

        /// <summary>
        /// The join nibbles.
        /// </summary>
        /// <param name="high">The h.</param>
        /// <param name="low">The l.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte JoinNibbles(byte high, byte low)
        {
            byte value = 0;
            value = (byte)((value & 0xF0) | (low & 0x0F));
            value = (byte)((value & 0x0F) | (high << 4));
            return value;
        }

        /// <summary>
        /// The high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int HighWord(this int n) => (n >> 0x10) & 0xffff;

        /// <summary>
        /// The high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint HighWord(this uint n) => (n >> 0x10) & 0xffff;

        /// <summary>
        /// The signed high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedHighWord(this int n) => (short)((n >> 0x10) & 0xffff);

        /// <summary>
        /// The signed high word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedHighWord(this IntPtr n) => SignedHighWord((int)(long)n);

        /// <summary>
        /// The low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LowWord(this int n) => n & 0xffff;

        /// <summary>
        /// The low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint LowWord(this uint n) => n & 0xffff;

        /// <summary>
        /// The signed low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedLowWord(this int n) => (short)(n & 0xffff);

        /// <summary>
        /// The signed low word.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SignedLowWord(this IntPtr n) => SignedLowWord((int)(long)n);

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReverseEndianness(ulong host) => (ulong)((ReverseEndianness((uint)host) & 0xffffffffL) << 0x20) | (ulong)(ReverseEndianness((uint)(host >> 0x20)) & 0xffffffffL);

        /// <summary>
        /// Converts a long value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A long value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReverseEndianness(long host) => ((ReverseEndianness((int)host) & 0xffffffffL) << 0x20) | (ReverseEndianness((int)(host >> 0x20)) & 0xffffffffL);

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseEndianness(uint host) => (uint)((ReverseEndianness((ushort)host) & 0xffff) << 0x10) | (uint)(ReverseEndianness((ushort)(host >> 0x10)) & 0xffff);

        /// <summary>
        /// Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseEndianness(int host) => ((ReverseEndianness((short)host) & 0xffff) << 0x10) | (ReverseEndianness((short)(host >> 0x10)) & 0xffff);

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReverseEndianness(ushort host) => (ushort)(((host & 0xff) << 8) | ((host >> 8) & 0xff));

        /// <summary>
        /// Converts a short value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order. </param>
        /// <returns>A short value, expressed in network byte order.</returns>
        /// <remarks><para>http://referencesource.microsoft.com/#System/net/System/Net/IPAddress.cs</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReverseEndianness(short host) => (short)(((host & 0xff) << 8) | ((host >> 8) & 0xff));
    }
}
