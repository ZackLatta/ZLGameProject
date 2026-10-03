using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ZLGameProject;

public class BobAttack: Attack
{


    //the timer for when a new ball is added
    private double addBall;

    private int _ballCount = 0;

    public BobAttack()
    {
        Bullets = new List<Bullet>();
        System.Random rand = new System.Random();
        for(int i = 0; i < 5; i++)
        {
            Bullets.Add(new Bullet(new Vector2(rand.Next(500,700), rand.Next(400, 600)), rand.Next(3,7), rand.Next(3,7),(float)rand.NextDouble() * 1.5f)) ;
        }
    }

    /// <summary>
    /// Updates the state of the attack
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime, BoundingBox b)
    {
        addBall += gameTime.ElapsedGameTime.TotalSeconds;
        _attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(addBall >= 2 && _ballCount < 5)
        {
            _ballCount++;
            addBall = 0;
        }

        for(int i = 0; i < _ballCount; i++)
        {
            Bullets[i].Update();
            Bullets[i].CollideWithBoundingBox(b);
        }

        if(_attackTime >= 30)
        {
            IsDone = true;
            addBall = 0;
            _attackTime = 0;
        }

 
    }

        public void CollideWithBullet(PlayerSoul p)
    {
        for(int i = 0; i < _ballCount; i++)
        {
            if(!p.Invunerable && Math.Pow(p.Center.X * 0.75f + Bullets[i].Radius, 2)>= Math.Pow(p.Position.X - Bullets[i].Position.X, 2) + Math.Pow(p.Position.Y- Bullets[i].Position.Y, 2))
            {
                p.Health -= 5;
                p.Invunerable = true;
                if(p.Health <= 0)
                {
                    p.death.Play();
                }
                else
                {
                    p.hit.Play();
                }

            }
        }
        

    }


    /// <summary>
    /// Draws the attack
    /// </summary>
    /// 
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        for(int i = 0; i < _ballCount; i++)
        {
            Bullets[i].Draw(spriteBatch);
        }
    }

}

    
    
