using Engine.Geometry;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using static Engine.EngineReflection;

namespace Engine.Winforms
{
    /// <summary>
    /// 
    /// </summary>
    public static class WinformsReflection
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Attribute angleEditorAttribute = new EditorAttribute(typeof(AngleEditor), typeof(UITypeEditor));
        
        /// <summary>
        /// 
        /// </summary>
        static WinformsReflection()
        {
            TypeDescriptor.AddAttributes(typeof(StringCollection), new EditorAttribute("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = b03f5f7f11d50a3a", typeof(UITypeEditor)));

            ReplacePropertyAttribute(typeof(Ellipse), new GeometryAngleAttribute(), angleEditorAttribute);
            ReplacePropertyAttribute(typeof(ParametricDelegateCurve), new GeometryAngleAttribute(), angleEditorAttribute);
        }

        /// <summary>
        /// Touch the <see cref="WinformsReflection"/> class so the static constructor initializes.
        /// </summary>
        public static void Tickle()
        {
        }
    }
}
