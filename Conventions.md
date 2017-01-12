# Coding Style and Syntax Conventions

This document is not intended to state the law. Rather, it is intended as a guideline for code consistency to make the entire project feel like it is a unified platform.

For the most part, follow the standards for .NET libraries in [C# Coding Conventions (C# Programming Guide)](https://msdn.microsoft.com/en-us/library/ff926074.aspx). Visual Studio defaults fit these standards. The Ctrl + k + CTRL + d shortcut key combination are your best friend for beautifying code.

This project is being written in C# 7.0. Which brings some interesting constructs into the language to do things that were previously impossible.

In high level summary:

## Commenting

Use comments as needed to clarify code for other coders. 

### XML Comments

Please use triple slash XML comments for all methods, and objects. type out three slashes "\\\" and let Visual Studio build out the XML comments. Briefly describe what each does. These comments are used as the Intelisense text in tool tips. They also act as valuable documentation.

```
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
        public double MyMethod(double myParameter)
            => return myParameter % 2;
    }
}
```

### Copyright notice

Code files should start with an XML copyright header like the following, to indicate copyright and license.

```c#
// <copyright file="Filenme.cs" >
//     Copyright (c) Year Copyright holder. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="username">Author</author>
// <summary></summary>
```

### Naming and Casing Guidelines

Use the Microsoft [Naming Guidelines](https://msdn.microsoft.com/en-us/library/ms229042.aspx) for Class Library Developers.

Use abbreviations sparingly.

**Hungarian Notation** is where identifiers are prefixed with three letter codes meant to identify the type of the construct used. 

Please do not use Hungarian notation. It is inconsistent with the naming conventions of the .NET framework, and is renderd unnessisary with intelisense.

**All Caps** All Caps is where every character in an identifier is capitalized. 

Please avoid Screaming All Caps. They are difficult to read and is inconsistent with the naming conventions of the .NET framework.

**Pascal** casing is where words are concatenated together with only the first leter of each word capitalized as indication of word seporation.

Use Pascal casing for public Namespace, class, structure, enumeration, event, property and method names. As well as public Tupple deffinitions.

**Camel** casing is similar to Pascal casing except that the first character is lower case.

Use Camel casing for parameters, private member variables, and local variables. 

```c#
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

Use the Visual Studio default of 4 spaces, tabs interpreted as spaces. With the default Visual Studio install; Ctrl + k + d should auto correct indentation in most cases.

### Bracing 

In general, use Allman style spacing with curly braces on their own lines.

```c#
private bool test()
{
    return true;
}
```

Property setters and getters that only have one line should be placed on one line

```c#
public int Number
{
    get { return number; }
    set { number = value; }
}
``` 

```c#
public double X { get; set; }
```

For methods _such as default constructors with pass through_ which are intended to be empty, placing opening and closing curly braces on the same line is recommended. The only intent is to signal that the method is indeed intended to be empty and not a placeholder for a future method.

```c#
private Cleaner()
{}
```

### Multiple lines

For classes that inherit from another class, use interfaces, or methods that pass through to other methods, such as constructors, place the colon and the inherit/interface/this on it's own line to make it easier to see differences in code review.

```c#
public Point()
    : this(0, 0)
{}
```

Method parameters should generally be on the same line, unless the number of parameters are unwieldy long and would make more sense grouped together on separate lines. Methods involving the raw parameters of Matrices, for example should be on multiple logically organized lines.

## C# 6 Features

### Lambda Methods/Properties

For methods and read-only properties where they make sense, please feel free to use the Lambda syntax. A little profiling indicates there may be some speed improvements with them.
I am placing the 

```c#
public int Count
   => Array.Length;

public ToString()
   => base.ToString()
```

### Var declaration

Use var where apropriate. Declaring a field in a method as var, you can let the compiler determine what the return type is. This can make long List or array declarations easier to read. 

```c#
var list = new List<double> {3, 1, 4, 1, 5, 9,  2, 6};
```

## C# 7 Features

### Tuples

Feel free to use the new Tuple syntax where ever it makes sense. Tuples work great where you need a single use struct that will only be used within a single method, or for generalizing the return struct for several structs that could potentially use the same method with different type returns.

```c#
(double X, double Y) tempPoint = (x, y);
```

### Interpolated Strings

Use interpolated strings inplace of `String.Format(...)` or wherever you need a simple string concatenation. Interpolated strings tend to be more readable, unless the concatination is for splaying out a line of text accross multiple lines.

```c#
public string ConvertToString()
    => $"Point:{x},{y}"
```

## Profiling

If you have several choices of how  to do things, profile the various methods, checking for accuracy and speed, then find the best compromise of the two.

## Regions

In order to help keep classes organised as they become unweildly long, I'm using regions to keep similar parts together, so I can go to a file and know where to look for what.

Here is the general format I am using.

```c#
    public struct RegionObject
    {
        #region Implementors
        // Public static read only fields.
        #endregion
        
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties
        // Read-write properties.
        #endregion

        #region Accessors
        // Read only properties.
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

To standardize these specific methods throughought the Engine to work the same, please use the following conventions.

### Tuple Constructors

If a struct/class can be generalized by a numaric tuple; please use a tuple constructor and explicit operator so you can take advantage of any existing methods with the same signature.

```c#
    public struct NumaricObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NumaricObject"/> struct from a tuple.
        /// </summary>
        /// <param name="tuple">A Tuple containing the values for this <see cref="NumaricObject"/>.</param>
        [DebuggerStepThrough]
        public NumaricObject((double A, double B, double C) tuple)
            => (A, B, C) = tuple;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumaricObject"/> struct from a tuple.
        /// </summary>
        /// <param name="a">The a parameter.</param>
        /// <param name="b">The b parameter.</param>
        /// <param name="c">The c parameter.</param>
        [DebuggerStepThrough]
        public NumaricObject(double a, double b, double c)
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

        #region Operators

        /// <summary>
        /// Convert a tuple to a <see cref="NumaricObject"/> struct.
        /// </summary>
        /// <param name="tuple">The source Tuple.</param>
        /// <returns>A new instance of the <see cref="NumaricObject"/> struct with the contents of the tuple.</returns>
        [DebuggerStepThrough]
        public static implicit operator NumaricObject((double A, double B, double C) tuple)
            => new NumaricObject(tuple);

        #endregion
    }
```

### Equality Comparison

To reduce the chance of errors in equality comparisons accross various comparison operators, please use the following as a template for modeling Structs/Classes that need equality comparisons.

```c#
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
        /// Compares two <see cref="ComparableObject"/> instances for exact equality.
        /// </summary>
        /// <param name="a">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="b">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="ComparableObject"/> instances are exactly equal.
        /// The return value is true if they are equal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator ==(ComparableObject a, ComparableObject b)
            => Equals(a, b);

        /// <summary>
        /// Compares two <see cref="ComparableObject"/> instances for exact inequality.
        /// </summary>
        /// <param name="a">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="b">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator !=(ComparableObject a, ComparableObject b)
            => !Equals(a, b);

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
        /// <param name="a">The object to comare.</param>
        /// <param name="b">The object to compare against.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(ComparableObject a, ComparableObject b)
            => Equals(a, b);

        /// <summary>
        /// Compares two <see cref="ComparableObject"/> instances for exact equality.
        /// </summary>
        /// <param name="a">The first <see cref="ComparableObject"/> to compare</param>
        /// <param name="b">The second <see cref="ComparableObject"/> to compare</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(ComparableObject a, ComparableObject b)
            => a?.A == b?.A
             & a?.B == b?.B
             & a?.C == b?.C;

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is ComparableObject && Equals(this, (ComparableObject)obj);

        /// <summary>
        /// Compares this <see cref="ComparableObject"/> with the passed in <see cref="ComparableObject"/>.
        /// </summary>
        /// <param name="value">The <see cref="ComparableObject"/> to compare to this <see cref="ComparableObject"/> to.</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="ComparableObject"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ComparableObject value)
            => Equals(this, value);

        #endregion
    }
```

### IFormattable

Please use the following as a template for IFormatable Structs/Classes, or objects that need to provide a string representation of the self.

```c#
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
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>Returns an instance of the <see cref="FormatableObject"/> struct converted from the provided string.</returns>
        public static FormatableObject Parse(string source, IFormatProvider provider)
        {
            // Initialize the tokenizer.
            var tokenizer = new Tokenizer(source, provider);

            // Fetch the values from the tokens.
            var value = new FormatableObject(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));

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
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by provider.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by format and provider.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="FormatableObject"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by format and provider.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // Capture the culture's list ceparator character.
            char sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(FormatableObject)}({nameof(A)}={A.ToString(format, provider)}{sep}{nameof(B)}={B.ToString(format, provider)}{sep}{nameof(C)}={C.ToString(format, provider)})";
        }

        #endregion
    }
```
