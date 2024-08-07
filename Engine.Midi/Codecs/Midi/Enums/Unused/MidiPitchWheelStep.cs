﻿// <copyright file="MidiPitchWheelSteps.cs" company="Shkyrockett">
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
/// Pitch wheel steps.
/// </summary>
public enum MidiPitchWheelStep
    : int
{
    /// <summary>
    /// Whole Step Up.
    /// </summary>
    WholeStepUp = 16383,

    /// <summary>
    /// Three Quarter Step Up.
    /// </summary>
    ThreeQuarterStepUp = 13568,

    /// <summary>
    /// Half Step Up.
    /// </summary>
    HalfStepUp = 12288,

    /// <summary>
    /// Quarter Step Up.
    /// </summary>
    QuarterStepUp = 9472,

    /// <summary>
    /// No Step.
    /// </summary>
    NoStep = 8192,

    /// <summary>
    /// Quarter Step Down.
    /// </summary>
    QuarterStepDown = 5376,

    /// <summary>
    /// Half Step Down.
    /// </summary>
    HalfStepDown = 4096,

    /// <summary>
    /// Three Quarter Step Down.
    /// </summary>
    ThreeQuarterStepDown = 1280,

    /// <summary>
    /// Whole Step Down.
    /// </summary>
    WholeStepDown = 0
}
