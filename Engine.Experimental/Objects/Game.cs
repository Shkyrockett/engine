// <copyright file="Game.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// Root game object.
/// </summary>
public class Game
    : IGameElement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class.
    /// </summary>
    public Game()
    {
        Name = nameof(Game);
        DisplayName = nameof(Game);
        Scenes = [];
        Actors = [];
        ActorsDialog = [];
        Audio = [];
        Graphics = [];
        Variables = [];
    }

    /// <summary>
    /// Gets or sets the name of the game.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the display name for the editor designer.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the list of scenes 
    /// </summary>
    public List<Scene> Scenes { get; set; }

    /// <summary>
    /// Gets or sets the actors.
    /// </summary>
    public List<Actor> Actors { get; set; }

    /// <summary>
    /// Gets or sets the actors dialog.
    /// </summary>
    public List<ActorDialog> ActorsDialog { get; set; }

    /// <summary>
    /// Gets or sets the audio.
    /// </summary>
    public List<IAudio> Audio { get; set; }

    /// <summary>
    /// Gets or sets the graphics.
    /// </summary>
    public List<IBitmap> Graphics { get; set; }

    /// <summary>
    /// Gets or sets the variables.
    /// </summary>
    public Dictionary<string, object> Variables { get; set; }
}
