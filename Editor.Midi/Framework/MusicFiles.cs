using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Engine;
using Engine.File;

namespace EventEditorMidi
{
    /// <summary>
    /// 
    /// </summary>
    //[ElementName(nameof(MusicFiles))]
    //[DisplayName("Music Files")]
    public class MusicFiles
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MediaFile> Midi { get; set; } = new List<MediaFile>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListFileFormats()
        {
            var shapeType = typeof(IMediaContainer);
            var assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyInterfaces(assembly, shapeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Type> ListEventFormats()
        {
            var shapeType = typeof(IMidiEvent);
            var assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyInterfaces(assembly, shapeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static List<Type> GetAssemblyTypes(Assembly assembly, Type classType)
        {
            var typeList = new List<Type>();
            foreach (Type type in assembly.GetTypes().ToArray())
            {
                if (type.BaseType == classType)
                    typeList.Add(type);
            }

            return typeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static List<Type> GetAssemblyInterfaces(Assembly assembly, Type classType)
        {
            var typeList = new List<Type>();
            foreach (Type type in assembly.GetTypes().Where(t => t.GetInterfaces().Contains(classType)).ToArray())
                typeList.Add(type);

            return typeList;
        }
    }
}
