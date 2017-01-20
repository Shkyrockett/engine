// <copyright file="LengthUnits.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public static class LengthUnits
    {
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

        /// <summary>
        /// The number of Kilometers in a Mile.
        /// </summary>
        public const double KilometersInMile = MetersInMile * MetersInKilometer;

        /// <summary>
        /// The number of Kilometers in a Nautical Mile.
        /// </summary>
        public const double KilometersInNauticalMile = KilometersInMile * MilesInNauticalMile;

        /// <summary>
        /// The number of Miles in a Nautical Mile.
        /// </summary>
        public const double MilesInNauticalMile = MetersInMile * MetersInNauticalMile;
    }
}
