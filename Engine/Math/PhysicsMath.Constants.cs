// <copyright file="Physics.Constants.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Physics
{
    /// <summary>
    /// Common Physics constants.
    /// </summary>
    public partial class PhysicsMath
    {
        #region Motion

        /// <summary>
        /// The speed of light in a vacuum.
        /// </summary>
        public static readonly Speed SpeedOfLightInVacuum = new Speed((Meters)299790000d, (Seconds)1d);

        /// <summary>
        /// Speed of sound at 20 Degrees C.
        /// </summary>
        public static readonly Speed SpeedOfSoundAt20DegreesC = new Speed((Meters)343d, (Seconds)1d);

        /// <summary>
        /// The Earth gravitational constant.
        /// </summary>
        public static readonly Acceleration EarthGravity = new Acceleration(new Velocity(new Speed(new Meters(9.8d), new Seconds(1d)), new Degrees(0d)), new Seconds(1d));

        #endregion

        /// <summary>
        /// Standard Atmospheric Pressure.
        /// </summary>
        public const double StandardAtmosphericPressure = 1.01e11d; // Pa

        #region Distances

        /// <summary>
        /// Astronomical Unit. Average Earth-Sun distance.
        /// </summary>
        public static readonly ILength AstronomicalUnit = new Meters(1.5e11d);

        /// <summary>
        /// Average Earth Moon distance.
        /// </summary>
        public static readonly ILength AverageEarthMoonDistance = new Meters(3.84e8d);

        /// <summary>
        /// Average radius of Sun.
        /// </summary>
        public static readonly ILength AverageRadiusOfSun = new Meters(6.96e8d);

        /// <summary>
        /// Equatorial radius of Earth.
        /// </summary>
        public static readonly ILength EquatorialRadiusOfEarth = new Meters(6.37e6d);

        /// <summary>
        /// Radius of Earth's orbit.
        /// </summary>
        public static readonly ILength RadiusOfEarthsOrbit = AstronomicalUnit;

        /// <summary>
        /// Average radius of Moon
        /// </summary>
        public static readonly ILength AverageRadiusOfMoon = new Meters(1.74e6d);

        /// <summary>
        /// Radius of Moon's orbit.
        /// </summary>
        public static readonly ILength RadiusOfMoonsOrbit = new Meters(3.84e8d);

        /// <summary>
        /// Equatorial radius of Jupiter.
        /// </summary>
        public static readonly ILength EquatorialRadiusOfJupiter = new Meters(7.14e7d);

        /// <summary>
        /// Radius of Hydrogen atom.
        /// </summary>
        public static readonly ILength RadiusOfHydrogenAtom = new Meters(5e-11d);

        #endregion

        #region Mass

        /// <summary>
        /// Mass of Sun.
        /// </summary>
        public static readonly IMass MassOfSun = new Kilograms(1.99e30d);

        /// <summary>
        /// Mass of Earth.
        /// </summary>
        public static readonly IMass MassOfEarth = new Kilograms(5.98e24d);

        /// <summary>
        /// Mass of Moon.
        /// </summary>
        public static readonly IMass MassOfMoon = new Kilograms(7.36e22d);

        /// <summary>
        /// Mass of Jupiter.
        /// </summary>
        public static readonly IMass MassOfJupiter = new Kilograms(1.90e7d);

        /// <summary>
        /// Mass of Proton.
        /// 938.27231 MeV
        /// </summary>
        public static readonly IMass MassOfProton = new Kilograms(1.6726231e-27d);

        /// <summary>
        /// Mass of Neutron.
        /// 939.56563 MeV
        /// </summary>
        public static readonly IMass MassOfNeutron = new Kilograms(1.6749286e-27d);

        /// <summary>
        /// Mass of Electron.
        /// 0.51099906 MeV
        /// </summary>
        public static readonly IMass MassOfElectron = new Kilograms(9.1093897e-31d);

        #endregion

        /// <summary>
        /// Charge of Electron.
        /// </summary>
        public const double ChargeOfElectron = 1.602e-19d; // C

        /// <summary>
        /// Gravitational constant.
        /// </summary>
        public const double GravitationalConstant = 6.672759e-11d; // Nm^2/kg^2

        /// <summary>
        /// A fundamental constant, h, that relates the energy of light quanta to their frequency.
        /// 4.1356692e-15 eVs
        /// </summary>
        public static readonly Joules PlanksConstant = new Joules(6.6260755e-34d); // s

        /// <summary>
        /// Avogadro's number
        /// </summary>
        public const double AvogadrosNumber = 6.0221367e23d; // mol

        /// <summary>
        /// Black body radiation constant.
        /// </summary>
        public const double BlackBodyRadiationConstant = 5.67051e-0d; // W/m^2K^4
    }
}
