#Speed testing Modulus Methods

The Modulus, or Magnitude of a vector is the size of the vector. The bottleneck of this particular method, is getting the Square root. There are two main ways to get it, raise the result to the power of 1/2, or use the built in Square root function. 

## Results

It looks like Math.Pow is rather slow. Math.Sqrt runs comparably quicker in the tests.

| Iterations | Values | Math.Sqrt | Math.Pow |
|---:|:---:|---:|---:|
| 1000000 | i: 2, j:2 | **14 ms** | 94 ms |

## Method 1: Using Math.Sqrt

```c#
        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i">I vector component.</param>
        /// <param name="j">J vector component.</param>
        /// <returns>The modulus of a vector.</returns>
        public static double ModulusSqrt(double i, double j)
        {
            return Math.Sqrt((i * i) + (j * j));
        }
```

## Method 2: Raising Math.Pow to 1/2

```c#
        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i">I vector component.</param>
        /// <param name="j">J vector component.</param>
        /// <returns>The modulus of a vector.</returns>
        public static double ModulusPow(double i, double j)
        {
            return Math.Pow((i * i) + (j * j), 0.5d);
        }
```
