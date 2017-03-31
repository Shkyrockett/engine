# Interesting and Clever Coding Found While Researching

This is a collection of interesting and clever code snippets to simply solve complex issues.

### Process every line segment of an open point chain, excluding end to start.

For an open polygon, the number of connecting segments is ![][n-1], or one less than the number of points in the collection. So you can start your index at one, then subtract one to get the zero point, and use the current index as the next point for the side.

```c#
if (points.Count < 1)
    result = ProcessSegment(points[0], points[0]);
for (int i = 1; i < points.Count; i++)
{
    result = ProcessSegment(points[i - 1], points[i]);
}
```

### Process every line segment of a closed point chain, including the end to start.

For a closed polygon, the number of sides are the same as the number of points in the collection. However, you have to wrap back to the start for the final side. This can be cleverly solved by finding the mod of the next point to the number of points, which for the final point; will end up wrapping back to zero.  
This method avoids out of bounds errors on either side of the list by wrapping.

```c#
for (int i = 0; i < points.Count; i++)
{
    result = ProcessSegment(points[i], points[(i + 1) % points.Count]);
}
```

The "%" modulus operator can be a little expensive. So, modifying the algorithm to filter out all, but the last for the wrap can bring a little speed back.

```c#
for (int i = 0; i < points.Length; i++)
{
    result = (i < points.Length - 1)? ProcessSegment(points[i], points[i + 1]): ProcessSegment(points[i], points[(i + 1) % points.Length]);
}
```

While using the "%" Modulus operator is clever, and an if speeds it up, it is still executing an if statement for every iteration through the loop. So, it is still faster to process the loop as if open, then append the final side to the end.  
However, the number of points in the list processed must be two or more, or you will encounter an overflow error. To match the same results, you would have to alternatively process the special case for indexes less than one.

```c#
if (points.Count < 1)
    result = ProcessSegment(points[0], points[0]);
for (int i = 1; i < points.Length; i++)
{
    result = ProcessSegment(points[i - 1], points[i]);
}
result = ProcessSegment(points[points.Length - 1], points[0]);
```

A more traditional method for solving this issue is using cursors. This one uses an if at the end.

```c#
var curPoint = points[0];
for (var i = 1; i <= points.Count; ++i)
{
    var nextPoint = (i == points.Count ? points[0] : points[i]);
    result = ProcessSegment(curPoint, nextPoint);
}
```

The final method just works, however is still a tad slow, so long as you don't care about the order of the segments.

```c#
for (int i = points.Length - 1, j = 0; j < points.Length; i = j++)
{
    result = ProcessSegment(points[i], points[j]);
}
```

[n-1]: http://latex.codecogs.com/svg.latex?%5Cinline%20n-1
