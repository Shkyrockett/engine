// <copyright file="PaletteFileExtensions.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Diagnostics.CodeAnalysis;

namespace Engine.File.Palettes
{
    /// <summary>
    /// Enumeration of file extensions for pallet files.
    /// </summary>
    public enum PaletteFileExtensions
    {
        /// <summary>
        /// An extension has not yet been set.
        /// </summary>
        unknown,

        /// <summary>
        /// AutoDesk AutoCAD Color Book .acb file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        acb,

        /// <summary>
        /// Adobe Color .aco file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        aco,

        /// <summary>
        /// Adobe Color .act file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        act,

        /// <summary>
        /// Corel color palette .cpl file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        cpl,

        /// <summary>
        /// Microsoft Paint, or Corel Paint Shop Pro palette .pal file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        pal,

        /// <summary>
        /// Palette .palette file.
        /// </summary>
        palette,

        /// <summary>
        /// Text document .txt text file.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        txt,
    }
}
