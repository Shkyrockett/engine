# Interaction Methods Specification

There are various methods of interacting with shapes and the types of information you would want to get back from the shape, and for what purpose.

## Primitive Structures  

The geometrical calculations use the following basic primitives:

### Mathematical Primitives  

- [x] **Polynomial** A mathematical primitive used to store the polynomial function of a polynomial based shape. Used mostly with Bézier curve calculations.
- [x] **2x2 Matrix**
- [x] **3x3 Matrix**
- [x] **4x4 Matrix**
- [x] **2x3 Translation Matrix** A 2x2 matrix with a location.
- [x] **2D Vector** A direction and distance in two dimensional Euclidean space.
- [x] **3D Vector** A direction and distance in three dimensional Euclidean space.
- [x] **4D Vector** A direction and distance in four dimensional Euclidean space.

### Geometric Primitives  

- [x] **2D Point** The two dimensional primitive Euclidean geometry coordinate point.
- [x] **3D Point** The three dimensional primitive Euclidean geometry coordinate point.
- [ ] **4D Point** The four dimensional primitive Euclidean geometry coordinate point.
- [x] **3D Orientation** Roll, Pitch and Yaw.
- [x] **Quaternion** 
- [x] **2D Size** The height and width of an object in relative space.
- [x] **3D Size** The height, width, and depth of an object in relative space.
- [ ] **4D Size** The height, width, depth, and w of an object in relative space.
- [x] **2D Transform** A Rotation and Translation Matrix for 2D.
- [x] **3D Transform** A Rotation and Translation Matrix for 3D.
- [ ] **4D Transform** A Rotation and Translation Matrix for 4D.
- [x] **2D Accumulator Point** A point that also includes an accumulation of the length of previous points.
- [x] **3D Accumulator Point** A point that also includes an accumulation of the length of previous points.
- [ ] **4D Accumulator Point** A point that also includes an accumulation of the length of previous points.

## Shapes

The following 2D shapes should be renderable.

- [x] **Point** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <circle cx="15" cy="15" r="5" style="stroke:blue" />
    <g>
      <line x1="15" y1="5" x2="15" y2="25" style="stroke:red" />
      <line x1="5" y1="15" x2="25" y2="15" style="stroke:red" />
    </g>
  </g>
</svg>
- [x] **Line** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="0" x2="20" y2="25" style="stroke:blue" />
  </g>
</svg>
- [x] **Ray** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="10" x2="25" y2="15" style="stroke:blue" />
  </g>
</svg>
- [x] **Bezier Segment** 
- [x] **Line Segment** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="5" x2="25" y2="25" style="stroke:blue" />
  </g>
</svg>
- [x] **Quadratic Bezier** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <path d="M1,1 q11,55 21,-21" style="stroke:blue" />
  </g>
</svg>
- [x] **Cubic Bezier** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <path d="M11,1 c-11,11 21,11 -10,20" style="stroke:blue" />
  </g>
</svg>
- [ ] **PolyBezier Contour** A Polyline like structure that is constructed out of Bézier curves of various degrees. Generally a start point, Bezier line segments, Quadratic curves, and Cubic curves.
- [ ] **PolyBezier** A set of PolyBezier Contours.
- [x] **Triangle** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <polygon points="10,0  0,23 23,23" style="stroke:blue" />
  </g>
</svg> _May be implemented through Polygon._
- [x] **Rectangle** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <rect x="1" y="1" width="23" height="23" style="stroke:blue" />
  </g>
</svg>
- [ ] **Rotated Rectangle** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-1 9) rotate(-30)">
    <rect x="1" y="1" width="17" height="17" style="stroke:blue" />
  </g>
