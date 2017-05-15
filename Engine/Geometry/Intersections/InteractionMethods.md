# Uniform Interaction Methods Specification

## Primitive Structures

The geometrical calculations use the following basic primitives:

### Mathmatical Primitives 

- [x] **Polynomial** A mathematical primitive used to store the polynomial function of a polynomial based shape. Used mostly with Bezier curve calculations.
- [x] **2x2 Matrix**
- [x] **3x3 Matrix**
- [x] **4x4 Matrix**
- [x] **2x3 Translation Matrix** A 2x2 matrix with a location.

### Geometric Primitives 

- [x] **2D Point** The two dimensional primitive Euclidean geometry coordinate point.
- [x] **3D Point** The three dimensional primitive Euclidean geometry coordinate point.
- [ ] **4D Point** The four dimensional primitive Euclidean geometry coordinate point.
- [x] **2D Vector** A direction and distance in two dimensional Euclidean space.
- [x] **3D Vector** A direction and distance in three dimensional Euclidean space.
- [x] **4D Vector** A direction and distance in four dimensional Euclidean space.
- [x] **3D Orientation** Roll, Pitch and Yaw.
- [x] **Quaternion** 
- [x] **2D Size** The height and width of an object in relative space.
- [x] **3D Size** The height, width, and depth of an object in relative space.
- [ ] **4D Size** The height, width, depth, and w of an object in relative space.
- [x] **2D Transform**
- [x] **3D Transform**
- [x] **2D Accumulator Point**
- [x] **3D Accumulator Point**

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
- [ ] **Line** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="0" x2="20" y2="25" style="stroke:blue" />
  </g>
</svg>
- [ ] **Ray** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="10" x2="25" y2="15" style="stroke:blue" />
  </g>
</svg>
- [x] **Line Segment** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <line x1="5" y1="5" x2="25" y2="25" style="stroke:blue" />
  </g>
</svg>
- [ ] **Triangle** <svg width="25" height="25">
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
- [x] **Polyline Contour**
- [ ] **Polyline Set** _Needs testing._
- [x] **Polygon Contour**
- [ ] **Polygon Set** _Needs testing._
- [x] **Polycurve Contour** _Needs work._
- [ ] **Polycurve Set** _Needs work._
- [ ] **PolyBezier** A Polyline like structure that is constructed out of Bezier curves of various degrees. Generally a start point, Bezier line segments, Quadratic curves, and Cubic curves.
- [ ] **PolyBezier Set**
- [x] **Circle** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;">
    <circle cx="15" cy="15" r="9" stroke="green" style="stroke:blue" />
  </g>
</svg>
- [x] **Ellipse** <svg width="25" height="25">
  <g style="fill:transparent;stroke-width:1;" transform="translate(-5 9) rotate(-30)">
    <ellipse cx="15" cy="15" rx="9" ry="5" style="stroke:blue" />
  </g>
</svg>
- [x] **Circular Arc**
- [x] **Elliptical Arc**
- [ ] **Oval** _May be implemented through Polycurve._
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [x] **Bezier Segment**
- [ ] **Parametric Delegate Curve** _Mostly used for testing._

## Area

- [ ] **Point** _Always 0._
- [ ] **Line**
- [ ] **Ray**
- [ ] **Line Segment**
- [x] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [x] **Polyline Contour**
- [ ] **Polyline Set**
- [x] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [x] **Circle**
- [x] **Ellipse**
- [x] **Circular Arc**
- [x] **Elliptical Arc**
- [ ] **Oval**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## double Length

- [x] **Point** _Always 0._
- [ ] **Line** _Always Infinity._
- [ ] **Ray** _Always Infinity._
- [x] **Line Segment**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour** _Perimeter._
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [x] **Circle** _Circumference._
- [x] **Ellipse** _Elliptical Circumference._
- [x] **Circular Arc**
- [x] **Elliptical Arc**
- [ ] **Oval**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [x] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## Rectangle Bounds

- [ ] **Point** _Empty Rectangle at Point's location._
- [ ] **Line** _Undefined?_
- [ ] **Ray** _Undefined?_
- [x] **Line Segment** _Always the two ends._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [x] **Polyline Contour**
- [ ] **Polyline Set**
- [x] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [x] **Circle**
- [x] **Ellipse**
- [ ] **Circular Arc** _Broken_
- [x] **Elliptical Arc**
- [ ] **Oval**
- [x] **Quadratic Bezier** _Use Bezier Segment._
- [x] **Cubic Bezier** _Use Bezier Segment._
- [x] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## double Distance(Point)

- [x] **Point**
- [x] **Line**
- [ ] **Ray**
- [x] **Line Segment**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [ ] **Circle**
- [ ] **Ellipse**
- [ ] **Circular Arc**
- [ ] **Elliptical Arc**
- [ ] **Oval**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [x] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## bool NearestPoint(Point)

