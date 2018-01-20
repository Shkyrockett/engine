// <copyright file="Colors.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
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
        /// <summary>
        /// The color.
        /// </summary>
        public static Dictionary<RGBA, string> Color = new Dictionary<RGBA, string>
        {
            { Transparent, Transparent.Name },
            { AliceBlue, AliceBlue.Name },
            { AntiqueWhite, AntiqueWhite.Name },
            { Aqua, Aqua.Name },
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
            { Fuchsia, Fuchsia.Name },
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

        /// <summary>
        /// Transparent
        /// </summary>
        public static readonly RGBA Transparent = new RGBA(0x00FFFFFF, nameof(Transparent));

        /// <summary>
        /// Alice Blue
        /// </summary>
        public static readonly RGBA AliceBlue = new RGBA(unchecked((int)0xFFF0F8FF), nameof(AliceBlue));

        /// <summary>
        /// Antique White
        /// </summary>
        public static readonly RGBA AntiqueWhite = new RGBA(unchecked((int)0xFFFAEBD7), nameof(AntiqueWhite));

        /// <summary>
        /// Aqua
        /// </summary>
        public static readonly RGBA Aqua = new RGBA(unchecked((int)0xFF00FFFF), nameof(Aqua));

        /// <summary>
        /// Aquamarine
        /// </summary>
        public static readonly RGBA Aquamarine = new RGBA(unchecked((int)0xFF7FFFD4), nameof(Aquamarine));

        /// <summary>
        /// Azure
        /// </summary>
        public static readonly RGBA Azure = new RGBA(unchecked((int)0xFFF0FFFF), nameof(Azure));

        /// <summary>
        /// Beige
        /// </summary>
        public static readonly RGBA Beige = new RGBA(unchecked((int)0xFFF5F5DC), nameof(Beige));

        /// <summary>
        /// Bisque
        /// </summary>
        public static readonly RGBA Bisque = new RGBA(unchecked(unchecked((int)0xFFFFE4C4)), nameof(Bisque));

        /// <summary>
        /// Black
        /// </summary>
        public static readonly RGBA Black = new RGBA(unchecked((int)0xFF000000), nameof(Black));

        /// <summary>
        /// Blanched Almond
        /// </summary>
        public static readonly RGBA BlanchedAlmond = new RGBA(unchecked((int)0xFFFFEBCD), nameof(BlanchedAlmond));

        /// <summary>
        /// Blue
        /// </summary>
        public static readonly RGBA Blue = new RGBA(unchecked((int)0xFF0000FF), nameof(Blue));

        /// <summary>
        /// Blue Violet
        /// </summary>
        public static readonly RGBA BlueViolet = new RGBA(unchecked((int)0xFF8A2BE2), nameof(BlueViolet));

        /// <summary>
        /// Brown
        /// </summary>
        public static readonly RGBA Brown = new RGBA(unchecked((int)0xFFA52A2A), nameof(Brown));

        /// <summary>
        /// Burly Wood
        /// </summary>
        public static readonly RGBA BurlyWood = new RGBA(unchecked((int)0xFFDEB887), nameof(BurlyWood));

        /// <summary>
        /// Cadet Blue
        /// </summary>
        public static readonly RGBA CadetBlue = new RGBA(unchecked((int)0xFF5F9EA0), nameof(CadetBlue));

        /// <summary>
        /// Chartreuse
        /// </summary>
        public static readonly RGBA Chartreuse = new RGBA(unchecked((int)0xFF7FFF00), nameof(Chartreuse));

        /// <summary>
        /// Chocolate
        /// </summary>
        public static readonly RGBA Chocolate = new RGBA(unchecked((int)0xFFD2691E), nameof(Chocolate));

        /// <summary>
        /// Coral
        /// </summary>
        public static readonly RGBA Coral = new RGBA(unchecked((int)0xFFFF7F50), nameof(Coral));

        /// <summary>
        /// Cornflower Blue
        /// </summary>
        public static readonly RGBA CornflowerBlue = new RGBA(unchecked((int)0xFF6495ED), nameof(CornflowerBlue));

        /// <summary>
        /// Corn-silk
        /// </summary>
        public static readonly RGBA Cornsilk = new RGBA(unchecked((int)0xFFFFF8DC), nameof(Cornsilk));

        /// <summary>
        /// Crimson
        /// </summary>
        public static readonly RGBA Crimson = new RGBA(unchecked((int)0xFFDC143C), nameof(Crimson));

        /// <summary>
        /// Cyan
        /// </summary>
        public static readonly RGBA Cyan = new RGBA(unchecked((int)0xFF00FFFF), nameof(Cyan));

        /// <summary>
        /// Dark Blue
        /// </summary>
        public static readonly RGBA DarkBlue = new RGBA(unchecked((int)0xFF00008B), nameof(DarkBlue));

        /// <summary>
        /// Dark Cyan
        /// </summary>
        public static readonly RGBA DarkCyan = new RGBA(unchecked((int)0xFF008B8B), nameof(DarkCyan));

        /// <summary>
        /// Dark Goldenrod
        /// </summary>
        public static readonly RGBA DarkGoldenrod = new RGBA(unchecked((int)0xFFB8860B), nameof(DarkGoldenrod));

        /// <summary>
        /// Dark Gray
        /// </summary>
        public static readonly RGBA DarkGray = new RGBA(unchecked((int)0xFFA9A9A9), nameof(DarkGray));

        /// <summary>
        /// Dark Green
        /// </summary>
        public static readonly RGBA DarkGreen = new RGBA(unchecked((int)0xFF006400), nameof(DarkGreen));

        /// <summary>
        /// Dark Khaki
        /// </summary>
        public static readonly RGBA DarkKhaki = new RGBA(unchecked((int)0xFFBDB76B), nameof(DarkKhaki));

        /// <summary>
        /// Dark Magenta
        /// </summary>
        public static readonly RGBA DarkMagenta = new RGBA(unchecked((int)0xFF8B008B), nameof(DarkMagenta));

        /// <summary>
        /// Dark Olive Green
        /// </summary>
        public static readonly RGBA DarkOliveGreen = new RGBA(unchecked((int)0xFF556B2F), nameof(DarkOliveGreen));

        /// <summary>
        /// Dark Orange
        /// </summary>
        public static readonly RGBA DarkOrange = new RGBA(unchecked((int)0xFFFF8C00), nameof(DarkOrange));

        /// <summary>
        /// Dark Orchid
        /// </summary>
        public static readonly RGBA DarkOrchid = new RGBA(unchecked((int)0xFF9932CC), nameof(DarkOrchid));

        /// <summary>
        /// Dark Red
        /// </summary>
        public static readonly RGBA DarkRed = new RGBA(unchecked((int)0xFF8B0000), nameof(DarkRed));

        /// <summary>
        /// Dark Salmon
        /// </summary>
        public static readonly RGBA DarkSalmon = new RGBA(unchecked((int)0xFFE9967A), nameof(DarkSalmon));

        /// <summary>
        /// Dark Sea Green
        /// </summary>
        public static readonly RGBA DarkSeaGreen = new RGBA(unchecked((int)0xFF8FBC8B), nameof(DarkSeaGreen));

        /// <summary>
        /// Dark Slate Blue
        /// </summary>
        public static readonly RGBA DarkSlateBlue = new RGBA(unchecked((int)0xFF483D8B), nameof(DarkSlateBlue));

        /// <summary>
        /// Dark Slate Gray
        /// </summary>
        public static readonly RGBA DarkSlateGray = new RGBA(unchecked((int)0xFF2F4F4F), nameof(DarkSlateGray));

        /// <summary>
        /// Dark Turquoise
        /// </summary>
        public static readonly RGBA DarkTurquoise = new RGBA(unchecked((int)0xFF00CED1), nameof(DarkTurquoise));

        /// <summary>
        /// Dark Violet
        /// </summary>
        public static readonly RGBA DarkViolet = new RGBA(unchecked((int)0xFF9400D3), nameof(DarkViolet));

        /// <summary>
        /// Deep Pink
        /// </summary>
        public static readonly RGBA DeepPink = new RGBA(unchecked((int)0xFFFF1493), nameof(DeepPink));

        /// <summary>
        /// Deep Sky Blue
        /// </summary>
        public static readonly RGBA DeepSkyBlue = new RGBA(unchecked((int)0xFF00BFFF), nameof(DeepSkyBlue));

        /// <summary>
        /// Dim Gray
        /// </summary>
        public static readonly RGBA DimGray = new RGBA(unchecked((int)0xFF696969), nameof(DimGray));

        /// <summary>
        /// Dodger Blue
        /// </summary>
        public static readonly RGBA DodgerBlue = new RGBA(unchecked((int)0xFF1E90FF), nameof(DodgerBlue));

        /// <summary>
        /// Firebrick
        /// </summary>
        public static readonly RGBA Firebrick = new RGBA(unchecked((int)0xFFB22222), nameof(Firebrick));

        /// <summary>
        /// Floral White
        /// </summary>
        public static readonly RGBA FloralWhite = new RGBA(unchecked((int)0xFFFFFAF0), nameof(FloralWhite));

        /// <summary>
        /// Forest Green
        /// </summary>
        public static readonly RGBA ForestGreen = new RGBA(unchecked((int)0xFF228B22), nameof(ForestGreen));

        /// <summary>
        /// Fuchsia
        /// </summary>
        public static readonly RGBA Fuchsia = new RGBA(unchecked((int)0xFFFF00FF), nameof(Fuchsia));

        /// <summary>
        /// Gainsboro
        /// </summary>
        public static readonly RGBA Gainsboro = new RGBA(unchecked((int)0xFFDCDCDC), nameof(Gainsboro));

        /// <summary>
        /// Ghost White
        /// </summary>
        public static readonly RGBA GhostWhite = new RGBA(unchecked((int)0xFFF8F8FF), nameof(GhostWhite));

        /// <summary>
        /// Gold
        /// </summary>
        public static readonly RGBA Gold = new RGBA(unchecked((int)0xFFFFD700), nameof(Gold));

        /// <summary>
        /// Goldenrod
        /// </summary>
        public static readonly RGBA Goldenrod = new RGBA(unchecked((int)0xFFDAA520), nameof(Goldenrod));

        /// <summary>
        /// Gray
        /// </summary>
        public static readonly RGBA Gray = new RGBA(unchecked((int)0xFF808080), nameof(Gray));

        /// <summary>
        /// Green
        /// </summary>
        public static readonly RGBA Green = new RGBA(unchecked((int)0xFF008000), nameof(Green));

        /// <summary>
        /// Green Yellow
        /// </summary>
        public static readonly RGBA GreenYellow = new RGBA(unchecked((int)0xFFADFF2F), nameof(GreenYellow));

        /// <summary>
        /// Honeydew
        /// </summary>
        public static readonly RGBA Honeydew = new RGBA(unchecked((int)0xFFF0FFF0), nameof(Honeydew));

        /// <summary>
        /// Hot Pink
        /// </summary>
        public static readonly RGBA HotPink = new RGBA(unchecked((int)0xFFFF69B4), nameof(HotPink));

        /// <summary>
        /// Indian Red
        /// </summary>
        public static readonly RGBA IndianRed = new RGBA(unchecked((int)0xFFCD5C5C), nameof(IndianRed));

        /// <summary>
        /// Indigo
        /// </summary>
        public static readonly RGBA Indigo = new RGBA(unchecked((int)0xFF4B0082), nameof(Indigo));

        /// <summary>
        /// Ivory
        /// </summary>
        public static readonly RGBA Ivory = new RGBA(unchecked((int)0xFFFFFFF0), nameof(Ivory));

        /// <summary>
        /// Khaki
        /// </summary>
        public static readonly RGBA Khaki = new RGBA(unchecked((int)0xFFF0E68C), nameof(Khaki));

        /// <summary>
        /// Lavender
        /// </summary>
        public static readonly RGBA Lavender = new RGBA(unchecked((int)0xFFE6E6FA), nameof(Lavender));

        /// <summary>
        /// Lavender Blush
        /// </summary>
        public static readonly RGBA LavenderBlush = new RGBA(unchecked((int)0xFFFFF0F5), nameof(LavenderBlush));

        /// <summary>
        /// Lawn Green
        /// </summary>
        public static readonly RGBA LawnGreen = new RGBA(unchecked((int)0xFF7CFC00), nameof(LawnGreen));

        /// <summary>
        /// Lemon Chiffon
        /// </summary>
        public static readonly RGBA LemonChiffon = new RGBA(unchecked((int)0xFFFFFACD), nameof(LemonChiffon));

        /// <summary>
        /// Light Blue
        /// </summary>
        public static readonly RGBA LightBlue = new RGBA(unchecked((int)0xFFADD8E6), nameof(LightBlue));

        /// <summary>
        /// Light Coral
        /// </summary>
        public static readonly RGBA LightCoral = new RGBA(unchecked((int)0xFFF08080), nameof(LightCoral));

        /// <summary>
        /// Light Cyan
        /// </summary>
        public static readonly RGBA LightCyan = new RGBA(unchecked((int)0xFFE0FFFF), nameof(LightCyan));

        /// <summary>
        /// Light Goldenrod Yellow
        /// </summary>
        public static readonly RGBA LightGoldenrodYellow = new RGBA(unchecked((int)0xFFFAFAD2), nameof(LightGoldenrodYellow));

        /// <summary>
        /// Light Green
        /// </summary>
        public static readonly RGBA LightGreen = new RGBA(unchecked((int)0xFFD3D3D3), nameof(LightGreen));

        /// <summary>
        /// Light Gray
        /// </summary>
        public static readonly RGBA LightGray = new RGBA(unchecked((int)0xFF90EE90), nameof(LightGray));

        /// <summary>
        /// Light Pink
        /// </summary>
        public static readonly RGBA LightPink = new RGBA(unchecked((int)0xFFFFB6C1), nameof(LightPink));

        /// <summary>
        /// Light Salmon
        /// </summary>
        public static readonly RGBA LightSalmon = new RGBA(unchecked((int)0xFFFFA07A), nameof(LightSalmon));

        /// <summary>
        /// Light Sea Green
        /// </summary>
        public static readonly RGBA LightSeaGreen = new RGBA(unchecked((int)0xFF20B2AA), nameof(LightSeaGreen));

        /// <summary>
        /// Light Sky Blue
        /// </summary>
        public static readonly RGBA LightSkyBlue = new RGBA(unchecked((int)0xFF87CEFA), nameof(LightSkyBlue));

        /// <summary>
        /// Light Slate Gray
        /// </summary>
        public static readonly RGBA LightSlateGray = new RGBA(unchecked((int)0xFF778899), nameof(LightSlateGray));

        /// <summary>
        /// Light Steel Blue
        /// </summary>
        public static readonly RGBA LightSteelBlue = new RGBA(unchecked((int)0xFFB0C4DE), nameof(LightSteelBlue));

        /// <summary>
        /// Light Yellow
        /// </summary>
        public static readonly RGBA LightYellow = new RGBA(unchecked((int)0xFFFFFFE0), nameof(LightYellow));

        /// <summary>
        /// Lime
        /// </summary>
        public static readonly RGBA Lime = new RGBA(unchecked((int)0xFF00FF00), nameof(Lime));

        /// <summary>
        /// Lime Green
        /// </summary>
        public static readonly RGBA LimeGreen = new RGBA(unchecked((int)0xFF32CD32), nameof(LimeGreen));

        /// <summary>
        /// Linen
        /// </summary>
        public static readonly RGBA Linen = new RGBA(unchecked((int)0xFFFAF0E6), nameof(Linen));

        /// <summary>
        /// Magenta
        /// </summary>
        public static readonly RGBA Magenta = new RGBA(unchecked((int)0xFFFF00FF), nameof(Magenta));

        /// <summary>
        /// Maroon
        /// </summary>
        public static readonly RGBA Maroon = new RGBA(unchecked((int)0xFF800000), nameof(Maroon));

        /// <summary>
        /// Medium Aquamarine
        /// </summary>
        public static readonly RGBA MediumAquamarine = new RGBA(unchecked((int)0xFF66CDAA), nameof(MediumAquamarine));

        /// <summary>
        /// Medium Blue
        /// </summary>
        public static readonly RGBA MediumBlue = new RGBA(unchecked((int)0xFF0000CD), nameof(MediumBlue));

        /// <summary>
        /// Medium Orchid
        /// </summary>
        public static readonly RGBA MediumOrchid = new RGBA(unchecked((int)0xFFBA55D3), nameof(MediumOrchid));

        /// <summary>
        /// Medium Purple
        /// </summary>
        public static readonly RGBA MediumPurple = new RGBA(unchecked((int)0xFF9370DB), nameof(MediumPurple));

        /// <summary>
        /// Medium Sea Green
        /// </summary>
        public static readonly RGBA MediumSeaGreen = new RGBA(unchecked((int)0xFF3CB371), nameof(MediumSeaGreen));

        /// <summary>
        /// Medium Slate Blue
        /// </summary>
        public static readonly RGBA MediumSlateBlue = new RGBA(unchecked((int)0xFF7B68EE), nameof(MediumSlateBlue));

        /// <summary>
        /// Medium Spring Green
        /// </summary>
        public static readonly RGBA MediumSpringGreen = new RGBA(unchecked((int)0xFF00FA9A), nameof(MediumSpringGreen));

        /// <summary>
        /// Medium Turquoise
        /// </summary>
        public static readonly RGBA MediumTurquoise = new RGBA(unchecked((int)0xFF48D1CC), nameof(MediumTurquoise));

        /// <summary>
        /// Medium Violet Red
        /// </summary>
        public static readonly RGBA MediumVioletRed = new RGBA(unchecked((int)0xFFC71585), nameof(MediumVioletRed));

        /// <summary>
        /// Midnight Blue
        /// </summary>
        public static readonly RGBA MidnightBlue = new RGBA(unchecked((int)0xFF191970), nameof(MidnightBlue));

        /// <summary>
        /// Mint Cream
        /// </summary>
        public static readonly RGBA MintCream = new RGBA(unchecked((int)0xFFF5FFFA), nameof(MintCream));

        /// <summary>
        /// Misty Rose
        /// </summary>
        public static readonly RGBA MistyRose = new RGBA(unchecked((int)0xFFFFE4E1), nameof(MistyRose));

        /// <summary>
        /// Moccasin
        /// </summary>
        public static readonly RGBA Moccasin = new RGBA(unchecked((int)0xFFFFE4B5), nameof(Moccasin));

        /// <summary>
        /// Navajo White
        /// </summary>
        public static readonly RGBA NavajoWhite = new RGBA(unchecked((int)0xFFFFDEAD), nameof(NavajoWhite));

        /// <summary>
        /// Navy
        /// </summary>
        public static readonly RGBA Navy = new RGBA(unchecked((int)0xFF000080), nameof(Navy));

        /// <summary>
        /// Old Lace
        /// </summary>
        public static readonly RGBA OldLace = new RGBA(unchecked((int)0xFFFDF5E6), nameof(OldLace));

        /// <summary>
        /// Olive
        /// </summary>
        public static readonly RGBA Olive = new RGBA(unchecked((int)0xFF808000), nameof(Olive));

        /// <summary>
        /// Olive Drab
        /// </summary>
        public static readonly RGBA OliveDrab = new RGBA(unchecked((int)0xFF6B8E23), nameof(OliveDrab));

        /// <summary>
        /// Orange
        /// </summary>
        public static readonly RGBA Orange = new RGBA(unchecked((int)0xFFFFA500), nameof(Orange));

        /// <summary>
        /// Orange Red
        /// </summary>
        public static readonly RGBA OrangeRed = new RGBA(unchecked((int)0xFFFF4500), nameof(OrangeRed));

        /// <summary>
        /// Orchid
        /// </summary>
        public static readonly RGBA Orchid = new RGBA(unchecked((int)0xFFDA70D6), nameof(Orchid));

        /// <summary>
        /// Pale Goldenrod
        /// </summary>
        public static readonly RGBA PaleGoldenrod = new RGBA(unchecked((int)0xFFEEE8AA), nameof(PaleGoldenrod));

        /// <summary>
        /// Pale Green
        /// </summary>
        public static readonly RGBA PaleGreen = new RGBA(unchecked((int)0xFF98FB98), nameof(PaleGreen));

        /// <summary>
        /// PaleTurquoise
        /// </summary>
        public static readonly RGBA PaleTurquoise = new RGBA(unchecked((int)0xFFAFEEEE), nameof(PaleTurquoise));

        /// <summary>
        /// Pale Violet Red
        /// </summary>
        public static readonly RGBA PaleVioletRed = new RGBA(unchecked((int)0xFFDB7093), nameof(PaleVioletRed));

        /// <summary>
        /// Papaya Whip
        /// </summary>
        public static readonly RGBA PapayaWhip = new RGBA(unchecked((int)0xFFFFEFD5), nameof(PapayaWhip));

        /// <summary>
        /// Peach Puff
        /// </summary>
        public static readonly RGBA PeachPuff = new RGBA(unchecked((int)0xFFFFDAB9), nameof(PeachPuff));

        /// <summary>
        /// Peru
        /// </summary>
        public static readonly RGBA Peru = new RGBA(unchecked((int)0xFFCD853F), nameof(Peru));

        /// <summary>
        /// Pink
        /// </summary>
        public static readonly RGBA Pink = new RGBA(unchecked((int)0xFFFFC0CB), nameof(Pink));

        /// <summary>
        /// Plum
        /// </summary>
        public static readonly RGBA Plum = new RGBA(unchecked((int)0xFFDDA0DD), nameof(Plum));

        /// <summary>
        /// Powder Blue
        /// </summary>
        public static readonly RGBA PowderBlue = new RGBA(unchecked((int)0xFFB0E0E6), nameof(PowderBlue));

        /// <summary>
        /// Purple
        /// </summary>
        public static readonly RGBA Purple = new RGBA(unchecked((int)0xFF800080), nameof(Purple));

        /// <summary>
        /// Red
        /// </summary>
        public static readonly RGBA Red = new RGBA(unchecked((int)0xFFFF0000), nameof(Red));

        /// <summary>
        /// Rosy Brown
        /// </summary>
        public static readonly RGBA RosyBrown = new RGBA(unchecked((int)0xFFBC8F8F), nameof(RosyBrown));

        /// <summary>
        /// Royal Blue
        /// </summary>
        public static readonly RGBA RoyalBlue = new RGBA(unchecked((int)0xFF4169E1), nameof(RoyalBlue));

        /// <summary>
        /// Saddle Brown
        /// </summary>
        public static readonly RGBA SaddleBrown = new RGBA(unchecked((int)0xFF8B4513), nameof(SaddleBrown));

        /// <summary>
        /// Salmon
        /// </summary>
        public static readonly RGBA Salmon = new RGBA(unchecked((int)0xFFFA8072), nameof(Salmon));

        /// <summary>
        /// Sandy Brown
        /// </summary>
        public static readonly RGBA SandyBrown = new RGBA(unchecked((int)0xFFF4A460), nameof(SandyBrown));

        /// <summary>
        /// Sea Green
        /// </summary>
        public static readonly RGBA SeaGreen = new RGBA(unchecked((int)0xFF2E8B57), nameof(SeaGreen));

        /// <summary>
        /// Sea Shell
        /// </summary>
        public static readonly RGBA SeaShell = new RGBA(unchecked((int)0xFFFFF5EE), nameof(SeaShell));

        /// <summary>
        /// Sienna
        /// </summary>
        public static readonly RGBA Sienna = new RGBA(unchecked((int)0xFFA0522D), nameof(Sienna));

        /// <summary>
        /// Silver
        /// </summary>
        public static readonly RGBA Silver = new RGBA(unchecked((int)0xFFC0C0C0), nameof(Silver));

        /// <summary>
        /// Sky Blue
        /// </summary>
        public static readonly RGBA SkyBlue = new RGBA(unchecked((int)0xFF87CEEB), nameof(SkyBlue));

        /// <summary>
        /// Slate Blue
        /// </summary>
        public static readonly RGBA SlateBlue = new RGBA(unchecked((int)0xFF6A5ACD), nameof(SlateBlue));

        /// <summary>
        /// Slate Gray
        /// </summary>
        public static readonly RGBA SlateGray = new RGBA(unchecked((int)0xFF708090), nameof(SlateGray));

        /// <summary>
        /// Snow
        /// </summary>
        public static readonly RGBA Snow = new RGBA(unchecked((int)0xFFFFFAFA), nameof(Snow));

        /// <summary>
        /// Spring Green
        /// </summary>
        public static readonly RGBA SpringGreen = new RGBA(unchecked((int)0xFF00FF7F), nameof(SpringGreen));

        /// <summary>
        /// Steel Blue
        /// </summary>
        public static readonly RGBA SteelBlue = new RGBA(unchecked((int)0xFF4682B4), nameof(SteelBlue));

        /// <summary>
        /// Tan
        /// </summary>
        public static readonly RGBA Tan = new RGBA(unchecked((int)0xFFD2B48C), nameof(Tan));

        /// <summary>
        /// Teal
        /// </summary>
        public static readonly RGBA Teal = new RGBA(unchecked((int)0xFF008080), nameof(Teal));

        /// <summary>
        /// Thistle
        /// </summary>
        public static readonly RGBA Thistle = new RGBA(unchecked((int)0xFFD8BFD8), nameof(Thistle));

        /// <summary>
        /// Tomato
        /// </summary>
        public static readonly RGBA Tomato = new RGBA(unchecked((int)0xFFFF6347), nameof(Tomato));

        /// <summary>
        /// Turquoise
        /// </summary>
        public static readonly RGBA Turquoise = new RGBA(unchecked((int)0xFF40E0D0), nameof(Turquoise));

        /// <summary>
        /// Violet
        /// </summary>
        public static readonly RGBA Violet = new RGBA(unchecked((int)0xFFEE82EE), nameof(Violet));

        /// <summary>
        /// Wheat
        /// </summary>
        public static readonly RGBA Wheat = new RGBA(unchecked((int)0xFFF5DEB3), nameof(Wheat));

        /// <summary>
        /// White
        /// </summary>
        public static readonly RGBA White = new RGBA(unchecked((int)0xFFFFFFFF), nameof(White));

        /// <summary>
        /// White Smoke
        /// </summary>
        public static readonly RGBA WhiteSmoke = new RGBA(unchecked((int)0xFFF5F5F5), nameof(WhiteSmoke));

        /// <summary>
        /// Yellow
        /// </summary>
        public static readonly RGBA Yellow = new RGBA(unchecked((int)0xFFFFFF00), nameof(Yellow));

        /// <summary>
        /// Yellow Green
        /// </summary>
        public static readonly RGBA YellowGreen = new RGBA(unchecked((int)0xFF9ACD32), nameof(YellowGreen));
    }
}
