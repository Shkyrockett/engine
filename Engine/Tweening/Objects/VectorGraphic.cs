using System.Collections.Generic;
using System.Drawing;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class VectorGraphic
        : IImage
    {
        /// <summary>
        /// 
        /// </summary>
        public VectorGraphic()
        {
            Bitmap = new Bitmap(1, 1);
            Handles = new List<PointF>();
            Filename = "VectorGraphics.vector";
            Name = "VectorGraphics";
            DisplayName = "VectorGraphics";
            Items = new List<IRenderable>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PointF> Handles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IRenderable> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone() => new object();
    }
}
