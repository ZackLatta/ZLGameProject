using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.Dynamic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;



namespace ZLGameProject;

/// <summary>
/// Temporary bullet class
/// </summary>
public class TempRectBullet : TempBullet
{
    public BoundingRectangle HitBox;

    public Vector2 WidthHeight;


    public TempRectBullet(Texture2D pixel, bool breakable, Vector2 position, int width, int height, Vector2 velocity, int damage) : base(pixel, breakable, damage)
    {
        Position = position;
        HitBox = new BoundingRectangle(position, width, height);
        WidthHeight = new Vector2(width, height);
        Velocity = velocity; 
    }

    /// <summary>
    /// Updates the state of the bullet
    /// </summary>
    public override void Update(GameTime gameTime, Player p)
    {
        Position += Velocity;
        HitBox.X = Position.X;
        HitBox.Y = Position.Y;
        if(p.Soul.HasSwung)
        {
            if(CollisionHelper.Collides(HitBox, p.Soul.SwordHitCircle))
            {
                if(p.Soul.Facing == PlayerSoul.Direction.Left)
                {
                    p.Soul.ReflectedInitialVelocity += new Vector2(5, 0);
                }
                if(p.Soul.Facing == PlayerSoul.Direction.Right)
                {
                    p.Soul.ReflectedInitialVelocity += new Vector2(-5, 0);
                }
                if(p.Soul.Facing == PlayerSoul.Direction.Up)
                {
                    p.Soul.ReflectedInitialVelocity += new Vector2(0, 5);
                }
                if(p.Soul.Facing == PlayerSoul.Direction.Down)
                {
                    if(p.Soul.SoulState == PlayerSoul.State.Blue && p.Soul.JumpState == PlayerSoul.BlueState.Falling)
                    {
                        p.Soul.ReflectedInitialVelocity += new Vector2(0,-8);
                    }
                    else
                    {
                        p.Soul.ReflectedInitialVelocity += new Vector2(0, -5);
                    }
                    
                    
                }
            }
        }

        if(!p.Invunerable && CollisionHelper.Collides(HitBox, p.Soul.SoulBoundingCircle))
        {
            p.CurrentHealth -= _damage;
            p.Invunerable = true;
        }

    }

    /// <summary>
    /// Draws the bullet
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, Position, null, _color, 0f, Vector2.Zero, WidthHeight, SpriteEffects.None, 0f);
    }

    public override bool CollidesWithPlayer(PlayerSoul p)
    {
        return CollisionHelper.Collides(p.SoulBoundingCircle, HitBox);
    }

}