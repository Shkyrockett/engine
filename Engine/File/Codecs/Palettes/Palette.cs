// <copyright file="Palette.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Engine.File.Palettes
{
    /// <summary>
    /// The <see cref="Palette"/> class used for loading, saving and storing color palette entries.
    /// </summary>
    [FileObject]
    [DisplayName("Palette")]
    public class Palette
        : IEnumerable
    {
        ///// <summary>
        ///// // ToDo: Add a named color lookup.
        ///// </summary>
        //private HashSet<KeyValuePair<string, Color>> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette" /> class.
        /// </summary>
        public Palette()
        {
            Colors = new List<Color>();
            PaletteMimeFormat = PaletteMimeFormats.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette" /> class.
        /// </summary>
        /// <param name="colors">An array of colors to add to the palette.</param>
        public Palette(Color[] colors)
        {
            Colors = new List<Color>();
            AddRange(colors);
            PaletteMimeFormat = PaletteMimeFormats.Default;
        }

        /// <summary>
        /// Gets or sets the list of colors in the palette.
        /// </summary>
        public List<Color> Colors { get; set; }

        /// <summary>
        /// Gets or sets the name of the palette file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the palette's MIME format.
        /// </summary>
        public PaletteMimeFormats PaletteMimeFormat { get; set; }

        /// <summary>
        /// Gets the number of colors in the palette.
        /// </summary>
        public int Count
            => Colors.Count;

        /// <summary>
        /// Gets the palette color entry at a specific index.
        /// </summary>
        /// <param name="index"><see cref="int"/> index of a <see cref="Palette"/> entry <see cref="Color"/>.</param>
        /// <returns>A value representing the <see cref="Color"/> at the specified index in the <see cref="Palette"/>.</returns>
        public Color this[int index]
            => Colors[index];

        /// <summary>
        /// Load a palette file from a file path.
        /// </summary>
        /// <param name="fileName">A string representing the name of a palette file to open.</param>
        public void Load(string fileName)
        {
            FileName = fileName;
            PaletteFileExtensions format = CheckExtensionSupport(FileName);
            if (format != PaletteFileExtensions.unknown)
            {
                using (Stream paletteStream = new FileStream(FileName, FileMode.Open))
                    Load(paletteStream, format);
            }
        }

        /// <summary>
        /// Load a palette from a stream.
        /// </summary>
        /// <param name="stream">The file stream of the opened file.</param>
        /// <param name="format">The extension format of the file opened.</param>
        public void Load(Stream stream, PaletteFileExtensions format)
        {
            if (stream == Stream.Null) return;

            // If we have a stream, the file should have successfully opened. Clear the colors list.
            Colors = new List<Color>();

            switch (format)
            {
                case PaletteFileExtensions.acb:
                    ReadAutoDeskPalette(stream);
                    break;
                case PaletteFileExtensions.aco:
                case PaletteFileExtensions.act:
                    ReadAdobePalette(stream);
                    break;
                case PaletteFileExtensions.cpl:
                    ReadCorelPalette(stream);
                    break;
                case PaletteFileExtensions.txt:
                    ReadTextPalette(stream);
                    break;
                case PaletteFileExtensions.pal:
                    ReadPalPalette(stream);
                    break;
                case PaletteFileExtensions.unknown:
                    break;
                default:
                    string header = null;
                    //header = this.ReadString(stream, 4);
                    header = stream.ReadString(4);
                    break;
            }
        }

        /// <summary>
        /// Save a palette to a file.
        /// </summary>
        /// <param name="fileName">The Filename to save the file as.</param>
        /// <param name="format">The MIME format of the file to save.</param>
        public void Save(string fileName, PaletteMimeFormats format)
        {
            // ToDo: Add Save functionality.
            switch (format)
            {
                case PaletteMimeFormats.Adobe:
                case PaletteMimeFormats.AutoDesk:
                case PaletteMimeFormats.Corel:
                    //throw new NotImplementedException();
                    break;
                case PaletteMimeFormats.JascPal0100:
                    WriteJascPalette(fileName);
                    break;
                case PaletteMimeFormats.Text:
                case PaletteMimeFormats.ComaDelimiated:
                case PaletteMimeFormats.SpaceDelimiated:
                //throw new NotImplementedException();
                    break;
                case PaletteMimeFormats.PaintDotNet:
                    WritePaintDotNetPalette(fileName);
                    break;
                case PaletteMimeFormats.RiffPal:
                    WriteRiffPalette(fileName);
                    break;
                case PaletteMimeFormats.Binary:
                case PaletteMimeFormats.Win31Pal:
                //throw new NotImplementedException();
                case PaletteMimeFormats.Default:
                    break;
                default:
                    //throw new NotImplementedException();
                    break;
            }
        }

        /// <summary>
        /// Draw the palette to a bitmap.
        /// </summary>
        /// <param name="bounds">The limiting bounds of the container control.</param>
        /// <param name="selection1">The index of the first palette entry that should be selected.</param>
        /// <param name="selection2">The index of the second palette entry that should be selected.</param>
        /// <param name="selection3">The index of the third palette entry that should be selected.</param>
        /// <param name="selection4">The index of the fourth palette entry that should be selected.</param>
        /// <param name="selection5">The index of the fifth palette entry that should be selected.</param>
        /// <param name="highlight">The highlight queue index.</param>
        /// <returns>A <see cref="Bitmap"/> representing the palette as a grid of color entries.</returns>
        public Bitmap DrawPalette(Rectangle bounds, int selection1, int selection2, int selection3, int selection4, int selection5, int highlight = -1)
        {
            // Exit if data is not properly formated.
            if (Colors == null)
                return null;

            if (Colors.Count == 0)
                return null;

            var grid = new RectangleCellGrid(new Rectangle(bounds.Location, new Size(bounds.Size.Width - 1, bounds.Size.Height - 1)), Colors.Count);
            //RectangleCellGrid grid = new RectangleCellGrid(bounds, this.colors.Count);

            // Create the Bitmap and graphics object to draw on.
            var image = new Bitmap(grid.InnerBounds.Width + 1, grid.InnerBounds.Height + 1);
            Graphics canvas = Graphics.FromImage(image);

            // Iterate through each color in the list and draw it on the canvas
            int index = 0;
            foreach (Color item in Colors)
            {
                // Calculate the location of the cell to draw.
                RectangleF cell = grid[index];

                // Draw the color cell to the canvas
                canvas.FillRectangle(new SolidBrush(item), cell);
                canvas.DrawRectangle(new Pen(Color.White), Rectangle.Round(cell));

                // Iterate to the next index in the palette entry list.
                index++;
            }

            // Add any borders for any selected colors.
            if (selection1 >= 0 && selection1 <= Colors.Count)
            {
                RectangleF cell = grid[selection1];
                canvas.DrawRectangle(new Pen(Color.Yellow), Rectangle.Round(cell));
            }

            if (selection2 >= 0 && selection2 <= Colors.Count)
            {
                RectangleF cell = grid[selection2];
                canvas.DrawRectangle(new Pen(Color.Red), Rectangle.Round(cell));
            }

            if (selection3 >= 0 && selection3 <= Colors.Count)
            {
                RectangleF cell = grid[selection3];
                canvas.DrawRectangle(new Pen(Color.Blue), Rectangle.Round(cell));
            }

            if (selection4 >= 0 && selection4 <= Colors.Count)
            {
                RectangleF cell = grid[selection4];
                canvas.DrawRectangle(new Pen(Color.Lime), Rectangle.Round(cell));
            }

            if (selection5 >= 0 && selection5 <= Colors.Count)
            {
                RectangleF cell = grid[selection5];
                canvas.DrawRectangle(new Pen(Color.Cyan), Rectangle.Round(cell));
            }

            if (highlight >= 0 && highlight <= Colors.Count + 1)
            {
                RectangleF cell = grid[highlight];
                Color highlightColor = Color.FromArgb(128, Color.CornflowerBlue.R, Color.CornflowerBlue.G, Color.CornflowerBlue.B);
                canvas.DrawRectangle(new Pen(highlightColor, 3), Rectangle.Round(cell));
            }

            return image;
        }

        /// <summary>
        /// Looks up the palette entry index for the specified point in the palette grid.
        /// </summary>
        /// <param name="location">The location to look in the palette grid.</param>
        /// <param name="bounds">The limiting bounds of the container control.</param>
        /// <returns>The index of the palette entry at the specified coordinates of the grid.</returns>
        public int PointToPaletteEntry(Point location, Rectangle bounds)
        {
            int value = -1;

            // Exit if data is not properly formated.
            if (Colors != null || Colors.Count != 0)
            {
                var grid = new RectangleCellGrid(bounds, Colors.Count);

                // Calculate the index of the item under the point location.
                value = grid[location];
                value = (value < Colors.Count) ? value : -1;
            }

            return value;
        }

        /// <summary>
        /// Add a color to the end of the palette.
        /// </summary>
        /// <param name="item">The color to add to the palette.</param>
        public void Add(Color item)
            => Colors.Add(item);

        /// <summary>
        /// Add a list of palette color entries to the end of the palette.
        /// </summary>
        /// <param name="items">The colors to add to the palette.</param>
        public void AddRange(IEnumerable<Color> items)
            => Colors.AddRange(items);

        /// <summary>
        /// Adds a palette entry at a specified index.
        /// </summary>
        /// <param name="index">Index to insert the palette entry color.</param>
        /// <param name="item">Palette entry color to add to the list.</param>
        public void Insert(int index, Color item)
            => Colors.Insert(index, item);

        /// <summary>
        /// Adds a list of palette entries at a specified index.
        /// </summary>
        /// <param name="index">Index to insert the palette entry color.</param>
        /// <param name="item">List of palette entry colors to add to the list.</param>
        public void InsertRange(int index, IEnumerable<Color> item) => Colors.InsertRange(index, item);

        /// <summary>
        /// Clears all palette entries from the list of colors.
        /// </summary>
        public void Clear() => Colors.Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => (IEnumerator)Colors;

        /// <summary>
        /// Remove the first instance of a specified color entry in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was removed.</returns>
        public bool RemoveFirstInstance(Color item) => Colors.Remove(Colors[Colors.IndexOf(item)]);

        /// <summary>
        /// Remove the last instance of a specified color entry in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was removed.</returns>
        public bool RemoveLastInstance(Color item) => Colors.Remove(Colors[Colors.LastIndexOf(item)]);

        /// <summary>
        /// Determines whether the specified palette item is in the palette list of colors.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was found in the list.</returns>
        public bool Contains(Color item) => Colors.Contains(item);

        /// <summary>
        /// Searches for a the specific color in the palette and returns its index if found.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>The first index of the color in the palette.</returns>
        public int IndexOf(Color item) => Colors.IndexOf(item);

        /// <summary>
        /// Returns the last index of a given color in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>Returns the index of the last instance of the given color.</returns>
        public int LastIndexOf(Color item) => Colors.LastIndexOf(item);

        /// <summary>
        /// Reverses the order of the colors in the colors in the palette.
        /// </summary>
        public void Reverse() => Colors.Reverse();

        /// <summary>
        /// Sorts the colors in the palette by the default sorting order.
        /// </summary>
        public void Sort() => Colors.Sort();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadAutoDeskPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.AutoDesk;
            long startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            //{
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadAdobePalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Adobe;
            long startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            //{
            //}
        }

        /// <summary>
        /// http://www.selapa.net/swatches/colors/fileformats.php
        /// </summary>
        /// <param name="stream"></param>
        private void ReadCorelPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Corel;
            long startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            {
                ushort mimeVersion = FileEx.NetworkToHostOrder(binaryReader.ReadUInt16());
                ushort colorCount = 0;
                string name = string.Empty;
                if (mimeVersion == 0xccbc)
                {
                     // Version 5-8
                }
                else if (mimeVersion == 0xdddc) // Version 9-X3
                {
                    int headerBlocks = binaryReader.ReadInt32();
                    int test1 = binaryReader.ReadInt32();
                    int offset = binaryReader.ReadInt32();
                    int offset1 = binaryReader.ReadInt32();
                    int offset2 = binaryReader.ReadInt32();
                    int offset3 = binaryReader.ReadInt32();
                    int offset4 = binaryReader.ReadInt32();
                    byte stringLen = binaryReader.ReadByte();
                    name = binaryReader.ReadString16(stringLen * 2);
                    short paletteType = binaryReader.ReadInt16();
                    short entryCount = binaryReader.ReadInt16();
                    // 4 byte BGR-A String
                    while (stream.Position < stream.Length)
                    {
                        int colorId = binaryReader.ReadInt32();
                        short colorModel = binaryReader.ReadInt16();
                        short colorType = binaryReader.ReadInt16();
                        byte blue = binaryReader.ReadByte();
                        byte green = binaryReader.ReadByte();
                        byte red = binaryReader.ReadByte();
                        byte alpha = binaryReader.ReadByte();
                        Colors.Add(Color.FromArgb(255 - alpha, red, green, blue));
                        byte stringLen2 = binaryReader.ReadByte();
                        string colorName = binaryReader.ReadString16(stringLen2 * 2);
                        if (Colors.Count == entryCount) break;
                    }
                    //while (stream.Position < stream.Length)
                    //{
                    //    short colorModel = binaryReader.ReadInt16();
                    //    short colorType = binaryReader.ReadInt16();
                    //    byte red = binaryReader.ReadByte();
                    //    byte green = binaryReader.ReadByte();
                    //    byte blue = binaryReader.ReadByte();
                    //    this.colors.Add(Color.FromArgb(red, green, blue));
                    //    if (colors.Count == entryCount) break;
                    //}
                }
#pragma warning disable
                else if (mimeVersion == 0xccdc)
                {
                    ; // ?
                }
                else if (mimeVersion == 0xcddc)
                {
                    ; // ?
                }
                else if (mimeVersion == 0xcddd)
                {
                    ; // Version X4
                }
#pragma warning restore
                else if (mimeVersion == 0xdcdc) // Custom palettes
                {
                    byte namelength = binaryReader.ReadByte();
                    name = binaryReader.ReadString(namelength);
                    colorCount = binaryReader.ReadUInt16();
                    var colorModel = (CorelColorModel)binaryReader.ReadUInt16();
                    ushort colorType = binaryReader.ReadUInt16();
                    long streamEnd = stream.Position + colorCount;
                    // Three byte RGB 
                    while (stream.Position < stream.Length)
                    {
                        byte red = binaryReader.ReadByte();
                        byte green = binaryReader.ReadByte();
                        byte blue = binaryReader.ReadByte();
                        //byte alpha = binaryReader.ReadByte();
                        //byte beta = binaryReader.ReadByte();
                        //byte gama = binaryReader.ReadByte();
                        //byte delta = binaryReader.ReadByte();
                        //byte epsilon = binaryReader.ReadByte();
                        //byte colorNameLength = binaryReader.ReadByte();
                        //string colorName = binaryReader.ReadString(colorNameLength);
                        Colors.Add(Color.FromArgb(red, green, blue));
                        //this.colors.Add(Color.FromArgb(255 - alpha, red, green, blue));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadPalPalette(Stream stream)
        {
            long startPossition = stream.Position;
            string headder = stream.ReadString(4);

            if (headder.StartsWith("RIFF", StringComparison.InvariantCultureIgnoreCase))
            {
                stream.Position = startPossition;
                ReadRiffPalette(stream);
            }
            else if (headder.StartsWith("JASC", StringComparison.InvariantCultureIgnoreCase))
            {
                stream.Position = startPossition;
                ReadJascPalette(stream);
            }
            else
            {
                stream.Position = startPossition;
                ReadBinaryPalette(stream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private void ReadRiffPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.RiffPal;
            long startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            {
                // RIFF header
                string riff = binaryReader.ReadString(4); // "RIFF"
                int dataSize = binaryReader.ReadInt32();
                string type = binaryReader.ReadString(4); // "PAL "

                // Data chunk
                string chunkType = binaryReader.ReadString(4); // "data"
                int chunkSize = binaryReader.ReadInt32();
                short palVersion = binaryReader.ReadInt16(); // always 0x0300
                short palEntries = binaryReader.ReadInt16();

                // Colors
                for (int i = 0; i < palEntries; i++)
                {
                    byte red = binaryReader.ReadByte();
                    byte green = binaryReader.ReadByte();
                    byte blue = binaryReader.ReadByte();
                    byte alpha = binaryReader.ReadByte();
                    Colors.Add(Color.FromArgb(255 - alpha, red, green, blue));
                    //this.colors.Add(Color.FromArgb(br.ReadInt32()));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadBinaryPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Binary;
            long startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            {
                long Length = binaryReader.BaseStream.Length / 4 - 1;
                for (int Index = 0; (Index <= Length); Index++)
                {
                    byte blue = binaryReader.ReadByte();
                    byte green = binaryReader.ReadByte();
                    byte red = binaryReader.ReadByte();
                    byte alpha = binaryReader.ReadByte();
                    Colors.Add(Color.FromArgb(255 - alpha, red, green, blue));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadJascPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.JascPal0100;
            long startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            using (var streamReader = new StreamReader(stream))
            using (var StrReader = new StringReader(streamReader.ReadToEnd()))
            {
                string head = StrReader.ReadLine();
                string version = StrReader.ReadLine();
                string len = StrReader.ReadLine();
                double length = (double.Parse(len) - 1);
                for (int Index = 0; (Index <= length); Index++)
                {
                    string[] ReadStr = StrReader.ReadLine().Split(new char[] { ' ' });
                    if (ReadStr.Length == 3)
                        Colors.Add(Color.FromArgb(int.Parse(ReadStr[0]), int.Parse(ReadStr[1]), int.Parse(ReadStr[2])));
                    else if (ReadStr.Length == 4)
                        Colors.Add(Color.FromArgb(int.Parse(ReadStr[3]), int.Parse(ReadStr[0]), int.Parse(ReadStr[1]), int.Parse(ReadStr[2])));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void ReadTextPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Text;
            long startPossition = stream.Position;

            using (var streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith(";", StringComparison.InvariantCulture))
                    {
                        PaletteMimeFormat = PaletteMimeFormats.PaintDotNet;
                    }
                    else if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] argb;
                        var color = new Color();
                        if (line.Contains(" ") || line.Contains(","))
                        {
                            if (PaletteMimeFormat == PaletteMimeFormats.Text && line.Contains(" "))
                                PaletteMimeFormat = PaletteMimeFormats.SpaceDelimiated;
                            else if (PaletteMimeFormat == PaletteMimeFormats.Text && line.Contains(","))
                                PaletteMimeFormat = PaletteMimeFormats.ComaDelimiated;

                            argb = line.Split(new char[] { ' ', ',' });

                            if (argb.Length == 3)
                                color = Color.FromArgb(int.Parse(argb[0]), int.Parse(argb[1]), int.Parse(argb[2]));
                            else if (argb.Length == 4)
                                color = Color.FromArgb(int.Parse(argb[3]), int.Parse(argb[0]), int.Parse(argb[1]), int.Parse(argb[2]));
                        }
                        else
                        {
                            color = Color.FromArgb(int.Parse(line, System.Globalization.NumberStyles.HexNumber));
                        }

                        color = LookupNamedColor(color);
                        Colors.Add(color);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void WriteRiffPalette(string filename)
        {
            // Calculate file length
            int length = 4 + 4 + 4 + 4 + 2 + 2 + Colors.Count * 4;

            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var bw = new BinaryWriter(stream))
            {
                // RIFF header
                bw.WriteString("RIFF");
                bw.Write(length);
                bw.WriteString("PAL ");

                // Data chunk
                bw.WriteString("data");
                bw.Write(Colors.Count * 4 + 4);
                bw.Write((short)0x0300); // PAL version
                bw.Write((short)Colors.Count);

                // Colors
                foreach (Color color in Colors)
                {
                    bw.Write(color.R);
                    bw.Write(color.G);
                    bw.Write(color.B);
                    bw.Write((byte)(255 - color.A));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void WriteJascPalette(string filename)
        {
            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var bw = new StreamWriter(stream))
            {
                // RIFF header
                bw.WriteLine("JASC-PAL");

                // Version line
                bw.WriteLine("0100");

                // length
                bw.WriteLine(Colors.Count);

                // Colors
                foreach (Color color in Colors)
                    bw.WriteLine(color.R + " " + color.G + " " + color.B + " " + color.A);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void WritePaintDotNetPalette(string filename)
        {
            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var bw = new StreamWriter(stream))
            {
                // Header
                bw.WriteLine("; paint.net Palette File");
                bw.WriteLine("; Lines that start with a semicolon are comments");
                bw.WriteLine("; Colors are written as 8-digit hexadecimal numbers: aarrggbb");
                bw.WriteLine("; For example, this would specify green: FF00FF00");
                bw.WriteLine("; The alpha ('aa') value specifies how transparent a color is. FF is fully opaque, 00 is fully transparent.");
                bw.WriteLine("; A palette must consist of ninety six (96) colors. If there are less than this, the remaining color");
                bw.WriteLine("; slots will be set to white (FFFFFFFF). If there are more, then the remaining colors will be ignored.");

                // Colors
                foreach (Color color in Colors)
                    bw.WriteLine("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static PaletteFileExtensions CheckExtensionSupport(string filepath)
        {
            PaletteFileExtensions format = PaletteFileExtensions.unknown;
            string extension = System.IO.Path.GetExtension(filepath).Substring(1);
            Enum.TryParse(extension, true, out format);
            return format;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testColor"></param>
        /// <returns></returns>
        private Color LookupNamedColor(Color testColor)
        {
            Color known = (
                        from prop in typeof(Color)
                            .GetProperties(BindingFlags.Public | BindingFlags.Static)
                        where prop.PropertyType == typeof(Color)
                        let color = (Color)prop.GetValue(null, null)
                        where color.A == testColor.A && color.R == testColor.R
                            && color.G == testColor.G && color.B == testColor.B
                        select color)
                        .FirstOrDefault();
            if (known != new Color()) return known;
            return testColor;
        }
    }
}
