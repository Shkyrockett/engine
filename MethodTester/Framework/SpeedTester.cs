﻿// <copyright file="SpeedTester.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on the class found at: http://www.vcskicks.com/algorithm-performance.php </remarks>

using System.ComponentModel;
using System.Diagnostics;

namespace MethodSpeedTester;

/// <summary>
/// Class for testing the speed that methods run.
/// </summary>
public class SpeedTester
{
    /// <summary>
    /// Delegate of the method to run.
    /// </summary>
    private readonly Delegate method;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeedTester"/> struct.
    /// </summary>
    /// <param name="methodToTest">Delegate of the method to run.</param>
    /// <param name="methodName">The signature of the method to run.</param>
    public SpeedTester(Func<object> methodToTest, string methodName)
    {
        method = methodToTest;
        MethodSignature = methodName;
    }

    /// <summary>
    /// Gets the signature of the method to run.
    /// </summary>
    [DisplayName("Method Signature")]
    public string MethodSignature { get; }

    /// <summary>
    /// Gets the total milliseconds it took to run the method trials.
    /// </summary>
    [DisplayName("Total Running Time")]
    public int? TotalRunningTime { get; private set; } = null;

    /// <summary>
    /// Gets the total time over the number of trials.
    /// </summary>
    [DisplayName("Average Running Time")]
    public double? AverageRunningTime { get; private set; } = null;

    /// <summary>
    /// Gets the value that the method returns.
    /// </summary>
    [DisplayName("Return Value")]
    public object ReturnValue { get; private set; } = null;

    /// <summary>
    /// Runs trials of the method delegate a specified number of times.
    /// </summary>
    /// <param name="trials">The number of times to run the tests. Default is 10,000 trials.</param>
    public void RunTest(int trials = 10000)
    {
        var watch = new Stopwatch();
        watch.Reset();
        watch.Start();
        for (var i = 0; i < trials; i++)
        {
            //run the method
            ReturnValue = (method as Func<object>).Invoke();
        }
        watch.Stop();

        TotalRunningTime = (int)watch.ElapsedMilliseconds;
        AverageRunningTime = (double)TotalRunningTime / trials;
    }
}
