# Speed testing Line Intersection Point Methods

Text explaining the intended method goes here.

### Method Header Prototype

```c#
    public static Point2D Intersecttion(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
```

## Results

Remarks on results go here.

| Method | Iterations | Input | Output | Time |
|:---|---:|:---:|---:|---:|
| IntersecttionV1 | 1000000 | aX: 0, aY:0 bX: 2, bY:2 <br/> cX: 2, cY:0 dX: 0, dY: 2 | Output | **xx ms** |
| IntersecttionV2 | 1000000 | Input | Output | Time ms |

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
    public static Point2D IntersecttionV1(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
    {
        // Calculate the delta vectors for the line segments.
        double dx = (bX - aX);
        double dy = (bY - aY);
        double da = (dX - cX);
        double db = (dY - cY);

        // Check if the segments are parallel.
        if ((da * dy) == (db * dx))
        {
            return null;
        }

        double t = (((da * (aY - cY)) + (db * (cX - aX))) / ((db * dx) - (da * dy)));

        // If it exists, the point of intersection is:
        return new Point2D(aX + t * dx, aY + t * dy);
    }
```

## Method 2:

```c#
```

## Method 3:

```c#
```
