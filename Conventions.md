# Coding Style and Syntax Conventions

This document is not intended to state the law. Rather, it is intended as a guideline for code consistency to make the entire project feel like it is a unified platform.

For the most part, follow the standards for .NET libraries in [C# Coding Conventions (C# Programming Guide)](https://msdn.microsoft.com/en-us/library/ff926074.aspx). Visual Studio defaults fit these standards. The `Ctrl` + `k` + `CTRL` + `d` shortcut key combination are your best friend for beautifying code.

This project is being written in C# Previews Which brings some interesting constructs into the language to do things that were previously impossible.

In high level summary:

## Commenting

Use comments as needed to clarify code for other coders. 

### XML Comments

Please use triple slash XML comments for all methods, and objects. type out three slashes "\\\" and let Visual Studio build out the XML comments. Briefly describe what each does. These comments are used as the IntelliSense text in tool tips. They also act as valuable documentation.

```csharp
namespace Engine
{
    /// <summary>
    /// An enumeration of Values.
    /// </summary>
    public enum MyEnum
    {
        /// <summary>
        /// The first value of the enumeration.
        /// </summary>
        Value1,

        /// <summary>
        /// The second value of the enumeration.
        /// </summary>
        Value2
    }

    /// <summary>
    /// A structure with an auto property.
    /// </summary>
    public struct MyStruct
    {
        /// <summary>
        /// Gets or sets an integer vlue in the <see cref="MyStruct"/> struct.
        /// </summary>
        public int MyProperty { get; set; }
    }

    /// <summary>
    /// A class with a method.
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// A method with a parameter. 
        /// </summary>
        /// <param name="myParameter">A parameter that gets fed into the method.</param>
        /// <returns>A double precision floating point value that is the result of this method.</returns>
        public double MyMethod(double myParameter) => return myParameter % 2;
    }
}
```

### Copyright notice

Code files should start with an XML copyright header like the following, to indicate copyright and license.

```csharp
// <copyright file="Filenme.cs" >
// Copyright © Year Copyright holder. All rights reserved.
// </copyright>
// <author id="username">Author</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
```

### Attribution

Methods found online should be attributed to their original source in an acknowledgment section of the method's XML header. This permits going back to the source while troubleshooting; to compare results with the original code, or to apply updates as needed. As well as to provide credit for the original author.  

```csharp
/// <summary>
/// The do something method.
/// </summary>
/// <param name="blah"></param>
/// <acknowledgment>
///     This method was found at: https://example.com/blah/
/// </acknowledgment>
public static void DoSomething(bool blah)
{
    Something done here...
}
```

### Sources

When looking online for methods that might solve a particular issue, be aware of the license restrictions of any code found. Look for licenses that are compatible with the MIT License used for this project.  

### Naming and Casing Guidelines

Use the Microsoft [Naming Guidelines](https://msdn.microsoft.com/en-us/library/ms229042.aspx) for Class Library Developers.

Use abbreviations sparingly.

**Hungarian Notation** is where identifiers are prefixed with three letter codes meant to identify the type of the construct used. 

Please do not use Hungarian notation. It is inconsistent with the naming conventions of the .NET framework, and is rendered unnecessary with IntelliSense.

**All Caps** All Caps is where every character in an identifier is capitalized. 

Please avoid Screaming All Caps. They are difficult to read and is inconsistent with the naming conventions of the .NET framework.

**Pascal** casing is where words are concatenated together with only the first letter of each word capitalized as indication of word separation.

Use Pascal casing for public Namespace, class, structure, enumeration, event, property and method names. As well as public Tuple definitions.

**Camel** casing is similar to Pascal casing except that the first character is lower case.

Use Camel casing for parameters, private member variables, and local variables. 

```csharp
namespace Engine
{
    public enum MyEnum
    {
        Value1,
        Value2
    }

    public struct MyStruct
    {
        private int myField = 0;

        public (double X, double Y) MyAutoProperty { get; set; } = (0, 0);

        public int MyProperty
        {
            get{ return myField; }
            set{ myField = value;}
        }
    }

    public class MyClass
    {
        private MyClass()
        { }

        public int MyMethod(double myParameter)
        {
            double myPrivateVariable = 2d;
            return return myParameter % myPrivateVariable;
        }
    }
}
```

### Indentation

Use the Visual Studio default of 4 spaces, tabs interpreted as spaces. With the default Visual Studio install; `Ctrl` + `k` + `Ctrl` + `d` should auto correct indentation in most cases.

### Bracing

In general, use Allman style spacing with curly braces on their own lines.

```csharp
private bool test()
{
    return true;
}
```

Property setters and getters that only have one line should be placed on one line

```csharp
public int Number
{
    get { return number; }
    set { number = value; }
}
```

```csharp
public double X { get; set; }
```

For methods _such as default constructors with pass through_ which are intended to be empty, placing opening and closing curly braces on the same line is recommended. The only intent is to signal that the method is indeed intended to be empty and not a placeholder for a future method.

```csharp
private Cleaner()
{}
```

### Multiple lines

For classes that inherit from another class, use interfaces, or methods that pass through to other methods, such as constructors, place the colon and the inherit/interface/this on it's own line to make it easier to see differences in code review.

```csharp
public Point()
    : this(0, 0)
{}
```

Method parameters should generally be on the same line, unless the number of parameters are unwieldy long and would make more sense grouped together on separate lines. Methods involving the raw parameters of Matrices, for example should be on multiple logically organized lines.

## Exceptions

Having a game crash at a precarious point, causing the player to loose everything is a frustrating experience. Exceptions should be used with restraint. For the times they are needed and they need to be handled here are some best practices. Throw them only where necessary, such as in situations that will corrupt the game state. In general follow [Microsoft best practices](https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions)

### Re-throwing an exception

When you need to re-throw an exception after processing it, do so with the default throw without passing the exception object. This way the stack is preserved the rest of the way up the chain.

```csharp
var response = string.Empty;
try
{
    response = GetFatalCrashThing();
}
catch (Exception)
{
    DoSomeThingHere();
    throw;
}
finally
{

}
```

### Adding Context to an exception

When you need to add additional context to an exception, do so for the specific exception, then rethrow with the additional detail passing the exception object. Then end the throw catch with a default throw without passing the exception object.

```csharp
var response = string.Empty;
try
{
    response = GetFatalCrashThing();
}
catch (SpecializedException e)
{
    throw new Exception($"There was a problem with the GetFatalCrashThing. The response was: \n\r{response}", e);
}
catch (Exception)
{
    throw;
}
finally
{

}
```

## [C# 6 Features](https://docs.microsoft.com/dotnet/csharp/whats-new/csharp-6)

### Expression Body Methods/Properties

For methods and read-only properties where they make sense, please feel free to use the expression body syntax. A little profiling indicates there may be some speed improvements with them.

```csharp
public int Count => Array.Length;

public ToString() => base.ToString()
```

### Var declaration

Use var where appropriate. Declaring a field in a method as var, you can let the compiler determine what the return type is. This can make long List or array declarations easier to read.  

```csharp
var list = new List<double> {3, 1, 4, 1, 5, 9,  2, 6};
```

## [C# 7 Features](https://docs.microsoft.com/dotnet/csharp/whats-new/csharp-7)

### Tuples

Feel free to use the new ValueTuple syntax where ever it makes sense. ValueTuples work great where you need a single use struct that will only be used within a single method, or for generalizing the return struct for several structs that could potentially use the same method with different similar type returns.

```csharp
(double X, double Y) tempPoint = (x, y);
```

```csharp
(double X, double Y) = (x, y);
```

```csharp
private MyClass(double x, double y, double z)
{
    (X, Y, Z) = (x, y, z);
}
```

### Interpolated Strings

Use interpolated strings in place of `String.Format(...)` or wherever you need a simple string concatenation. Interpolated strings tend to be more readable, unless the concatenation is for splaying out a line of text across multiple lines.

```csharp
public string ConvertToString() => $"Point:{x},{y}"
```

### Switch Pattern Matching

Please do use switch pattern matching to use a pattern to select branch to take. For example to select what to do based on the type of an object.

```csharp
switch (type)
{
    case byte b:
        return $"byte: {b}";
    case int i:
        return $"integer: {i}";
    case float f:
        return $"Float: {f}";
    case double d:
        return $"double: {d}";
    case long l:
        return $"long: {l}";
    default:
        return "Not Supported";
};
```

## [C# 7.1 Features](https://docs.microsoft.com/dotnet/csharp/whats-new/csharp-7-1)

## [C# 7.2 Features](https://docs.microsoft.com/dotnet/csharp/whats-new/csharp-7-2)

## [C# 7.3 Features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-3)

## [C# 8 Features](https://docs.microsoft.com/dotnet/csharp/whats-new/csharp-8)

### Switch Expressions

Use Switch expressions where they make sense, for example when you need to call one of various methods and return the results depending on an enum value, or string.

```csharp
return count switch
{
    ValueEnum.Empty => 0d,
    ValueEnum.Const => CalculateConstant(x),
    ValueEnum.Linear => CalculateLinear(x),
    ValueEnum.Square => CalculateSquare(x),
    _ => double.NaN;
};
```

### Switch Property Patterns

```csharp
return radianText switch
{
    { Name: "PI" } => Math.PI,
    { Name: "Tau" } => Math.Tau,
    { Name: "Pau" } => Math.Pau,
    _ => 0d
};
```

### Switch Tuple Patterns

```csharp
return (first, second) switch
{
    ("a", "b") => "Polynomial",
    ("i", "j") => "Vector",
    ("x", "y") => "Point",
    (_, _) => "unknown"
};
```

### Switch Positional Patterns

```csharp
return point switch
{
    (0, 0) => Quadrant.Origin,
    var (x, y) when x > 0 && y > 0 => Quadrant.One,
    var (x, y) when x < 0 && y > 0 => Quadrant.Two,
    var (x, y) when x < 0 && y < 0 => Quadrant.Three,
    var (x, y) when x > 0 && y < 0 => Quadrant.Four,
    var (_, _) => Quadrant.OnBorder,
    _ => Quadrant.Unknown
};
```

### Using Declarations

Feel free to use the Using declarations when working with IDisposable types. Disposable variables declared with using will be scheduled for being recycled once they fall out of scope, without having to brace off your code.

```csharp
using var pen = new SolidPen(Color.Red);
```

### Indices and Ranges

Feel free to use the Indices and ranges feature.

```csharp
var last = exampleArray[^1];
```

```csharp
var lastCouple = exampleArray[^2..^0]
```

### Default Interface Methods

If there is a good use for default interface methods, they might as well be used. I was hoping they might be more useful than they are. I'm trying to abuse them in some structs. But the boxing/unboxing might be an issue.

```csharp
public interface iInterface
{
    string Text() => ToString();
    string Text => ToString();
}
```

### Nullable Reference Types

Use Nullable reference type notation where a type is expected to be null.

```csharp
public static int? Index(int? index)
{
    var test = index ?? 0;
    return test == 0 ? (int?)null : test;
}
```

Some thought will need to go into figuring what should or should not be nullable before `NullableContextOptions` gets turned on.

### Null-coalescing assignment

Use null-coalescing assignment in place of if statements to simplify null checks and assignment.

```csharp
var test = index ?? 0;
```

## Profiling

If you have several choices of how  to do things, profile the various methods, checking for accuracy and speed, then find the best compromise of the two.

## Regions

In order to help keep classes organized as they become unwieldy long, I'm using regions to keep similar parts together, so I can go to a file and know where to look for what.

Here is the general format I am using.

```csharp
    public struct RegionObject
    {
        #region Implementations
        // Public static read-only fields that implement the class/struct.
        #endregion

        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Indexers
        #endregion

        #region Properties
        // Read-write properties.
        #endregion

        #region Accessors
        // Read-only properties.
        #endregion

        #region Operators
        #endregion

        #region Factories
        #endregion

        #region Methods
        #endregion
    }
```

## Standard Methods

To standardize these specific methods throughout the Engine to work the same, please use the following conventions.

### Tuple Constructors Deconstructors

If a struct/class can be generalized by a numeric tuple; please use a tuple constructor and explicit operator so you can take advantage of any existing methods with the same signature.

```csharp
    public struct NumericObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericObject"/> struct from a tuple.
        /// </summary>
        /// <param name="tuple">A Tuple containing the values for this <see cref="NumericObject"/>.</param>
        [DebuggerStepThrough]
        public NumericObject((double A, double B, double C) tuple) => (A, B, C) = tuple;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericObject"/> struct from a tuple.
        /// </summary>
        /// <param name="a">The a parameter.</param>
        /// <param name="b">The b parameter.</param>
        /// <param name="c">The c parameter.</param>
        [DebuggerStepThrough]
        public NumericObject(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// The deconstruct method.
        /// </summary>
        /// <param name="a">The a parameter.</param>
        /// <param name="b">The b parameter.</param>
        /// <param name="c">The c parameter.</param>
        [DebuggerStepThrough]
        public void Deconstruct(out double a, out double b, out double c)
        {
            a = this.A;
            b = this.B;
            c = this.C;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets A.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Gets or sets B.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Gets or sets C.
        /// </summary>
        public double C { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// Convert a tuple to a <see cref="NumericObject"/> struct.
        /// </summary>
        /// <param name="tuple">The source Tuple.</param>
        /// <returns>A new instance of the <see cref="NumericObject"/> struct with the contents of the tuple.</returns>
        [DebuggerStepThrough]
        public static implicit operator NumericObject((double A, double B, double C) tuple)
            => new NumericObject(tuple);

        #endregion
    }
```

### Equality Comparison

To reduce the chance of errors in equality comparisons across various comparison operators, please use the following as a template for modeling Structs/Classes that need equality comparisons.

If the Structs/Classes are also relational merge with the Relational Comparisons below, using the relational equivalents when there are duplicates.

```csharp
    public struct ComparableObject
        : IEquatable<ComparableObject>
    {
        #region Properties

        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// The == operator compares two <see cref="ComparableObject"/> instances for exact equality.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// Returns a boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly equal.
        /// The return value is true if they are equal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator ==(ComparableObject left, ComparableObject right) => left.Equals(right);

        /// <summary>
        /// The != operator compares two <see cref="ComparableObject"/> instances for exact inequality.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// Returns a boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator !=(ComparableObject left, ComparableObject right) => !left.Equals(right);

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Returns a 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
            => unchecked(
            A.GetHashCode()
            ^ B.GetHashCode()
            ^ C.GetHashCode());

        /// <summary>
        /// Compares two <see cref="ComparableObject"/> structs.
        /// </summary>
        /// <param name="left">The object to comare.</param>
        /// <param name="right">The object to compare against.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool Compare(ComparableObject left, ComparableObject right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="ComparableObject"/> instances for exact equality.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool Equals(ComparableObject left, ComparableObject right)
            => left?.A == right?.A
             & left?.B == right?.B
             & left?.C == right?.C;

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public override bool Equals(object obj)
            => obj is ComparableObject && Equals(this, (ComparableObject)obj);

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> with the passed in <see cref="ComparableObject"/>.
        /// </summary>
        /// <param name="value">The <see cref="ComparableObject"/> to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Equals(ComparableObject value) => Equals(this, value);

        #endregion
    }
```

### Relational Comparison

To reduce the chance of errors in relational comparisons across various comparison operators, please use the following as a template for modeling Structs/Classes that need relational comparisons.

If the Structs/Classes are also relational merge with the Equality Comparisons above, using the relational versions when there are duplicates.

```csharp
    public struct ComparableObject
        : IComparable, IComparable<ComparableObject>
    {
        #region Properties

        public double A { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// The == operator compares two <see cref="ComparableObject"/> instances for exact equality.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// Returns a boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly equal.
        /// The return value is true if they are equal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator ==(ComparableObject left, ComparableObject right)
            => left.Equals(right);

        /// <summary>
        /// The != operator compares two <see cref="ComparableObject"/> instances for exact inequality.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// Returns a boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator !=(ComparableObject left, ComparableObject right)
            => !left.Equals(right);

        /// <summary>
        /// The operator &lt; returns a value that indicates whether a specified <see cref="ComparableObject"/> value
        /// is less than another specified <see cref="ComparableObject"/> value.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare.</param>
        /// <returns>Returns a boolean value indicating true if left is less than right; otherwise, false.</returns>
        public static bool operator <(ComparableObject left, ComparableObject right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// The operator &gt; returns a value that indicates whether a specified <see cref="ComparableObject"/> value
        /// is greater than another specified <see cref="ComparableObject"/> value.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare.</param>
        /// <returns>Returns a boolean value indicating true if left is greater than right; otherwise, false.</returns>
        public static bool operator >(ComparableObject left, ComparableObject right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// The &lt;= operator returns a value that indicates whether a specified <see cref="ComparableObject"/> value
        /// is less than or equal to another specified <see cref="ComparableObject"/> value.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare.</param>
        /// <returns>Returns a boolean value indicating true if left is less than or equal to right; otherwise, false.</returns>
        public static bool operator <=(ComparableObject left, ComparableObject right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// The &gt;= operator returns a value that indicates whether a specified <see cref="ComparableObject"/> value
        /// is greater than or equal to another specified <see cref="ComparableObject"/> value.
        /// </summary>
        /// <param name="left">The first <see cref="ComparableObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ComparableObject"/> to compare.</param>
        /// <returns>Returns a boolean value indicating true if left is greater than or equal to right; otherwise, false.</returns>
        public static bool operator >=(ComparableObject left, ComparableObject right)
            => left.CompareTo(right) >= 0;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Returns a 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
            => base.GetHashCode();

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        public override bool Equals(object obj)
            => obj is ComparableObject && CompareTo((ComparableObject)obj) == 0;

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> to another object, returning a value indicating the relation.
        /// Null is considered less than any instance.
        /// If object is not of type <see cref="ComparableObject"/>, this method throws an ArgumentException.
        /// </summary>
        /// <param name="other">The object to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// Returns an <see cref="int"/> value less than zero if this <see cref="ComparableObject"/> is less than the object,
        /// zero if this <see cref="ComparableObject"/> is the same value as the object, or a value greater than zero if this
        /// <see cref="ComparableObject"/> is greater than the object.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int CompareTo(object other)
            => other is null ? 1 : other is ComparableObject ? CompareTo((ComparableObject)other) : throw new ArgumentException("Object must be an ComparableObject.", nameof(other));

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> to another <see cref="ComparableObject"/>, returning a value indicating the relation.
        /// Null is considered less than any instance.
        /// </summary>
        /// <param name="other">The <see cref="ComparableObject"/> to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// Returns an <see cref="int"/> value less than zero if this <see cref="ComparableObject"/> is less than the other <see cref="ComparableObject"/>,
        /// zero if this <see cref="ComparableObject"/> is the same value as the other <see cref="ComparableObject"/>, or a value greater than zero if this
        /// <see cref="ComparableObject"/> is greater than the other <see cref="ComparableObject"/>.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int CompareTo(ComparableObject other)
            => Compare(this, other);

        /// <summary>
        /// Compares two <see cref="ComparableObject"/> objects, returning a value indicating the relation.
        /// Null is considered less than any instance.
        /// </summary>
        /// <param name="left">The <see cref="ComparableObject"/> to compare.</param>
        /// <param name="right">The <see cref="ComparableObject"/> to compare against.</param>
        /// <returns>
        /// Returns an <see cref="int"/> value less than zero if the left <see cref="ComparableObject"/> is less than the right <see cref="ComparableObject"/>,
        /// zero if the left <see cref="ComparableObject"/> is the same value as the right <see cref="ComparableObject"/>, or a value greater than zero if the left
        /// <see cref="ComparableObject"/> is greater than the right <see cref="ComparableObject"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static int Compare(ComparableObject left, ComparableObject right)
            => right.A.CompareTo(left.A);

        #endregion
    }
```

### IFormattable

Please use the following as a template for IFormatable Structs/Classes, or objects that need to provide a string representation of the object.

```csharp
    public struct FormatableObject
         : IFormattable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatableObject"/> struct.
        /// </summary>
        /// <param name="a">The <see cref="A"/> component of the <see cref="FormatableObject"/> struct.</param>
        /// <param name="b">The <see cref="B"/> component of the <see cref="FormatableObject"/> struct.</param>
        /// <param name="c">The <see cref="C"/> component of the <see cref="FormatableObject"/> struct.</param>
        public Vector2D(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        #endregion

        #region Properties

        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        #endregion

        #region Factories
        
        /// <summary>
        /// Parses the provided string using the current culture to create an instance of the <see cref="FormatableObject"/> struct.
        /// </summary>
        /// <param name="source">A string containinig the <see cref="FormatableObject"/> data</param>
        /// <returns>Returns an instance of the <see cref="FormatableObject"/> struct converted from the provided string.</returns>
        public static FormatableObject Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parses the provided string using the provided culture to create an instance of the <see cref="FormatableObject"/> struct.
        /// </summary>
        /// <param name="source">A string containinig the <see cref="FormatableObject"/> data</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>Returns an instance of the <see cref="FormatableObject"/> struct converted from the provided string.</returns>
        public static FormatableObject Parse(string source, IFormatProvider formatProvider)
        {
            // Initialize the tokenizer.
            var tokenizer = new Tokenizer(source, formatProvider);

            // Fetch the values from the tokens.
            var value = new FormatableObject(
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider));

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();

            return value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the current culture.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the IFormatProvider
        /// passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by provider.
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
            => ConvertToString(null /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by format and provider.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by format and provider.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider formatProvider)
        {
            // Capture the culture's list ceparator character.
            char sep = Tokenizer.GetNumericListSeparator(formatProvider);

            // Create the string representation of the struct.
            return $"{nameof(FormatableObject)}({nameof(A)}={A.ToString(format, formatProvider)}{sep}{nameof(B)}={B.ToString(format, formatProvider)}{sep}{nameof(C)}={C.ToString(format, formatProvider)})";
        }

        #endregion
    }
```

## Property Caching

With certain types of classes, there are often properties that have to be calculated using the values of other properties, where the calculation can take some time, and where the value is used frequently in some cases, but in other cases never touched. 

In this situation, it would be faster to run the calculation once and store it in a field. But if the field won't be used all of the time it can be difficult to justify the memory for the field, and justify the time spent calculating when the class is initialized, or updated.

This can be solved with property caching. The memory expense is a single dictionary that expands or shrinks as needed.

```csharp
        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [NonSerialized()]
        protected Dictionary<object, object> propertyCache;
```

To support it, you need a method that you pass a property to, and it checks whether the property's value is in the cache, keyed to the property's name, if it is; it returns the value, if not; it runs the calculation, caches the value, and returns the cached value.

```csharp
        /// <summary>
        /// Private method for caching computationally and memory intensive properties of child objects
        /// so that the intensive properties only get recalculated and stored when necessary.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected object CachingProperty(Func<object> property, [CallerMemberName]string name = "")
        {
            if (!propertyCache.ContainsKey(name))
            {
                var value = property.Invoke();
                propertyCache.Add(name, value);
                return value;
            }

            return propertyCache[name];
        }
```

In the property, the calculations get moved out to a separate method, and a call to the cachingProperty method gets called instead, with a lambda reference to the method that does the calculation.

```csharp
        /// <summary>
        /// Gets the axis aligned bounding box of the Shape.
        /// </summary>
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.CalculateBounds(x, y));
```

Hereafter, it is important that whenever a referenced property changes, the property cache gets cleared. Because if the property is ever accessed, the class instance will carry the resulting value of the calculated property around until the dictionary is emptied, or until the end of the life of the class instance. If a parameter has changed that would result in a different calculation and the cache had not been cleared, then the property would continue to present the old values.

Unfortunately this means that under the current syntax rules, if you use property caching you cannot use auto properties for any value that can affect a cached property, and you have to fall back to using properties with fields and a clearProperty() call in the setter.

```csharp
        /// <summary>
        /// The center x coordinate point of the Shape.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the Shape.
        /// </summary>
        private double y;

        /// <summary>
        /// Gets or sets the center of the Shape.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                ClearCache();
            }
        }

        /// <summary>
        /// This should be run anytime a property of the item is modified.
        /// </summary>
        public void ClearCache()
            => propertyCache.Clear();
```

Unfortunately, this does leave these properties open to problems with race conditions. So, it may need some refinement if changed from multiple processes.

In general, property caching should be used when the calculation time of a read only property exceeds the amount of time it would take to lookup the value in a dictionary. 

Right now, the shapes classes inherit property caching from their parent class, and have to have properties modified to use it.

## Meta Optimizations

There are various meta tag attributes that can be attached to a method for compiler based improvements.

### DebuggerStepThrough

The DebuggerStepThrough attribute is attached to a method that you wish to always step over while debugging. As an API set most methods should be marked so people debugging their own code don't have to wade through the API code. 

Methods that have been thoroughly tested and deemed to work properly are good candidates to be tagged with the DebuggerStepThrough attribute. When debugging a method that would normally be marked DebuggerStepThrough, be sure to comment out it out.

```csharp
[DebuggerStepThrough]
public static method()
{}
```

### MethodImplOptions AggressiveInlining

The compiler is designed to optimize code compilation for general solutions. But sometimes you really want your code to run as fast as possible. This can often be done by in-lining methods rather than making calls to the methods, but at the expense of building larger binaries.

The c# compiler has been given an attribute flag that can provide a hint to the compiler to optimize compilation with aggressive in-lining.

For right now AggressiveInlining is being used on most of the library methods. Though it would be good to test to see if it actually does help, and in which cases.

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
public static method()
{}
```
