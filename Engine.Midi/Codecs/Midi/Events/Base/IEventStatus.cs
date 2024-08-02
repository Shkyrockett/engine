// <copyright file="IEventStatus.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Engine.File.IMediaElement" />
public interface IEventStatus
    : IMediaElement
{
    /// <summary>
    /// Gets or sets the delta time.
    /// </summary>
    /// <value>
    /// The delta time.
    /// </value>
    uint DeltaTime { get; set; }

    /// <summary>
    /// Gets or sets the channel.
    /// </summary>
    /// <value>
    /// The channel.
    /// </value>
    byte Channel { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    MidiStatusMessage Message { get; set; }
}