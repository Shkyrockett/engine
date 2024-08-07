﻿// <copyright file="TimeLine.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The time line class.
/// </summary>
public class TimeLine
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeLine" /> class.
    /// </summary>
    public TimeLine()
        : this([])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeLine" /> class.
    /// </summary>
    /// <param name="actions">The actions.</param>
    public TimeLine(Dictionary<double, List<(Delegate, List<object>)>> actions)
    {
        Actions = actions;
    }

    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <value>
    /// The <see cref="List{T}"/>.
    /// </value>
    /// <param name="index">The index value.</param>
    /// <returns>
    /// One element of type List{(Delegate, List{object})}" .
    /// </returns>
    public List<(Delegate, List<object>)> this[double index]
    {
        get { return Actions[index]; }
        set
        {
            if (Actions[index] is null)
            {
                Actions.Add(index, value);
            }
            else
            {
                Actions[index] = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    /// <value>
    /// The time.
    /// </value>
    public double Time { get; set; }

    /// <summary>
    /// Gets or sets the tick.
    /// </summary>
    /// <value>
    /// The tick.
    /// </value>
    public double Tick { get; set; }

    /// <summary>
    /// Gets or sets the range.
    /// </summary>
    /// <value>
    /// The range.
    /// </value>
    public (double X, double Y) Range { get; set; }

    /// <summary>
    /// Gets or sets the actions.
    /// </summary>
    /// <value>
    /// The actions.
    /// </value>
    public Dictionary<double, List<(Delegate, List<object>)>> Actions { get; set; }

    /// <summary>
    /// Update.
    /// </summary>
    /// <returns>
    /// The <see cref="TimeLine" />.
    /// </returns>
    public TimeLine Update()
    {
        Time += Tick;
        return this;
    }

    /// <summary>
    /// Update.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="TimeLine" />.
    /// </returns>
    public TimeLine Update(double value)
    {
        Time += value;
        return this;
    }
}
