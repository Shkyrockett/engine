using Engine.Colorspace;
using Engine.Imaging;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class SolidFills
    {
        /// <summary>
        /// Transparent
        /// </summary>
        public static readonly IFillable Transparent = new SolidFill(Colors.Transparent);

        /// <summary>
        /// Alice Blue
        /// </summary>
        public static readonly IFillable AliceBlue = new SolidFill(Colors.AliceBlue);

        /// <summary>
        /// Antique White
        /// </summary>
        public static readonly IFillable AntiqueWhite = new SolidFill(Colors.AntiqueWhite);

        /// <summary>
        /// Aqua
        /// </summary>
        public static readonly IFillable Aqua = new SolidFill(Colors.Aqua);

        /// <summary>
        /// Aquamarine
        /// </summary>
        public static readonly IFillable Aquamarine = new SolidFill(Colors.Aquamarine);

        /// <summary>
        /// Azure
        /// </summary>
        public static readonly IFillable Azure = new SolidFill(Colors.Azure);

        /// <summary>
        /// Beige
        /// </summary>
        public static readonly IFillable Beige = new SolidFill(Colors.Beige);

        /// <summary>
        /// Bisque
        /// </summary>
        public static readonly IFillable Bisque = new SolidFill(Colors.Bisque);

        /// <summary>
        /// Black
        /// </summary>
        public static readonly IFillable Black = new SolidFill(Colors.Black);

        /// <summary>
        /// Blanched Almond
        /// </summary>
        public static readonly IFillable BlanchedAlmond = new SolidFill(Colors.BlanchedAlmond);

        /// <summary>
        /// Blue
        /// </summary>
        public static readonly IFillable Blue = new SolidFill(Colors.Blue);

        /// <summary>
        /// Blue Violet
        /// </summary>
        public static readonly IFillable BlueViolet = new SolidFill(Colors.BlueViolet);

        /// <summary>
        /// Brown
        /// </summary>
        public static readonly IFillable Brown = new SolidFill(Colors.Brown);

        /// <summary>
        /// Burly Wood
        /// </summary>
        public static readonly IFillable BurlyWood = new SolidFill(Colors.BurlyWood);

        /// <summary>
        /// Cadet Blue
        /// </summary>
        public static readonly IFillable CadetBlue = new SolidFill(Colors.CadetBlue);

        /// <summary>
        /// Chartreuse
        /// </summary>
        public static readonly IFillable Chartreuse = new SolidFill(Colors.Chartreuse);

        /// <summary>
        /// Chocolate
        /// </summary>
        public static readonly IFillable Chocolate = new SolidFill(Colors.Chocolate);

        /// <summary>
        /// Coral
        /// </summary>
        public static readonly IFillable Coral = new SolidFill(Colors.Coral);

        /// <summary>
        /// Cornflower Blue
        /// </summary>
        public static readonly IFillable CornflowerBlue = new SolidFill(Colors.CornflowerBlue);

        /// <summary>
        /// Corn-silk
        /// </summary>
        public static readonly IFillable Cornsilk = new SolidFill(Colors.Cornsilk);

        /// <summary>
        /// Crimson
        /// </summary>
        public static readonly IFillable Crimson = new SolidFill(Colors.Crimson);

        /// <summary>
        /// Cyan
        /// </summary>
        public static readonly IFillable Cyan = new SolidFill(Colors.Cyan);

        /// <summary>
        /// Dark Blue
        /// </summary>
        public static readonly IFillable DarkBlue = new SolidFill(Colors.DarkBlue);

        /// <summary>
        /// Dark Cyan
        /// </summary>
        public static readonly IFillable DarkCyan = new SolidFill(Colors.DarkCyan);

        /// <summary>
        /// Dark Goldenrod
        /// </summary>
        public static readonly IFillable DarkGoldenrod = new SolidFill(Colors.DarkGoldenrod);

        /// <summary>
        /// Dark Gray
        /// </summary>
        public static readonly IFillable DarkGray = new SolidFill(Colors.DarkGray);

        /// <summary>
        /// Dark Green
        /// </summary>
        public static readonly IFillable DarkGreen = new SolidFill(Colors.DarkGreen);

        /// <summary>
        /// Dark Khaki
        /// </summary>
        public static readonly IFillable DarkKhaki = new SolidFill(Colors.DarkKhaki);

        /// <summary>
        /// Dark Magenta
        /// </summary>
        public static readonly IFillable DarkMagenta = new SolidFill(Colors.DarkMagenta);

        /// <summary>
        /// Dark Olive Green
        /// </summary>
        public static readonly IFillable DarkOliveGreen = new SolidFill(Colors.DarkOliveGreen);

        /// <summary>
        /// Dark Orange
        /// </summary>
        public static readonly IFillable DarkOrange = new SolidFill(Colors.DarkOrange);

        /// <summary>
        /// Dark Orchid
        /// </summary>
        public static readonly IFillable DarkOrchid = new SolidFill(Colors.DarkOrchid);

        /// <summary>
        /// Dark Red
        /// </summary>
        public static readonly IFillable DarkRed = new SolidFill(Colors.DarkRed);

        /// <summary>
        /// Dark Salmon
        /// </summary>
        public static readonly IFillable DarkSalmon = new SolidFill(Colors.DarkSalmon);

        /// <summary>
        /// Dark Sea Green
        /// </summary>
        public static readonly IFillable DarkSeaGreen = new SolidFill(Colors.DarkSeaGreen);

        /// <summary>
        /// Dark Slate Blue
        /// </summary>
        public static readonly IFillable DarkSlateBlue = new SolidFill(Colors.DarkSlateBlue);

        /// <summary>
        /// Dark Slate Gray
        /// </summary>
        public static readonly IFillable DarkSlateGray = new SolidFill(Colors.DarkSlateGray);

        /// <summary>
        /// Dark Turquoise
        /// </summary>
        public static readonly IFillable DarkTurquoise = new SolidFill(Colors.DarkTurquoise);

        /// <summary>
        /// Dark Violet
        /// </summary>
        public static readonly IFillable DarkViolet = new SolidFill(Colors.DarkViolet);

        /// <summary>
        /// Deep Pink
        /// </summary>
        public static readonly IFillable DeepPink = new SolidFill(Colors.DeepPink);

        /// <summary>
        /// Deep Sky Blue
        /// </summary>
        public static readonly IFillable DeepSkyBlue = new SolidFill(Colors.DeepSkyBlue);

        /// <summary>
        /// Dim Gray
        /// </summary>
        public static readonly IFillable DimGray = new SolidFill(Colors.DimGray);

        /// <summary>
        /// Dodger Blue
        /// </summary>
        public static readonly IFillable DodgerBlue = new SolidFill(Colors.DodgerBlue);

        /// <summary>
        /// Firebrick
        /// </summary>
        public static readonly IFillable Firebrick = new SolidFill(Colors.Firebrick);

        /// <summary>
        /// Floral White
        /// </summary>
        public static readonly IFillable FloralWhite = new SolidFill(Colors.FloralWhite);

        /// <summary>
        /// Forest Green
        /// </summary>
        public static readonly IFillable ForestGreen = new SolidFill(Colors.ForestGreen);

        /// <summary>
        /// Fuchsia
        /// </summary>
        public static readonly IFillable Fuchsia = new SolidFill(Colors.Fuchsia);

        /// <summary>
        /// Gainsboro
        /// </summary>
        public static readonly IFillable Gainsboro = new SolidFill(Colors.Gainsboro);

        /// <summary>
        /// Ghost White
        /// </summary>
        public static readonly IFillable GhostWhite = new SolidFill(Colors.GhostWhite);

        /// <summary>
        /// Gold
        /// </summary>
        public static readonly IFillable Gold = new SolidFill(Colors.Gold);

        /// <summary>
        /// Goldenrod
        /// </summary>
        public static readonly IFillable Goldenrod = new SolidFill(Colors.Goldenrod);

        /// <summary>
        /// Gray
        /// </summary>
        public static readonly IFillable Gray = new SolidFill(Colors.Gray);

        /// <summary>
        /// Green
        /// </summary>
        public static readonly IFillable Green = new SolidFill(Colors.Green);

        /// <summary>
        /// Green Yellow
        /// </summary>
        public static readonly IFillable GreenYellow = new SolidFill(Colors.GreenYellow);

        /// <summary>
        /// Honeydew
        /// </summary>
        public static readonly IFillable Honeydew = new SolidFill(Colors.Honeydew);

        /// <summary>
        /// Hot Pink
        /// </summary>
        public static readonly IFillable HotPink = new SolidFill(Colors.HotPink);

        /// <summary>
        /// Indian Red
        /// </summary>
        public static readonly IFillable IndianRed = new SolidFill(Colors.IndianRed);

        /// <summary>
        /// Indigo
        /// </summary>
        public static readonly IFillable Indigo = new SolidFill(Colors.Indigo);

        /// <summary>
        /// Ivory
        /// </summary>
        public static readonly IFillable Ivory = new SolidFill(Colors.Ivory);

        /// <summary>
        /// Khaki
        /// </summary>
        public static readonly IFillable Khaki = new SolidFill(Colors.Khaki);

        /// <summary>
        /// Lavender
        /// </summary>
        public static readonly IFillable Lavender = new SolidFill(Colors.Lavender);

        /// <summary>
        /// Lavender Blush
        /// </summary>
        public static readonly IFillable LavenderBlush = new SolidFill(Colors.LavenderBlush);

        /// <summary>
        /// Lawn Green
        /// </summary>
        public static readonly IFillable LawnGreen = new SolidFill(Colors.LawnGreen);

        /// <summary>
        /// Lemon Chiffon
        /// </summary>
        public static readonly IFillable LemonChiffon = new SolidFill(Colors.LemonChiffon);

        /// <summary>
        /// Light Blue
        /// </summary>
        public static readonly IFillable LightBlue = new SolidFill(Colors.LightBlue);

        /// <summary>
        /// Light Coral
        /// </summary>
        public static readonly IFillable LightCoral = new SolidFill(Colors.LightCoral);

        /// <summary>
        /// Light Cyan
        /// </summary>
        public static readonly IFillable LightCyan = new SolidFill(Colors.LightCyan);

        /// <summary>
        /// Light Goldenrod Yellow
        /// </summary>
        public static readonly IFillable LightGoldenrodYellow = new SolidFill(Colors.LightGoldenrodYellow);

        /// <summary>
        /// Light Green
        /// </summary>
        public static readonly IFillable LightGreen = new SolidFill(Colors.LightGreen);

        /// <summary>
        /// Light Gray
        /// </summary>
        public static readonly IFillable LightGray = new SolidFill(Colors.LightGray);

        /// <summary>
        /// Light Pink
        /// </summary>
        public static readonly IFillable LightPink = new SolidFill(Colors.LightPink);

        /// <summary>
        /// Light Salmon
        /// </summary>
        public static readonly IFillable LightSalmon = new SolidFill(Colors.LightSalmon);

        /// <summary>
        /// Light Sea Green
        /// </summary>
        public static readonly IFillable LightSeaGreen = new SolidFill(Colors.LightSeaGreen);

        /// <summary>
        /// Light Sky Blue
        /// </summary>
        public static readonly IFillable LightSkyBlue = new SolidFill(Colors.LightSkyBlue);

        /// <summary>
        /// Light Slate Gray
        /// </summary>
        public static readonly IFillable LightSlateGray = new SolidFill(Colors.LightSlateGray);

        /// <summary>
        /// Light Steel Blue
        /// </summary>
        public static readonly IFillable LightSteelBlue = new SolidFill(Colors.LightSteelBlue);

        /// <summary>
        /// Light Yellow
        /// </summary>
        public static readonly IFillable LightYellow = new SolidFill(Colors.LightYellow);

        /// <summary>
        /// Lime
        /// </summary>
        public static readonly IFillable Lime = new SolidFill(Colors.Lime);

        /// <summary>
        /// Lime Green
        /// </summary>
        public static readonly IFillable LimeGreen = new SolidFill(Colors.LimeGreen);

        /// <summary>
        /// Linen
        /// </summary>
        public static readonly IFillable Linen = new SolidFill(Colors.Linen);

        /// <summary>
        /// Magenta
        /// </summary>
        public static readonly IFillable Magenta = new SolidFill(Colors.Magenta);

        /// <summary>
        /// Maroon
        /// </summary>
        public static readonly IFillable Maroon = new SolidFill(Colors.Maroon);

        /// <summary>
        /// Medium Aquamarine
        /// </summary>
        public static readonly IFillable MediumAquamarine = new SolidFill(Colors.MediumAquamarine);

        /// <summary>
        /// Medium Blue
        /// </summary>
        public static readonly IFillable MediumBlue = new SolidFill(Colors.MediumBlue);

        /// <summary>
        /// Medium Orchid
        /// </summary>
        public static readonly IFillable MediumOrchid = new SolidFill(Colors.MediumOrchid);

        /// <summary>
        /// Medium Purple
        /// </summary>
        public static readonly IFillable MediumPurple = new SolidFill(Colors.MediumPurple);

        /// <summary>
        /// Medium Sea Green
        /// </summary>
        public static readonly IFillable MediumSeaGreen = new SolidFill(Colors.MediumSeaGreen);

        /// <summary>
        /// Medium Slate Blue
        /// </summary>
        public static readonly IFillable MediumSlateBlue = new SolidFill(Colors.MediumSlateBlue);

        /// <summary>
        /// Medium Spring Green
        /// </summary>
        public static readonly IFillable MediumSpringGreen = new SolidFill(Colors.MediumSpringGreen);

        /// <summary>
        /// Medium Turquoise
        /// </summary>
        public static readonly IFillable MediumTurquoise = new SolidFill(Colors.MediumTurquoise);

        /// <summary>
        /// Medium Violet Red
        /// </summary>
        public static readonly IFillable MediumVioletRed = new SolidFill(Colors.MediumVioletRed);

        /// <summary>
        /// Midnight Blue
        /// </summary>
        public static readonly IFillable MidnightBlue = new SolidFill(Colors.MidnightBlue);

        /// <summary>
        /// Mint Cream
        /// </summary>
        public static readonly IFillable MintCream = new SolidFill(Colors.MintCream);

        /// <summary>
        /// Misty Rose
        /// </summary>
        public static readonly IFillable MistyRose = new SolidFill(Colors.MistyRose);

        /// <summary>
        /// Moccasin
        /// </summary>
        public static readonly IFillable Moccasin = new SolidFill(Colors.Moccasin);

        /// <summary>
        /// Navajo White
        /// </summary>
        public static readonly IFillable NavajoWhite = new SolidFill(Colors.NavajoWhite);

        /// <summary>
        /// Navy
        /// </summary>
        public static readonly IFillable Navy = new SolidFill(Colors.Navy);

        /// <summary>
        /// Old Lace
        /// </summary>
        public static readonly IFillable OldLace = new SolidFill(Colors.OldLace);

        /// <summary>
        /// Olive
        /// </summary>
        public static readonly IFillable Olive = new SolidFill(Colors.Olive);

        /// <summary>
        /// Olive Drab
        /// </summary>
        public static readonly IFillable OliveDrab = new SolidFill(Colors.OliveDrab);

        /// <summary>
        /// Orange
        /// </summary>
        public static readonly IFillable Orange = new SolidFill(Colors.Orange);

        /// <summary>
        /// Orange Red
        /// </summary>
        public static readonly IFillable OrangeRed = new SolidFill(Colors.OrangeRed);

        /// <summary>
        /// Orchid
        /// </summary>
        public static readonly IFillable Orchid = new SolidFill(Colors.Orchid);

        /// <summary>
        /// Pale Goldenrod
        /// </summary>
        public static readonly IFillable PaleGoldenrod = new SolidFill(Colors.PaleGoldenrod);

        /// <summary>
        /// Pale Green
        /// </summary>
        public static readonly IFillable PaleGreen = new SolidFill(Colors.PaleGreen);

        /// <summary>
        /// PaleTurquoise
        /// </summary>
        public static readonly IFillable PaleTurquoise = new SolidFill(Colors.PaleTurquoise);

        /// <summary>
        /// Pale Violet Red
        /// </summary>
        public static readonly IFillable PaleVioletRed = new SolidFill(Colors.PaleVioletRed);

        /// <summary>
        /// Papaya Whip
        /// </summary>
        public static readonly IFillable PapayaWhip = new SolidFill(Colors.PapayaWhip);

        /// <summary>
        /// Peach Puff
        /// </summary>
        public static readonly IFillable PeachPuff = new SolidFill(Colors.PeachPuff);

        /// <summary>
        /// Peru
        /// </summary>
        public static readonly IFillable Peru = new SolidFill(Colors.Peru);

        /// <summary>
        /// Pink
        /// </summary>
        public static readonly IFillable Pink = new SolidFill(Colors.Pink);

        /// <summary>
        /// Plum
        /// </summary>
        public static readonly IFillable Plum = new SolidFill(Colors.Plum);

        /// <summary>
        /// Powder Blue
        /// </summary>
        public static readonly IFillable PowderBlue = new SolidFill(Colors.PowderBlue);

        /// <summary>
        /// Purple
        /// </summary>
        public static readonly IFillable Purple = new SolidFill(Colors.Purple);

        /// <summary>
        /// Red
        /// </summary>
        public static readonly IFillable Red = new SolidFill(Colors.Red);

        /// <summary>
        /// Rosy Brown
        /// </summary>
        public static readonly IFillable RosyBrown = new SolidFill(Colors.RosyBrown);

        /// <summary>
        /// Royal Blue
        /// </summary>
        public static readonly IFillable RoyalBlue = new SolidFill(Colors.RoyalBlue);

        /// <summary>
        /// Saddle Brown
        /// </summary>
        public static readonly IFillable SaddleBrown = new SolidFill(Colors.SaddleBrown);

        /// <summary>
        /// Salmon
        /// </summary>
        public static readonly IFillable Salmon = new SolidFill(Colors.Salmon);

        /// <summary>
        /// Sandy Brown
        /// </summary>
        public static readonly IFillable SandyBrown = new SolidFill(Colors.SandyBrown);

        /// <summary>
        /// Sea Green
        /// </summary>
        public static readonly IFillable SeaGreen = new SolidFill(Colors.SeaGreen);

        /// <summary>
        /// Sea Shell
        /// </summary>
        public static readonly IFillable SeaShell = new SolidFill(Colors.SeaShell);

        /// <summary>
        /// Sienna
        /// </summary>
        public static readonly IFillable Sienna = new SolidFill(Colors.Sienna);

        /// <summary>
        /// Silver
        /// </summary>
        public static readonly IFillable Silver = new SolidFill(Colors.Silver);

        /// <summary>
        /// Sky Blue
        /// </summary>
        public static readonly IFillable SkyBlue = new SolidFill(Colors.SkyBlue);

        /// <summary>
        /// Slate Blue
        /// </summary>
        public static readonly IFillable SlateBlue = new SolidFill(Colors.SlateBlue);

        /// <summary>
        /// Slate Gray
        /// </summary>
        public static readonly IFillable SlateGray = new SolidFill(Colors.SlateGray);

        /// <summary>
        /// Snow
        /// </summary>
        public static readonly IFillable Snow = new SolidFill(Colors.Snow);

        /// <summary>
        /// Spring Green
        /// </summary>
        public static readonly IFillable SpringGreen = new SolidFill(Colors.SpringGreen);

        /// <summary>
        /// Steel Blue
        /// </summary>
        public static readonly IFillable SteelBlue = new SolidFill(Colors.SteelBlue);

        /// <summary>
        /// Tan
        /// </summary>
        public static readonly IFillable Tan = new SolidFill(Colors.Tan);

        /// <summary>
        /// Teal
        /// </summary>
        public static readonly IFillable Teal = new SolidFill(Colors.Teal);

        /// <summary>
        /// Thistle
        /// </summary>
        public static readonly IFillable Thistle = new SolidFill(Colors.Thistle);

        /// <summary>
        /// Tomato
        /// </summary>
        public static readonly IFillable Tomato = new SolidFill(Colors.Tomato);

        /// <summary>
        /// Turquoise
        /// </summary>
        public static readonly IFillable Turquoise = new SolidFill(Colors.Turquoise);

        /// <summary>
        /// Violet
        /// </summary>
        public static readonly IFillable Violet = new SolidFill(Colors.Violet);

        /// <summary>
        /// Wheat
        /// </summary>
        public static readonly IFillable Wheat = new SolidFill(Colors.Wheat);

        /// <summary>
        /// White
        /// </summary>
        public static readonly IFillable White = new SolidFill(Colors.White);

        /// <summary>
        /// White Smoke
        /// </summary>
        public static readonly IFillable WhiteSmoke = new SolidFill(Colors.WhiteSmoke);

        /// <summary>
        /// Yellow
        /// </summary>
        public static readonly IFillable Yellow = new SolidFill(Colors.Yellow);

        /// <summary>
        /// Yellow Green
        /// </summary>
        public static readonly IFillable YellowGreen = new SolidFill(Colors.YellowGreen);
    }
}
