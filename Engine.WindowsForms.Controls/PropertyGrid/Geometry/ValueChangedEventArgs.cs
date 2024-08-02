// <copyright file="AngleControl.cs" company="Shkyrockett" >
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
/// The value changed event args class.
/// </summary>
public class ValueChangedEventArgs
        : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueChangedEventArgs"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public ValueChangedEventArgs(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public double Value { get; set; }
}