- [x] **Point**
- [ ] **Line**
- [ ] **Ray**
- [x] **Line Segment**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [ ] **Circle**
- [ ] **Ellipse**
- [ ] **Circular Arc**
- [x] **Elliptical Arc**
- [ ] **Oval**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## bool NearestT(t)

- [ ] **Point**
- [ ] **Line**
- [ ] **Ray**
- [ ] **Line Segment**
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [ ] **Circle**
- [ ] **Ellipse**
- [ ] **Circular Arc**
- [ ] **Elliptical Arc**
- [ ] **Oval**
- [x] **Quadratic Bezier**
- [x] **Cubic Bezier**
- [x] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## Inclusion Contains(Point)

- [x] **Point**
- [ ] **Line**
- [ ] **Ray**
- [x] **Line Segment**
- [ ] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [x] **Polygon Contour**
- [x] **Polygon Set**
- [ ] **Polycurve Contour** _Implementation is buggy._
- [ ] **Polycurve Set**
- [x] **Circle**
- [x] **Ellipse**
- [x] **Circular Arc**
- [x] **Elliptical Arc**
- [ ] **Oval**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## bool Contains(Rectangle)

- [ ] **Point**
- [ ] **Line**
- [ ] **Ray**
- [ ] **Line Segment**
- [ ] **Triangle**
- [x] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [ ] **Circle**
- [ ] **Ellipse**
- [ ] **Circular Arc**
- [ ] **Elliptical Arc**
- [ ] **Oval**
- [ ] **Quadratic Bezier**
- [ ] **Cubic Bezier**
- [ ] **Bezier Segment**
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## bool Intersects(Shape)

Intersects methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

|  | Point | Line Segment | Ray | Line | Triangle | Rectangle | Polyline Contour | Polygon Contour | Polycurve Contour | Polyline Set | Polygon Set | Polycurve Set | Circle | Ellipse | Circular Arc | Elliptical Arc | Quadratic Bezier | Cubic Bezier | Oval |
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line Segment** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ray** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Triangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Rectangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline Contour** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Contour** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polycurve Contour** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polycurve Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ellipse** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circular Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Elliptical Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Quadratic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Cubic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Oval** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |

## Intersection Intersection(Shape)

Intersection methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

|  | Point | Line Segment | Ray | Line | Triangle | Rectangle | Polyline Contour | Polygon Contour | Polycurve Contour | Polyline Set | Polygon Set | Polycurve Set | Circle | Ellipse | Circular Arc | Elliptical Arc | Quadratic Bezier | Cubic Bezier | Oval |
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point** | [x] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line Segment** | [x] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Ray** | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line** | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Triangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Rectangle** | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Polyline Contour** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Contour** | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Polycurve Contour** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polycurve Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circle** | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Ellipse** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circular Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Elliptical Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Quadratic Bezier** | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Cubic Bezier** | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [ ] | [x] | [ ] | [ ] | [ ] | [ ] | [x] | [ ] | [ ] | [ ] | [x] | [x] | [ ] |
| **Oval** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |

## Split(t)

- [x] **Point** _Needs testing._
- [ ] **Line**
- [ ] **Ray**
- [x] **Line Segment** _Needs testing._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [x] **Circle** _Needs testing._
- [x] **Ellipse** _Needs testing._
- [x] **Circular Arc** _Needs testing._
- [x] **Elliptical Arc** _Needs testing._
- [ ] **Oval**
- [x] **Quadratic Bezier** _Needs testing._
- [x] **Cubic Bezier** _Needs testing._
- [x] **Bezier Segment** _Needs testing._
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier Segment Path**

## Split(t[])

- [x] **Point** _Needs testing._
- [ ] **Line**
- [ ] **Ray**
- [x] **Line Segment** _Needs testing._
- [ ] **Triangle**
- [ ] **Rectangle**
- [ ] **Rotated Rectangle**
- [ ] **Polyline Contour**
- [ ] **Polyline Set**
- [ ] **Polygon Contour**
- [ ] **Polygon Set**
- [ ] **Polycurve Contour**
- [ ] **Polycurve Set**
- [x] **Circle** _Needs testing._
- [x] **Ellipse** _Needs testing._
- [x] **Circular Arc** _Needs testing._
- [x] **Elliptical Arc** _Needs testing._
- [ ] **Oval** _?_
- [x] **Quadratic Bezier** _Needs testing._
- [x] **Cubic Bezier** _Needs testing._
- [x] **Bezier Segment** _Needs testing._
- [ ] **Parametric Delegate Curve**
- [ ] **Bezier** Segment Path
