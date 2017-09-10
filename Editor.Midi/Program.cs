// <copyright file="Program.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Windows.Forms;
using EventEditorMidi;
using Engine;

namespace Editor.Midi
{
    /// <summary>
    /// The main entry class for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Tickle reflection to get it to change attributes for WinForms.
            EngineWinformsReflection.Tickle();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMidiEventEditor());
        }
    }
}
