using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListUnits(Type units)
        {
            Assembly assembly = Assembly.GetAssembly(units);
            return GetAssemblyTypesByInterface(assembly, units);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static List<Type> GetAssemblyTypesByInterface(Assembly assembly, Type classType)
        {
            return new List<Type>
            (
                from type in assembly.GetTypes()
                where type.GetInterfaces().Contains(classType)
                select type
            );
        }
    }
}
