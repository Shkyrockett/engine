﻿// <copyright file="TuneRequest.cs" company="Shkyrockett">
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
/// Tune Request. Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.
/// </summary>
/// <remarks>
/// <para>nF 06 
/// Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</para>
/// </remarks>
[ElementName(nameof(TuneRequest))]
public class TuneRequest
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TuneRequest" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    public TuneRequest(IEventStatus status)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    { }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="TuneRequest" />.
    /// </returns>
    internal static new TuneRequest Read(BinaryReaderExtended reader, IEventStatus status)
    {
        _ = reader;
        return new TuneRequest(status);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Tune Request";
}
