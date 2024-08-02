// <copyright file="Maths.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Engine;

/// <summary>
/// Extended Math processing library.
/// </summary>
public static partial class Maths
{
    #region Random
    /// <summary>
    /// Initialize random number generator with seed based on time.
    /// </summary>
    [ThreadStatic]
    public static readonly Random RandomNumberGenerator = new((int)DateTime.Now.Ticks & 0x0000FFFF);

    /// <summary>
    /// The random.
    /// </summary>
    /// <param name="Lower">The Lower.</param>
    /// <param name="Upper">The Upper.</param>
    /// <returns>The <see cref="T"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Random<T>(this T Lower, T Upper) where T : INumber<T> => (T.CreateSaturating(RandomNumberGenerator.Next()) * (Upper - Lower + T.One)) + Lower;
    #endregion Random

    #region Array Math
    /// <summary>
    /// The min.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min<T>(params T[] values) where T : INumber<T> => values.Min();

    /// <summary>
    /// The min.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min<T>(List<T> values) where T : INumber<T> => values.Min();

    /// <summary>
    /// The min.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min<T>(IEnumerable<T> values) where T : INumber<T> => values.Min();

    /// <summary>
    /// The max.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max<T>(params T[] values) where T : INumber<T> => values.Max();

    /// <summary>
    /// The max.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max<T>(List<T> values) where T : INumber<T> => values.Max();

    /// <summary>
    /// The max.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max<T>(IEnumerable<T> values) where T : INumber<T> => values.Max();

    /// <summary>
    /// Sums the specified source.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException"></exception>
    private static TResult Sum<TSource, TResult>(this IEnumerable<TSource> source)
        where TSource : struct, INumber<TSource>
        where TResult : struct, INumber<TResult>
    {
        if (source is null)
        {
            throw new NullReferenceException();
        }

        if (source is not null)
        {
            return Sum<TSource, TResult>(source.AsSpan());
        }

        TResult sum = TResult.Zero;
        foreach (TSource value in source)
        {
            checked { sum += TResult.CreateChecked(value); }
        }

        return sum;
    }

    /// <summary>
    /// Sums the specified span.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="span">The span.</param>
    /// <returns></returns>
    private static TResult Sum<T, TResult>(ReadOnlySpan<T> span)
        where T : struct, INumber<T>
        where TResult : struct, INumber<TResult>
    {
        if (typeof(T) == typeof(TResult)
            && Vector<T>.IsSupported
            && Vector.IsHardwareAccelerated
            && Vector<T>.Count > 2
            && span.Length >= Vector<T>.Count * 4)
        {
            // For cases where the vector may only contain two elements vectorization doesn't add any benefit
            // due to the expense of overflow checking. This means that architectures where Vector<T> is 128 bit,
            // such as ARM or Intel without AVX, will only vectorize spans of ints and not longs.

            if (typeof(T) == typeof(long))
            {
                return (TResult)(object)SumSignedIntegersVectorized(MemoryMarshal.Cast<T, long>(span));
            }
            if (typeof(T) == typeof(int))
            {
                return (TResult)(object)SumSignedIntegersVectorized(MemoryMarshal.Cast<T, int>(span));
            }
        }

        TResult sum = TResult.Zero;
        foreach (T value in span)
        {
            checked { sum += TResult.CreateChecked(value); }
        }

        return sum;
    }

