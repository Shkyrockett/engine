using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Particle
    {
        Point2D Location;
        Vector2D Velocity;
        Vector2D Acceleration;
        double Lifespan;

        void ApplyForce(Vector2D force) => Acceleration += force;

        void Update()
        {
            Velocity += Acceleration;
            Location += Velocity;
        }
    }
}
