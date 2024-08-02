// <copyright file="LengthUnits.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine;

/// <summary>
/// The length units class.
/// </summary>
public static class LengthUnits<T>
    where T : INumber<T>
{
    /// <summary>
    /// The points per inch (static readonly). Value: 72d.
    /// </summary>
    public static readonly T PointsPerInch = T.CreateSaturating(72);

    /// <summary>
    /// The pixels per inch (static readonly). Value: 96.
    /// </summary>
    public static readonly T PixelsPerInch = T.CreateSaturating(96);

    /// <summary>
    /// The points per pixel (static readonly). Value: 72d / 96d.
    /// </summary>
    public static readonly T PointsPerPixel = PointsPerInch / PixelsPerInch;

    /// <summary>
    /// The Planck length in mega-parsecs (static readonly). Value: 1.90939E+57.
    /// </summary>
    public static readonly T PlanckLengthInMegaparsec = T.CreateSaturating(1.90939E+57);

    /// <summary>
    /// The number of mega-parsecs in a Planck length.
    /// </summary>
    public static readonly T MegaparsecsInPlanckLength = T.One / PlanckLengthInMegaparsec;

    #region Mil Conversions
    /// <summary>
    /// The number of mils in a mil.
    /// </summary>
    public static readonly T MilsInMil = T.One;

    /// <summary>
    /// The number of mils in a millimeter.
    /// </summary>
    public static readonly T MilsInMillimeter = MilsInCentimeter * MillimetersInCentimeter;

    /// <summary>
    /// The number of Mils in a Centimeter.
    /// </summary>
    public static readonly T MilsInCentimeter = T.CreateSaturating(393.700787);

    /// <summary>
    /// The number of Mils in an Inch.
    /// </summary>
    public static readonly T MilsInInch = T.CreateSaturating(1000);

    /// <summary>
    /// The number of Mils in a Foot.
    /// </summary>
    public static readonly T MilsInFoot = MilsInInch * InchesInFoot;

    /// <summary>
    /// The number of Mils in a Yard.
    /// </summary>
    public static readonly T MilsInYard = MilsInFoot * FeetInYard;

    /// <summary>
    /// The number of Mils in a Meter.
    /// </summary>
    public static readonly T MilsInMeter = MilsInCentimeter * CentimetersInMeter;

    /// <summary>
    /// The number of Mils in a Smoot.
    /// </summary>
    public static readonly T MilsInSmoot = MilsInInch * InchesInSmoot;

    /// <summary>
    /// The number of Mils in a Kilometer.
    /// </summary>
    public static readonly T MilsInKilometer = MilsInMeter * MetersInKilometer;

    /// <summary>
    /// The number of Mils in a Mile.
    /// </summary>
    public static readonly T MilsInMile = MilsInYard * YardsInMile;

    /// <summary>
    /// The number of Mils in a Nautical Mile.
    /// </summary>
    public static readonly T MilsInNauticalMile = T.CreateSaturating(7.2913E+7);
    #endregion Mil Conversions

    #region Millimeter Conversions
    /// <summary>
    /// The number of millimeters in a mil.
    /// </summary>
    public static readonly T MillimetersInMil = T.One / MilsInCentimeter * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a millimeter.
    /// </summary>
    public static readonly T MillimetersInMillimeter = T.One;

    /// <summary>
    /// The number of millimeters in a centimeter.
    /// </summary>
    public static readonly T MillimetersInCentimeter = T.CreateSaturating(100);

    /// <summary>
    /// The number of millimeters in an Inch.
    /// </summary>
    public static readonly T MillimetersInInch = T.CreateSaturating(2.54) * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Foot.
    /// </summary>
    public static readonly T MillimetersInFoot = CentimetersInInch * InchesInFoot * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Yard.
    /// </summary>
    public static readonly T MillimetersInYard = CentimetersInFoot * FeetInYard * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Meter.
    /// </summary>
    public static readonly T MillimetersInMeter = T.CreateSaturating(100) * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Smoot.
    /// </summary>
    public static readonly T MillimetersInSmoot = CentimetersInInch * InchesInSmoot * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Kilometer.
    /// </summary>
    public static readonly T MillimetersInKilometer = CentimetersInMeter * MetersInKilometer * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Mile.
    /// </summary>
    public static readonly T MillimetersInMile = CentimetersInYard * YardsInMile * MillimetersInCentimeter;

    /// <summary>
    /// The number of millimeters in a Nautical Mile.
    /// </summary>
    public static readonly T MillimetersInNauticalMile = CentimetersInMile * MilesInNauticalMile * MillimetersInCentimeter;
    #endregion Millimeter Conversions

    #region Pica Conversions
    /// <summary>
    /// The number of Picas in a mil.
    /// </summary>
    public static readonly T PicasInMil = T.CreateSaturating(0.006);

    /// <summary>
    /// The number of Picas in a millimeter.
    /// </summary>
    public static readonly T PicasInMillimeter = T.CreateSaturating(0.236220474);

    /// <summary>
    /// The number of Picas in a Pica.
    /// </summary>
    public static readonly T PicasInPica = T.One;

    /// <summary>
    /// The number of Picas in a centimeter.
    /// </summary>
    public static readonly T PicasInCentimeter = T.CreateSaturating(2.362204743);

    /// <summary>
    /// The number of Picas in an Inch.
    /// </summary>
    public static readonly T PicasInInch = T.CreateSaturating(6.000000047);

    /// <summary>
    /// The number of Picas in a Foot.
    /// </summary>
    public static readonly T PicasInFoot = T.CreateSaturating(72.00000057);

    /// <summary>
    /// The number of Picas in a Yard.
    /// </summary>
    public static readonly T PicasInYard = T.CreateSaturating(216.0000017);

    /// <summary>
    /// The number of Picas in a Meter.
    /// </summary>
    public static readonly T PicasInMeter = T.CreateSaturating(236.2204743);

    /// <summary>
    /// The number of Picas in a Smoot.
    /// </summary>
    public static readonly T PicasInSmoot = T.CreateSaturating(402.0000032);

    /// <summary>
    /// The number of Picas in a Kilometer.
    /// </summary>
    public static readonly T PicasInKilometer = T.CreateSaturating(236220.4743);

    /// <summary>
    /// The number of Picas in a Mile.
    /// </summary>
    public static readonly T PicasInMile = T.CreateSaturating(380160.003);

    /// <summary>
    /// The number of Picas in a Nautical Mile.
    /// </summary>
    public static readonly T PicasInNauticalMile = T.CreateSaturating(437480.3184);
    #endregion Pica Conversions

    #region Centimeter Conversions
    /// <summary>
    /// The number of centimeters in a mil.
    /// </summary>
    public static readonly T CentimetersInMil = T.One / MilsInCentimeter;

    /// <summary>
    /// The number of centimeters in a millimeter.
    /// </summary>
    public static readonly T CentimetersInMillimeter = T.One / MillimetersInCentimeter;

    /// <summary>
    /// The number of centimeters in a centimeter.
    /// </summary>
    public static readonly T CentimetersInCentimeter = T.One;

    /// <summary>
    /// The number of Centimeters in an Inch.
    /// </summary>
    public static readonly T CentimetersInInch = T.CreateSaturating(2.54);

    /// <summary>
    /// The number of Centimeters in a Foot.
    /// </summary>
    public static readonly T CentimetersInFoot = CentimetersInInch * InchesInFoot;

    /// <summary>
    /// The number of Centimeters in a Yard.
    /// </summary>
    public static readonly T CentimetersInYard = CentimetersInFoot * FeetInYard;

    /// <summary>
    /// The number of Centimeters in a Meter.
    /// </summary>
    public static readonly T CentimetersInMeter = T.CreateSaturating(100);

    /// <summary>
    /// The number of Centimeters in a Smoot.
    /// </summary>
    public static readonly T CentimetersInSmoot = CentimetersInInch * InchesInSmoot;

    /// <summary>
    /// The number of Centimeters in a Kilometer.
    /// </summary>
    public static readonly T CentimetersInKilometer = CentimetersInMeter * MetersInKilometer;

    /// <summary>
    /// The number of Centimeters in a Mile.
    /// </summary>
    public static readonly T CentimetersInMile = CentimetersInYard * YardsInMile;

    /// <summary>
    /// The number of Centimeters in a Nautical Mile.
    /// </summary>
    public static readonly T CentimetersInNauticalMile = CentimetersInMile * MilesInNauticalMile;
    #endregion Centimeter Conversions

    #region Inch Conversions
    /// <summary>
    /// The number of inches in a mil.
    /// </summary>
    public static readonly T InchesInMil = T.One / MilsInInch;

    /// <summary>
    /// The number of Inches in an millimeter.
    /// </summary>
    public static readonly T InchesInMillimeter = T.One / MillimetersInInch;

    /// <summary>
    /// The number of inches in a centimeter.
    /// </summary>
    public static readonly T InchesInCentimeter = T.One / CentimetersInInch;

    /// <summary>
    /// The number of inches in an inch.
    /// </summary>
    public static readonly T InchesInInch = T.One;

    /// <summary>
    /// The number of Inches in a Foot.
    /// </summary>
    public static readonly T InchesInFoot = T.CreateSaturating(12);

    /// <summary>
    /// The number of Inches in a Yard.
    /// </summary>
    public static readonly T InchesInYard = InchesInFoot * FeetInYard;

    /// <summary>
    /// The number of Inches in a Meter.
    /// </summary>
    public static readonly T InchesInMeter = CentimetersInInch * CentimetersInMeter;

    /// <summary>
    /// The number of Inches in a Smoot.
    /// </summary>
    public static readonly T InchesInSmoot = T.CreateSaturating(67);

    /// <summary>
    /// The number of Inches in a Kilometer.
    /// </summary>
    public static readonly T InchesInKilometer = InchesInMeter * MetersInKilometer;

    /// <summary>
    /// The number of Inches in a Mile.
    /// </summary>
    public static readonly T InchesInMile = InchesInYard * YardsInMile;

    /// <summary>
    /// The number of Inches in a Nautical Mile.
    /// </summary>
    public static readonly T InchesInNauticalMile = InchesInMile * MilesInNauticalMile;
    #endregion Inch Conversions

    #region Foot Conversions
    /// <summary>
    /// The number of feet in a mil.
    /// </summary>
    public static readonly T FeetInMil = T.One / MilsInFoot;

    /// <summary>
    /// The number of Feet in a millimeter.
    /// </summary>
    public static readonly T FeetInMillimeter = T.One / MillimetersInFoot;

    /// <summary>
    /// The number of feet in a centimeter.
    /// </summary>
    public static readonly T FeetInCentimeter = T.One / CentimetersInFoot;

    /// <summary>
    /// The number of feet in an inch.
    /// </summary>
    public static readonly T FeetInInch = T.One / InchesInFoot;

    /// <summary>
    /// The number of feet in a foot.
    /// </summary>
    public static readonly T FeetInFoot = T.One;

    /// <summary>
    /// The number of Feet in a Yard.
    /// </summary>
    public static readonly T FeetInYard = T.CreateSaturating(3);

    /// <summary>
    /// The number of Feet in a Meter.
    /// </summary>
    public static readonly T FeetInMeter = CentimetersInFoot * CentimetersInMeter;

    /// <summary>
    /// The number of Feet in a Smoot.
    /// </summary>
    public static readonly T FeetInSmoot = InchesInSmoot * InchesInFoot;

    /// <summary>
    /// The number of Feet in a Kilometer.
    /// </summary>
    public static readonly T FeetInKilometer = FeetInMeter * MetersInKilometer;

    /// <summary>
    /// The number of Feet in a Mile.
    /// </summary>
    public static readonly T FeetInMile = FeetInYard * YardsInMile;

    /// <summary>
    /// The number of Feet in a Nautical Mile.
    /// </summary>
    public static readonly T FeetInNauticalMile = FeetInMile * MilesInNauticalMile;
    #endregion Foot Conversions

    #region Yard Conversions
    /// <summary>
    /// The number of yards in a mil.
    /// </summary>
    public static readonly T YardsInMil = T.One / MilsInYard;

    /// <summary>
    /// The number of Yards in a millimeter.
    /// </summary>
    public static readonly T YardsInMillimeter = T.One / MillimetersInYard;

    /// <summary>
    /// The number of yards in a centimeter.
    /// </summary>
    public static readonly T YardsInCentimeter = T.One / CentimetersInYard;

    /// <summary>
    /// The number of yards in an inch.
    /// </summary>
    public static readonly T YardsInInch = T.One / InchesInYard;

    /// <summary>
    /// The number of yards in a foot.
    /// </summary>
    public static readonly T YardsInFoot = T.One / FeetInYard;

    /// <summary>
    /// The number of yards in a yard.
    /// </summary>
    public static readonly T YardsInYard = T.One;

    /// <summary>
    /// The number of Yards in a Meter.
    /// </summary>
    public static readonly T YardsInMeter = FeetInMeter * FeetInYard;

    /// <summary>
    /// The number of Yards in a Smoot.
    /// </summary>
    public static readonly T YardsInSmoot = FeetInSmoot * FeetInYard;

    /// <summary>
    /// The number of Yards in a Kilometer.
    /// </summary>
    public static readonly T YardsInKilometer = YardsInMeter * MetersInKilometer;

    /// <summary>
    /// The number of Yards in a Mile.
    /// </summary>
    public static readonly T YardsInMile = T.CreateSaturating(1760);

    /// <summary>
    /// The number of Yards in a Nautical Mile.
    /// </summary>
    public static readonly T YardsInNauticalMile = YardsInMile * MilesInNauticalMile;
    #endregion Yard Conversions

    #region Meter Conversions
    /// <summary>
    /// The number of meters in a mil.
    /// </summary>
    public static readonly T MetersInMil = T.One / MilsInMeter;

    /// <summary>
    /// The number of Meters in a millimeter.
    /// </summary>
    public static readonly T MetersInMillimeter = T.One / MillimetersInMeter;

    /// <summary>
    /// The number of meters in a centimeter.
    /// </summary>
    public static readonly T MetersInCentimeter = T.One / CentimetersInMeter;

    /// <summary>
    /// The number of meters in a inch.
    /// </summary>
    public static readonly T MetersInInch = T.One / InchesInMeter;

    /// <summary>
    /// The number of meters in a foot.
    /// </summary>
    public static readonly T MetersInFoot = T.One / FeetInMeter;

    /// <summary>
    /// The number of meters in a yard.
    /// </summary>
    public static readonly T MetersInYard = T.One / YardsInMeter;

    /// <summary>
    /// The number of meters in a meter.
    /// </summary>
    public static readonly T MetersInMeter = T.One;

    /// <summary>
    /// The number of Meters in a Smoot.
    /// </summary>
    public static readonly T MetersInSmoot = InchesInMeter * InchesInSmoot;

    /// <summary>
    /// The number of Meters in a Kilometer.
    /// </summary>
    public static readonly T MetersInKilometer = T.CreateSaturating(1000);

    /// <summary>
    /// The number of Meters in a Mile.
    /// </summary>
    public static readonly T MetersInMile = YardsInMeter * YardsInMile;

    /// <summary>
    /// The number of Meters in a Nautical Mile.
    /// </summary>
    public static readonly T MetersInNauticalMile = T.CreateSaturating(1852);
    #endregion Meter Conversions

    #region Smoot Conversions
    /// <summary>
    /// The number of smoots in a mil.
    /// </summary>
    public static readonly T SmootsInMil = T.One / MilsInSmoot;

    /// <summary>
    /// The number of Smoots in a millimeter.
    /// </summary>
    public static readonly T SmootsInMillimeter = T.One / MillimetersInSmoot;

    /// <summary>
    /// The number of smoots in a centimeter.
    /// </summary>
    public static readonly T SmootsInCentimeter = T.One / CentimetersInSmoot;

    /// <summary>
    /// The number of smoots in an inch.
    /// </summary>
    public static readonly T SmootsInInch = T.One / InchesInSmoot;

    /// <summary>
    /// The number of smoots in a foot.
    /// </summary>
    public static readonly T SmootsInFoot = T.One / FeetInSmoot;

    /// <summary>
    /// The number of smoots in a yard.
    /// </summary>
    public static readonly T SmootsInYard = T.One / YardsInSmoot;

    /// <summary>
    /// The number of smoots in a meter.
    /// </summary>
    public static readonly T SmootsInMeter = T.One / MetersInSmoot;

    /// <summary>
    /// The number of smoots in smoot.
    /// </summary>
    public static readonly T SmootsInSmoot = T.One;

    /// <summary>
    /// The number of Smoots in a Kilometer.
    /// </summary>
    public static readonly T SmootsInKilometer = InchesInKilometer * InchesInSmoot;

    /// <summary>
    /// The number of Smoots in a Mile.
    /// </summary>
    public static readonly T SmootsInMile = InchesInMile * InchesInSmoot;

    /// <summary>
    /// The number of Smoots in a Nautical Mile.
    /// </summary>
    public static readonly T SmootsInNauticalMile = InchesInNauticalMile * InchesInSmoot;
    #endregion Smoot Conversions

    #region Kilometer Conversions
    /// <summary>
    /// The number of kilometers in a mil.
    /// </summary>
    public static readonly T KilometersInMil = T.One / MilsInKilometer;

    /// <summary>
    /// The number of Kilometers in a millimeter.
    /// </summary>
    public static readonly T KilometersInMillimeter = T.One / MillimetersInKilometer;

    /// <summary>
    /// The number of kilometers in a centimeter.
    /// </summary>
    public static readonly T KilometersInCentimeter = T.One / CentimetersInKilometer;

    /// <summary>
    /// The number of kilometers in an inch.
    /// </summary>
    public static readonly T KilometersInInch = T.One / InchesInKilometer;

    /// <summary>
    /// The number of kilometers in a foot.
    /// </summary>
    public static readonly T KilometersInFoot = T.One / FeetInKilometer;

    /// <summary>
    /// The number of kilometers in a yard.
    /// </summary>
    public static readonly T KilometersInYard = T.One / YardsInKilometer;

    /// <summary>
    /// The number of kilometers in a meter.
    /// </summary>
    public static readonly T KilometersInMeter = T.One / MetersInKilometer;

    /// <summary>
    /// The number of kilometers in a smoot.
    /// </summary>
    public static readonly T KilometersInSmoot = T.One / SmootsInKilometer;

    /// <summary>
    /// The number of kilometers in a kilometer.
    /// </summary>
    public static readonly T KilometersinKilometer = T.One;

    /// <summary>
    /// The number of Kilometers in a Mile.
    /// </summary>
    public static readonly T KilometersInMile = MetersInMile * MetersInKilometer;

    /// <summary>
    /// The number of Kilometers in a Nautical Mile.
    /// </summary>
    public static readonly T KilometersInNauticalMile = KilometersInMile * MilesInNauticalMile;
    #endregion Kilometer Conversions

    #region Mile Conversions
    /// <summary>
    /// The number of miles in a mil.
    /// </summary>
    public static readonly T MilesInMil = T.One / MilesInMile;

    /// <summary>
    /// The number of Miles in a millimeter.
    /// </summary>
    public static readonly T MilesInMillimeter = T.One / MillimetersInMile;

    /// <summary>
    /// The number of miles in a centimeter.
    /// </summary>
    public static readonly T MilesInCentimeter = T.One / CentimetersInMile;

    /// <summary>
    /// The number of miles in an inch.
    /// </summary>
    public static readonly T MilesInInch = T.One / InchesInMile;

    /// <summary>
    /// The number of miles in  foot.
    /// </summary>
    public static readonly T MilesInFoot = T.One / FeetInMile;

    /// <summary>
    /// The number of miles in a yard.
    /// </summary>
    public static readonly T MilesInYard = T.One / YardsInMile;

    /// <summary>
    /// The number of miles in a meter.
    /// </summary>
    public static readonly T MilesInMeter = T.One / MetersInMile;

    /// <summary>
    /// The number of miles in a smoot.
    /// </summary>
    public static readonly T MilesInSmoot = T.One / SmootsInMile;

    /// <summary>
    /// The number of miles in a kilometer.
    /// </summary>
    public static readonly T MilesInKilometer = T.One / KilometersInMile;

    /// <summary>
    /// The number of miles in a mile.
    /// </summary>
    public static readonly T MilesInMile = T.One;

    /// <summary>
    /// The number of Miles in a Nautical Mile.
    /// </summary>
    public static readonly T MilesInNauticalMile = MetersInMile * MetersInNauticalMile;
    #endregion Mile Conversions

    #region NauticalMile Conversions
    /// <summary>
    /// The number of nautical miles in a mil.
    /// </summary>
    public static readonly T NauticalMilesInMil = T.One / MilesInNauticalMile;

    /// <summary>
    /// The number of Nautical Miles in a millimeter.
    /// </summary>
    public static readonly T NauticalMilesInMillimeter = T.One / MillimetersInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a centimeter.
    /// </summary>
    public static readonly T NauticalMilesInCentimeter = T.One / CentimetersInNauticalMile;

    /// <summary>
    /// The number of nautical miles in an inch.
    /// </summary>
    public static readonly T NauticalMilesInInch = T.One / InchesInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a foot.
    /// </summary>
    public static readonly T NauticalMilesInFoot = T.One / FeetInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a yard.
    /// </summary>
    public static readonly T NauticalMilesInYard = T.One / YardsInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a meter.
    /// </summary>
    public static readonly T NauticalMilesInMeter = T.One / MetersInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a smoot.
    /// </summary>
    public static readonly T NauticalMilesInSmoot = T.One / SmootsInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a kilometer.
    /// </summary>
    public static readonly T NauticalMilesInKilometer = T.One / KilometersInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a mile.
    /// </summary>
    public static readonly T NauticalMilesInMile = T.One / MilesInNauticalMile;

    /// <summary>
    /// The number of nautical miles in a nautical mile.
    /// </summary>
    public static readonly T NauticalMilesInNauticalMile = T.One;
    #endregion NauticalMile Conversions
}
