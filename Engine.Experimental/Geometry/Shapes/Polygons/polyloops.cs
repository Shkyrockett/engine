using System.Diagnostics;
using System.Drawing;
//using System.Runtime;
//using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The poly loops class.
/// </summary>
public static class PolyLoops
{
    /// <summary>
    /// Test.
    /// </summary>
    public static void Test()
    {
        var watch = new Stopwatch();
        const int trials = 10000;

        int TotalRunningTime;
        double AverageRunningTime;

        var points = new PointF[]{
        new(1, 1),
        new(2, 1),
        new(3, 1),
        new(4, 1),
        new(5, 1),
        new(6, 1),
        new(7, 1),
        new(8, 1),
        new(9, 1),
        new(10, 1),
        new(11, 1),
        new(12, 1),
        new(13, 1),
        new(14, 1),
        new(15, 1),
        new(16, 1),
        new(17, 1),
        new(18, 1),
        new(19, 1),
        new(20, 1),
        new(21, 1),
        new(22, 1),
        new(23, 1),
        new(24, 1),
        new(25, 1),
    };

        float result = 0;

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //GCLatencyMode oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 1; i < points.Length; i++)
            {
                result = ProcessSegment(points[i - 1], points[i]);
            }
        }

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[for (var i = 1; i < points.Length; i++)]\n\rTotal running time:\t{TotalRunningTime}\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 0; i < points.Length; i++)
            {
                result = ProcessSegment(points[i], points[(i + 1) % points.Length]);
            }
        }

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[result = ProcessSegment(points[i], points[(i + 1) % points.Length]);])]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 0; i < points.Length; i++)
            {
                result = (i < points.Length - 1) ? ProcessSegment(points[i], points[i + 1]) : ProcessSegment(points[i], points[(i + 1) % points.Length]);
            }
        }

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[result = (i < points.Length - 1) ? ProcessSegment(points[i], points[i + 1]) : ProcessSegment(points[i], points[(i + 1) % points.Length]);]]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 0; i < points.Length; i++)
            {
                result = i < points.Length - 1 ? ProcessSegment(points[i], points[i + 1]) : ProcessSegment(points[i], points[(i + 1) % points.Length]);
            }
        }

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[if (i < points.Length - 1)]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 0; i < points.Length - 1; i++)
            {
                result = ProcessSegment(points[i], points[i + 1]);
            }
        }

        result = ProcessSegment(points[^1], points[0]);

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[for (var i = 0; i < points.Length - 1; i++)], points[0])]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (var i = 1; i < points.Length; i++)
            {
                result = ProcessSegment(points[i - 1], points[i]);
            }
        }

        result = ProcessSegment(points[^1], points[0]);

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[for (var i = 1; i < points.Length; i++)], points[0])]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //GC.WaitForPendingFinalizers();
        //oldMode = GCSettings.LatencyMode;
        //RuntimeHelpers.PrepareConstrainedRegions();
        //GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        watch.Reset();
        watch.Start();

        for (var z = 0; z < trials; z++)
        {
            for (int i = points.Length - 1, j = 0; j < points.Length; i = j++)
            {
                result = ProcessSegment(points[i], points[j]);
            }
        }

        watch.Stop();
        //GCSettings.LatencyMode = oldMode;

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / (trials * points.Length);

        Console.WriteLine($"[for (int i = points.Length - 1, j = 0; j < points.Length; i = j++)]\n\rTotal running time:\t{TotalRunningTime}ms.\n\rAverage running time:\t{AverageRunningTime}\n\r");
    }

    /// <summary>
    /// Process the segment.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="float"/>.</returns>
    public static float ProcessSegment(PointF a, PointF b) => (a.X * b.X) - (a.Y * b.Y);
}