// <copyright file="Palette.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
using System.IO;
using System.Linq;
using Engine.Colorspace;

namespace Engine.File.Palettes
{
    /// <summary>
    /// The <see cref="Palette"/> class used for loading, saving and storing color palette entries.
    /// </summary>
    [FileObject]
    [DisplayName(nameof(Palette))]
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
            Colors = new List<RGBA>();
            PaletteMimeFormat = PaletteMimeFormats.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette" /> class.
        /// </summary>
        /// <param name="colors">An array of colors to add to the palette.</param>
        public Palette(RGBA[] colors)
        {
            Colors = new List<RGBA>();
            AddRange(colors);
            PaletteMimeFormat = PaletteMimeFormats.Default;
        }

        /// <summary>
        /// Gets or sets the list of colors in the palette.
        /// </summary>
        public List<RGBA> Colors { get; set; }

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
        /// <param name="index"><see cref="int"/> index of a <see cref="Palette"/> entry <see cref="RGBA"/>.</param>
        /// <returns>A value representing the <see cref="RGBA"/> at the specified index in the <see cref="Palette"/>.</returns>
        public RGBA this[int index]
            => Colors[index];

        /// <summary>
        /// Load a palette file from a file path.
        /// </summary>
        /// <param name="fileName">A string representing the name of a palette file to open.</param>
        public void Load(string fileName)
        {
            FileName = fileName;
            var format = CheckExtensionSupport(FileName);
            if (format != PaletteFileExtensions.unknown)
            {
                using (Stream paletteStream = new FileStream(FileName, FileMode.Open))
                {
                    Load(paletteStream, format);
                }
            }
        }

        /// <summary>
        /// Load a palette from a stream.
        /// </summary>
        /// <param name="stream">The file stream of the opened file.</param>
        /// <param name="format">The extension format of the file opened.</param>
        public void Load(Stream stream, PaletteFileExtensions format)
        {
            if (stream == Stream.Null)
            {
                return;
            }

            // If we have a stream, the file should have successfully opened. Clear the colors list.
            Colors = new List<RGBA>();

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
                    var header = string.Empty;
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
        /// Add a color to the end of the palette.
        /// </summary>
        /// <param name="item">The color to add to the palette.</param>
        public void Add(RGBA item)
            => Colors.Add(item);

        /// <summary>
        /// Add a list of palette color entries to the end of the palette.
        /// </summary>
        /// <param name="items">The colors to add to the palette.</param>
        public void AddRange(IEnumerable<RGBA> items)
            => Colors.AddRange(items);

        /// <summary>
        /// Adds a palette entry at a specified index.
        /// </summary>
        /// <param name="index">Index to insert the palette entry color.</param>
        /// <param name="item">Palette entry color to add to the list.</param>
        public void Insert(int index, RGBA item)
            => Colors.Insert(index, item);

        /// <summary>
        /// Adds a list of palette entries at a specified index.
        /// </summary>
        /// <param name="index">Index to insert the palette entry color.</param>
        /// <param name="item">List of palette entry colors to add to the list.</param>
        public void InsertRange(int index, IEnumerable<RGBA> item)
            => Colors.InsertRange(index, item);

        /// <summary>
        /// Clears all palette entries from the list of colors.
        /// </summary>
        public void Clear()
            => Colors.Clear();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        public IEnumerator GetEnumerator()
            => (IEnumerator)Colors;

        /// <summary>
        /// Remove the first instance of a specified color entry in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was removed.</returns>
        public bool RemoveFirstInstance(RGBA item)
            => Colors.Remove(Colors[Colors.IndexOf(item)]);

        /// <summary>
        /// Remove the last instance of a specified color entry in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was removed.</returns>
        public bool RemoveLastInstance(RGBA item)
            => Colors.Remove(Colors[Colors.LastIndexOf(item)]);

        /// <summary>
        /// Determines whether the specified palette item is in the palette list of colors.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>A value indicating whether the color was found in the list.</returns>
        public bool Contains(RGBA item)
            => Colors.Contains(item);

        /// <summary>
        /// Searches for a the specific color in the palette and returns its index if found.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>The first index of the color in the palette.</returns>
        public int IndexOf(RGBA item)
            => Colors.IndexOf(item);

        /// <summary>
        /// Returns the last index of a given color in the palette.
        /// </summary>
        /// <param name="item">The color to look for.</param>
        /// <returns>Returns the index of the last instance of the given color.</returns>
        public int LastIndexOf(RGBA item)
            => Colors.LastIndexOf(item);

        /// <summary>
        /// Reverses the order of the colors in the colors in the palette.
        /// </summary>
        public void Reverse()
            => Colors.Reverse();

        /// <summary>
        /// Sorts the colors in the palette by the default sorting order.
        /// </summary>
        public void Sort()
            => Colors.Sort();

        /// <summary>
        /// Read the auto desk palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadAutoDeskPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.AutoDesk;
            var startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            //{
            //}
        }

