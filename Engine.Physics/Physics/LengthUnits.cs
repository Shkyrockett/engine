// <copyright file="LengthUnits.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The length units class.
    /// </summary>
    public static class LengthUnits
    {
        /// <summary>
        /// The points per inch (const). Value: 72d.
        /// </summary>
        public const double PointsPerInch = 72d;

        /// <summary>
        /// The pixels per inch (const). Value: 96.
        /// </summary>
        public const double PixelsPerInch = 96d;

        /// <summary>
        /// The points per pixel (const). Value: 72d / 96d.
        /// </summary>
        public const double PointsPerPixel = PointsPerInch / PixelsPerInch;

        /// <summary>
        /// The Planck length in mega-parsecs (const). Value: 1.90939E+57.
        /// </summary>
        public const double PlanckLengthInMegaparsec = 1.90939E+57d;

        /// <summary>
        /// The number of mega-parsecs in a Planck length.
        /// </summary>
        public const double MegaparsecsInPlanckLength = 1d / PlanckLengthInMegaparsec;

        #region Mil Conversions
        /// <summary>
        /// The number of mils in a mil.
        /// </summary>
        public const double MilsInMil = 1d;

        /// <summary>
        /// The number of mils in a millimeter.
        /// </summary>
        public const double MilsInMillimeter = MilsInCentimeter * MillimetersInCentimeter;

        /// <summary>
        /// The number of Mils in a Centimeter.
        /// </summary>
        public const double MilsInCentimeter = 393.700787d;

        /// <summary>
        /// The number of Mils in an Inch.
        /// </summary>
        public const double MilsInInch = 1000d;

        /// <summary>
        /// The number of Mils in a Foot.
        /// </summary>
        public const double MilsInFoot = MilsInInch * InchesInFoot;

        /// <summary>
        /// The number of Mils in a Yard.
        /// </summary>
        public const double MilsInYard = MilsInFoot * FeetInYard;

        /// <summary>
        /// The number of Mils in a Meter.
        /// </summary>
        public const double MilsInMeter = MilsInCentimeter * CentimetersInMeter;

        /// <summary>
        /// The number of Mils in a Smoot.
        /// </summary>
        public const double MilsInSmoot = MilsInInch * InchesInSmoot;

        /// <summary>
        /// The number of Mils in a Kilometer.
        /// </summary>
        public const double MilsInKilometer = MilsInMeter * MetersInKilometer;

        /// <summary>
        /// The number of Mils in a Mile.
        /// </summary>
        public const double MilsInMile = MilsInYard * YardsInMile;

        /// <summary>
        /// The number of Mils in a Nautical Mile.
        /// </summary>
        public const double MilsInNauticalMile = 7.2913E+7d;
        #endregion Mil Conversions

        #region Millimeter Conversions
        /// <summary>
        /// The number of millimeters in a mil.
        /// </summary>
        public const double MillimetersInMil = 1d / MilsInCentimeter * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a millimeter.
        /// </summary>
        public const double MillimetersInMillimeter = 1d;

        /// <summary>
        /// The number of millimeters in a centimeter.
        /// </summary>
        public const double MillimetersInCentimeter = 100d;

        /// <summary>
        /// The number of millimeters in an Inch.
        /// </summary>
        public const double MillimetersInInch = 2.54d * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Foot.
        /// </summary>
        public const double MillimetersInFoot = CentimetersInInch * InchesInFoot * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Yard.
        /// </summary>
        public const double MillimetersInYard = CentimetersInFoot * FeetInYard * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Meter.
        /// </summary>
        public const double MillimetersInMeter = 100d * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Smoot.
        /// </summary>
        public const double MillimetersInSmoot = CentimetersInInch * InchesInSmoot * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Kilometer.
        /// </summary>
        public const double MillimetersInKilometer = CentimetersInMeter * MetersInKilometer * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Mile.
        /// </summary>
        public const double MillimetersInMile = CentimetersInYard * YardsInMile * MillimetersInCentimeter;

        /// <summary>
        /// The number of millimeters in a Nautical Mile.
        /// </summary>
        public const double MillimetersInNauticalMile = CentimetersInMile * MilesInNauticalMile * MillimetersInCentimeter;
        #endregion Millimeter Conversions

        #region Pica Conversions
        /// <summary>
        /// The number of Picas in a mil.
        /// </summary>
        public const double PicasInMil = 0.006d;

        /// <summary>
        /// The number of Picas in a millimeter.
        /// </summary>
        public const double PicasInMillimeter = 0.236220474d;

        /// <summary>
        /// The number of Picas in a Pica.
        /// </summary>
        public const double PicasInPica = 1;

        /// <summary>
        /// The number of Picas in a centimeter.
        /// </summary>
        public const double PicasInCentimeter = 2.362204743d;

        /// <summary>
        /// The number of Picas in an Inch.
        /// </summary>
        public const double PicasInInch = 6.000000047d;

        /// <summary>
        /// The number of Picas in a Foot.
        /// </summary>
        public const double PicasInFoot = 72.00000057d;

        /// <summary>
        /// The number of Picas in a Yard.
        /// </summary>
        public const double PicasInYard = 216.0000017d;

        /// <summary>
        /// The number of Picas in a Meter.
        /// </summary>
        public const double PicasInMeter = 236.2204743d;

        /// <summary>
        /// The number of Picas in a Smoot.
        /// </summary>
        public const double PicasInSmoot = 402.0000032d;

        /// <summary>
        /// The number of Picas in a Kilometer.
        /// </summary>
        public const double PicasInKilometer = 236220.4743d;

        /// <summary>
        /// The number of Picas in a Mile.
        /// </summary>
        public const double PicasInMile = 380160.003;

        /// <summary>
        /// The number of Picas in a Nautical Mile.
        /// </summary>
        public const double PicasInNauticalMile = 437480.3184d;
        #endregion Pica Conversions

        #region Centimeter Conversions
        /// <summary>
        /// The number of centimeters in a mil.
        /// </summary>
        public const double CentimetersInMil = 1d / MilsInCentimeter;

        /// <summary>
        /// The number of centimeters in a millimeter.
        /// </summary>
        public const double CentimetersInMillimeter = 1d / MillimetersInCentimeter;

        /// <summary>
        /// The number of centimeters in a centimeter.
        /// </summary>
        public const double CentimetersInCentimeter = 1d;

        /// <summary>
        /// The number of Centimeters in an Inch.
        /// </summary>
        public const double CentimetersInInch = 2.54d;

        /// <summary>
        /// The number of Centimeters in a Foot.
        /// </summary>
        public const double CentimetersInFoot = CentimetersInInch * InchesInFoot;

        /// <summary>
        /// The number of Centimeters in a Yard.
        /// </summary>
        public const double CentimetersInYard = CentimetersInFoot * FeetInYard;

        /// <summary>
        /// The number of Centimeters in a Meter.
        /// </summary>
        public const double CentimetersInMeter = 100d;

        /// <summary>
        /// The number of Centimeters in a Smoot.
        /// </summary>
        public const double CentimetersInSmoot = CentimetersInInch * InchesInSmoot;

        /// <summary>
        /// The number of Centimeters in a Kilometer.
        /// </summary>
        public const double CentimetersInKilometer = CentimetersInMeter * MetersInKilometer;

        /// <summary>
        /// The number of Centimeters in a Mile.
        /// </summary>
        public const double CentimetersInMile = CentimetersInYard * YardsInMile;

        /// <summary>
        /// The number of Centimeters in a Nautical Mile.
        /// </summary>
        public const double CentimetersInNauticalMile = CentimetersInMile * MilesInNauticalMile;
        #endregion Centimeter Conversions

        #region Inch Conversions
        /// <summary>
        /// The number of inches in a mil.
        /// </summary>
        public const double InchesInMil = 1d / MilsInInch;

        /// <summary>
        /// The number of Inches in an millimeter.
        /// </summary>
        public const double InchesInMillimeter = 1d / MillimetersInInch;

        /// <summary>
        /// The number of inches in a centimeter.
        /// </summary>
        public const double InchesInCentimeter = 1d / CentimetersInInch;

        /// <summary>
        /// The number of inches in an inch.
        /// </summary>
        public const double InchesInInch = 1d;

        /// <summary>
        /// The number of Inches in a Foot.
        /// </summary>
        public const double InchesInFoot = 12d;

        /// <summary>
        /// The number of Inches in a Yard.
        /// </summary>
        public const double InchesInYard = InchesInFoot * FeetInYard;

        /// <summary>
        /// The number of Inches in a Meter.
        /// </summary>
        public const double InchesInMeter = CentimetersInInch * CentimetersInMeter;

        /// <summary>
        /// The number of Inches in a Smoot.
        /// </summary>
        public const double InchesInSmoot = 67d;

        /// <summary>
        /// The number of Inches in a Kilometer.
        /// </summary>
        public const double InchesInKilometer = InchesInMeter * MetersInKilometer;

        /// <summary>
        /// The number of Inches in a Mile.
        /// </summary>
        public const double InchesInMile = InchesInYard * YardsInMile;

        /// <summary>
        /// The number of Inches in a Nautical Mile.
        /// </summary>
        public const double InchesInNauticalMile = InchesInMile * MilesInNauticalMile;
        #endregion Inch Conversions

        #region Foot Conversions
        /// <summary>
        /// The number of feet in a mil.
        /// </summary>
        public const double FeetInMil = 1d / MilsInFoot;

        /// <summary>
        /// The number of Feet in a millimeter.
        /// </summary>
        public const double FeetInMillimeter = 1d / MillimetersInFoot;

        /// <summary>
        /// The number of feet in a centimeter.
        /// </summary>
        public const double FeetInCentimeter = 1d / CentimetersInFoot;

        /// <summary>
        /// The number of feet in an inch.
        /// </summary>
        public const double FeetInInch = 1d / InchesInFoot;

        /// <summary>
        /// The number of feet in a foot.
        /// </summary>
        public const double FeetInFoot = 1d;

        /// <summary>
        /// The number of Feet in a Yard.
        /// </summary>
        public const double FeetInYard = 3d;

        /// <summary>
        /// The number of Feet in a Meter.
        /// </summary>
        public const double FeetInMeter = CentimetersInFoot * CentimetersInMeter;

        /// <summary>
        /// The number of Feet in a Smoot.
        /// </summary>
        public const double FeetInSmoot = InchesInSmoot * InchesInFoot;

        /// <summary>
        /// The number of Feet in a Kilometer.
        /// </summary>
        public const double FeetInKilometer = FeetInMeter * MetersInKilometer;

        /// <summary>
        /// The number of Feet in a Mile.
        /// </summary>
        public const double FeetInMile = FeetInYard * YardsInMile;

        /// <summary>
        /// The number of Feet in a Nautical Mile.
        /// </summary>
        public const double FeetInNauticalMile = FeetInMile * MilesInNauticalMile;
        #endregion Foot Conversions

        #region Yard Conversions
        /// <summary>
        /// The number of yards in a mil.
        /// </summary>
        public const double YardsInMil = 1d / MilsInYard;

        /// <summary>
        /// The number of Yards in a millimeter.
        /// </summary>
        public const double YardsInMillimeter = 1d / MillimetersInYard;

        /// <summary>
        /// The number of yards in a centimeter.
        /// </summary>
        public const double YardsInCentimeter = 1d / CentimetersInYard;

        /// <summary>
        /// The number of yards in an inch.
        /// </summary>
        public const double YardsInInch = 1d / InchesInYard;

        /// <summary>
        /// The number of yards in a foot.
        /// </summary>
        public const double YardsInFoot = 1d / FeetInYard;

        /// <summary>
        /// The number of yards in a yard.
        /// </summary>
        public const double YardsInYard = 1d;

        /// <summary>
        /// The number of Yards in a Meter.
        /// </summary>
        public const double YardsInMeter = FeetInMeter * FeetInYard;

        /// <summary>
        /// The number of Yards in a Smoot.
        /// </summary>
        public const double YardsInSmoot = FeetInSmoot * FeetInYard;

        /// <summary>
        /// The number of Yards in a Kilometer.
        /// </summary>
        public const double YardsInKilometer = YardsInMeter * MetersInKilometer;

        /// <summary>
        /// The number of Yards in a Mile.
        /// </summary>
        public const double YardsInMile = 1760d;

        /// <summary>
        /// The number of Yards in a Nautical Mile.
        /// </summary>
        public const double YardsInNauticalMile = YardsInMile * MilesInNauticalMile;
        #endregion Yard Conversions

        #region Meter Conversions
        /// <summary>
        /// The number of meters in a mil.
        /// </summary>
        public const double MetersInMil = 1d / MilsInMeter;

        /// <summary>
        /// The number of Meters in a millimeter.
        /// </summary>
        public const double MetersInMillimeter = 1d / MillimetersInMeter;

        /// <summary>
        /// The number of meters in a centimeter.
        /// </summary>
        public const double MetersInCentimeter = 1d / CentimetersInMeter;

        /// <summary>
        /// The number of meters in a inch.
        /// </summary>
        public const double MetersInInch = 1d / InchesInMeter;

        /// <summary>
        /// The number of meters in a foot.
        /// </summary>
        public const double MetersInFoot = 1d / FeetInMeter;

        /// <summary>
        /// The number of meters in a yard.
        /// </summary>
        public const double MetersInYard = 1d / YardsInMeter;

        /// <summary>
        /// The number of meters in a meter.
        /// </summary>
        public const double MetersInMeter = 1d;

        /// <summary>
        /// The number of Meters in a Smoot.
        /// </summary>
        public const double MetersInSmoot = InchesInMeter * InchesInSmoot;

        /// <summary>
        /// The number of Meters in a Kilometer.
        /// </summary>
        public const double MetersInKilometer = 1000d;

        /// <summary>
        /// The number of Meters in a Mile.
        /// </summary>
        public const double MetersInMile = YardsInMeter * YardsInMile;

        /// <summary>
        /// The number of Meters in a Nautical Mile.
        /// </summary>
        public const double MetersInNauticalMile = 1852d;
        #endregion Meter Conversions

        #region Smoot Conversions
        /// <summary>
        /// The number of smoots in a mil.
        /// </summary>
        public const double SmootsInMil = 1d / MilsInSmoot;

        /// <summary>
        /// The number of Smoots in a millimeter.
        /// </summary>
        public const double SmootsInMillimeter = 1d / MillimetersInSmoot;

        /// <summary>
        /// The number of smoots in a centimeter.
        /// </summary>
        public const double SmootsInCentimeter = 1d / CentimetersInSmoot;

        /// <summary>
        /// The number of smoots in an inch.
        /// </summary>
        public const double SmootsInInch = 1d / InchesInSmoot;

        /// <summary>
        /// The number of smoots in a foot.
        /// </summary>
        public const double SmootsInFoot = 1d / FeetInSmoot;

        /// <summary>
        /// The number of smoots in a yard.
        /// </summary>
        public const double SmootsInYard = 1d / YardsInSmoot;

        /// <summary>
        /// The number of smoots in a meter.
        /// </summary>
        public const double SmootsInMeter = 1d / MetersInSmoot;

        /// <summary>
        /// The number of smoots in smoot.
        /// </summary>
        public const double SmootsInSmoot = 1d;

        /// <summary>
        /// The number of Smoots in a Kilometer.
        /// </summary>
        public const double SmootsInKilometer = InchesInKilometer * InchesInSmoot;

        /// <summary>
        /// The number of Smoots in a Mile.
        /// </summary>
        public const double SmootsInMile = InchesInMile * InchesInSmoot;

        /// <summary>
        /// The number of Smoots in a Nautical Mile.
        /// </summary>
        public const double SmootsInNauticalMile = InchesInNauticalMile * InchesInSmoot;
        #endregion Smoot Conversions

        #region Kilometer Conversions
        /// <summary>
        /// The number of kilometers in a mil.
        /// </summary>
        public const double KilometersInMil = 1d / MilsInKilometer;

        /// <summary>
        /// The number of Kilometers in a millimeter.
        /// </summary>
        public const double KilometersInMillimeter = 1d / MillimetersInKilometer;

        /// <summary>
        /// The number of kilometers in a centimeter.
        /// </summary>
        public const double KilometersInCentimeter = 1d / CentimetersInKilometer;

        /// <summary>
        /// The number of kilometers in an inch.
        /// </summary>
        public const double KilometersInInch = 1d / InchesInKilometer;

        /// <summary>
        /// The number of kilometers in a foot.
        /// </summary>
        public const double KilometersInFoot = 1d / FeetInKilometer;

        /// <summary>
        /// The number of kilometers in a yard.
        /// </summary>
        public const double KilometersInYard = 1d / YardsInKilometer;

        /// <summary>
        /// The number of kilometers in a meter.
        /// </summary>
        public const double KilometersInMeter = 1d / MetersInKilometer;

        /// <summary>
        /// The number of kilometers in a smoot.
        /// </summary>
        public const double KilometersInSmoot = 1d / SmootsInKilometer;

        /// <summary>
        /// The number of kilometers in a kilometer.
        /// </summary>
        public const double KilometersinKilometer = 1d;

        /// <summary>
        /// The number of Kilometers in a Mile.
        /// </summary>
        public const double KilometersInMile = MetersInMile * MetersInKilometer;

        /// <summary>
        /// The number of Kilometers in a Nautical Mile.
        /// </summary>
        public const double KilometersInNauticalMile = KilometersInMile * MilesInNauticalMile;
        #endregion Kilometer Conversions

        #region Mile Conversions
        /// <summary>
        /// The number of miles in a mil.
        /// </summary>
        public const double MilesInMil = 1d / MilesInMile;

        /// <summary>
        /// The number of Miles in a millimeter.
        /// </summary>
        public const double MilesInMillimeter = 1d / MillimetersInMile;

        /// <summary>
        /// The number of miles in a centimeter.
        /// </summary>
        public const double MilesInCentimeter = 1d / CentimetersInMile;

        /// <summary>
        /// The number of miles in an inch.
        /// </summary>
        public const double MilesInInch = 1d / InchesInMile;

        /// <summary>
        /// The number of miles in  foot.
        /// </summary>
        public const double MilesInFoot = 1d / FeetInMile;

        /// <summary>
        /// The number of miles in a yard.
        /// </summary>
        public const double MilesInYard = 1d / YardsInMile;

        /// <summary>
        /// The number of miles in a meter.
        /// </summary>
        public const double MilesInMeter = 1d / MetersInMile;

        /// <summary>
        /// The number of miles in a smoot.
        /// </summary>
        public const double MilesInSmoot = 1d / SmootsInMile;

        /// <summary>
        /// The number of miles in a kilometer.
        /// </summary>
        public const double MilesInKilometer = 1d / KilometersInMile;

        /// <summary>
        /// The number of miles in a mile.
        /// </summary>
        public const double MilesInMile = 1d;

        /// <summary>
        /// The number of Miles in a Nautical Mile.
        /// </summary>
        public const double MilesInNauticalMile = MetersInMile * MetersInNauticalMile;
        #endregion Mile Conversions

        #region NauticalMile Conversions
        /// <summary>
        /// The number of nautical miles in a mil.
        /// </summary>
        public const double NauticalMilesInMil = 1d / MilesInNauticalMile;

        /// <summary>
        /// The number of Nautical Miles in a millimeter.
        /// </summary>
        public const double NauticalMilesInMillimeter = 1d / MillimetersInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a centimeter.
        /// </summary>
        public const double NauticalMilesInCentimeter = 1d / CentimetersInNauticalMile;

        /// <summary>
        /// The number of nautical miles in an inch.
        /// </summary>
        public const double NauticalMilesInInch = 1d / InchesInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a foot.
        /// </summary>
        public const double NauticalMilesInFoot = 1d / FeetInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a yard.
        /// </summary>
        public const double NauticalMilesInYard = 1d / YardsInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a meter.
        /// </summary>
        public const double NauticalMilesInMeter = 1d / MetersInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a smoot.
        /// </summary>
        public const double NauticalMilesInSmoot = 1d / SmootsInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a kilometer.
        /// </summary>
        public const double NauticalMilesInKilometer = 1d / KilometersInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a mile.
        /// </summary>
        public const double NauticalMilesInMile = 1d / MilesInNauticalMile;

        /// <summary>
        /// The number of nautical miles in a nautical mile.
        /// </summary>
        public const double NauticalMilesInNauticalMile = 1d;
        #endregion NauticalMile Conversions
    }
}
