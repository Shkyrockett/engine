﻿// <copyright file="PaletteMimeFormats.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

namespace Engine.File.Palettes
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Enumeration of palette MIME formats
    /// </summary>
    public enum PaletteMimeFormats
    {
        /// <summary>
        /// There is no MIME format.
        /// </summary>
        Default,

        /// <summary>
        /// Adobe's format of pallets.
        /// </summary>
        Adobe,

        /// <summary>
        /// AutoDesk's format of pallets.
        /// </summary>
        AutoDesk,

        /// <summary>
        /// Corel's format of pallets.
        /// </summary>
        Corel,

        /// <summary>
        /// Jasc text palette format.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        JascPal0100,

        /// <summary>
        /// Paint.NET Palette File.
        /// </summary>
        PaintDotNet,

        /// <summary>
        /// Binary palette format.
        /// </summary>
        Binary,

        /// <summary>
        /// Windows 3.1 MSPaint palette format.
        /// </summary>
        Win31Pal,

        /// <summary>
        /// Riff wrapper formatted palette.
        /// </summary>
        RiffPal,

        /// <summary>
        /// Plain text format.
        /// </summary>
        Text,

        /// <summary>
        /// Coma delimiated text format.
        /// </summary>
        ComaDelimiated,

        /// <summary>
        /// Space delimiated text format.
        /// </summary>
        SpaceDelimiated,
    }
}