</svg> _Need to think through location issues._
- [x] **Polyline** An open set of line segments. _Stored as Points._
- [x] **Polygon Contour** A closed set of line segments. _Stored as Points._
- [ ] **Polygon** A set of Polygon Contours.
- [x] **Circle** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <circle cx="15" cy="15" r="9" stroke="green" style="stroke:blue" /></g></svg> _May be implemented trough Ellipse._
- [x] **Circular Arc** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-5 9) rotate(-30)">
    <path d = "M1,11 a10,10 30 1 1 10 16" style="stroke:blue" />
  </g>
</svg> _May be implemented through Rotated Elliptical Arc._
- [x] **Orthogonal Ellipse** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <ellipse cx="15" cy="15" rx="9" ry="5" style="stroke:blue" />
  </g> _May be implemented through Rotated Ellipse._
</svg>
- [x] **Rotated Ellipse** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-5 9) rotate(-30)">
    <ellipse cx="15" cy="15" rx="9" ry="5" style="stroke:blue" />
  </g>
</svg>
- [x] **Orthogonal Elliptical Arc** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-5 9) rotate(-45)">
    <path d = "M1,11 a7,15 45 1 1 10 11" style="stroke:blue" />
  </g> _May be implemented through Rotated Elliptical Arc._
</svg>
- [x] **Rotated Elliptical Arc** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-5 9) rotate(-30)">
    <path d = "M1,11 a7,15 45 1 1 10 11" style="stroke:blue" />
  </g>
</svg>
- [x] **Polycurve Contour** A combination of the previous open shapes.
- [ ] **Polycurve** A set of Polycurve Contours or other closed shapes.
- [ ] **Parametric Delegate Curve** _Mostly used for testing._

## Area

- [ ] **Point** _Always 0._
- [ ] **Line** _Always 0._
- [ ] **Ray** _Always 0._
- [ ] **Bezier Segment**
- [ ] **Line Segment**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [x] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [x] **Polyline**
- [x] **Polygon Contour**
- [ ] **Polygon**
- [x] **Circle**
- [x] **Circular Arc**
- [x] **Orthogonal Ellipse**
- [x] **Rotated Ellipse**
- [x] **Orthogonal Elliptical Arc**
- [x] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**
- [ ] **Parametric Delegate Curve**

## double Length

- [x] **Point** _Always 0._
- [ ] **Line** _Always Infinity._
- [ ] **Ray** _Always Infinity._
- [x] **Bezier Segment**
- [x] **Line Segment**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour** _Perimeter._
- [ ] **Polygon**
- [x] **Circle** _Circumference._
- [x] **Circular Arc**
- [x] **Orthogonal Ellipse** _Elliptical Circumference._
- [x] **Rotated Ellipse**
- [x] **Orthogonal Elliptical Arc**
- [x] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**

## Rectangle Bounds

- [ ] **Point** _Empty Rectangle at Point's location._
- [ ] **Line** _Undefined?_
- [ ] **Ray** _Undefined?_
- [x] **Bezier Segment**
- [x] **Line Segment** _Always the two ends._
- [x] **Quadratic Bezier** _Use Bezier Segment._
- [x] **Cubic Bezier** _Use Bezier Segment._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [x] **Polyline**
- [x] **Polygon Contour**
- [ ] **Polygon**
- [x] **Circle**
- [ ] **Circular Arc** _Broken_
- [x] **Orthogonal Ellipse**
- [x] **Rotated Ellipse**
- [x] **Orthogonal Elliptical Arc**
- [x] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**
- [x] **Parametric Delegate Curve**

## double Distance(Point)

- [x] **Point**
- [x] **Line**
- [ ] **Ray**
- [x] **Bezier Segment**
- [x] **Line Segment**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [ ] **Circle**
- [ ] **Circular Arc**
- [ ] **Orthogonal Ellipse**
- [ ] **Rotated Ellipse**
- [ ] **Orthogonal Elliptical Arc**
- [ ] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**

## bool NearestPoint(Point)

