# Engine

An attempt to write a 2D vector based Graphical Adventure game engine from scratch, using C#, having no previous experience ever writing a game engine.

This is mostly a playground to play with new language features in preview versions of c#, and learn how to implement various algorithms. It is currently using .NET Standard 2.0, and .NET Framework 4.7, so it will only compile with the preview builds of Visual Studio. Minus some weird troubles with Tuples.

Because this is a playground, APIs get refactored frequently. Files get moved around. As an engine this is very early pre-alpha. Classes may be there one commit and disappear the next. Methods may move from one class to another just because I didn't like where they were, and it makes more sense to put them in the new location. 

## Acknowledgments

- Many of the geometry methods are inspired by samples from [C# Helper](http://csharphelper.com/) written by Rod Stephens.
- A portion of code for Bézier curves has been ported from the awesome information found on [A Primer on Bézier Curves](https://pomax.github.io/bezierinfo/) by Pomax.
- Polygon path finding based on [code](http://alienryderflex.com/shortest_path/) posted by Darel Rex Finley.
- Tweener code based on [Glide](https://bitbucket.org/jacobalbano/glide) by Jacob Albano.
- Pieces of the [Clipper](http://angusj.com/delphi/clipper.php) library by Angus Johnson have been used.
