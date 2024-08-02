// <copyright file="Lunar.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using static System.Math;

namespace Engine.Chrono;

/// <summary>
/// The lunar class.
/// </summary>
public static class Lunar
{
    /// <summary>
    /// http://www.codeproject.com/Articles/100174/Calculate-and-Draw-Moon-Phase
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    private static int JulianDate(int year, int month, int day)
    {
        int mm, yy;
        int k1, k2, k3;
        int j;

        yy = year - ((12 - month) / 10);
        mm = month + 9;
        if (mm >= 12)
        {
            mm -= 12;
        }

        k1 = (int)(365.25 * (yy + 4712));
        k2 = (int)((30.6001 * mm) + 0.5);
        k3 = (int)(((yy / 100) + 49) * 0.75) - 38;
        // 'j' for dates in Julian calendar:
        j = k1 + k2 + day + 59;
        if (j > 2299160)
        {
            // For Gregorian calendar:
            j -= k3; // 'j' is the Julian date at 12h UT (Universal Time)
        }
        return j;
    }

    /// <summary>
    /// http://www.codeproject.com/Articles/100174/Calculate-and-Draw-Moon-Phase
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public static double MoonAge(int year, int month, int day)
    {
        var j = JulianDate(year, month, day);
        // Calculate the approximate phase of the moon
        var ip = (j + 4.867) / 29.53059;
        ip -= Floor(ip);
        // After several trials I've seen to add the following lines,
        // which gave the result was not bad
        var ag = ip < 0.5 ? (ip * 29.53059) + (29.53059 / 2) : (ip * 29.53059) - (29.53059 / 2);
        // Moon's age in days
        ag = Floor(ag) + 1;
        return ag;
    }

    /// <summary>
    /// calculates the moon phase (0-7), accurate to 1 segment.
    /// 0 = > new moon.
    /// 4 => full moon.
    /// http://www.voidware.com/moon_phase.htm
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public static int MoonPhase(int year, int month, int day)
    {
        int c, e;
        double jd;
        int b;

        if (month < 3)
        {
            year--;
            month += 12;
        }

        ++month;
        c = (int)Round(365.25 * year);
        e = (int)Round(30.6 * month);
        jd = c + e + day - 694039.09;  /* jd is total days elapsed */
        jd /= 29.53;           /* divide by the moon cycle (29.53 days) */
        b = (int)Round(jd);		   /* int(jd) -> b, take integer part of jd */
        jd -= b;		   /* subtract integer part to leave fractional part of original jd */
        b = (int)Round((jd * 8) + 0.5);	   /* scale fraction from 0-8 and round by adding 0.5 */
        b &= 7;		   /* 0 and 8 are the same so turn 8 into 0 */
        return b;
    }
}
