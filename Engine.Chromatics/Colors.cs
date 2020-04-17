// <copyright file="Colors.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine.Colorspace
{
    /// <summary>
    /// The colors class.
    /// </summary>
    public static class Colors
    {
        // Note: These color integers are currently in RRGGBBAA order. Windows .NET orders these in AARRGGBB order. Consider whether to change the order to match .NET or not.

        /// <summary>
        /// Transparent
        /// </summary>
        public static readonly RGBA Transparent = new RGBA(unchecked((int)0xFFFFFF00), nameof(Transparent));

        /// <summary>
        /// Alice Blue
        /// </summary>
        public static readonly RGBA AliceBlue = new RGBA(unchecked((int)0xF0F8FFFF), nameof(AliceBlue));

        /// <summary>
        /// Antique White
        /// </summary>
        public static readonly RGBA AntiqueWhite = new RGBA(unchecked((int)0xFAEBD7FF), nameof(AntiqueWhite));

        /// <summary>
        /// Aqua
        /// </summary>
        public static readonly RGBA Aqua = new RGBA(unchecked((int)0x00FFFFFF), nameof(Aqua));

        /// <summary>
        /// Aquamarine
        /// </summary>
        public static readonly RGBA Aquamarine = new RGBA(unchecked((int)0x7FFFD4FF), nameof(Aquamarine));

        /// <summary>
        /// Azure
        /// </summary>
        public static readonly RGBA Azure = new RGBA(unchecked((int)0xF0FFFFFF), nameof(Azure));

        /// <summary>
        /// Beige
        /// </summary>
        public static readonly RGBA Beige = new RGBA(unchecked((int)0xF5F5DCFF), nameof(Beige));

        /// <summary>
        /// Bisque
        /// </summary>
        public static readonly RGBA Bisque = new RGBA(unchecked(unchecked((int)0xFFE4C4FF)), nameof(Bisque));

        /// <summary>
        /// Black
        /// </summary>
        public static readonly RGBA Black = new RGBA(unchecked((int)0x000000FF), nameof(Black));

        /// <summary>
        /// Blanched Almond
        /// </summary>
        public static readonly RGBA BlanchedAlmond = new RGBA(unchecked((int)0xFFEBCDFF), nameof(BlanchedAlmond));

        /// <summary>
        /// Blue
        /// </summary>
        public static readonly RGBA Blue = new RGBA(unchecked((int)0x0000FFFF), nameof(Blue));

        /// <summary>
        /// Blue Violet
        /// </summary>
        public static readonly RGBA BlueViolet = new RGBA(unchecked((int)0x8A2BE2FF), nameof(BlueViolet));

        /// <summary>
        /// Brown
        /// </summary>
        public static readonly RGBA Brown = new RGBA(unchecked((int)0xA52A2AFF), nameof(Brown));

        /// <summary>
        /// Burly Wood
        /// </summary>
        public static readonly RGBA BurlyWood = new RGBA(unchecked((int)0xDEB887FF), nameof(BurlyWood));

        /// <summary>
        /// Cadet Blue
        /// </summary>
        public static readonly RGBA CadetBlue = new RGBA(unchecked((int)0x5F9EA0FF), nameof(CadetBlue));

        /// <summary>
        /// Chartreuse
        /// </summary>
        public static readonly RGBA Chartreuse = new RGBA(unchecked((int)0x7FFF00FF), nameof(Chartreuse));

        /// <summary>
        /// Chocolate
        /// </summary>
        public static readonly RGBA Chocolate = new RGBA(unchecked((int)0xD2691EFF), nameof(Chocolate));

        /// <summary>
        /// Coral
        /// </summary>
        public static readonly RGBA Coral = new RGBA(unchecked((int)0xFF7F50FF), nameof(Coral));

        /// <summary>
        /// Cornflower Blue
        /// </summary>
        public static readonly RGBA CornflowerBlue = new RGBA(unchecked((int)0x6495EDFF), nameof(CornflowerBlue));

        /// <summary>
        /// Corn-silk
        /// </summary>
        public static readonly RGBA Cornsilk = new RGBA(unchecked((int)0xFFF8DCFF), nameof(Cornsilk));

        /// <summary>
        /// Crimson
        /// </summary>
        public static readonly RGBA Crimson = new RGBA(unchecked((int)0xDC143CFF), nameof(Crimson));

        /// <summary>
        /// Cyan
        /// </summary>
        public static readonly RGBA Cyan = new RGBA(unchecked((int)0x00FFFFFF), nameof(Cyan));

        /// <summary>
        /// Dark Blue
        /// </summary>
        public static readonly RGBA DarkBlue = new RGBA(unchecked((int)0x00008BFF), nameof(DarkBlue));

        /// <summary>
        /// Dark Cyan
        /// </summary>
        public static readonly RGBA DarkCyan = new RGBA(unchecked((int)0x008B8BFF), nameof(DarkCyan));

        /// <summary>
        /// Dark Goldenrod
        /// </summary>
        public static readonly RGBA DarkGoldenrod = new RGBA(unchecked((int)0xB8860BFF), nameof(DarkGoldenrod));

        /// <summary>
        /// Dark Gray
        /// </summary>
        public static readonly RGBA DarkGray = new RGBA(unchecked((int)0xA9A9A9FF), nameof(DarkGray));

        /// <summary>
        /// Dark Green
        /// </summary>
        public static readonly RGBA DarkGreen = new RGBA(unchecked((int)0x006400FF), nameof(DarkGreen));

        /// <summary>
        /// Dark Khaki
        /// </summary>
        public static readonly RGBA DarkKhaki = new RGBA(unchecked((int)0xBDB76BFF), nameof(DarkKhaki));

        /// <summary>
        /// Dark Magenta
        /// </summary>
        public static readonly RGBA DarkMagenta = new RGBA(unchecked((int)0x8B008BFF), nameof(DarkMagenta));

        /// <summary>
        /// Dark Olive Green
        /// </summary>
        public static readonly RGBA DarkOliveGreen = new RGBA(unchecked((int)0x556B2FFF), nameof(DarkOliveGreen));

        /// <summary>
        /// Dark Orange
        /// </summary>
        public static readonly RGBA DarkOrange = new RGBA(unchecked((int)0xFF8C00FF), nameof(DarkOrange));

        /// <summary>
        /// Dark Orchid
        /// </summary>
        public static readonly RGBA DarkOrchid = new RGBA(unchecked((int)0x9932CCFF), nameof(DarkOrchid));

        /// <summary>
        /// Dark Red
        /// </summary>
        public static readonly RGBA DarkRed = new RGBA(unchecked((int)0x8B0000FF), nameof(DarkRed));

        /// <summary>
        /// Dark Salmon
        /// </summary>
        public static readonly RGBA DarkSalmon = new RGBA(unchecked((int)0xE9967AFF), nameof(DarkSalmon));

        /// <summary>
        /// Dark Sea Green
        /// </summary>
        public static readonly RGBA DarkSeaGreen = new RGBA(unchecked((int)0x8FBC8BFF), nameof(DarkSeaGreen));

        /// <summary>
        /// Dark Slate Blue
        /// </summary>
        public static readonly RGBA DarkSlateBlue = new RGBA(unchecked((int)0x483D8BFF), nameof(DarkSlateBlue));

        /// <summary>
        /// Dark Slate Gray
        /// </summary>
        public static readonly RGBA DarkSlateGray = new RGBA(unchecked((int)0x2F4F4FFF), nameof(DarkSlateGray));

        /// <summary>
        /// Dark Turquoise
        /// </summary>
        public static readonly RGBA DarkTurquoise = new RGBA(unchecked((int)0x00CED1FF), nameof(DarkTurquoise));

        /// <summary>
        /// Dark Violet
        /// </summary>
        public static readonly RGBA DarkViolet = new RGBA(unchecked((int)0x9400D3FF), nameof(DarkViolet));

        /// <summary>
        /// Deep Pink
        /// </summary>
        public static readonly RGBA DeepPink = new RGBA(unchecked((int)0xFF1493FF), nameof(DeepPink));

        /// <summary>
        /// Deep Sky Blue
        /// </summary>
        public static readonly RGBA DeepSkyBlue = new RGBA(unchecked((int)0x00BFFFFF), nameof(DeepSkyBlue));

        /// <summary>
        /// Dim Gray
        /// </summary>
        public static readonly RGBA DimGray = new RGBA(unchecked((int)0x696969FF), nameof(DimGray));

        /// <summary>
        /// Dodger Blue
        /// </summary>
        public static readonly RGBA DodgerBlue = new RGBA(unchecked((int)0x1E90FFFF), nameof(DodgerBlue));

        /// <summary>
        /// Firebrick
        /// </summary>
        public static readonly RGBA Firebrick = new RGBA(unchecked((int)0xB22222FF), nameof(Firebrick));

        /// <summary>
        /// Floral White
        /// </summary>
        public static readonly RGBA FloralWhite = new RGBA(unchecked((int)0xFFFAF0FF), nameof(FloralWhite));

        /// <summary>
        /// Forest Green
        /// </summary>
        public static readonly RGBA ForestGreen = new RGBA(unchecked((int)0x228B22FF), nameof(ForestGreen));

        /// <summary>
        /// Fuchsia
        /// </summary>
        public static readonly RGBA Fuchsia = new RGBA(unchecked((int)0xFF00FFFF), nameof(Fuchsia));

        /// <summary>
        /// Gainsboro
        /// </summary>
        public static readonly RGBA Gainsboro = new RGBA(unchecked((int)0xDCDCDCFF), nameof(Gainsboro));

        /// <summary>
        /// Ghost White
        /// </summary>
        public static readonly RGBA GhostWhite = new RGBA(unchecked((int)0xF8F8FFFF), nameof(GhostWhite));

        /// <summary>
        /// Gold
        /// </summary>
        public static readonly RGBA Gold = new RGBA(unchecked((int)0xFFD700FF), nameof(Gold));

        /// <summary>
        /// Goldenrod
        /// </summary>
        public static readonly RGBA Goldenrod = new RGBA(unchecked((int)0xDAA520FF), nameof(Goldenrod));

        /// <summary>
        /// Gray
        /// </summary>
        public static readonly RGBA Gray = new RGBA(unchecked((int)0x808080FF), nameof(Gray));

        /// <summary>
        /// Green
        /// </summary>
        public static readonly RGBA Green = new RGBA(unchecked((int)0x008000FF), nameof(Green));

        /// <summary>
        /// Green Yellow
        /// </summary>
        public static readonly RGBA GreenYellow = new RGBA(unchecked((int)0xADFF2FFF), nameof(GreenYellow));

        /// <summary>
        /// Honeydew
        /// </summary>
        public static readonly RGBA Honeydew = new RGBA(unchecked((int)0xF0FFF0FF), nameof(Honeydew));

        /// <summary>
        /// Hot Pink
        /// </summary>
        public static readonly RGBA HotPink = new RGBA(unchecked((int)0xFF69B4FF), nameof(HotPink));

        /// <summary>
        /// Indian Red
        /// </summary>
        public static readonly RGBA IndianRed = new RGBA(unchecked((int)0xCD5C5CFF), nameof(IndianRed));

        /// <summary>
        /// Indigo
        /// </summary>
        public static readonly RGBA Indigo = new RGBA(unchecked((int)0x4B0082FF), nameof(Indigo));

        /// <summary>
        /// Ivory
        /// </summary>
        public static readonly RGBA Ivory = new RGBA(unchecked((int)0xFFFFF0FF), nameof(Ivory));

        /// <summary>
        /// Khaki
        /// </summary>
        public static readonly RGBA Khaki = new RGBA(unchecked((int)0xF0E68CFF), nameof(Khaki));

        /// <summary>
        /// Lavender
        /// </summary>
        public static readonly RGBA Lavender = new RGBA(unchecked((int)0xE6E6FAFF), nameof(Lavender));

        /// <summary>
        /// Lavender Blush
        /// </summary>
        public static readonly RGBA LavenderBlush = new RGBA(unchecked((int)0xFFF0F5FF), nameof(LavenderBlush));

        /// <summary>
        /// Lawn Green
        /// </summary>
        public static readonly RGBA LawnGreen = new RGBA(unchecked((int)0x7CFC00FF), nameof(LawnGreen));

        /// <summary>
        /// Lemon Chiffon
        /// </summary>
        public static readonly RGBA LemonChiffon = new RGBA(unchecked((int)0xFFFACDFF), nameof(LemonChiffon));

        /// <summary>
        /// Light Blue
        /// </summary>
        public static readonly RGBA LightBlue = new RGBA(unchecked((int)0xADD8E6FF), nameof(LightBlue));

        /// <summary>
        /// Light Coral
        /// </summary>
        public static readonly RGBA LightCoral = new RGBA(unchecked((int)0xF08080FF), nameof(LightCoral));

        /// <summary>
        /// Light Cyan
        /// </summary>
        public static readonly RGBA LightCyan = new RGBA(unchecked((int)0xE0FFFFFF), nameof(LightCyan));

        /// <summary>
        /// Light Goldenrod Yellow
        /// </summary>
        public static readonly RGBA LightGoldenrodYellow = new RGBA(unchecked((int)0xFAFAD2FF), nameof(LightGoldenrodYellow));

        /// <summary>
        /// Light Green
        /// </summary>
        public static readonly RGBA LightGreen = new RGBA(unchecked((int)0xD3D3D3FF), nameof(LightGreen));

        /// <summary>
        /// Light Gray
        /// </summary>
        public static readonly RGBA LightGray = new RGBA(unchecked((int)0x90EE90FF), nameof(LightGray));

        /// <summary>
        /// Light Pink
        /// </summary>
        public static readonly RGBA LightPink = new RGBA(unchecked((int)0xFFB6C1FF), nameof(LightPink));

        /// <summary>
        /// Light Salmon
        /// </summary>
        public static readonly RGBA LightSalmon = new RGBA(unchecked((int)0xFFA07AFF), nameof(LightSalmon));

        /// <summary>
        /// Light Sea Green
        /// </summary>
        public static readonly RGBA LightSeaGreen = new RGBA(unchecked((int)0x20B2AAFF), nameof(LightSeaGreen));

        /// <summary>
        /// Light Sky Blue
        /// </summary>
        public static readonly RGBA LightSkyBlue = new RGBA(unchecked((int)0x87CEFAFF), nameof(LightSkyBlue));

        /// <summary>
        /// Light Slate Gray
        /// </summary>
        public static readonly RGBA LightSlateGray = new RGBA(unchecked((int)0x778899FF), nameof(LightSlateGray));

        /// <summary>
        /// Light Steel Blue
        /// </summary>
        public static readonly RGBA LightSteelBlue = new RGBA(unchecked((int)0xB0C4DEFF), nameof(LightSteelBlue));

        /// <summary>
        /// Light Yellow
        /// </summary>
        public static readonly RGBA LightYellow = new RGBA(unchecked((int)0xFFFFE0FF), nameof(LightYellow));

        /// <summary>
        /// Lime
        /// </summary>
        public static readonly RGBA Lime = new RGBA(unchecked((int)0x00FF00FF), nameof(Lime));

        /// <summary>
        /// Lime Green
        /// </summary>
        public static readonly RGBA LimeGreen = new RGBA(unchecked((int)0x32CD32FF), nameof(LimeGreen));

        /// <summary>
        /// Linen
        /// </summary>
        public static readonly RGBA Linen = new RGBA(unchecked((int)0xFAF0E6FF), nameof(Linen));

        /// <summary>
        /// Magenta
        /// </summary>
        public static readonly RGBA Magenta = new RGBA(unchecked((int)0xFF00FFFF), nameof(Magenta));

        /// <summary>
        /// Maroon
        /// </summary>
        public static readonly RGBA Maroon = new RGBA(unchecked((int)0x800000FF), nameof(Maroon));

        /// <summary>
        /// Medium Aquamarine
        /// </summary>
        public static readonly RGBA MediumAquamarine = new RGBA(unchecked((int)0x66CDAAFF), nameof(MediumAquamarine));

        /// <summary>
        /// Medium Blue
        /// </summary>
        public static readonly RGBA MediumBlue = new RGBA(unchecked((int)0x0000CDFF), nameof(MediumBlue));

        /// <summary>
        /// Medium Orchid
        /// </summary>
        public static readonly RGBA MediumOrchid = new RGBA(unchecked((int)0xBA55D3FF), nameof(MediumOrchid));

        /// <summary>
        /// Medium Purple
        /// </summary>
        public static readonly RGBA MediumPurple = new RGBA(unchecked((int)0x9370DBFF), nameof(MediumPurple));

        /// <summary>
        /// Medium Sea Green
        /// </summary>
        public static readonly RGBA MediumSeaGreen = new RGBA(unchecked((int)0x3CB371FF), nameof(MediumSeaGreen));

        /// <summary>
        /// Medium Slate Blue
        /// </summary>
        public static readonly RGBA MediumSlateBlue = new RGBA(unchecked((int)0x7B68EEFF), nameof(MediumSlateBlue));

        /// <summary>
        /// Medium Spring Green
        /// </summary>
        public static readonly RGBA MediumSpringGreen = new RGBA(unchecked((int)0x00FA9AFF), nameof(MediumSpringGreen));

        /// <summary>
        /// Medium Turquoise
        /// </summary>
        public static readonly RGBA MediumTurquoise = new RGBA(unchecked((int)0x48D1CCFF), nameof(MediumTurquoise));

        /// <summary>
        /// Medium Violet Red
        /// </summary>
        public static readonly RGBA MediumVioletRed = new RGBA(unchecked((int)0xC71585FF), nameof(MediumVioletRed));

        /// <summary>
        /// Midnight Blue
        /// </summary>
        public static readonly RGBA MidnightBlue = new RGBA(unchecked((int)0x191970FF), nameof(MidnightBlue));

        /// <summary>
        /// Mint Cream
        /// </summary>
        public static readonly RGBA MintCream = new RGBA(unchecked((int)0xF5FFFAFF), nameof(MintCream));

        /// <summary>
        /// Misty Rose
        /// </summary>
        public static readonly RGBA MistyRose = new RGBA(unchecked((int)0xFFE4E1FF), nameof(MistyRose));

        /// <summary>
        /// Moccasin
        /// </summary>
        public static readonly RGBA Moccasin = new RGBA(unchecked((int)0xFFE4B5FF), nameof(Moccasin));

        /// <summary>
        /// Navajo White
        /// </summary>
        public static readonly RGBA NavajoWhite = new RGBA(unchecked((int)0xFFDEADFF), nameof(NavajoWhite));

        /// <summary>
        /// Navy
        /// </summary>
        public static readonly RGBA Navy = new RGBA(unchecked((int)0x000080FF), nameof(Navy));

        /// <summary>
        /// Old Lace
        /// </summary>
        public static readonly RGBA OldLace = new RGBA(unchecked((int)0xFDF5E6FF), nameof(OldLace));

        /// <summary>
        /// Olive
        /// </summary>
        public static readonly RGBA Olive = new RGBA(unchecked((int)0x808000FF), nameof(Olive));

        /// <summary>
        /// Olive Drab
        /// </summary>
        public static readonly RGBA OliveDrab = new RGBA(unchecked((int)0x6B8E23FF), nameof(OliveDrab));

        /// <summary>
        /// Orange
        /// </summary>
        public static readonly RGBA Orange = new RGBA(unchecked((int)0xFFA500FF), nameof(Orange));

        /// <summary>
        /// Orange Red
        /// </summary>
        public static readonly RGBA OrangeRed = new RGBA(unchecked((int)0xFF4500FF), nameof(OrangeRed));

        /// <summary>
        /// Orchid
        /// </summary>
        public static readonly RGBA Orchid = new RGBA(unchecked((int)0xDA70D6FF), nameof(Orchid));

        /// <summary>
        /// Pale Goldenrod
        /// </summary>
        public static readonly RGBA PaleGoldenrod = new RGBA(unchecked((int)0xEEE8AAFF), nameof(PaleGoldenrod));

        /// <summary>
        /// Pale Green
        /// </summary>
        public static readonly RGBA PaleGreen = new RGBA(unchecked((int)0x98FB98FF), nameof(PaleGreen));

        /// <summary>
        /// PaleTurquoise
        /// </summary>
        public static readonly RGBA PaleTurquoise = new RGBA(unchecked((int)0xAFEEEEFF), nameof(PaleTurquoise));

        /// <summary>
        /// Pale Violet Red
        /// </summary>
        public static readonly RGBA PaleVioletRed = new RGBA(unchecked((int)0xDB7093FF), nameof(PaleVioletRed));

        /// <summary>
        /// Papaya Whip
        /// </summary>
        public static readonly RGBA PapayaWhip = new RGBA(unchecked((int)0xFFEFD5FF), nameof(PapayaWhip));

        /// <summary>
        /// Peach Puff
        /// </summary>
        public static readonly RGBA PeachPuff = new RGBA(unchecked((int)0xFFDAB9FF), nameof(PeachPuff));

        /// <summary>
        /// Peru
        /// </summary>
        public static readonly RGBA Peru = new RGBA(unchecked((int)0xCD853FFF), nameof(Peru));

        /// <summary>
        /// Pink
        /// </summary>
        public static readonly RGBA Pink = new RGBA(unchecked((int)0xFFC0CBFF), nameof(Pink));

        /// <summary>
        /// Plum
        /// </summary>
        public static readonly RGBA Plum = new RGBA(unchecked((int)0xDDA0DDFF), nameof(Plum));

        /// <summary>
        /// Powder Blue
        /// </summary>
        public static readonly RGBA PowderBlue = new RGBA(unchecked((int)0xB0E0E6FF), nameof(PowderBlue));

        /// <summary>
        /// Purple
        /// </summary>
        public static readonly RGBA Purple = new RGBA(unchecked((int)0x800080FF), nameof(Purple));

        /// <summary>
        /// Red
        /// </summary>
        public static readonly RGBA Red = new RGBA(unchecked((int)0xFF0000FF), nameof(Red));

        /// <summary>
        /// Rosy Brown
        /// </summary>
        public static readonly RGBA RosyBrown = new RGBA(unchecked((int)0xBC8F8FFF), nameof(RosyBrown));

        /// <summary>
        /// Royal Blue
        /// </summary>
        public static readonly RGBA RoyalBlue = new RGBA(unchecked((int)0x4169E1FF), nameof(RoyalBlue));

        /// <summary>
        /// Saddle Brown
        /// </summary>
        public static readonly RGBA SaddleBrown = new RGBA(unchecked((int)0x8B4513FF), nameof(SaddleBrown));

        /// <summary>
        /// Salmon
        /// </summary>
        public static readonly RGBA Salmon = new RGBA(unchecked((int)0xFA8072FF), nameof(Salmon));

        /// <summary>
        /// Sandy Brown
        /// </summary>
        public static readonly RGBA SandyBrown = new RGBA(unchecked((int)0xF4A460FF), nameof(SandyBrown));

        /// <summary>
        /// Sea Green
        /// </summary>
        public static readonly RGBA SeaGreen = new RGBA(unchecked((int)0x2E8B57FF), nameof(SeaGreen));

        /// <summary>
        /// Sea Shell
        /// </summary>
        public static readonly RGBA SeaShell = new RGBA(unchecked((int)0xFFF5EEFF), nameof(SeaShell));

        /// <summary>
        /// Sienna
        /// </summary>
        public static readonly RGBA Sienna = new RGBA(unchecked((int)0xA0522DFF), nameof(Sienna));

        /// <summary>
        /// Silver
        /// </summary>
        public static readonly RGBA Silver = new RGBA(unchecked((int)0xC0C0C0FF), nameof(Silver));

        /// <summary>
        /// Sky Blue
        /// </summary>
        public static readonly RGBA SkyBlue = new RGBA(unchecked((int)0x87CEEBFF), nameof(SkyBlue));

        /// <summary>
        /// Slate Blue
        /// </summary>
        public static readonly RGBA SlateBlue = new RGBA(unchecked((int)0x6A5ACDFF), nameof(SlateBlue));

        /// <summary>
        /// Slate Gray
        /// </summary>
        public static readonly RGBA SlateGray = new RGBA(unchecked((int)0x708090FF), nameof(SlateGray));

        /// <summary>
        /// Snow
        /// </summary>
        public static readonly RGBA Snow = new RGBA(unchecked((int)0xFFFAFAFF), nameof(Snow));

        /// <summary>
        /// Spring Green
        /// </summary>
        public static readonly RGBA SpringGreen = new RGBA(unchecked((int)0x00FF7FFF), nameof(SpringGreen));

        /// <summary>
        /// Steel Blue
        /// </summary>
        public static readonly RGBA SteelBlue = new RGBA(unchecked((int)0x4682B4FF), nameof(SteelBlue));

        /// <summary>
        /// Tan
        /// </summary>
        public static readonly RGBA Tan = new RGBA(unchecked((int)0xD2B48CFF), nameof(Tan));

        /// <summary>
        /// Teal
        /// </summary>
        public static readonly RGBA Teal = new RGBA(unchecked((int)0x008080FF), nameof(Teal));

        /// <summary>
        /// Thistle
        /// </summary>
        public static readonly RGBA Thistle = new RGBA(unchecked((int)0xD8BFD8FF), nameof(Thistle));

        /// <summary>
        /// Tomato
        /// </summary>
        public static readonly RGBA Tomato = new RGBA(unchecked((int)0xFF6347FF), nameof(Tomato));

        /// <summary>
        /// Turquoise
        /// </summary>
        public static readonly RGBA Turquoise = new RGBA(unchecked((int)0x40E0D0FF), nameof(Turquoise));

        /// <summary>
        /// Violet
        /// </summary>
        public static readonly RGBA Violet = new RGBA(unchecked((int)0xEE82EEFF), nameof(Violet));

        /// <summary>
        /// Wheat
        /// </summary>
        public static readonly RGBA Wheat = new RGBA(unchecked((int)0xF5DEB3FF), nameof(Wheat));

        /// <summary>
        /// White
        /// </summary>
        public static readonly RGBA White = new RGBA(unchecked((int)0xFFFFFFFF), nameof(White));

        /// <summary>
        /// White Smoke
        /// </summary>
        public static readonly RGBA WhiteSmoke = new RGBA(unchecked((int)0xF5F5F5FF), nameof(WhiteSmoke));

        /// <summary>
        /// Yellow
        /// </summary>
        public static readonly RGBA Yellow = new RGBA(unchecked((int)0xFFFF00FF), nameof(Yellow));

        /// <summary>
        /// Yellow Green
        /// </summary>
        public static readonly RGBA YellowGreen = new RGBA(unchecked((int)0x9ACD32FF), nameof(YellowGreen));

        // Important: The Color Dictionary must follow the color definitions in order to populate it with the colors.

        /// <summary>
        /// The color dictionary for looking up color names.
        /// </summary>
        public static readonly Dictionary<RGBA, string> Color = new Dictionary<RGBA, string>
        {
            { Transparent, Transparent.Name },
            { AliceBlue, AliceBlue.Name },
            { AntiqueWhite, AntiqueWhite.Name },
            //{ Aqua, Aqua.Name }, // Aqua and Cyan are the same.
            { Aquamarine, Aquamarine.Name },
            { Azure, Azure.Name },
            { Beige, Beige.Name },
            { Bisque, Bisque.Name },
            { Black, Black.Name },
            { BlanchedAlmond, BlanchedAlmond.Name },
            { Blue, Blue.Name },
            { BlueViolet, BlueViolet.Name },
            { Brown, Brown.Name },
            { BurlyWood, BurlyWood.Name },
            { CadetBlue, CadetBlue.Name },
            { Chartreuse, Chartreuse.Name },
            { Chocolate, Chocolate.Name },
            { Coral, Coral.Name },
            { CornflowerBlue, CornflowerBlue.Name },
            { Cornsilk, Cornsilk.Name },
            { Crimson, Crimson.Name },
            { Cyan, Cyan.Name },
            { DarkBlue, DarkBlue.Name },
            { DarkGoldenrod, DarkGoldenrod.Name },
            { DarkGray, DarkGray.Name },
            { DarkGreen, DarkGreen.Name },
            { DarkKhaki, DarkKhaki.Name },
            { DarkMagenta, DarkMagenta.Name },
            { DarkOliveGreen, DarkOliveGreen.Name },
            { DarkOrange, DarkOrange.Name },
            { DarkOrchid, DarkOrchid.Name },
            { DarkRed, DarkRed.Name },
            { DarkSalmon, DarkSalmon.Name },
            { DarkSeaGreen, DarkSeaGreen.Name },
            { DarkSlateBlue, DarkSlateBlue.Name },
            { DarkSlateGray, DarkSlateGray.Name },
            { DarkTurquoise, DarkTurquoise.Name },
            { DarkViolet, DarkViolet.Name },
            { DeepPink, DeepPink.Name },
            { DeepSkyBlue, DeepSkyBlue.Name },
            { DimGray, DimGray.Name },
            { DodgerBlue, DodgerBlue.Name },
            { Firebrick, Firebrick.Name },
            { FloralWhite, FloralWhite.Name },
            { ForestGreen, ForestGreen.Name },
            //{ Fuchsia, Fuchsia.Name }, Fuchsia and Magenta are the same.
            { Gainsboro, Gainsboro.Name },
            { GhostWhite, GhostWhite.Name },
            { Gold, Gold.Name },
            { Goldenrod, Goldenrod.Name },
            { Gray, Gray.Name },
            { Green, Green.Name },
            { GreenYellow, GreenYellow.Name },
            { Honeydew, Honeydew.Name },
            { HotPink, HotPink.Name },
            { IndianRed, IndianRed.Name },
            { Indigo, Indigo.Name },
            { Ivory, Ivory.Name },
            { Khaki, Khaki.Name },
            { Lavender, Lavender.Name },
            { LavenderBlush, LavenderBlush.Name },
            { LawnGreen, LawnGreen.Name },
            { LemonChiffon, LemonChiffon.Name },
            { LightBlue, LightBlue.Name },
            { LightCoral, LightCoral.Name },
            { LightCyan, LightCyan.Name },
            { LightGoldenrodYellow, LightGoldenrodYellow.Name },
            { LightGreen, LightGreen.Name },
            { LightGray, LightGray.Name },
            { LightPink, LightPink.Name },
            { LightSalmon, LightSalmon.Name },
            { LightSeaGreen, LightSeaGreen.Name },
            { LightSkyBlue, LightSkyBlue.Name },
            { LightSlateGray, LightSlateGray.Name },
            { LightSteelBlue, LightSteelBlue.Name },
            { LightYellow, LightYellow.Name },
            { Lime, Lime.Name },
            { LimeGreen, LimeGreen.Name },
            { Linen, Linen.Name },
            { Magenta, Magenta.Name },
            { Maroon, Maroon.Name },
            { MediumAquamarine, MediumAquamarine.Name },
            { MediumBlue, MediumBlue.Name },
            { MediumOrchid, MediumOrchid.Name },
            { MediumPurple, MediumPurple.Name },
            { MediumSeaGreen, MediumSeaGreen.Name },
            { MediumSlateBlue, MediumSlateBlue.Name },
            { MediumSpringGreen, MediumSpringGreen.Name },
            { MediumTurquoise, MediumTurquoise.Name },
            { MediumVioletRed, MediumVioletRed.Name },
            { MidnightBlue, MidnightBlue.Name },
            { MintCream, MintCream.Name },
            { MistyRose, MistyRose.Name },
            { Moccasin, Moccasin.Name },
            { NavajoWhite, NavajoWhite.Name },
            { Navy, Navy.Name },
            { OldLace, OldLace.Name },
            { Olive, Olive.Name },
            { OliveDrab, OliveDrab.Name },
            { Orange, Orange.Name },
            { OrangeRed, OrangeRed.Name },
            { Orchid, Orchid.Name },
            { PaleGoldenrod, PaleGoldenrod.Name },
            { PaleGreen, PaleGreen.Name },
            { PaleTurquoise, PaleTurquoise.Name },
            { PaleVioletRed, PaleVioletRed.Name },
            { PapayaWhip, PapayaWhip.Name },
            { PeachPuff, PeachPuff.Name },
            { Peru, Peru.Name },
            { Pink, Pink.Name },
            { Plum, Plum.Name },
            { PowderBlue, PowderBlue.Name },
            { Purple, Purple.Name },
            { Red, Red.Name },
            { RosyBrown, RosyBrown.Name },
            { RoyalBlue, RoyalBlue.Name },
            { SaddleBrown, SaddleBrown.Name },
            { Salmon, Salmon.Name },
            { SandyBrown, SandyBrown.Name },
            { SeaGreen, SeaGreen.Name },
            { SeaShell, SeaShell.Name },
            { Sienna, Sienna.Name },
            { Silver, Silver.Name },
            { SkyBlue, SkyBlue.Name },
            { SlateBlue, SlateBlue.Name },
            { SlateGray, SlateGray.Name },
            { Snow, Snow.Name },
            { SpringGreen, SpringGreen.Name },
            { SteelBlue, SteelBlue.Name },
            { Tan, Tan.Name },
            { Teal, Teal.Name },
            { Thistle, Thistle.Name },
            { Tomato, Tomato.Name },
            { Turquoise, Turquoise.Name },
            { Violet, Violet.Name },
            { Wheat, Wheat.Name },
            { White, White.Name },
            { WhiteSmoke, WhiteSmoke.Name },
            { Yellow, Yellow.Name },
            { YellowGreen, YellowGreen.Name },
        };
    }
}
