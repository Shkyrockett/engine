#Speed testing Line Intersection Point Methods


## Results


| Iterations | Values | V1 | 2 | 3 |
|---:|:---:|---:|---:|---:|
| 1000000 | aX: 0, aY:0 bX: 2, bY:2 <br/> cX: 2, cY:0 dX: 0, dY: 2 | **xx ms** | xx ms | xx ms

## Method 1: 

```
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

```
```

## Method 3:

```
```
