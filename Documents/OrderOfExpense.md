# Order of Expense

This is an attempt to calculate an order for which calculations are more or less expensive, to give an idea of what sorts of operations can be substituted with alternative forms to improve performance.

| Operation | Example | Time | Notes |
|---|---|---|---|
| * | `1 * 0.5` | 100000000 in 0.28 sec | Multiply is a surprisingly fast operator. |
| + | `0 + 0.5` | 100000000 in 0.29 sec | Addition is decently fast. |
| - | `1 - 0.5` | 100000000 in 0.29 sec | Subtraction is the same as Addition. |
| / | `1 / 2` | 100000000 in 0.36 sec | Multiplying by a decimal is faster than a divide. |
| Sqrt | `Sqrt(2)` | 100000000 in 0.87 sec | Sqrt isn't exceptionally slow, but should still be used sparingly.  |
| Atan2 | `Atan2(3, 4)` | 100000000 in 4.77 sec | Finding the angle of a slope using Atan2 can be slow compared to just using trig identities. |
| Sin | `Sin(Pi)` | 100000000 in 5.67 sec | Sin is fairly slow. It is best to calculate the value once and store it to use the stored value multiple times. |
| cos | `Cos(Pi)` | 100000000 in 6.17 sec | Cos is fairly slow. It is best to calculate the value once and store it to use the stored value multiple times. |
| Pow | `Pow(2, 2)` | 100000000 in 9.04 sec | Using multiplying values multiple times is much faster than using Pow. |

See Also: [Compare the performance of simple arithmetic operations in C#](http://csharphelper.com/blog/2017/05/compare-the-performance-of-simple-arithmetic-operations-in-c/)