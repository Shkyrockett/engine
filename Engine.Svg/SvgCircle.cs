using System.Diagnostics;
#if WCF_SUPPORTED
    using System.Runtime.Serialization;
#endif
using System.Xml.Serialization;

namespace Engine.Svg
{
#if WCF_SUPPORTED
    [DataContract(Name = "circle", Namespace = "http://www.w3.org/2000/svg")]
#endif
    [XmlType(TypeName = "circle", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("Center = {circle.Center} Radius={circle.Radius}")]
    public class SvgCircle
    {
        public Circle circle;
    }
}
