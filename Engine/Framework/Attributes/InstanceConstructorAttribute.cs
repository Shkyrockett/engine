/*
 * Copyright (c) 2005-2007 Jonathan Mark Porter
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
 * the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
    public sealed class InstanceConstructorAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        string[] parameterNames;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterNames">"CSV list"</param>
        public InstanceConstructorAttribute(string parameterNames)
        {
            this.parameterNames = parameterNames.Split(',');
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] ParameterNames
            => parameterNames;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="paramNames"></param>
        /// <returns></returns>
        public static ConstructorInfo GetConstructor(Type t, out string[] paramNames)
        {
            foreach (ConstructorInfo method in t.GetConstructors())
            {
                object[] atts = method.GetCustomAttributes(typeof(InstanceConstructorAttribute), true);

                if (atts.Length > 0)
                {
                    var att = (InstanceConstructorAttribute)atts[0];
                    if (method.GetParameters().Length == att.ParameterNames.Length)
                    {
                        paramNames = att.ParameterNames;
                        return method;
                    }
                }
            }
            paramNames = null;
            return null;
        }
    }
}
