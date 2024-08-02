// <copyright file="Cultures.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Localization;

/// <summary>
/// Listing of common cultures.
/// </summary>
public static class Cultures
{
    /// <summary>
    /// English Australia.
    /// </summary>
    public static readonly Culture en_AU = new(Language.en, Country.AU);

    /// <summary>
    /// English Canada.
    /// </summary>
    public static readonly Culture en_CA = new(Language.en, Country.CA);

    /// <summary>
    /// English Great Britain.
    /// </summary>
    public static readonly Culture en_GB = new(Language.en, Country.GB);

    /// <summary>
    /// English Grenada.
    /// </summary>
    public static readonly Culture en_GD = new(Language.en, Country.GD);

    /// <summary>
    /// English Liberia.
    /// </summary>
    public static readonly Culture en_LR = new(Language.en, Country.LR);

    /// <summary>
    /// English United States.
    /// </summary>
    public static readonly Culture en_US = new(Language.en, Country.US);

    /// <summary>
    /// Pashto Afghanistan.
    /// </summary>
    public static readonly Culture ps_AF = new(Language.ps, Country.AF);
}
