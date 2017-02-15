# Uniform Interaction Methods Specification

## Primitives

- [x] Polynomial
- [x] 2D Point
- [x] 3D Point
- [x] 2D Vector
- [x] 3D Vector
- [x] 4D Vector
- [x] 3D Orientation
- [x] Quaternion
- [x] 2D Size
- [x] 3D Size
- [x] 2x2 Matrix
- [x] 3x3 Matrix
- [x] 4x4 Matrix
- [x] 2x3 Translation Matrix
- [x] 2D Transform
- [x] 3D Transform
- [x] 2D Accumulator Point
- [x] 3D Accumulator Point

## Shapes

The following shapes should be renderable.

- [x] Point
- [ ] Line
- [ ] Ray
- [x] Line Segment
- [ ] Triangle _May be implemented through Polygon._
- [x] Rectangle
- [ ] Rotated Rectangle _Need to think through location issues._
- [x] Polyline
- [ ] Polyline Set _Needs testing._
- [x] Polygon
- [ ] Polygon Set _Needs testing._
- [x] Circle
- [x] Ellipse
- [x] Circular Arc
- [x] Elliptical Arc
- [ ] Oval _May be implemented through Geometry Path._
- [x] Quadratic Bezier
- [x] Cubic Bezier
- [x] Bezier Segment
- [ ] Parametric Delegate Curve _Mostly used for testing._
- [x] Geometry Path
- [ ] Bezier Segment Path _Not implemented_

## Area

- [ ] Point _Always 0._
- [ ] Line
- [ ] Ray
- [ ] Line Segment
- [x] Triangle
- [x] Rectangle
- [ ] Rotated Rectangle
- [x] Polyline
- [ ] Polyline Set
- [x] Polygon
- [ ] Polygon Set
- [x] Circle
- [x] Ellipse
- [x] Circular Arc
- [x] Elliptical Arc
- [ ] Oval
- [ ] Quadratic Bezier
- [ ] Cubic Bezier
- [ ] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## double Length

- [x] Point _Always 0._
- [ ] Line _Always Infinity._
- [ ] Ray _Always Infinity._
- [x] Line Segment
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon _Perimeter._
- [ ] Polygon Set
- [x] Circle _Circumference._
- [x] Ellipse _Elliptical Circumference._
- [x] Circular Arc
- [x] Elliptical Arc
- [ ] Oval
- [x] Quadratic Bezier
- [x] Cubic Bezier
- [x] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## Rectangle Bounds

- [ ] Point _Empty Rectangle at Point's location._
- [ ] Line _Undefined?_
- [ ] Ray _Undefined?_
- [x] Line Segment _Always the two ends._
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [x] Polyline
- [ ] Polyline Set
- [x] Polygon
- [ ] Polygon Set
- [x] Circle
- [x] Ellipse
- [ ] Circular Arc _Broken_
- [x] Elliptical Arc
- [ ] Oval
- [x] Quadratic Bezier _Use Bezier Segment._
- [x] Cubic Bezier _Use Bezier Segment._
- [x] Bezier Segment
- [ ] Parametric Delegate Curve
- [x] Geometry Path
- [ ] Bezier Segment Path

## double Distance(Point)

- [x] Point
- [x] Line
- [ ] Ray
- [x] Line Segment
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon
- [ ] Polygon Set
- [ ] Circle
- [ ] Ellipse
- [ ] Circular Arc
- [ ] Elliptical Arc
- [ ] Oval
- [x] Quadratic Bezier
- [x] Cubic Bezier
- [x] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## bool NearestPoint(Point)

- [x] Point
- [ ] Line
- [ ] Ray
- [x] Line Segment
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon
- [ ] Polygon Set
- [ ] Circle
- [ ] Ellipse
- [ ] Circular Arc
- [x] Elliptical Arc
- [ ] Oval
- [ ] Quadratic Bezier
- [ ] Cubic Bezier
- [ ] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## bool NearestT(t)

- [ ] Point
- [ ] Line
- [ ] Ray
- [ ] Line Segment
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon
- [ ] Polygon Set
- [ ] Circle
- [ ] Ellipse
- [ ] Circular Arc
- [ ] Elliptical Arc
- [ ] Oval
- [x] Quadratic Bezier
- [x] Cubic Bezier
- [x] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## Inclusion Contains(Point)

- [x] Point
- [ ] Line
- [ ] Ray
- [x] Line Segment
- [ ] Triangle
- [x] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [x] Polygon
- [x] Polygon Set
- [x] Circle
- [x] Ellipse
- [x] Circular Arc
- [x] Elliptical Arc
- [ ] Oval
- [ ] Quadratic Bezier
- [ ] Cubic Bezier
- [ ] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path _Implementation is buggy._
- [ ] Bezier Segment Path

## bool Contains(Rectangle)

- [ ] Point
- [ ] Line
- [ ] Ray
- [ ] Line Segment
- [ ] Triangle
- [x] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon
- [ ] Polygon Set
- [ ] Circle
- [ ] Ellipse
- [ ] Circular Arc
- [ ] Elliptical Arc
- [ ] Oval
- [ ] Quadratic Bezier
- [ ] Cubic Bezier
- [ ] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path

## bool Intersects(Shape)

Intersects methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

|  | Point | Line Segment | Ray | Line | Triangle | Rectangle | Polyline | Polygon | Polyline Set | Polygon Set | Circle | Ellipse | Circular Arc | Elliptical Arc | Quadratic Bezier | Cubic Bezier | Path | Oval |
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point** | [x] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line Segment** | [x] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ray** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Triangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Rectangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ellipse** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circular Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Elliptical Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Quadratic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Cubic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Path** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Oval** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
|  | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |

## Intersection Intersection(Shape)

Intersection methods in the Intersections class should be marked as public static with the first parameter marked with this to make it an extension method.

|  | Point | Line Segment | Ray | Line | Triangle | Rectangle | Polyline | Polygon | Polyline Set | Polygon Set | Circle | Ellipse | Circular Arc | Elliptical Arc | Quadratic Bezier | Cubic Bezier | Path | Oval |
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| **Point** | [x] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line Segment** | [x] | [x] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ray** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Line** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Triangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Rectangle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polyline Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Polygon Set** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circle** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Ellipse** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Circular Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Elliptical Arc** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Quadratic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Cubic Bezier** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Path** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
| **Oval** | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |
|  | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] | [ ] |

## Split

- [ ] Point
- [ ] Line
- [ ] Ray
- [x] Line Segment
- [ ] Triangle
- [ ] Rectangle
- [ ] Rotated Rectangle
- [ ] Polyline
- [ ] Polyline Set
- [ ] Polygon
- [ ] Polygon Set
- [ ] Circle
- [ ] Ellipse
- [x] Circular Arc
- [ ] Elliptical Arc
- [ ] Oval
- [x] Quadratic Bezier
- [x] Cubic Bezier
- [x] Bezier Segment
- [ ] Parametric Delegate Curve
- [ ] Geometry Path
- [ ] Bezier Segment Path
