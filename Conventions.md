# Coding Style and Syntax Conventions

This document is not intended to state the law. Rather, it is intended as a guideline for code consistency to make the entire project feel like it is a unified platform.

For the most part, follow the standards for .NET libraries in [C# Coding Conventions (C# Programming Guide)](https://msdn.microsoft.com/en-us/library/ff926074.aspx). Visual Studio defaults fit these standards. The Ctrl + k + CTRL + d shortcut key combination are your best friend for beautifying code.

This project is being written in C# 7.0. Which brings some interesting constructs into the language to do things that were previously impossible.

In high level summary:

## Commenting

Use comments as needed to clarify code for other coders. 

### XML Comments

Please use triple slash XML comments for all methods, and objects. type out three slashes "\\\" and let Visual Studio build out the XML comments. Briefly describe what each does. These comments are used as the IntelliSense text in tool tips. They also act as valuable documentation.

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
//     Copyright © Year Copyright holder. All rights reserved.
// </copyright>
// <author id="username">Author</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
```

### Attribution

Methods found online should be attributed to their original source in an acknowledgment section of the method's xml header. This permits going back to the source while troubleshooting; to compare results with the original code, or to apply updates as needed. As well as to provide credit for the original author. 

```c#
        /// <summary>
        /// 
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

### Expression Body Methods/Properties

For methods and read-only properties where they make sense, please feel free to use the expression body syntax. A little profiling indicates there may be some speed improvements with them.
I am placing the 

```c#
public int Count
   => Array.Length;

public ToString()
   => base.ToString()
```

### Var declaration

Use var where appropriate. Declaring a field in a method as var, you can let the compiler determine what the return type is. This can make long List or array declarations easier to read. 

```c#
var list = new List<double> {3, 1, 4, 1, 5, 9,  2, 6};
```

## C# 7 Features

### Tuples

Feel free to use the new Tuple syntax where ever it makes sense. Tuples work great where you need a single use struct that will only be used within a single method, or for generalizing the return struct for several structs that could potentially use the same method with different type returns.

```c#
(double X, double Y) tempPoint = (x, y);
```

```c#
(double X, double Y) = (x, y);
```

### Interpolated Strings

Use interpolated strings in place of `String.Format(...)` or wherever you need a simple string concatenation. Interpolated strings tend to be more readable, unless the concatenation is for splaying out a line of text across multiple lines.

```c#
public string ConvertToString()
    => $"Point:{x},{y}"
```

## Profiling

If you have several choices of how  to do things, profile the various methods, checking for accuracy and speed, then find the best compromise of the two.

## Regions

In order to help keep classes organized as they become unwieldy long, I'm using regions to keep similar parts together, so I can go to a file and know where to look for what.

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

To standardize these specific methods throughout the Engine to work the same, please use the following conventions.

### Tuple Constructors Deconstructors

If a struct/class can be generalized by a numeric tuple; please use a tuple constructor and explicit operator so you can take advantage of any existing methods with the same signature.

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

        #region Deconstructors
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [DebuggerStepThrough]
        public void Deconstruct(out double a, out double b, out double c)
        {
            a = this.A;
            b = this.B;
            c = this.C;
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

To reduce the chance of errors in equality comparisons across various comparison operators, please use the following as a template for modeling Structs/Classes that need equality comparisons.

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

## Property Caching

With certain types of classes, there are often properties that have to be calculated using the values of other properties, where the calculation can take some time, and where the value is used frequently in some cases, but in other cases never touched. 

In this situation, it would be faster to run the calculation once and store it in a field. But if the field won't be used all of the time it can be difficult to justify the memory for the field, and justify the time spent calculating when the class is initialized, or updated.

This can be solved with property caching. The memory expense is a single dictionary that expands or shrinks as needed.

```c#
        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [NonSerialized()]
        protected Dictionary<object, object> propertyCache;
```

To support it, you need a method that you pass a property to, and it checks whether the property's value is in the cache, keyed to the property's name, if it is; it returns the value, if not; it runs the calculation, caches the value, and returns the cached value.

```c#
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

```c#
        /// <summary>
        /// Gets the axis aligned bounding box of the Shape.
        /// </summary>
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.CalculateBounds(x, y));
```

Hereafter, it is important that whenever a referenced property changes, the property cache gets cleared. Because if the property is ever accessed, the class instance will carry the resulting value of the calculated property around until the dictionary is emptied, or until the end of the life of the class instance. If a parameter has changed that would result in a different calculation and the cache had not been cleared, then the property would continue to present the old values.

Unfortunately this means that under the current syntax rules, if you use property caching you cannot use auto properties for any value that can affect a cached property, and you have to fall back to using properties with fields and a clearProperty() call in the setter.

```c#
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
