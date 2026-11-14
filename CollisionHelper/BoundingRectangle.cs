using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZLGameProject;

///
/// A struct representing BoundingRectangle
///
public struct BoundingRectangle
{
    public float X;
    public float Y;
    public float Width;
    public float Height;
    public float Left => X;
    public float Right => X + Width;
    public float Top => Y;
    public float Bottom => Y + Height;

    public BoundingRectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public BoundingRectangle(Vector2 vector, float width, float height)
    {
        X = vector.X;
        Y = vector.Y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Detects collision between this and another rectangle
    /// </summary>
    /// <param name="other">other rectangle</param>
    /// <returns>True if collision, false otherwise</returns>
    public bool CollidesWith(BoundingRectangle other)
    {
        return CollisionHelper.Collides(this, other);
    }

    public bool CollidesWith(BoundingCircle other)
    {
        return CollisionHelper.Collides(other, this);
    }
}