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
        : UITypeEditor, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private AngleControl uiDisplay;

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

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

        #region IDisposable Support

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="AngleEditor"/> 
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    uiDisplay?.Dispose();

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AngleEditor() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        /// <summary>
        /// Releases all resources used by the System.ComponentModel.Component.
        /// </summary>
        /// <remarks>This code added to correctly implement the disposable pattern.</remarks>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
