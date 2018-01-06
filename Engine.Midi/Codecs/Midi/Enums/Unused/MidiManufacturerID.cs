// <copyright file="MidiManufacturerID.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://www.midi.org/specifications/item/manufacturer-id-numbers
// </references>

namespace Engine.File
{
    /// <summary>
    /// Midi Manufacture ID.
    /// </summary>
    public enum MidiManufacturerID
        : byte
    {
        /// <summary>
        /// Manufacture Unknown.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Manufacture Sequential Circuits.
        /// </summary>
        SequentialCircuits = 0x01,

        /// <summary>
        /// Manufacture Big Briar.
        /// </summary>
        BigBriar = 0x02,

        /// <summary>
        /// Manufacture Octave Plateau.
        /// </summary>
        OctavePlateau = 0x03,

        /// <summary>
        /// Manufacture Moog.
        /// </summary>
        Moog = 0x04,

        /// <summary>
        /// Manufacture Passport Designs.
        /// </summary>
        PassportDesigns = 0x05,

        /// <summary>
        /// Manufacture Lexicon.
        /// </summary>
        Lexicon = 0x06,

        /// <summary>
        /// Manufacture Kurzweil.
        /// </summary>
        Kurzweil = 0x07,

        /// <summary>
        /// Manufacture Fender.
        /// </summary>
        Fender = 0x08,

        /// <summary>
        /// Manufacture Gulbransen.
        /// </summary>
        Gulbransen = 0x09,

        /// <summary>
        /// Manufacture Delta Labs.
        /// </summary>
        DeltaLabs = 0x0A,

        /// <summary>
        /// Manufacture Sound Comp.
        /// </summary>
        SoundComp = 0x0B,

        /// <summary>
        /// Manufacture General Electro.
        /// </summary>
        GeneralElectro = 0x0C,

        /// <summary>
        /// Manufacture Techmar.
        /// </summary>
        Techmar = 0x0D,

        /// <summary>
        /// Manufacture Matthews Research.
        /// </summary>
        MatthewsResearch = 0x0E,

        /// <summary>
        /// Manufacture Oberheim.
        /// </summary>
        Oberheim = 0x10,

        /// <summary>
        /// Manufacture PAIA.
        /// </summary>
        PAIA = 0x11,

        /// <summary>
        /// Manufacture Simmons.
        /// </summary>
        Simmons = 0x12,

        /// <summary>
        /// Manufacture Digi Design.
        /// </summary>
        DigiDesign = 0x13,

        /// <summary>
        /// Manufacture Fairlight.
        /// </summary>
        Fairlight = 0x14,

        /// <summary>
        /// Manufacture JL Cooper.
        /// </summary>
        JLCooper = 0x15,

        /// <summary>
        /// Manufacture Lowery.
        /// </summary>
        Lowery = 0x16,

        /// <summary>
        /// Manufacture Lin.
        /// </summary>
        Lin = 0x17,

        /// <summary>
        /// Manufacture Emu.
        /// </summary>
        Emu = 0x18,

        /// <summary>
        /// Manufacture Peavey.
        /// </summary>
        Peavey = 0x1B,

        /// <summary>
        /// Manufacture Bon Tempi.
        /// </summary>
        BonTempi = 0x20,

        /// <summary>
        /// Manufacture SIEL.
        /// </summary>
        SIEL = 0x21,

        /// <summary>
        /// Manufacture Synthe Axe.
        /// </summary>
        SyntheAxe = 0x23,

        /// <summary>
        /// Manufacture Hohner.
        /// </summary>
        Hohner = 0x24,

        /// <summary>
        /// Manufacture Crumar.
        /// </summary>
        Crumar = 0x25,

        /// <summary>
        /// Manufacture Solton.
        /// </summary>
        Solton = 0x26,

        /// <summary>
        /// Manufacture Jellinghaus Ms.
        /// </summary>
        JellinghausMs = 0x27,

        /// <summary>
        /// Manufacture CTS.
        /// </summary>
        CTS = 0x28,

        /// <summary>
        /// Manufacture PPG.
        /// </summary>
        PPG = 0x29,

        /// <summary>
        /// Manufacture Elka.
        /// </summary>
        Elka = 0x2F,

        /// <summary>
        /// Manufacture Cheetah.
        /// </summary>
        Cheetah = 0x36,

        /// <summary>
        /// Manufacture Waldorf.
        /// </summary>
        Waldorf = 0x3E,

        /// <summary>
        /// Manufacture Kawai.
        /// </summary>
        Kawai = 0x40,

        /// <summary>
        /// Manufacture Roland.
        /// </summary>
        Roland = 0x41,

        /// <summary>
        /// Manufacture Korg.
        /// </summary>
        Korg = 0x42,

        /// <summary>
        /// Manufacture Yamaha.
        /// </summary>
        Yamaha = 0x43,

        /// <summary>
        /// Manufacture Casio.
        /// </summary>
        Casio = 0x44,

        /// <summary>
        /// Manufacture Kamiya Studio.
        /// </summary>
        KamiyaStudio = 0x46,

        /// <summary>
        /// Manufacture Akai.
        /// </summary>
        Akai = 0x47,

        /// <summary>
        /// Manufacture Victor.
        /// </summary>
        Victor = 0x48,

        /// <summary>
        /// Manufacture Fujitsu.
        /// </summary>
        Fujitsu = 0x4B,

        /// <summary>
        /// Manufacture Sony.
        /// </summary>
        Sony = 0x4C,

        /// <summary>
        /// Manufacture Teac.
        /// </summary>
        Teac = 0x4E,

        /// <summary>
        /// Manufacture Matsushita.
        /// </summary>
        Matsushita = 0x50,

        /// <summary>
        /// Manufacture Fostex.
        /// </summary>
        Fostex = 0x51,

        /// <summary>
        /// Manufacture Zoom.
        /// </summary>
        Zoom = 0x52,

        /// <summary>
        /// Manufacture Matsushita (Might be a mistake).
        /// </summary>
        Matsushita_ = 0x54,

        /// <summary>
        /// Manufacture Suzuki.
        /// </summary>
        Suzuki = 0x55,

        /// <summary>
        /// Manufacture Fuji Sound.
        /// </summary>
        FujiSound = 0x56,

        /// <summary>
        /// Manufacture Acoustic Technical Laboratory.
        /// </summary>
        AcousticTechnicalLaboratory = 0x57,

        /// <summary>
        /// Manufacture Educational Use.
        /// </summary>
        EducationalUse = 0x7D
    }
}