        /// <summary>
        /// Read the adobe palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadAdobePalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Adobe;
            var startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            //{
            //}
        }

        /// <summary>
        /// Read the Corel palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <remarks>
        /// http://www.selapa.net/swatches/colors/fileformats.php
        /// </remarks>
        private void ReadCorelPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Corel;
            var startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            {
                var mimeVersion = Maths.NetworkToHostOrder(binaryReader.ReadUInt16());
                ushort colorCount = 0;
                var name = string.Empty;
                if (mimeVersion == 0xccbc)
                {
                     // Version 5-8
                }
                else if (mimeVersion == 0xdddc) // Version 9-X3
                {
                    var headerBlocks = binaryReader.ReadInt32();
                    var test1 = binaryReader.ReadInt32();
                    var offset = binaryReader.ReadInt32();
                    var offset1 = binaryReader.ReadInt32();
                    var offset2 = binaryReader.ReadInt32();
                    var offset3 = binaryReader.ReadInt32();
                    var offset4 = binaryReader.ReadInt32();
                    var stringLen = binaryReader.ReadByte();
                    name = binaryReader.ReadString16(stringLen * 2);
                    var paletteType = binaryReader.ReadInt16();
                    var entryCount = binaryReader.ReadInt16();
                    // 4 byte BGR-A String
                    while (stream.Position < stream.Length)
                    {
                        var colorId = binaryReader.ReadInt32();
                        var colorModel = binaryReader.ReadInt16();
                        var colorType = binaryReader.ReadInt16();
                        var blue = binaryReader.ReadByte();
                        var green = binaryReader.ReadByte();
                        var red = binaryReader.ReadByte();
                        var alpha = binaryReader.ReadByte();
                        Colors.Add(new RGBA(red, green, blue, (byte)(255 - alpha)));
                        var stringLen2 = binaryReader.ReadByte();
                        var colorName = binaryReader.ReadString16(stringLen2 * 2);
                        if (Colors.Count == entryCount)
                        {
                            break;
                        }
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
                    var namelength = binaryReader.ReadByte();
                    name = binaryReader.ReadString(namelength);
                    colorCount = binaryReader.ReadUInt16();
                    var colorModel = (CorelColorModel)binaryReader.ReadUInt16();
                    var colorType = binaryReader.ReadUInt16();
                    var streamEnd = stream.Position + colorCount;
                    // Three byte RGB 
                    while (stream.Position < stream.Length)
                    {
                        var red = binaryReader.ReadByte();
                        var green = binaryReader.ReadByte();
                        var blue = binaryReader.ReadByte();
                        //byte alpha = binaryReader.ReadByte();
                        //byte beta = binaryReader.ReadByte();
                        //byte gama = binaryReader.ReadByte();
                        //byte delta = binaryReader.ReadByte();
                        //byte epsilon = binaryReader.ReadByte();
                        //byte colorNameLength = binaryReader.ReadByte();
                        //string colorName = binaryReader.ReadString(colorNameLength);
                        Colors.Add(new RGBA(red, green, blue));
                        //this.colors.Add(Color.FromArgb(255 - alpha, red, green, blue));
                    }
                }
            }
        }

        /// <summary>
        /// Read the pal palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadPalPalette(Stream stream)
        {
            var startPossition = stream.Position;
            var headder = stream.ReadString(4);

            if (headder.StartsWith("RIFF", StringComparison.OrdinalIgnoreCase))
            {
                stream.Position = startPossition;
                ReadRiffPalette(stream);
            }
            else if (headder.StartsWith("JASC", StringComparison.OrdinalIgnoreCase))
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
        /// Read the riff palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadRiffPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.RiffPal;
            var startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            {
                // RIFF header
                var riff = binaryReader.ReadString(4); // "RIFF"
                var dataSize = binaryReader.ReadInt32();
                var type = binaryReader.ReadString(4); // "PAL "

                // Data chunk
                var chunkType = binaryReader.ReadString(4); // "data"
                var chunkSize = binaryReader.ReadInt32();
                var palVersion = binaryReader.ReadInt16(); // always 0x0300
                var palEntries = binaryReader.ReadInt16();

                // Colors
                for (var i = 0; i < palEntries; i++)
                {
                    var red = binaryReader.ReadByte();
                    var green = binaryReader.ReadByte();
                    var blue = binaryReader.ReadByte();
                    var alpha = binaryReader.ReadByte();
                    Colors.Add(new RGBA(red, green, blue, (byte)(255 - alpha)));
                    //this.colors.Add(Color.FromArgb(br.ReadInt32()));
                }
            }
        }

        /// <summary>
        /// Read the binary palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadBinaryPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Binary;
            var startPossition = stream.Position;

            using (var binaryReader = new BinaryReader(stream))
            //using (StreamReader streamReader = new StreamReader(stream))
            {
                var Length = (binaryReader.BaseStream.Length / 4) - 1;
                for (var Index = 0; Index <= Length; Index++)
                {
                    var blue = binaryReader.ReadByte();
                    var green = binaryReader.ReadByte();
                    var red = binaryReader.ReadByte();
                    var alpha = binaryReader.ReadByte();
                    Colors.Add(new RGBA(red, green, blue, (byte)(255 - alpha)));
                }
            }
        }

        /// <summary>
        /// Read the Jasc palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadJascPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.JascPal0100;
            var startPossition = stream.Position;

            //using (BinaryReader binaryReader = new BinaryReader(stream))
            using (var streamReader = new StreamReader(stream))
            using (var StrReader = new StringReader(streamReader.ReadToEnd()))
            {
                var head = StrReader.ReadLine();
                var version = StrReader.ReadLine();
                var len = StrReader.ReadLine();
                var length = double.Parse(len) - 1;
                for (var Index = 0; Index <= length; Index++)
                {
                    var ReadStr = StrReader.ReadLine().Split(new char[] { ' ' });
                    if (ReadStr.Length == 3)
                    {
                        Colors.Add(new RGBA(byte.Parse(ReadStr[0]), byte.Parse(ReadStr[1]), byte.Parse(ReadStr[2])));
                    }
                    else if (ReadStr.Length == 4)
                    {
                        Colors.Add(new RGBA(byte.Parse(ReadStr[0]), byte.Parse(ReadStr[1]), byte.Parse(ReadStr[2]), byte.Parse(ReadStr[3])));
                    }
                }
            }
        }

        /// <summary>
        /// Read the text palette.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private void ReadTextPalette(Stream stream)
        {
            PaletteMimeFormat = PaletteMimeFormats.Text;
            var startPossition = stream.Position;

            using (var streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith(";", StringComparison.OrdinalIgnoreCase))
                    {
                        PaletteMimeFormat = PaletteMimeFormats.PaintDotNet;
                    }
                    else if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] argb;
                        var color = new RGBA();
                        if (line.Contains(" ") || line.Contains(","))
                        {
                            if (PaletteMimeFormat == PaletteMimeFormats.Text && line.Contains(" "))
                            {
                                PaletteMimeFormat = PaletteMimeFormats.SpaceDelimiated;
                            }
                            else if (PaletteMimeFormat == PaletteMimeFormats.Text && line.Contains(","))
                            {
                                PaletteMimeFormat = PaletteMimeFormats.ComaDelimiated;
                            }

                            argb = line.Split(new char[] { ' ', ',' });

                            if (argb.Length == 3)
                            {
                                color = new RGBA(byte.Parse(argb[0]), byte.Parse(argb[1]), byte.Parse(argb[2]));
                            }
                            else if (argb.Length == 4)
                            {
                                color = new RGBA(byte.Parse(argb[0]), byte.Parse(argb[1]), byte.Parse(argb[2]), byte.Parse(argb[3]));
                            }
                        }
                        else
                        {
                            color = new RGBA(int.Parse(line, System.Globalization.NumberStyles.HexNumber));
                        }

                        color = LookupNamedColor(color);
                        Colors.Add(color);
                    }
                }
            }
        }

        /// <summary>
        /// Write the riff palette.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void WriteRiffPalette(string filename)
        {
            // Calculate file length
            var length = 4 + 4 + 4 + 4 + 2 + 2 + (Colors.Count * 4);

            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            using (var bw = new BinaryWriter(stream))
            {
                // RIFF header
                bw.WriteString("RIFF");
                bw.Write(length);
                bw.WriteString("PAL ");

                // Data chunk
                bw.WriteString("data");
                bw.Write((Colors.Count * 4) + 4);
                bw.Write((short)0x0300); // PAL version
                bw.Write((short)Colors.Count);

                // Colors
                foreach (RGBA color in Colors)
                {
                    bw.Write(color.Red);
                    bw.Write(color.Green);
                    bw.Write(color.Blue);
                    bw.Write((byte)(255 - color.Alpha));
                }
            }
        }

        /// <summary>
        /// Write the jasc palette.
        /// </summary>
        /// <param name="filename">The filename.</param>
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
                foreach (RGBA color in Colors)
                {
                    bw.WriteLine(color.Red + " " + color.Green + " " + color.Blue + " " + color.Alpha);
                }
            }
        }

        /// <summary>
        /// Write the paint dot net palette.
        /// </summary>
        /// <param name="filename">The filename.</param>
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
                foreach (RGBA color in Colors)
                {
                    bw.WriteLine("{0:X2}{1:X2}{2:X2}{3:X2}", color.Alpha, color.Red, color.Green, color.Blue);
                }
            }
        }

        /// <summary>
        /// Check the extension support.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>The <see cref="PaletteFileExtensions"/>.</returns>
        private static PaletteFileExtensions CheckExtensionSupport(string filepath)
        {
            var format = PaletteFileExtensions.unknown;
            var extension = Path.GetExtension(filepath).Substring(1);
            Enum.TryParse(extension, true, out format);
            return format;
        }

        /// <summary>
        /// The lookup named color.
        /// </summary>
        /// <param name="testColor">The testColor.</param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        private static RGBA LookupNamedColor(RGBA testColor)
        {
            //var known = testColor;
            //if (Colorspace.Colors.Color.ContainsKey(testColor)) return Colorspace.Colors.Color.Keys.FirstOrDefault();
            //    known = Colorspace.Colors.Color[testColor];

            //var known = (
            //            from prop in typeof(ARGB)
            //                .GetRuntimeProperties(BindingFlags.Public | BindingFlags.Static)
            //            where prop.PropertyType == typeof(ARGB)
            //            let color = (ARGB)prop.GetValue(null, null)
            //            where color.Alpha == testColor.Alpha && color.Red == testColor.Red
            //                && color.Green == testColor.Green && color.Blue == testColor.Blue
            //            select color)
            //            .FirstOrDefault();

            var known = (
                        from key in Colorspace.Colors.Color.Keys
                        where key.Alpha == testColor.Alpha && key.Red == testColor.Red
                            && key.Green == testColor.Green && key.Blue == testColor.Blue
                        select key)
                        .FirstOrDefault();
            if (known != default)
            {
                return known;
            }

            return testColor;
        }
    }
}