- [x] **Point**
- [ ] **Line**
- [ ] **Ray**
- [ ] **Bezier Segment**
- [x] **Line Segment**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [ ] **Circle**
- [ ] **Circular Arc**
- [ ] **Orthogonal Ellipse**
- [ ] **Rotated Ellipse**
- [ ] **Orthogonal Elliptical Arc**
- [ ] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**

## bool NearestT(t)

- [ ] **Point**
- [ ] **Line**
- [ ] **Ray**
- [x] **Bezier Segment**
- [ ] **Line Segment**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [ ] **Circle**
- [ ] **Circular Arc**
- [ ] **Orthogonal Ellipse**
- [ ] **Rotated Ellipse**
- [ ] **Orthogonal Elliptical Arc**
- [ ] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**

## Interaction Methods

| Method | Return Type | Description |
|---|---|---|
| Contains | Inclusion | Provides information regarding whether one shape contains the other. Because of this, only closed shapes can contain another shape. |
| Intersects | Boolean | Provides information regarding whether two shapes intersect. |
| Intersection | (Point[], State) | Provides information as to the location and nature of the intersection, provided two shapes intersect. |
| Parametrized Intersection | (double[], double[]) | Provides the Parametrized t for iteration of each shape where the shapes intersect. The intended purpose is to help with splitting shapes. |
| Scan-beam Intersection | double[] | Provides a list of t, where the intersections occur on a horizontal scan-beam line. |
| Scan-beam To Left Intersection | int | Provides a count of the number of intersections to the left of a point on a scan-beam cross section of a shape. |
| Scan-beam To Right Intersection | int | Provides a count of the number of intersections to the right of a point on a scan-beam cross section of a shape. |

### Inclusion Contains(Point)

- [x] **Point**
- [ ] **Line**
- [ ] **Ray**
- [ ] **Bezier Segment**
- [x] **Line Segment**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [x] **Polygon Contour**
- [x] **Polygon**
- [x] **Circle**
- [x] **Circular Arc**
- [x] **Ellipse**
- [x] **Elliptical Arc**
- [ ] **Polycurve Contour** _Implementation is buggy._
- [ ] **Polycurve**
- [ ] **Parametric Delegate Curve**

### bool Contains(Rectangle)

- [ ] **Point**
- [ ] **Line**
- [ ] **Ray**
- [ ] **Bezier Segment**
- [ ] **Line Segment**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [ ] **Circle**
- [ ] **Circular Arc**
- [ ] **Ellipse**
- [ ] **Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**
- [ ] **Parametric Delegate Curve**

### bool Intersects(Shape)

Intersects methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

| **Intersects** | **Point** | **Line** | **Ray** | **Line Segment** | **Quadratic Bezier** | **Cubic Bezier** | **Triangle** | **Rectangle** | **Polyline** | **Polygon Contour** | **Circle** | **Circular Arc** | **Orthogonal Ellipse** | **Rotated Ellipse** | **Orthogonal Elliptical Arc** | **Rotated Elliptical Arc** | **Polycurve Contour** |
|:----------------------:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point**                     | ✔ | ❌ | ✔ | ✔ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Line**                      | ❌ | ✔ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Ray**                       | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Line Segment**              | ✔  | ❌ | ❌ | ✔ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Quadratic Bezier**          | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Cubic Bezier**              | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Triangle**                  | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Rectangle**                 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✔ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Polyline**                  | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Polygon Contour**           | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Circle**                    | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✔ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Circular Arc**              | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Orthogonal Ellipse**        | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Rotated Ellipse**           | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Orthogonal Elliptical Arc** | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Rotated Elliptical Arc**    | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Polycurve Contour**         | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |

### Intersection Intersection(Shape)

Intersection methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

