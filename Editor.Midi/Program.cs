// <copyright file="Program.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using EventEditorMidi;

namespace Editor.Midi;

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
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using var formMidiEventEditor = new FormMidiEventEditor();
        Application.Run(formMidiEventEditor);
    }
}
