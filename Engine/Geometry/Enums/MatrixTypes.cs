using System;

namespace Engine.Geometry
{
    /// <summary>
    /// MatrixTypes
    /// </summary>
    /// <remarks>http://referencesource.microsoft.com</remarks>
    [Flags]
    internal enum MatrixTypes
    {
        IDENTITY = 0,
        TRANSLATION = 1,
        SCALING = 2,
        UNKNOWN = 4
    }
}
