namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D location;

        /// <summary>
        /// 
        /// </summary>
        private Vector2D velocity;

        /// <summary>
        /// 
        /// </summary>
        private Vector2D acceleration;

        /// <summary>
        /// 
        /// </summary>
        private double lifespan;

        /// <summary>
        /// 
        /// </summary>
        public Particle()
        {
            location = Point2D.Empty;
            velocity = Vector2D.Empty;
            acceleration = Vector2D.Empty;
            lifespan = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location { get { return location; } set { location = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Vector2D Velocity { get { return velocity; } set { velocity = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Vector2D Acceleration { get { return acceleration; } set { acceleration = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double Lifespan { get { return lifespan; } set { lifespan = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        void ApplyForce(Vector2D force) => acceleration += force;

        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            velocity += acceleration;
            location += velocity;
        }
    }
}
