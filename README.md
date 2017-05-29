# Engine

An attempt to write a 2D vector based Graphical Adventure game engine from scratch, using C#, having no previous experience ever writing a game engine.

This is mostly a playground to play with new language features in preview versions of c#, and learn how to implement various algorithms. It is currently using [.NET Core 2.0 Preview 1](https://www.microsoft.com/net/core/preview#windowscmd), and [.NET Framework 4.7](https://www.microsoft.com/en-us/download/details.aspx?id=55170), so it will only compile using [Visual Studio Preview](https://www.visualstudio.com/vs/preview/) (Community, Professional, or Enterprise should all work).

Because this is a playground, APIs get refactored frequently and files get moved around frequently. As an engine this is very early pre-alpha. Classes may be there one commit and disappear the next. Methods may move from one class to another just because I didn't like where they were, and it makes more sense to put them in the new location. Methods may change the way they work, because a bug was found that needed to be squashed. 

## Acknowledgments

- Many of the geometry methods are inspired by samples from [C# Helper](http://csharphelper.com/) written by Rod Stephens.
- A portion of code for Bézier curves has been ported from the awesome information found on [A Primer on Bézier Curves](https://pomax.github.io/bezierinfo/) by Pomax.
- Polygon path finding based on [code](http://alienryderflex.com/shortest_path/) posted by Darel Rex Finley.
- Tweener code based on [Glide](https://bitbucket.org/jacobalbano/glide) by Jacob Albano.
- Pieces of the [Clipper](http://angusj.com/delphi/clipper.php) library by Angus Johnson have been used.
