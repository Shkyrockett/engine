// <copyright file="AngleEditor.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Engine
{
    /// <summary>
    /// The angle editor class.
    /// </summary>
    public class AngleEditor
        : UITypeEditor, IDisposable
    {
        /// <summary>
        /// The ui display.
        /// </summary>
        private AngleControl uiDisplay;

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Get the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="UITypeEditorEditStyle"/>.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            => UITypeEditorEditStyle.DropDown;

        /// <summary>
        /// The edit value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // ----- Prompt the user using a drop-down editor.
            IWindowsFormsEditorService useService;

            // ----- Prepare the interface to the drop-down control.
            useService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (uiDisplay is null)
            {
                uiDisplay = new AngleControl();
            }

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
                {
                    if (uiDisplay != null)
                    {
                        uiDisplay.Dispose();
                    }
                }

                // ToDo: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // ToDo: set large fields to null.
                disposedValue = true;
            }
        }

        // ToDo: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AngleEditor() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        /// <summary>
        /// Releases all resources used by the System.ComponentModel.Component.
        /// </summary>
        /// <remarks><para>This code added to correctly implement the disposable pattern.</para></remarks>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // ToDo: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support
    }
}
