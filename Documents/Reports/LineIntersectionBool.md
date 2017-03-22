# Speed testing Line Intersection Bool Methods

Text explaining the intended method goes here.

### Method Header Prototype

```c#
    public static bool Intersecttion(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
```

## Results

Remarks on results go here.

| Method | Iterations | Input | Output | Time |
|:---|---:|:---:|---:|---:|
| IntersecttionV1 | 1000000 | aX: 0, aY:0 bX: 2, bY:2 <br/> cX: 2, cY:0 dX: 0, dY: 2 | Output | **xx ms** |

## Method 1:

```c#
    /// <summary>
    /// Find the intersection point between two lines.
    /// </summary>
    /// <param name="aX">The x component of the first point of the first line.</param>
    /// <param name="aY">The y component of the first point of the first line.</param>
    /// <param name="bX">The x component of the second point of the first line.</param>
    /// <param name="bY">The y component of the second point of the first line.</param>
    /// <param name="cX">The x component of the first point of the second line.</param>
    /// <param name="cY">The y component of the first point of the second line.</param>
    /// <param name="dX">The x component of the second point of the second line.</param>
    /// <param name="dY">The y component of the second point of the second line.</param>
    /// <returns>Returns the point of intersection.</returns>
    public static bool IntersecttionV1(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
    {
        // Calculate the delta vectors for the line segments.
        double dx = (bX - aX);
        double dy = (bY - aY);
        double da = (dX - cX);
        double db = (dY - cY);

        // Check if the segments are parallel.
        if ((da * dy) == (db * dx))
        {
            return false;
        }

        double s = (((dx * (cY - aY)) + (dy * (aX - cX))) / ((da * dy) - (db * dx)));
        double t = (((da * (aY - cY)) + (db * (cX - aX))) / ((db * dx) - (da * dy)));
        return ((s >= 0.0d) && (s <= 1.0d) && (t >= 0.0d) && (t <= 1.0d));
    }
```

## Method 2:

```c#
```

## Method 3:

```c#
```