| **Intersections** | **Point** | **Line** | **Ray** | **Line Segment** | **Quadratic Bezier** | **Cubic Bezier** | **Triangle** | **Rectangle** | **Polyline** | **Polygon Contour** | **Circle** | **Circular Arc** | **Orthogonal Ellipse** | **Rotated Ellipse** | **Orthogonal Elliptical Arc** | **Rotated Elliptical Arc** | **Polycurve Contour** |
|:-----------------------|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point**                    | ✔  | ✔  | ✔  | ✔  | ❌ | ❌ | ✔  | ✔  | ✔  | ✔  | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Line**                     | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ❌ |
| **Ray**                      | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ❌ |
| **Line Segment**             | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ❌ |
| **Quadratic Bezier**         | ❌ | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ❌ | ❌ | ❌ | ❌ |
| **Cubic Bezier**             | ❌ | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ❌ | ❌ | ❌ | ❌ |
| **Triangle**                 | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Rectangle**                | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Polyline**                 | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Polygon Contour**          | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Circle**                   | ❌ | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Circular Arc**             | ❌ | ✔  | ✔  | ✔  | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Orthogonal Ellipse**       | ❌ | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Rotated Ellipse**          | ❌ | ✔  | ✔  | ✔  | ❌ | ❌ | ✔  | ✔  | ✔  | ✔  | ✔  | ❌ | ✔  | ✔  | ❌ | ❌ | ❌ |
| **Orthogonal Elliptical Arc**| ❌ | ✔  | ✔  | ✔  | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Rotated Elliptical Arc**   | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| **Polycurve Contour**        | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |

#### Intersections Signatures

```c#
public static (double x, double y)[] PointPointIntersections(
  (double x, double y) point1, /* Point */
  (double x, double y) point2, /* Point */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointLineIntersections(
  (double x, double y) point, /* Point */
  (double x, double y, double i, double j) line, /* Line */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointRayIntersections(
  (double x, double y) point, /* Point */
  (double x, double y, double i, double j) ray, /* Ray */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointLineSegmentIntersections(
  (double x, double y) point, /* Point */
  (double x1, double y1, double x2, double y2) segment, /* Line Segment */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointQuadraticBezierSegmentIntersections(
  (double x, double y) point, /* Point */
  (double ax, double ay, double bx, double by, double cx, double cy) segment, /* Quadratic Bezier Segment */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointCubicBezierSegmentIntersections(
  (double x, double y) point, /* Point */
  (double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy) segment, /* Cubic Bezier Segment */
  double epsilon = Double.Epsilon);
```

```c#
public static (double x, double y)[] PointTriangleIntersections(
  (double x, double y) point, /* Point */
  (double ax, double ay, double bx, double by, double cx, double cy) polygon, /* Triangle */
  double epsilon = Double.Epsilon);
```

## Split(t)

- [x] **Point** _Needs testing._
- [ ] **Line**
- [ ] **Ray**
- [x] **Bezier Segment** _Needs testing._
- [x] **Line Segment** _Needs testing._
- [x] **Quadratic Bezier** _Needs testing._
- [x] **Cubic Bezier** _Needs testing._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [x] **Circle** _Needs testing._
- [x] **Circular Arc** _Needs testing._
- [x] **Orthogonal Ellipse** _Needs testing._
- [x] **Rotated Ellipse**
- [x] **Orthogonal Elliptical Arc** _Needs testing._
- [x] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**
- [ ] **Parametric Delegate Curve**

## Split(t[])

- [x] **Point** _Needs testing._
- [ ] **Line**
- [ ] **Ray**
- [x] **Bezier Segment** _Needs testing._
- [x] **Line Segment** _Needs testing._
- [x] **Quadratic Bezier** _Needs testing._
- [x] **Cubic Bezier** _Needs testing._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline**
- [ ] **Polygon Contour**
- [ ] **Polygon**
- [x] **Circle** _Needs testing._
- [x] **Circular Arc** _Needs testing._
- [x] **Orthogonal Ellipse** _Needs testing._
- [x] **Rotated Ellipse**
- [x] **Orthogonal Elliptical Arc** _Needs testing._
- [x] **Rotated Elliptical Arc**
- [ ] **Polycurve Contour**
- [ ] **Polycurve**
- [ ] **Parametric Delegate Curve**
