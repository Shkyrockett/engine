using Engine;
using Engine.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventEditorMidi
{
    /// <summary>
    /// The music files class.
    /// </summary>
    //[ElementName(nameof(MusicFiles))]
    //[DisplayName("Music Files")]
    public class MusicFiles
    {
        /// <summary>
        /// Gets or sets the midi.
        /// </summary>
        public List<MediaFile> Midi { get; set; } = new List<MediaFile>();

        /// <summary>
        /// The list file formats.
        /// </summary>
        /// <returns>The <see cref="List{Type}"/>.</returns>
        public static List<Type> ListFileFormats()
        {
            var shapeType = typeof(IMediaContainer);
            var assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyInterfaces(assembly, shapeType);
        }

        /// <summary>
        /// The list event formats.
        /// </summary>
        /// <returns>The <see cref="List{Type}"/>.</returns>
        public static List<Type> ListEventFormats()
        {
            var shapeType = typeof(IMidiEvent);
            var assembly = Assembly.GetAssembly(shapeType);
            return GetAssemblyInterfaces(assembly, shapeType);
        }

        /// <summary>
        /// Get the assembly types.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="classType">The classType.</param>
        /// <returns>The <see cref="List{Type}"/>.</returns>
        public static List<Type> GetAssemblyTypes(Assembly assembly, Type classType)
        {
            var typeList = new List<Type>();
            foreach (var type in assembly?.GetTypes().ToArray())
            {
                if (type.BaseType == classType)
                {
                    typeList.Add(type);
                }
            }

            return typeList;
        }

        /// <summary>
        /// Get the assembly interfaces.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="classType">The classType.</param>
        /// <returns>The <see cref="List{Type}"/>.</returns>
        private static List<Type> GetAssemblyInterfaces(Assembly assembly, Type classType)
        {
            var typeList = new List<Type>();
            foreach (var type in assembly.GetTypes().Where(t => t.GetInterfaces().Contains(classType)).ToArray())
            {
                typeList.Add(type);
            }

            return typeList;
        }
    }
}
