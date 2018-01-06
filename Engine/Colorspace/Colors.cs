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
        public static Dictionary<ARGB, string> Color = new Dictionary<ARGB, string>
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
        public static readonly ARGB Transparent = new ARGB(0x00FFFFFF, nameof(Transparent));

        /// <summary>
        /// Alice Blue
        /// </summary>
        public static readonly ARGB AliceBlue = new ARGB(unchecked((int)0xFFF0F8FF), nameof(AliceBlue));

        /// <summary>
        /// Antique White
        /// </summary>
        public static readonly ARGB AntiqueWhite = new ARGB(unchecked((int)0xFFFAEBD7), nameof(AntiqueWhite));

        /// <summary>
        /// Aqua
        /// </summary>
        public static readonly ARGB Aqua = new ARGB(unchecked((int)0xFF00FFFF), nameof(Aqua));

        /// <summary>
        /// Aquamarine
        /// </summary>
        public static readonly ARGB Aquamarine = new ARGB(unchecked((int)0xFF7FFFD4), nameof(Aquamarine));

        /// <summary>
        /// Azure
        /// </summary>
        public static readonly ARGB Azure = new ARGB(unchecked((int)0xFFF0FFFF), nameof(Azure));

        /// <summary>
        /// Beige
        /// </summary>
        public static readonly ARGB Beige = new ARGB(unchecked((int)0xFFF5F5DC), nameof(Beige));

        /// <summary>
        /// Bisque
        /// </summary>
        public static readonly ARGB Bisque = new ARGB(unchecked(unchecked((int)0xFFFFE4C4)), nameof(Bisque));

        /// <summary>
        /// Black
        /// </summary>
        public static readonly ARGB Black = new ARGB(unchecked((int)0xFF000000), nameof(Black));

        /// <summary>
        /// Blanched Almond
        /// </summary>
        public static readonly ARGB BlanchedAlmond = new ARGB(unchecked((int)0xFFFFEBCD), nameof(BlanchedAlmond));

        /// <summary>
        /// Blue
        /// </summary>
        public static readonly ARGB Blue = new ARGB(unchecked((int)0xFF0000FF), nameof(Blue));

        /// <summary>
        /// Blue Violet
        /// </summary>
        public static readonly ARGB BlueViolet = new ARGB(unchecked((int)0xFF8A2BE2), nameof(BlueViolet));

        /// <summary>
        /// Brown
        /// </summary>
        public static readonly ARGB Brown = new ARGB(unchecked((int)0xFFA52A2A), nameof(Brown));

        /// <summary>
        /// Burly Wood
        /// </summary>
        public static readonly ARGB BurlyWood = new ARGB(unchecked((int)0xFFDEB887), nameof(BurlyWood));

        /// <summary>
        /// Cadet Blue
        /// </summary>
        public static readonly ARGB CadetBlue = new ARGB(unchecked((int)0xFF5F9EA0), nameof(CadetBlue));

        /// <summary>
        /// Chartreuse
        /// </summary>
        public static readonly ARGB Chartreuse = new ARGB(unchecked((int)0xFF7FFF00), nameof(Chartreuse));

        /// <summary>
        /// Chocolate
        /// </summary>
        public static readonly ARGB Chocolate = new ARGB(unchecked((int)0xFFD2691E), nameof(Chocolate));

        /// <summary>
        /// Coral
        /// </summary>
        public static readonly ARGB Coral = new ARGB(unchecked((int)0xFFFF7F50), nameof(Coral));

        /// <summary>
        /// Cornflower Blue
        /// </summary>
        public static readonly ARGB CornflowerBlue = new ARGB(unchecked((int)0xFF6495ED), nameof(CornflowerBlue));

        /// <summary>
        /// Corn-silk
        /// </summary>
        public static readonly ARGB Cornsilk = new ARGB(unchecked((int)0xFFFFF8DC), nameof(Cornsilk));

        /// <summary>
        /// Crimson
        /// </summary>
        public static readonly ARGB Crimson = new ARGB(unchecked((int)0xFFDC143C), nameof(Crimson));

        /// <summary>
        /// Cyan
        /// </summary>
        public static readonly ARGB Cyan = new ARGB(unchecked((int)0xFF00FFFF), nameof(Cyan));

        /// <summary>
        /// Dark Blue
        /// </summary>
        public static readonly ARGB DarkBlue = new ARGB(unchecked((int)0xFF00008B), nameof(DarkBlue));

        /// <summary>
        /// Dark Cyan
        /// </summary>
        public static readonly ARGB DarkCyan = new ARGB(unchecked((int)0xFF008B8B), nameof(DarkCyan));

        /// <summary>
        /// Dark Goldenrod
        /// </summary>
        public static readonly ARGB DarkGoldenrod = new ARGB(unchecked((int)0xFFB8860B), nameof(DarkGoldenrod));

        /// <summary>
        /// Dark Gray
        /// </summary>
        public static readonly ARGB DarkGray = new ARGB(unchecked((int)0xFFA9A9A9), nameof(DarkGray));

        /// <summary>
        /// Dark Green
        /// </summary>
        public static readonly ARGB DarkGreen = new ARGB(unchecked((int)0xFF006400), nameof(DarkGreen));

        /// <summary>
        /// Dark Khaki
        /// </summary>
        public static readonly ARGB DarkKhaki = new ARGB(unchecked((int)0xFFBDB76B), nameof(DarkKhaki));

        /// <summary>
        /// Dark Magenta
        /// </summary>
        public static readonly ARGB DarkMagenta = new ARGB(unchecked((int)0xFF8B008B), nameof(DarkMagenta));

        /// <summary>
        /// Dark Olive Green
        /// </summary>
        public static readonly ARGB DarkOliveGreen = new ARGB(unchecked((int)0xFF556B2F), nameof(DarkOliveGreen));

        /// <summary>
        /// Dark Orange
        /// </summary>
        public static readonly ARGB DarkOrange = new ARGB(unchecked((int)0xFFFF8C00), nameof(DarkOrange));

        /// <summary>
        /// Dark Orchid
        /// </summary>
        public static readonly ARGB DarkOrchid = new ARGB(unchecked((int)0xFF9932CC), nameof(DarkOrchid));

        /// <summary>
        /// Dark Red
        /// </summary>
        public static readonly ARGB DarkRed = new ARGB(unchecked((int)0xFF8B0000), nameof(DarkRed));

        /// <summary>
        /// Dark Salmon
        /// </summary>
        public static readonly ARGB DarkSalmon = new ARGB(unchecked((int)0xFFE9967A), nameof(DarkSalmon));

        /// <summary>
        /// Dark Sea Green
        /// </summary>
        public static readonly ARGB DarkSeaGreen = new ARGB(unchecked((int)0xFF8FBC8B), nameof(DarkSeaGreen));

        /// <summary>
        /// Dark Slate Blue
        /// </summary>
        public static readonly ARGB DarkSlateBlue = new ARGB(unchecked((int)0xFF483D8B), nameof(DarkSlateBlue));

        /// <summary>
        /// Dark Slate Gray
        /// </summary>
        public static readonly ARGB DarkSlateGray = new ARGB(unchecked((int)0xFF2F4F4F), nameof(DarkSlateGray));

        /// <summary>
        /// Dark Turquoise
        /// </summary>
        public static readonly ARGB DarkTurquoise = new ARGB(unchecked((int)0xFF00CED1), nameof(DarkTurquoise));

        /// <summary>
        /// Dark Violet
        /// </summary>
        public static readonly ARGB DarkViolet = new ARGB(unchecked((int)0xFF9400D3), nameof(DarkViolet));

        /// <summary>
        /// Deep Pink
        /// </summary>
        public static readonly ARGB DeepPink = new ARGB(unchecked((int)0xFFFF1493), nameof(DeepPink));

        /// <summary>
        /// Deep Sky Blue
        /// </summary>
        public static readonly ARGB DeepSkyBlue = new ARGB(unchecked((int)0xFF00BFFF), nameof(DeepSkyBlue));

        /// <summary>
        /// Dim Gray
        /// </summary>
        public static readonly ARGB DimGray = new ARGB(unchecked((int)0xFF696969), nameof(DimGray));

        /// <summary>
        /// Dodger Blue
        /// </summary>
        public static readonly ARGB DodgerBlue = new ARGB(unchecked((int)0xFF1E90FF), nameof(DodgerBlue));

        /// <summary>
        /// Firebrick
        /// </summary>
        public static readonly ARGB Firebrick = new ARGB(unchecked((int)0xFFB22222), nameof(Firebrick));

        /// <summary>
        /// Floral White
        /// </summary>
        public static readonly ARGB FloralWhite = new ARGB(unchecked((int)0xFFFFFAF0), nameof(FloralWhite));

        /// <summary>
        /// Forest Green
        /// </summary>
        public static readonly ARGB ForestGreen = new ARGB(unchecked((int)0xFF228B22), nameof(ForestGreen));

        /// <summary>
        /// Fuchsia
        /// </summary>
        public static readonly ARGB Fuchsia = new ARGB(unchecked((int)0xFFFF00FF), nameof(Fuchsia));

        /// <summary>
        /// Gainsboro
        /// </summary>
        public static readonly ARGB Gainsboro = new ARGB(unchecked((int)0xFFDCDCDC), nameof(Gainsboro));

        /// <summary>
        /// Ghost White
        /// </summary>
        public static readonly ARGB GhostWhite = new ARGB(unchecked((int)0xFFF8F8FF), nameof(GhostWhite));

        /// <summary>
        /// Gold
        /// </summary>
        public static readonly ARGB Gold = new ARGB(unchecked((int)0xFFFFD700), nameof(Gold));

        /// <summary>
        /// Goldenrod
        /// </summary>
        public static readonly ARGB Goldenrod = new ARGB(unchecked((int)0xFFDAA520), nameof(Goldenrod));

        /// <summary>
        /// Gray
        /// </summary>
        public static readonly ARGB Gray = new ARGB(unchecked((int)0xFF808080), nameof(Gray));

        /// <summary>
        /// Green
        /// </summary>
        public static readonly ARGB Green = new ARGB(unchecked((int)0xFF008000), nameof(Green));

        /// <summary>
        /// Green Yellow
        /// </summary>
        public static readonly ARGB GreenYellow = new ARGB(unchecked((int)0xFFADFF2F), nameof(GreenYellow));

        /// <summary>
        /// Honeydew
        /// </summary>
        public static readonly ARGB Honeydew = new ARGB(unchecked((int)0xFFF0FFF0), nameof(Honeydew));

        /// <summary>
        /// Hot Pink
        /// </summary>
        public static readonly ARGB HotPink = new ARGB(unchecked((int)0xFFFF69B4), nameof(HotPink));

        /// <summary>
        /// Indian Red
        /// </summary>
        public static readonly ARGB IndianRed = new ARGB(unchecked((int)0xFFCD5C5C), nameof(IndianRed));

        /// <summary>
        /// Indigo
        /// </summary>
        public static readonly ARGB Indigo = new ARGB(unchecked((int)0xFF4B0082), nameof(Indigo));

        /// <summary>
        /// Ivory
        /// </summary>
        public static readonly ARGB Ivory = new ARGB(unchecked((int)0xFFFFFFF0), nameof(Ivory));

        /// <summary>
        /// Khaki
        /// </summary>
        public static readonly ARGB Khaki = new ARGB(unchecked((int)0xFFF0E68C), nameof(Khaki));

        /// <summary>
        /// Lavender
        /// </summary>
        public static readonly ARGB Lavender = new ARGB(unchecked((int)0xFFE6E6FA), nameof(Lavender));

        /// <summary>
        /// Lavender Blush
        /// </summary>
        public static readonly ARGB LavenderBlush = new ARGB(unchecked((int)0xFFFFF0F5), nameof(LavenderBlush));

        /// <summary>
        /// Lawn Green
        /// </summary>
        public static readonly ARGB LawnGreen = new ARGB(unchecked((int)0xFF7CFC00), nameof(LawnGreen));

        /// <summary>
        /// Lemon Chiffon
        /// </summary>
        public static readonly ARGB LemonChiffon = new ARGB(unchecked((int)0xFFFFFACD), nameof(LemonChiffon));

        /// <summary>
        /// Light Blue
        /// </summary>
        public static readonly ARGB LightBlue = new ARGB(unchecked((int)0xFFADD8E6), nameof(LightBlue));

        /// <summary>
        /// Light Coral
        /// </summary>
        public static readonly ARGB LightCoral = new ARGB(unchecked((int)0xFFF08080), nameof(LightCoral));

        /// <summary>
        /// Light Cyan
        /// </summary>
        public static readonly ARGB LightCyan = new ARGB(unchecked((int)0xFFE0FFFF), nameof(LightCyan));

        /// <summary>
        /// Light Goldenrod Yellow
        /// </summary>
        public static readonly ARGB LightGoldenrodYellow = new ARGB(unchecked((int)0xFFFAFAD2), nameof(LightGoldenrodYellow));

        /// <summary>
        /// Light Green
        /// </summary>
        public static readonly ARGB LightGreen = new ARGB(unchecked((int)0xFFD3D3D3), nameof(LightGreen));

        /// <summary>
        /// Light Gray
        /// </summary>
        public static readonly ARGB LightGray = new ARGB(unchecked((int)0xFF90EE90), nameof(LightGray));

        /// <summary>
        /// Light Pink
        /// </summary>
        public static readonly ARGB LightPink = new ARGB(unchecked((int)0xFFFFB6C1), nameof(LightPink));

        /// <summary>
        /// Light Salmon
        /// </summary>
        public static readonly ARGB LightSalmon = new ARGB(unchecked((int)0xFFFFA07A), nameof(LightSalmon));

        /// <summary>
        /// Light Sea Green
        /// </summary>
        public static readonly ARGB LightSeaGreen = new ARGB(unchecked((int)0xFF20B2AA), nameof(LightSeaGreen));

        /// <summary>
        /// Light Sky Blue
        /// </summary>
        public static readonly ARGB LightSkyBlue = new ARGB(unchecked((int)0xFF87CEFA), nameof(LightSkyBlue));

        /// <summary>
        /// Light Slate Gray
        /// </summary>
        public static readonly ARGB LightSlateGray = new ARGB(unchecked((int)0xFF778899), nameof(LightSlateGray));

        /// <summary>
        /// Light Steel Blue
        /// </summary>
        public static readonly ARGB LightSteelBlue = new ARGB(unchecked((int)0xFFB0C4DE), nameof(LightSteelBlue));

        /// <summary>
        /// Light Yellow
        /// </summary>
        public static readonly ARGB LightYellow = new ARGB(unchecked((int)0xFFFFFFE0), nameof(LightYellow));

        /// <summary>
        /// Lime
        /// </summary>
        public static readonly ARGB Lime = new ARGB(unchecked((int)0xFF00FF00), nameof(Lime));

        /// <summary>
        /// Lime Green
        /// </summary>
        public static readonly ARGB LimeGreen = new ARGB(unchecked((int)0xFF32CD32), nameof(LimeGreen));

        /// <summary>
        /// Linen
        /// </summary>
        public static readonly ARGB Linen = new ARGB(unchecked((int)0xFFFAF0E6), nameof(Linen));

        /// <summary>
        /// Magenta
        /// </summary>
        public static readonly ARGB Magenta = new ARGB(unchecked((int)0xFFFF00FF), nameof(Magenta));

        /// <summary>
        /// Maroon
        /// </summary>
        public static readonly ARGB Maroon = new ARGB(unchecked((int)0xFF800000), nameof(Maroon));

        /// <summary>
        /// Medium Aquamarine
        /// </summary>
        public static readonly ARGB MediumAquamarine = new ARGB(unchecked((int)0xFF66CDAA), nameof(MediumAquamarine));

        /// <summary>
        /// Medium Blue
        /// </summary>
        public static readonly ARGB MediumBlue = new ARGB(unchecked((int)0xFF0000CD), nameof(MediumBlue));

        /// <summary>
        /// Medium Orchid
        /// </summary>
        public static readonly ARGB MediumOrchid = new ARGB(unchecked((int)0xFFBA55D3), nameof(MediumOrchid));

        /// <summary>
        /// Medium Purple
        /// </summary>
        public static readonly ARGB MediumPurple = new ARGB(unchecked((int)0xFF9370DB), nameof(MediumPurple));

        /// <summary>
        /// Medium Sea Green
        /// </summary>
        public static readonly ARGB MediumSeaGreen = new ARGB(unchecked((int)0xFF3CB371), nameof(MediumSeaGreen));

        /// <summary>
        /// Medium Slate Blue
        /// </summary>
        public static readonly ARGB MediumSlateBlue = new ARGB(unchecked((int)0xFF7B68EE), nameof(MediumSlateBlue));

        /// <summary>
        /// Medium Spring Green
        /// </summary>
        public static readonly ARGB MediumSpringGreen = new ARGB(unchecked((int)0xFF00FA9A), nameof(MediumSpringGreen));

        /// <summary>
        /// Medium Turquoise
        /// </summary>
        public static readonly ARGB MediumTurquoise = new ARGB(unchecked((int)0xFF48D1CC), nameof(MediumTurquoise));

        /// <summary>
        /// Medium Violet Red
        /// </summary>
        public static readonly ARGB MediumVioletRed = new ARGB(unchecked((int)0xFFC71585), nameof(MediumVioletRed));

        /// <summary>
        /// Midnight Blue
        /// </summary>
        public static readonly ARGB MidnightBlue = new ARGB(unchecked((int)0xFF191970), nameof(MidnightBlue));

        /// <summary>
        /// Mint Cream
        /// </summary>
        public static readonly ARGB MintCream = new ARGB(unchecked((int)0xFFF5FFFA), nameof(MintCream));

        /// <summary>
        /// Misty Rose
        /// </summary>
        public static readonly ARGB MistyRose = new ARGB(unchecked((int)0xFFFFE4E1), nameof(MistyRose));

        /// <summary>
        /// Moccasin
        /// </summary>
        public static readonly ARGB Moccasin = new ARGB(unchecked((int)0xFFFFE4B5), nameof(Moccasin));

        /// <summary>
        /// Navajo White
        /// </summary>
        public static readonly ARGB NavajoWhite = new ARGB(unchecked((int)0xFFFFDEAD), nameof(NavajoWhite));

        /// <summary>
        /// Navy
        /// </summary>
        public static readonly ARGB Navy = new ARGB(unchecked((int)0xFF000080), nameof(Navy));

        /// <summary>
        /// Old Lace
        /// </summary>
        public static readonly ARGB OldLace = new ARGB(unchecked((int)0xFFFDF5E6), nameof(OldLace));

        /// <summary>
        /// Olive
        /// </summary>
        public static readonly ARGB Olive = new ARGB(unchecked((int)0xFF808000), nameof(Olive));

        /// <summary>
        /// Olive Drab
        /// </summary>
        public static readonly ARGB OliveDrab = new ARGB(unchecked((int)0xFF6B8E23), nameof(OliveDrab));

        /// <summary>
        /// Orange
        /// </summary>
        public static readonly ARGB Orange = new ARGB(unchecked((int)0xFFFFA500), nameof(Orange));

        /// <summary>
        /// Orange Red
        /// </summary>
        public static readonly ARGB OrangeRed = new ARGB(unchecked((int)0xFFFF4500), nameof(OrangeRed));

        /// <summary>
        /// Orchid
        /// </summary>
        public static readonly ARGB Orchid = new ARGB(unchecked((int)0xFFDA70D6), nameof(Orchid));

        /// <summary>
        /// Pale Goldenrod
        /// </summary>
        public static readonly ARGB PaleGoldenrod = new ARGB(unchecked((int)0xFFEEE8AA), nameof(PaleGoldenrod));

        /// <summary>
        /// Pale Green
        /// </summary>
        public static readonly ARGB PaleGreen = new ARGB(unchecked((int)0xFF98FB98), nameof(PaleGreen));

        /// <summary>
        /// PaleTurquoise
        /// </summary>
        public static readonly ARGB PaleTurquoise = new ARGB(unchecked((int)0xFFAFEEEE), nameof(PaleTurquoise));

        /// <summary>
        /// Pale Violet Red
        /// </summary>
        public static readonly ARGB PaleVioletRed = new ARGB(unchecked((int)0xFFDB7093), nameof(PaleVioletRed));

        /// <summary>
        /// Papaya Whip
        /// </summary>
        public static readonly ARGB PapayaWhip = new ARGB(unchecked((int)0xFFFFEFD5), nameof(PapayaWhip));

        /// <summary>
        /// Peach Puff
        /// </summary>
        public static readonly ARGB PeachPuff = new ARGB(unchecked((int)0xFFFFDAB9), nameof(PeachPuff));

        /// <summary>
        /// Peru
        /// </summary>
        public static readonly ARGB Peru = new ARGB(unchecked((int)0xFFCD853F), nameof(Peru));

        /// <summary>
        /// Pink
        /// </summary>
        public static readonly ARGB Pink = new ARGB(unchecked((int)0xFFFFC0CB), nameof(Pink));

        /// <summary>
        /// Plum
        /// </summary>
        public static readonly ARGB Plum = new ARGB(unchecked((int)0xFFDDA0DD), nameof(Plum));

        /// <summary>
        /// Powder Blue
        /// </summary>
        public static readonly ARGB PowderBlue = new ARGB(unchecked((int)0xFFB0E0E6), nameof(PowderBlue));

        /// <summary>
        /// Purple
        /// </summary>
        public static readonly ARGB Purple = new ARGB(unchecked((int)0xFF800080), nameof(Purple));

        /// <summary>
        /// Red
        /// </summary>
        public static readonly ARGB Red = new ARGB(unchecked((int)0xFFFF0000), nameof(Red));

        /// <summary>
        /// Rosy Brown
        /// </summary>
        public static readonly ARGB RosyBrown = new ARGB(unchecked((int)0xFFBC8F8F), nameof(RosyBrown));

        /// <summary>
        /// Royal Blue
        /// </summary>
        public static readonly ARGB RoyalBlue = new ARGB(unchecked((int)0xFF4169E1), nameof(RoyalBlue));

        /// <summary>
        /// Saddle Brown
        /// </summary>
        public static readonly ARGB SaddleBrown = new ARGB(unchecked((int)0xFF8B4513), nameof(SaddleBrown));

        /// <summary>
        /// Salmon
        /// </summary>
        public static readonly ARGB Salmon = new ARGB(unchecked((int)0xFFFA8072), nameof(Salmon));

        /// <summary>
        /// Sandy Brown
        /// </summary>
        public static readonly ARGB SandyBrown = new ARGB(unchecked((int)0xFFF4A460), nameof(SandyBrown));

        /// <summary>
        /// Sea Green
        /// </summary>
        public static readonly ARGB SeaGreen = new ARGB(unchecked((int)0xFF2E8B57), nameof(SeaGreen));

        /// <summary>
        /// Sea Shell
        /// </summary>
        public static readonly ARGB SeaShell = new ARGB(unchecked((int)0xFFFFF5EE), nameof(SeaShell));

        /// <summary>
        /// Sienna
        /// </summary>
        public static readonly ARGB Sienna = new ARGB(unchecked((int)0xFFA0522D), nameof(Sienna));

        /// <summary>
        /// Silver
        /// </summary>
        public static readonly ARGB Silver = new ARGB(unchecked((int)0xFFC0C0C0), nameof(Silver));

        /// <summary>
        /// Sky Blue
        /// </summary>
        public static readonly ARGB SkyBlue = new ARGB(unchecked((int)0xFF87CEEB), nameof(SkyBlue));

        /// <summary>
        /// Slate Blue
        /// </summary>
        public static readonly ARGB SlateBlue = new ARGB(unchecked((int)0xFF6A5ACD), nameof(SlateBlue));

        /// <summary>
        /// Slate Gray
        /// </summary>
        public static readonly ARGB SlateGray = new ARGB(unchecked((int)0xFF708090), nameof(SlateGray));

        /// <summary>
        /// Snow
        /// </summary>
        public static readonly ARGB Snow = new ARGB(unchecked((int)0xFFFFFAFA), nameof(Snow));

        /// <summary>
        /// Spring Green
        /// </summary>
        public static readonly ARGB SpringGreen = new ARGB(unchecked((int)0xFF00FF7F), nameof(SpringGreen));

        /// <summary>
        /// Steel Blue
        /// </summary>
        public static readonly ARGB SteelBlue = new ARGB(unchecked((int)0xFF4682B4), nameof(SteelBlue));

        /// <summary>
        /// Tan
        /// </summary>
        public static readonly ARGB Tan = new ARGB(unchecked((int)0xFFD2B48C), nameof(Tan));

        /// <summary>
        /// Teal
        /// </summary>
        public static readonly ARGB Teal = new ARGB(unchecked((int)0xFF008080), nameof(Teal));

        /// <summary>
        /// Thistle
        /// </summary>
        public static readonly ARGB Thistle = new ARGB(unchecked((int)0xFFD8BFD8), nameof(Thistle));

        /// <summary>
        /// Tomato
        /// </summary>
        public static readonly ARGB Tomato = new ARGB(unchecked((int)0xFFFF6347), nameof(Tomato));

        /// <summary>
        /// Turquoise
        /// </summary>
        public static readonly ARGB Turquoise = new ARGB(unchecked((int)0xFF40E0D0), nameof(Turquoise));

        /// <summary>
        /// Violet
        /// </summary>
        public static readonly ARGB Violet = new ARGB(unchecked((int)0xFFEE82EE), nameof(Violet));

        /// <summary>
        /// Wheat
        /// </summary>
        public static readonly ARGB Wheat = new ARGB(unchecked((int)0xFFF5DEB3), nameof(Wheat));

        /// <summary>
        /// White
        /// </summary>
        public static readonly ARGB White = new ARGB(unchecked((int)0xFFFFFFFF), nameof(White));

        /// <summary>
        /// White Smoke
        /// </summary>
        public static readonly ARGB WhiteSmoke = new ARGB(unchecked((int)0xFFF5F5F5), nameof(WhiteSmoke));

        /// <summary>
        /// Yellow
        /// </summary>
        public static readonly ARGB Yellow = new ARGB(unchecked((int)0xFFFFFF00), nameof(Yellow));

        /// <summary>
        /// Yellow Green
        /// </summary>
        public static readonly ARGB YellowGreen = new ARGB(unchecked((int)0xFF9ACD32), nameof(YellowGreen));
    }
}