    /// <summary>
    /// Sums the signed integers vectorized.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="span">The span.</param>
    /// <returns></returns>
    /// <exception cref="System.OverflowException"></exception>
    private static T SumSignedIntegersVectorized<T>(ReadOnlySpan<T> span)
        where T : struct, IBinaryInteger<T>, ISignedNumber<T>, IMinMaxValue<T>
    {
        Debug.Assert(span.Length >= Vector<T>.Count * 4);
        Debug.Assert(Vector<T>.Count > 2);
        Debug.Assert(Vector.IsHardwareAccelerated);

        ref T ptr = ref MemoryMarshal.GetReference(span);
        nuint length = (nuint)span.Length;

        // Overflow testing for vectors is based on setting the sign bit of the overflowTracking
        // vector for an element if the following are all true:
        //   - The two elements being summed have the same sign bit. If one element is positive
        //     and the other is negative then an overflow is not possible.
        //   - The sign bit of the sum is not the same as the sign bit of the previous accumulator.
        //     This indicates that the new sum wrapped around to the opposite sign.
        //
        // This is done by:
        //   overflowTracking |= (result ^ input1) & (result ^ input2);
        //
        // The general premise here is that we're doing signof(result) ^ signof(input1). This will produce
        // a sign-bit of 1 if they differ and 0 if they are the same. We do the same with
        // signof(result) ^ signof(input2), then combine both results together with a logical &.
        //
        // Thus, if we had a sign swap compared to both inputs, then signof(input1) == signof(input2) and
        // we must have overflowed.
        //
        // By bitwise or-ing the overflowTracking vector for each step we can save cycles by testing
        // the sign bits less often. If any iteration has the sign bit set in any element it indicates
        // there was an overflow.
        //
        // Note: The overflow checking in this algorithm is only correct for signed integers.
        // If support is ever added for unsigned integers then the overflow check should be:
        //   overflowTracking |= (input1 & input2) | Vector.AndNot(input1 | input2, result);

        Vector<T> accumulator = Vector<T>.Zero;

        // Build a test vector with only the sign bit set in each element.
        Vector<T> overflowTestVector = new(T.MinValue);

        // Unroll the loop to sum 4 vectors per iteration. This reduces range check
        // and overflow check frequency, allows us to eliminate move operations swapping
        // accumulators, and may have pipelining benefits.
        nuint index = 0;
        nuint limit = length - (nuint)Vector<T>.Count * 4;
        do
        {
            // Switch accumulators with each step to avoid an additional move operation
            Vector<T> data = Vector.LoadUnsafe(ref ptr, index);
            Vector<T> accumulator2 = accumulator + data;
            Vector<T> overflowTracking = (accumulator2 ^ accumulator) & (accumulator2 ^ data);

            data = Vector.LoadUnsafe(ref ptr, index + (nuint)Vector<T>.Count);
            accumulator = accumulator2 + data;
            overflowTracking |= (accumulator ^ accumulator2) & (accumulator ^ data);

            data = Vector.LoadUnsafe(ref ptr, index + (nuint)Vector<T>.Count * 2);
            accumulator2 = accumulator + data;
            overflowTracking |= (accumulator2 ^ accumulator) & (accumulator2 ^ data);

            data = Vector.LoadUnsafe(ref ptr, index + (nuint)Vector<T>.Count * 3);
            accumulator = accumulator2 + data;
            overflowTracking |= (accumulator ^ accumulator2) & (accumulator ^ data);

            if ((overflowTracking & overflowTestVector) != Vector<T>.Zero)
            {
                throw new OverflowException();
            }

            index += (nuint)Vector<T>.Count * 4;
        } while (index < limit);

        // Process remaining vectors, if any, without unrolling
        limit = length - (nuint)Vector<T>.Count;
        if (index < limit)
        {
            Vector<T> overflowTracking = Vector<T>.Zero;

            do
            {
                Vector<T> data = Vector.LoadUnsafe(ref ptr, index);
                Vector<T> accumulator2 = accumulator + data;
                overflowTracking |= (accumulator2 ^ accumulator) & (accumulator2 ^ data);
                accumulator = accumulator2;

                index += (nuint)Vector<T>.Count;
            } while (index < limit);

            if ((overflowTracking & overflowTestVector) != Vector<T>.Zero)
            {
                throw new OverflowException();
            }
        }

        // Add the elements in the vector horizontally.
        // Vector.Sum doesn't perform overflow checking, instead add elements individually.
        T result = T.Zero;
        for (int i = 0; i < Vector<T>.Count; i++)
        {
            checked { result += accumulator[i]; }
        }

        // Add any remaining elements
        while (index < length)
        {
            checked { result += Unsafe.Add(ref ptr, index); }

            index++;
        }

        return result;
    }

    /// <summary>
    /// The sum.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Sum<T>(params T[] values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values);

    /// <summary>
    /// Find the sum of an array of Numbers
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Sum<T>(List<T> values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values);

    /// <summary>
    /// Find the sum of an array of Numbers
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Sum<T>(IEnumerable<T> values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values);

    /// <summary>
    /// Returns the average value of a numeric array.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Average<T>(params T[] values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values) / T.CreateTruncating(values.Length);

    /// <summary>
    /// Returns the average value of a numeric array.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Average<T>(this List<T> values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values) / T.CreateTruncating(values.Count);

    /// <summary>
    /// Returns the average value of a numeric array.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Average<T>(this IEnumerable<T> values) where T : struct, INumber<T> => Sum<T, T>((IEnumerable<T>)values) / T.CreateTruncating(values.Count());
    #endregion Array Math

    /// <summary>
    /// Ases the span.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable">The enumerable.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">enumerable</exception>
    public static ReadOnlySpan<T> AsSpan<T>(this IEnumerable<T> enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        // Convert the enumerable to an array (or other suitable collection)
        var array = enumerable.ToArray();

        // Get a read-only span from the array
        return new ReadOnlySpan<T>(array);
    }
}
