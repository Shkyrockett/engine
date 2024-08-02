// <copyright file="StringFormat.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The text format class.
/// </summary>
public class TextFormat
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextFormat"/> class.
    /// </summary>
    /// <param name="formatFlags">The formatFlags.</param>
    /// <param name="digitSubstitutionLanguage">The digitSubstitutionLanguage.</param>
    public TextFormat(TextBoxFormatFlags formatFlags, int digitSubstitutionLanguage)
    {
        Format = formatFlags;
        Language = digitSubstitutionLanguage;
    }

    /// <summary>
    /// Gets or sets the format.
    /// </summary>
    public TextBoxFormatFlags Format { get; set; }

    /// <summary>
    /// Gets or sets the language.
    /// </summary>
    public int Language { get; set; }
}