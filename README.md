# Engine

An attempt to write a 2D vector based Graphical Adventure game engine from scratch, using C#, having no previous experience ever writing a game engine.

This is mostly a playground to play with new language features in preview versions of c#, and learn how to implement various algorithms. It is currently using [.NET Standard 2.0](https://www.microsoft.com/net/core/preview#windowscmd), and [.NET Framework 4.8](https://blogs.msdn.microsoft.com/dotnet/2018/10/30/announcing-net-framework-4-8-early-access-build-3673/), including the [Developer Pack](https://blogs.msdn.microsoft.com/dotnet/2018/10/30/announcing-net-framework-4-8-early-access-build-3673/), with C# 7.3, so it will only compile using Visual Studio 2017 15.7.0 Preview 5; The latest [Preview](https://www.visualstudio.com/vs/preview/) (Community, Professional, or Enterprise should all work).

Because this is a playground, APIs get refactored frequently and files get moved around frequently. As an engine this is very early pre-alpha. Classes may be there one commit and disappear the next. Methods may move from one class to another just because I didn't like where they were, and it makes more sense to put them in the new location. Methods may change the way they work, because a bug was found that needed to be squashed. 

## Acknowledgments

- Many of the geometry methods are inspired by samples from [C# Helper](http://csharphelper.com/) written by Rod Stephens.
- A portion of code for Bézier curves has been ported from the awesome information found on [A Primer on Bézier Curves](https://pomax.github.io/bezierinfo/) by Mike Kamermans (Pomax).
- Many of the Intersection methods have been ported or adapted from methods found at [kld-intersections](https://github.com/thelonious/kld-intersections) posted by Kevin Lindsey (thelonious).
- Many of the Intersection methods have also been adapted along the lines of code found at [the particleincell blog](https://www.particleincell.com/2013/cubic-line-intersection/) posted by Dr. Lubos Brieda.
- Polygon path finding based on [shortest path code](http://alienryderflex.com/shortest_path/) posted by Darel Rex Finley.
- Tweener code based on [Glide](https://bitbucket.org/jacobalbano/glide) by Jacob Albano.
- Pieces of the [Clipper](http://angusj.com/delphi/clipper.php) library by Angus Johnson have been adapted.
