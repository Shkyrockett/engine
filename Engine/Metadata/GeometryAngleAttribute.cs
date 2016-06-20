using System;

namespace Engine.Geometry
{
    /// <summary>
    /// Attribute used to mark properties as angles to later add the AngleEditor for WinForms PropertyGrid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Class|AttributeTargets.Struct, Inherited = true)]
    public class GeometryAngleAttribute
        : Attribute
    {
    }
}