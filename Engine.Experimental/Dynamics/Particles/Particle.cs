// <copyright file="Particle.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The particle class.
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle()
        {
            Location = Point2D.Empty;
            Velocity = Vector2D.Empty;
            Acceleration = Vector2D.Empty;
            Lifespan = 0;
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2D Velocity { get; set; }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public Vector2D Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the lifespan.
        /// </summary>
        public double Lifespan { get; set; }

        /// <summary>
        /// The apply force.
        /// </summary>
        /// <param name="force">The force.</param>
        public void ApplyForce(Vector2D force) => Acceleration += force;

        /// <summary>
        /// Update.
        /// </summary>
        public void Update()
        {
            Velocity += Acceleration;
            Location += Velocity;
        }
    }
}
