# Coding Style and Syntax Conventions

This document is not intended to state the law. Rather, it is intended as a guideline for code consistency to make the entire project feel like it is a unified platform.

For the most part, follow the standards for .NET libraries in [C# Coding Conventions (C# Programming Guide)](https://msdn.microsoft.com/en-us/library/ff926074.aspx). Visual Studio defaults fit these standards. The Ctrl + k + CTRL + d shortcut key combination are your best friend for beautifying code.

In high level summary:

### Commenting

Use comments as needed to clarify code for other coders.

### XML Comments

Please use triple slash XML comments for all methods, and objects. type out three slashes "\\\" and let Visual Studio build out the XML comments. Briefly describe what each does. These comments are used as the Intelisense text in tool tips. They also act as valuable documentation.

### Copyright notice

Code files should start with an XML copyright header like the following, to indicate copyright and license.

```
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

Please avoid Screaming All Caps and Hungarian notation. They are difficult to read and are inconsistent with the naming conventions of the .NET framework.

Use Pascal casing for public Namespace, class, structure, enumeration, event, property and method names.

```
namespace Engine
{
    public enum MyEnum
    {
        Value1,
        Value2
    }

    public struct MyStruct
    {
        public int MyProperty { get; set; }
    }

    public class MyClass
    {
        public int MyMethod()
        {
            return 0;
        }
    }
}
```

Use Camel casing for parameters, member variables, local variables, 

### Indentation

Use the Visual Studio default of 4 spaces, tabs interpreted as spaces. With the default Visual Studio install; Ctrl + k + d should auto correct indentation in most cases.

### Bracing 

In general, use Allman style spacing with curly braces on their own lines.

```
private bool test()
{
    return true;
}
```

Property setters and getters that only have one line should be placed on one line

```
public int Number
{
    get { return number; }
    set { number = value; }
}
``` 

```
public double X { get; set; }
```

For methods _such as default constructors with pass through_ which are intended to be empty, placing opening and closing curly braces on the same line is recommended. The only intent is to signal that the method is indeed intended to be empty and not a placeholder for a future method.

```
private Cleaner()
{}
```

### Multiple lines

For classes that inherit from another class, use interfaces, or methods that pass through to other methods, such as constructors, place the colon and the inherit/interface/this on it's own line to make it easier to see differences in code review.

```
public Point()
    : this(0, 0)
{}
```

Method parameters should generally be on the same line, unless the number of parameters are unwieldy long and would make more sense grouped together on separate lines. Methods involving the raw parameters of Matrices, for example should be on multiple logically organized lines.

## Profiling

If you have several choices of how  to do things, profile the various methods, checking for accuracy and speed, then find the best compromise of the two.
