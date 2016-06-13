using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class AngleEditor
        : UITypeEditor
    {
        /// <summary>
        /// 
        /// </summary>
        private AngleControl uiDisplay;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            => UITypeEditorEditStyle.DropDown;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // ----- Prompt the user using a drop-down editor.
            IWindowsFormsEditorService useService;

            // ----- Prepare the interface to the drop-down control.
            useService = (IWindowsFormsEditorService)(provider.GetService(typeof(IWindowsFormsEditorService)));
            if (uiDisplay == null) uiDisplay = new AngleControl();
            uiDisplay.EditorService = useService;
            uiDisplay.Angle = (double)value;

            // ----- Display the drop-down portion.
            useService.DropDownControl(uiDisplay);

            // ----- Return the result.
            return uiDisplay.Angle;
        }
    }
}
