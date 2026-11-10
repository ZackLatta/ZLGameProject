using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZLGameProject;

///
/// A static library for detecting collisions
/// 
public static class CollisionHelper
{
    /// <summary>
    /// Detects the collision between two BoundingCircles
    /// </summary>
    /// <param name="a">the first bounding circle</param>
    /// <param name="b">the second bounding circle</param>
    /// <returns>True for collision</returns>
    public static bool Collides(BoundingCircle a, BoundingCircle b)
    {
        //computationally less pricey than Math.Sqrt
        return Math.Pow(a.Radius + b.Radius, 2)>= Math.Pow(a.Center.X - b.Center.X, 2) + Math.Pow(a.Center.Y - b.Center.Y, 2);
    }

    /// <summary>
    /// Detects collisions between two bounding rectangles
    /// </summary>
    /// <param name="a">first rectangle</param>
    /// <param name="b">second rectangle</param>
    /// <returns>True for collision, false otherwise</returns>
    public static bool Collides(BoundingRectangle a, BoundingRectangle b)
    {
        //checks by seeing if the rectangles are not colliding, then inverts the result
        return !(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top);
    }

    /// <summary>
    /// Detects collision between a rectangle and a circle
    /// </summary>
    /// <param name="c">the BoundingCircle</param>
    /// <param name="r">the BoundingRectangle</param>
    /// <returns>whether a collision has occured</returns>
    public static bool Collides(BoundingCircle c, BoundingRectangle r)
    {
        float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
        float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

        return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);
    }

    public static bool Collides(BoundingRectangle r, BoundingCircle c) => Collides(c, r);
}