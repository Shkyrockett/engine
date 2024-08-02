// <copyright file="TestCases.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Editor;

/// <summary>
/// The scrubber class.
/// </summary>
public class Scrubber
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scrubber"/> class.
    /// </summary>
    /// <param name="t">The t.</param>
    public Scrubber(double t)
    {
        T = t;
    }

    /// <summary>
    /// Gets or sets the t.
    /// </summary>
    public double T { get; set; }
}
