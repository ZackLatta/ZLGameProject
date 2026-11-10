using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.ConstrainedExecution;

namespace ZLGameProject;

/// <summary>
/// test attack, just makes a bounding box that the player can interact with
/// </summary>
public class TestAttack : TempAttack
{   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackDuration"></param>
    /// <param name="pixel"></param>
    /// <param name="p"></param>
    public TestAttack(float attackDuration, Texture2D pixel, Player p, Vector2 c) : base(attackDuration, pixel, p, c)
    {
        
    }

    /// <summary>
    /// Updates the state of the attack
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach(TempRectBullet bullet in bullets)
        {
            bullet.Update(gameTime, _player);
            if(CollisionHelper.Collides(_boundingBox.Left, bullet.HitBox))
            {
                bullet.Velocity.X *= -1;
            }
            if(CollisionHelper.Collides(_boundingBox.Right, bullet.HitBox))
            {
                bullet.Velocity.X *= -1;
            }
            if(CollisionHelper.Collides(_boundingBox.Top, bullet.HitBox))
            {
                bullet.Velocity.Y *= -1;
            }
            if(CollisionHelper.Collides(_boundingBox.Bottom, bullet.HitBox))
            {
                bullet.Velocity.Y *= -1;
            }
        }

 
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach(TempRectBullet bullet in bullets)
        {
            bullet.Draw(spriteBatch);
        }
        
    }




}