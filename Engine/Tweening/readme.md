Glide is a super-simple tweening library for C#.

## Installation
 1. Copy all of the .cs files into your project folder.
 2. There is no step two.

## Use
Create a Tweener instance and use it to manage tweens:
    
```c#
var tweener = new Tweener();
tweener.Tween(...);
```

Every frame, update the tweener.

```c#
tweener.Update(ElapsedSeconds);
```

### Tweening
Tweening properties is done with a call to Tween. Pass the object to tween, an [anonymous type][1] instance containing value names and target values, and a duration, with an optional delay.

```c#
// This tween will move the X and Y properties of the target
tweener.Tween(target, new { X = toX, Y = toY }, duration, delay);
```
You can also use Glide to set up timed callbacks.

```c#
tweener.Timer(duration, delay).OnComplete(CompleteCallback);
```

### Control
If you need to control tweens after they are launched (for example, pausing or cancelling), you can hold a reference to the object returned By Tween():

```c#
var myTween = tweener.Tween(object, new {X = toX, Y = toY}, duration);
```

You can later use this object to control the tween:
    
```c#
myTween.Cancel();
myTween.CancelAndComplete();

myTween.Pause();
myTween.PauseToggle();

myTween.Resume();
```

Calling a control method on a tweener will affect all tweens it manages.

If you'd rather not keep tween controller objects around, you can also control them by passing an object being tweened to a target control function.

```c#

tweener.Tween(myObject, ...);

tweener.TargetCancel(myObject);
tweener.TargetCancelAndComplete(myObject);

tweener.TargetPause(myObject);
tweener.TargetPauseToggle(myObject);

tweener.TargetResume(myObject);
```

### Behavior
You can specify a number of special behaviors for a tween to use. Calls can be chained for setting more than one at a time.

```c#
// Glide comes with a full complement of easing functions
tweener.Tween(...).Ease(Ease.ElasticOut);

tweener.Tween(...).OnComplete(() => Console.WriteLine("done"));
tweener.Tween(...).OnUpdate(() => Console.WriteLine("updating"));

// Repeat twice
tweener.Tween(...).Repeat(2);

// Repeat forever
tweener.Tween(...).Repeat();

// Reverse the tween every other time it repeats
tweener.Tween(...).Repeat().Reflect();

// Swaps the end and start values of a tween.
// This is helpful if you want to set an object's properties to one set of values, and then tween back to the previous values.
tweener.Tween(...).Reverse();

// Smoothly interpolate a rotation value past the end of an axis.
tweener.Tween(...).Rotation();

// Round tweened properties to integer values
tweener.Tween(...).Round();
```

If you have any questions, find a bug, or want to request a feature, leave a message here or hit me up on Twitter [@jacobalbano][2]!

[1]: http://msdn.microsoft.com/en-us/library/vstudio/bb397696.aspx
[2]: http://www.twitter.com/jacobalbano
[3]: https://www.reddit.com/r/gamedev/comments/1fabdh/