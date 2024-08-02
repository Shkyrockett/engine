// <copyright file="Physics.Constants.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
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
/// Common Physics constants.
/// </summary>
public partial class PhysicsMath<T>
    where T : INumber<T>
{
    #region Motion
    /// <summary>
    /// The speed of light in a vacuum.
    /// </summary>
    public static readonly Speed<T> SpeedOfLightInVacuum = new((Meters<T>)T.CreateSaturating(299790000), (Seconds<T>)T.One);

    /// <summary>
    /// Speed of sound at 20 Degrees C.
    /// </summary>
    public static readonly Speed<T> SpeedOfSoundAt20DegreesC = new((Meters<T>)T.CreateSaturating(343), (Seconds<T>)T.One);

    /// <summary>
    /// The Earth gravitational constant.
    /// </summary>
    public static readonly Acceleration<T> EarthGravity = new(new Velocity<T>(new Speed<T>(new Meters<T>(T.CreateSaturating(9.8)), new Seconds<T>(T.One)), new Degrees<T>(T.Zero)), new Seconds<T>(T.One));
    #endregion Motion

    /// <summary>
    /// Standard Atmospheric Pressure.
    /// </summary>
    public static readonly T StandardAtmosphericPressure = T.CreateSaturating(1.01e11); // Pa

    #region Distances
    /// <summary>
    /// Astronomical Unit. Average Earth-Sun distance.
    /// </summary>
    public static readonly ILength<T> AstronomicalUnit = new Meters<T>(T.CreateSaturating(1.5e11));

    /// <summary>
    /// Average Earth Moon distance.
    /// </summary>
    public static readonly ILength<T> AverageEarthMoonDistance = new Meters<T>(T.CreateSaturating(3.84e8));

    /// <summary>
    /// Average radius of Sun.
    /// </summary>
    public static readonly ILength<T> AverageRadiusOfSun = new Meters<T>(T.CreateSaturating(6.96e8));

    /// <summary>
    /// Equatorial radius of Earth.
    /// </summary>
    public static readonly ILength<T> EquatorialRadiusOfEarth = new Meters<T>(T.CreateSaturating(6.37e6));

    /// <summary>
    /// Radius of Earth's orbit.
    /// </summary>
    public static readonly ILength<T> RadiusOfEarthsOrbit = AstronomicalUnit;

    /// <summary>
    /// Average radius of Moon
    /// </summary>
    public static readonly ILength<T> AverageRadiusOfMoon = new Meters<T>(T.CreateSaturating(1.74e6));

    /// <summary>
    /// Radius of Moon's orbit.
    /// </summary>
    public static readonly ILength<T> RadiusOfMoonsOrbit = new Meters<T>(T.CreateSaturating(3.84e8));

    /// <summary>
    /// Equatorial radius of Jupiter.
    /// </summary>
    public static readonly ILength<T> EquatorialRadiusOfJupiter = new Meters<T>(T.CreateSaturating(7.14e7));

    /// <summary>
    /// Radius of Hydrogen atom.
    /// </summary>
    public static readonly ILength<T> RadiusOfHydrogenAtom = new Meters<T>(T.CreateSaturating(5e-11));
    #endregion Distances

    #region Mass
    /// <summary>
    /// Mass of Sun.
    /// </summary>
    public static readonly IMass<T> MassOfSun = new Kilograms<T>(T.CreateSaturating(1.99e30));

    /// <summary>
    /// Mass of Earth.
    /// </summary>
    public static readonly IMass<T> MassOfEarth = new Kilograms<T>(T.CreateSaturating(5.98e24));

    /// <summary>
    /// Mass of Moon.
    /// </summary>
    public static readonly IMass<T> MassOfMoon = new Kilograms<T>(T.CreateSaturating(7.36e22));

    /// <summary>
    /// Mass of Jupiter.
    /// </summary>
    public static readonly IMass<T> MassOfJupiter = new Kilograms<T>(T.CreateSaturating(1.90e7));

    /// <summary>
    /// Mass of Proton.
    /// 938.27231 MeV
    /// </summary>
    public static readonly IMass<T> MassOfProton = new Kilograms<T>(T.CreateSaturating(1.6726231e-27));

    /// <summary>
    /// Mass of Neutron.
    /// 939.56563 MeV
    /// </summary>
    public static readonly IMass<T> MassOfNeutron = new Kilograms<T>(T.CreateSaturating(1.6749286e-27));

    /// <summary>
    /// Mass of Electron.
    /// 0.51099906 MeV
    /// </summary>
    public static readonly IMass<T> MassOfElectron = new Kilograms<T>(T.CreateSaturating(9.1093897e-31));
    #endregion Mass

    /// <summary>
    /// Charge of Electron.
    /// </summary>
    public static readonly T ChargeOfElectron = T.CreateSaturating(1.602e-19); // C

    /// <summary>
    /// Gravitational constant.
    /// </summary>
    public static readonly T GravitationalConstant = T.CreateSaturating(6.672759e-11); // Nm^2/kg^2

    /// <summary>
    /// A fundamental constant, h, that relates the energy of light quanta to their frequency.
    /// 4.1356692e-15 eVs
    /// </summary>
    public static readonly Joules<T> PlanksConstant = new(T.CreateSaturating(6.6260755e-34)); // s

    /// <summary>
    /// Avogadro's number
    /// </summary>
    public static readonly T AvogadroNumber = T.CreateSaturating(6.0221367e23); // mol

    /// <summary>
    /// Black body radiation constant.
    /// </summary>
    public static readonly T BlackBodyRadiationConstant = T.CreateSaturating(5.67051e-0); // W/m^2K^4
}
