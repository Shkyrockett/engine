using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyFrame3D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFrame3D"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="time">The time.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeyFrame3D(Point3D position, Quaternion4D rotation, Size3D scale, double time)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFrame3D"/> class.
        /// </summary>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeyFrame3D()
            : this(Point3D.Empty, Quaternion4D.Identity, Size3D.Unit, 0d)
        { }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Point3D Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public Quaternion4D Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public Size3D Scale { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public double Time { get; set; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeyFrame3D Copy()
            => new KeyFrame3D
            {
                Position = Position,
                Rotation = Rotation,
                Scale = Scale,
                Time = Time
            };
    }
}
