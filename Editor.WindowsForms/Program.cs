﻿// <copyright file="Program.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.WindowsForms;

namespace Editor;

/// <summary>
/// The program class.
/// </summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        WinformsReflection.Tickle();
        ApplicationConfiguration.Initialize();
        using var editorForm = new EditorForm();
        Application.Run(editorForm);
        //Application.Run(new Direct2DForm());
    }
}
