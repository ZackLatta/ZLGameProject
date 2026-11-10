using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZLGameProject;

///
/// A struct representing circular bounds
/// 
public struct BoundingCircle
{
    /// <summary>
    /// The center of the bounding circle
    /// </summary>
    public Vector2 Center;

    /// <summary>
    /// radius of the bounding circle
    /// </summary>
    public float Radius;

    /// <summary>
    /// constructs a new bounding circle
    /// </summary>
    /// <param name="center">center</param>
    /// <param name="radius">radius</param>
    public BoundingCircle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    /// <summary>
    /// Tests for a collisions between this and another bounding circle
    /// </summary>
    /// <param name="other">The other bounding circle</param>
    /// <returns>True for collision, false otherwise</returns>
    public bool CollidesWith(BoundingCircle other)
    {
        return CollisionHelper.Collides(this, other);
    }

        public bool CollidesWith(BoundingRectangle other)
    {
        return CollisionHelper.Collides(this, other);
    }
}

