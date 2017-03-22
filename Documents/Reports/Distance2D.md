# Speed testing 2D Distance Methods

The distance formula is based on the Pythagorean theorem ![][PythagoreanTheorem.Inline] where c is the length of the hypotenuse. 
Thus ![][Distance.Inline].

### Method Header Prototype

```c#
    public static (Type r1, Type r2) Name(Type p1, Type p2)
```

## Results

Surprisingly, caching the I and J vector delta values slows processing a little. There must be a bit of an allocation penalty for creating the double value variables to store the values. 
Calling the outside Modulus method adds even more overhead.

| Method | Iterations | Input | Output | Time |
|:---|---:|:---:|---:|---:|
| Unrolled | 1000000 | aX: 2, aY:2, bX, bY | Output | **16 ms** |
| Caching | 1000000 | aX: 2, aY:2, bX, bY | Output | 19 ms |
| Modulus | 1000000 | aX: 2, aY:2, bX, bY | Output | 21 ms |

## Method 1: Fully Unrolled

```c#
    /// <summary>
    /// Distance between two points.
    /// </summary>
    /// <param name="aX">First X component.</param>
    /// <param name="aY">First Y component.</param>
    /// <param name="bX">Second X component.</param>
    /// <param name="bY">Second Y component.</param>
    /// <returns>The distance between two points.</returns>
    public static double DistanceUnrolled(double aX, double aY, double bX, double bY)
    {
        return Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY));
    }
```

## Method 2: Caching the Vector Components

```c#
    /// <summary>
    /// Distance between two points.
    /// </summary>
    /// <param name="aX">First X component.</param>
    /// <param name="aY">First Y component.</param>
    /// <param name="bX">Second X component.</param>
    /// <param name="bY">Second Y component.</param>
    /// <returns>The distance between two points.</returns>
    public static double DistanceIJCaching(double aX, double aY, double bX, double bY)
    {
        double i = (bX - aX);
        double j = (bY - aY);
        return Math.Sqrt(i * i + j * j);
    }
```

## Method 3: Using Vector Modulus Method

```c#
    /// <summary>
    /// Distance between two points.
    /// </summary>
    /// <param name="aX">First Point X component.</param>
    /// <param name="aY">First Point Y component.</param>
    /// <param name="bX">Second Point X component.</param>
    /// <param name="bY">Second Point Y component.</param>
    /// <returns>The distance between two points.</returns>
    public static double DistanceModulus(double aX, double aY, double bX, double bY)
    {
        return Maths.Modulus(bX - aX, bY - aY);
    }

    /// <summary>
    /// Modulus of a Vector.
    /// </summary>
    /// <param name="i">I vector component.</param>
    /// <param name="j">J vector component.</param>
    /// <returns>The modulus of a vector.</returns>
    public static double Modulus(double i, double j)
    {
        return Math.Sqrt((i * i) + (j * j));
    }
```

[PythagoreanTheorem.Inline]: http://latex.codecogs.com/svg.latex?\inline&space;c^{2}&space;=&space;a^{2}&space;&plus;&space;b^{2}
[Distance.Inline]: http://latex.codecogs.com/svg.latex?\inline&space;c&space;=&space;\sqrt{a^{2}&space;&plus;&space;b^{2}}