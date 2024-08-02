// <copyright file="Palette.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.ColorSpace;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static System.Buffers.Binary.BinaryPrimitives;

namespace Engine.File.Palettes;

/// <summary>
/// The <see cref="Palette" /> class used for loading, saving and storing color palette entries.
/// </summary>
/// <seealso cref="IEnumerable" />
/// <seealso cref="IEquatable{T}" />
[FileObject]
public class Palette
    : IEnumerable, IEnumerable<IColor>, IEquatable<Palette>
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
        Colors = [];
        PaletteMimeFormat = PaletteMimeFormat.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Palette" /> class.
    /// </summary>
    /// <param name="colors">An array of colors to add to the palette.</param>
    public Palette(params IColor[] colors)
    {
        Colors = [];
        AddRange(colors);
        PaletteMimeFormat = PaletteMimeFormat.Default;
    }

    /// <summary>
    /// Gets or sets the list of colors in the palette.
    /// </summary>
    /// <value>
    /// The colors.
    /// </value>
    public List<IColor> Colors { get; set; }

    /// <summary>
    /// Gets or sets the name of the palette file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the palette's MIME format.
    /// </summary>
    /// <value>
    /// The palette MIME format.
    /// </value>
    public PaletteMimeFormat PaletteMimeFormat { get; set; }

    /// <summary>
    /// Gets the number of colors in the palette.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public int Count => Colors.Count;

    /// <summary>
    /// Gets the palette color entry at a specific index.
    /// </summary>
    /// <value>
    /// The <see cref="RGBA" />.
    /// </value>
    /// <param name="index"><see cref="int" /> index of a <see cref="Palette" /> entry <see cref="RGBA" />.</param>
    /// <returns>
    /// A value representing the <see cref="RGBA" /> at the specified index in the <see cref="Palette" />.
    /// </returns>
    public IColor this[int index] => Colors[index];

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Palette left, Palette right) => EqualityComparer<Palette>.Default.Equals(left, right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Palette left, Palette right) => !(left == right);

    /// <summary>
    /// Load a palette file from a file path.
    /// </summary>
    /// <param name="fileName">A string representing the name of a palette file to open.</param>
    public void Load(string fileName)
    {
        FileName = fileName;
        var format = CheckExtensionSupport(FileName);
        if (format != PaletteFileExtension.unknown)
        {
            using Stream paletteStream = new FileStream(FileName, FileMode.Open);
            Load(paletteStream, format);
        }
    }

    /// <summary>
    /// Load a palette from a stream.
    /// </summary>
    /// <param name="stream">The file stream of the opened file.</param>
    /// <param name="format">The extension format of the file opened.</param>
    public void Load(Stream stream, PaletteFileExtension format)
    {
        if (stream is null || stream == Stream.Null)
        {
            return;
        }

        // If we have a stream, the file should have successfully opened. Clear the colors list.
        Colors = [];

        switch (format)
        {
            case PaletteFileExtension.acb:
                ReadAutoDeskPalette(stream);
                break;
            case PaletteFileExtension.aco:
            case PaletteFileExtension.act:
                ReadAdobePalette(stream);
                break;
            case PaletteFileExtension.cpl:
                ReadCorelPalette(stream);
                break;
            case PaletteFileExtension.txt:
                ReadTextPalette(stream);
                break;
            case PaletteFileExtension.pal:
                ReadPalPalette(stream);
                break;
            case PaletteFileExtension.unknown:
                break;
            default:
                //header = this.ReadString(stream, 4);
                var header = stream.ReadString(4);
                _ = header;
                break;
        }
    }

    /// <summary>
    /// Save a palette to a file.
    /// </summary>
    /// <param name="fileName">The Filename to save the file as.</param>
    /// <param name="format">The MIME format of the file to save.</param>
    public void Save(string fileName, PaletteMimeFormat format)
    {
        // ToDo: Add Save functionality.
        switch (format)
        {
            case PaletteMimeFormat.Adobe:
            case PaletteMimeFormat.AutoDesk:
            case PaletteMimeFormat.Corel:
                //throw new NotImplementedException();
                break;
            case PaletteMimeFormat.JascPal0100:
                WriteJascPalette(fileName);
                break;
            case PaletteMimeFormat.Text:
            case PaletteMimeFormat.ComaDelimiated:
            case PaletteMimeFormat.SpaceDelimiated:
                //throw new NotImplementedException();
                break;
            case PaletteMimeFormat.PaintDotNet:
                WritePaintDotNetPalette(fileName);
                break;
            case PaletteMimeFormat.RiffPal:
                WriteRiffPalette(fileName);
                break;
            case PaletteMimeFormat.Binary:
            case PaletteMimeFormat.Win31Pal:
            //throw new NotImplementedException();
            case PaletteMimeFormat.Default:
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
    public void Add(IColor item) => Colors.Add(item);

    /// <summary>
    /// Add a list of palette color entries to the end of the palette.
    /// </summary>
    /// <param name="items">The colors to add to the palette.</param>
    public void AddRange(IEnumerable<IColor> items) => Colors.AddRange(items);

    /// <summary>
    /// Adds a palette entry at a specified index.
    /// </summary>
    /// <param name="index">Index to insert the palette entry color.</param>
    /// <param name="item">Palette entry color to add to the list.</param>
    public void Insert(int index, IColor item) => Colors.Insert(index, item);

    /// <summary>
    /// Adds a list of palette entries at a specified index.
    /// </summary>
    /// <param name="index">Index to insert the palette entry color.</param>
    /// <param name="item">List of palette entry colors to add to the list.</param>
    public void InsertRange(int index, IEnumerable<IColor> item) => Colors.InsertRange(index, item);

    /// <summary>
    /// Clears all palette entries from the list of colors.
    /// </summary>
    public void Clear() => Colors.Clear();

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator" />.
    /// </returns>
    public IEnumerator GetEnumerator() => (IEnumerator)Colors;

    /// <summary>
    /// Remove the first instance of a specified color entry in the palette.
    /// </summary>
    /// <param name="item">The color to look for.</param>
    /// <returns>
    /// A value indicating whether the color was removed.
    /// </returns>
    public bool RemoveFirstInstance(IColor item) => Colors.Remove(Colors[Colors.IndexOf(item)]);

    /// <summary>
    /// Remove the last instance of a specified color entry in the palette.
    /// </summary>
    /// <param name="item">The color to look for.</param>
    /// <returns>
    /// A value indicating whether the color was removed.
    /// </returns>
    public bool RemoveLastInstance(IColor item) => Colors.Remove(Colors[Colors.LastIndexOf(item)]);

    /// <summary>
    /// Determines whether the specified palette item is in the palette list of colors.
    /// </summary>
    /// <param name="item">The color to look for.</param>
    /// <returns>
    /// A value indicating whether the color was found in the list.
    /// </returns>
    public bool Contains(IColor item) => Colors.Contains(item);

    /// <summary>
    /// Searches for a the specific color in the palette and returns its index if found.
    /// </summary>
    /// <param name="item">The color to look for.</param>
    /// <returns>
    /// The first index of the color in the palette.
    /// </returns>
    public int IndexOf(IColor item) => Colors.IndexOf(item);

    /// <summary>
    /// Returns the last index of a given color in the palette.
    /// </summary>
    /// <param name="item">The color to look for.</param>
    /// <returns>
    /// Returns the index of the last instance of the given color.
    /// </returns>
    public int LastIndexOf(IColor item) => Colors.LastIndexOf(item);

    /// <summary>
    /// Reverses the order of the colors in the colors in the palette.
    /// </summary>
    public void Reverse() => Colors.Reverse();

    /// <summary>
    /// Sorts the colors in the palette by the default sorting order.
    /// </summary>
    public void Sort() => Colors.Sort();

    /// <summary>
    /// Read the auto desk palette.
    /// </summary>
    /// <param name="stream">The stream.</param>
    private void ReadAutoDeskPalette(Stream stream)
    {
        _ = stream;
        PaletteMimeFormat = PaletteMimeFormat.AutoDesk;
        //var startPossition = stream.Position;

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
        _ = stream;
        PaletteMimeFormat = PaletteMimeFormat.Adobe;
        //var startPossition = stream.Position;

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
    /// <para>http://www.selapa.net/swatches/colors/fileformats.php</para>
    /// </remarks>
    private void ReadCorelPalette(Stream stream)
    {
        PaletteMimeFormat = PaletteMimeFormat.Corel;
        //var startPossition = stream.Position;

        using var binaryReader = new BinaryReader(stream);
        var mimeVersion = ReverseEndianness(binaryReader.ReadUInt16());
        string name;
        if (mimeVersion == 0xccbc)
        {
            // Version 5-8
        }
        else if (mimeVersion == 0xdddc) // Version 9-X3
        {
            var headerBlocks = binaryReader.ReadInt32();
            _ = headerBlocks;
            var test1 = binaryReader.ReadInt32();
            _ = test1;
            var offset = binaryReader.ReadInt32();
            _ = offset;
            var offset1 = binaryReader.ReadInt32();
            _ = offset1;
            var offset2 = binaryReader.ReadInt32();
            _ = offset2;
            var offset3 = binaryReader.ReadInt32();
            _ = offset3;
            var offset4 = binaryReader.ReadInt32();
            _ = offset4;
            var stringLen = binaryReader.ReadByte();
            name = binaryReader.ReadString16(stringLen * 2);
            _ = name;
            var paletteType = binaryReader.ReadInt16();
            _ = paletteType;
            var entryCount = binaryReader.ReadInt16();
            // 4 byte BGR-A String
            while (stream.Position < stream.Length)
            {
                var colorId = binaryReader.ReadInt32();
                _ = colorId;
                var colorModel = binaryReader.ReadInt16();
                _ = colorModel;
                var colorType = binaryReader.ReadInt16();
                _ = colorType;
                var blue = binaryReader.ReadByte();
                var green = binaryReader.ReadByte();
                var red = binaryReader.ReadByte();
                var alpha = binaryReader.ReadByte();
                Colors.Add(new RGBA(red, green, blue, (byte)(255 - alpha)));
                var stringLen2 = binaryReader.ReadByte();
                var colorName = binaryReader.ReadString16(stringLen2 * 2);
                _ = colorName;
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
            _ = name;
            var colorCount = binaryReader.ReadUInt16();
            var colorModel = (CorelColorModel)binaryReader.ReadUInt16();
            _ = colorModel;
            var colorType = binaryReader.ReadUInt16();
            _ = colorType;
            var streamEnd = stream.Position + colorCount;
            _ = streamEnd;
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
        PaletteMimeFormat = PaletteMimeFormat.RiffPal;
        //var startPossition = stream.Position;

        using var binaryReader = new BinaryReader(stream);
        // RIFF header
        var riff = binaryReader.ReadString(4); // "RIFF"
        _ = riff;
        var dataSize = binaryReader.ReadInt32();
        _ = dataSize;
        var type = binaryReader.ReadString(4); // "PAL "
        _ = type;

        // Data chunk
        var chunkType = binaryReader.ReadString(4); // "data"
        _ = chunkType;
        var chunkSize = binaryReader.ReadInt32();
        _ = chunkSize;
        var palVersion = binaryReader.ReadInt16(); // always 0x0300
        _ = palVersion;
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

    /// <summary>
    /// Read the binary palette.
    /// </summary>
    /// <param name="stream">The stream.</param>
    private void ReadBinaryPalette(Stream stream)
    {
        PaletteMimeFormat = PaletteMimeFormat.Binary;
        //var startPossition = stream.Position;

        using var binaryReader = new BinaryReader(stream);
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

    /// <summary>
    /// Read the Jasc palette.
    /// </summary>
    /// <param name="stream">The stream.</param>
    private void ReadJascPalette(Stream stream)
    {
        PaletteMimeFormat = PaletteMimeFormat.JascPal0100;
        //var startPossition = stream.Position;

        //using (BinaryReader binaryReader = new BinaryReader(stream))
        using var streamReader = new StreamReader(stream);
        using var StrReader = new StringReader(streamReader.ReadToEnd());
        var head = StrReader.ReadLine();
        _ = head;
        var version = StrReader.ReadLine();
        _ = version;
        var len = StrReader.ReadLine();
        var length = double.Parse(len, NumberStyles.Integer, CultureInfo.InvariantCulture) - 1;
        for (var Index = 0; Index <= length; Index++)
        {
            var ReadStr = StrReader.ReadLine().Split(new char[] { ' ' });
            if (ReadStr.Length == 3)
            {
                Colors.Add(new RGBA(byte.Parse(ReadStr[0], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(ReadStr[1], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(ReadStr[2], NumberStyles.Integer, CultureInfo.InvariantCulture)));
            }
            else if (ReadStr.Length == 4)
            {
                Colors.Add(new RGBA(byte.Parse(ReadStr[0], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(ReadStr[1], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(ReadStr[2], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(ReadStr[3], NumberStyles.Integer, CultureInfo.InvariantCulture)));
            }
        }
    }

    /// <summary>
    /// Read the text palette.
    /// </summary>
    /// <param name="stream">The stream.</param>
    private void ReadTextPalette(Stream stream)
    {
        PaletteMimeFormat = PaletteMimeFormat.Text;
        //var startPossition = stream.Position;

        using var streamReader = new StreamReader(stream);
        string line;
        while ((line = streamReader.ReadLine()) is not null)
        {
            line = line.Trim();
            if (line.StartsWith(";", StringComparison.OrdinalIgnoreCase))
            {
                PaletteMimeFormat = PaletteMimeFormat.PaintDotNet;
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                string[] argb;
                var color = new RGBA();
                if (line.Contains(" ", StringComparison.OrdinalIgnoreCase) || line.Contains(",", StringComparison.OrdinalIgnoreCase))
                {
                    if (PaletteMimeFormat == PaletteMimeFormat.Text && line.Contains(" ", StringComparison.OrdinalIgnoreCase))
                    {
                        PaletteMimeFormat = PaletteMimeFormat.SpaceDelimiated;
                    }
                    else if (PaletteMimeFormat == PaletteMimeFormat.Text && line.Contains(",", StringComparison.OrdinalIgnoreCase))
                    {
                        PaletteMimeFormat = PaletteMimeFormat.ComaDelimiated;
                    }

                    argb = line.Split(new char[] { ' ', ',' });

                    if (argb.Length == 3)
                    {
                        color = new RGBA(byte.Parse(argb[0], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(argb[1], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(argb[2], NumberStyles.Integer, CultureInfo.InvariantCulture));
                    }
                    else if (argb.Length == 4)
                    {
                        color = new RGBA(byte.Parse(argb[0], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(argb[1], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(argb[2], NumberStyles.Integer, CultureInfo.InvariantCulture), byte.Parse(argb[3], NumberStyles.Integer, CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    color = new RGBA(int.Parse(line, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                }

                color = LookupNamedColor(color);
                Colors.Add(color);
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
        using var bw = new BinaryWriter(stream);
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
        foreach (var color in Colors)
        {
            var (Red, Green, Blue, Alpha) = color.ToRGBATuple();
            bw.Write(Red);
            bw.Write(Green);
            bw.Write(Blue);
            bw.Write((byte)(255 - Alpha));
        }
    }

    /// <summary>
    /// Write the jasc palette.
    /// </summary>
    /// <param name="filename">The filename.</param>
    private void WriteJascPalette(string filename)
    {
        var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        using var bw = new StreamWriter(stream);
        // RIFF header
        bw.WriteLine("JASC-PAL");

        // Version line
        bw.WriteLine("0100");

        // length
        bw.WriteLine(Colors.Count);

        // Colors
        foreach (var color in Colors)
        {
            var (Red, Green, Blue, Alpha) = color.ToRGBATuple();
            bw.WriteLine($"{Red} {Green} {Blue} {Alpha}");
        }
    }

    /// <summary>
    /// Write the paint dot net palette.
    /// </summary>
    /// <param name="filename">The filename.</param>
    private void WritePaintDotNetPalette(string filename)
    {
        var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        using var bw = new StreamWriter(stream);
        // Header
        bw.WriteLine("; paint.net Palette File");
        bw.WriteLine("; Lines that start with a semicolon are comments");
        bw.WriteLine("; Colors are written as 8-digit hexadecimal numbers: aarrggbb");
        bw.WriteLine("; For example, this would specify green: FF00FF00");
        bw.WriteLine("; The alpha ('aa') value specifies how transparent a color is. FF is fully opaque, 00 is fully transparent.");
        bw.WriteLine("; A palette must consist of ninety six (96) colors. If there are less than this, the remaining color");
        bw.WriteLine("; slots will be set to white (FFFFFFFF). If there are more, then the remaining colors will be ignored.");

        // Colors
        foreach (var color in Colors)
        {
            var (Red, Green, Blue, Alpha) = color.ToRGBATuple();
            bw.WriteLine("{0:X2}{1:X2}{2:X2}{3:X2}", Alpha, Red, Green, Blue);
        }
    }

    /// <summary>
    /// Check the extension support.
    /// </summary>
    /// <param name="filepath">The filepath.</param>
    /// <returns>
    /// The <see cref="PaletteFileExtension" />.
    /// </returns>
    private static PaletteFileExtension CheckExtensionSupport(string filepath)
    {
        var extension = Path.GetExtension(filepath).Substring(1);
        Enum.TryParse(extension, true, out PaletteFileExtension format);
        return format;
    }

    /// <summary>
    /// The lookup named color.
    /// </summary>
    /// <param name="testColor">The testColor.</param>
    /// <returns>
    /// The <see cref="RGBA" />.
    /// </returns>
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
                    from key in ColorSpace.Colors.Color.Keys
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

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    IEnumerator<IColor> IEnumerable<IColor>.GetEnumerator() => Colors.GetEnumerator();

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => Equals(obj as Palette);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals([AllowNull] Palette other) => other is not null && EqualityComparer<List<IColor>>.Default.Equals(Colors, other.Colors);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode() => HashCode.Combine(Colors);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public string ToString(IFormatProvider formatProvider) => ToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public string ToString(string format, IFormatProvider formatProvider)
    {
        _ = format;
        _ = formatProvider;
        return Colors.ToString();
    }
}
