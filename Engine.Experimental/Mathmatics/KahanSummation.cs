using System.Runtime.CompilerServices;

namespace Engine.Experimental;

/// <summary>
/// The kahan summation class.
/// https://rosettacode.org/wiki/Kahan_summation#Java
/// </summary>
public static class KahanSummation
{
    /// <summary>
    /// The kahan sum.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double KahanSum(params double[] values) => KahanSum((IEnumerable<double>)values);

    /// <summary>
    /// The kahan sum.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double KahanSum(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        // A running compensation for lost low-order bits.
        var c = 0d;

        // The summed values.
        var sum = 0d;
        foreach (var value in values)
        {
            var y = value - c;
            var t = sum + y;
            c = t - sum - y;
            sum = t;
        }

        return sum;
    }
}
