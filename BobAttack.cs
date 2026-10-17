using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;

namespace ZLGameProject;

public class BobAttack: Attack
{


    //the timer for when a new ball is added
    private double addBall;

    private int _ballCount = 0;

    private Bullet emitter;

    private bool _attackPhase1 = true;

    public bool AttackPhase1 {get => _attackPhase1;}

    private BulletParticleSystem bps;

    public BobAttack()
    {
        Bullets = new List<Bullet>();
        System.Random rand = new System.Random();
        for(int i = 0; i < 5; i++)
        {
            Bullets.Add(new Bullet(new Vector2(rand.Next(500,700), rand.Next(400, 600)), rand.Next(3,7), rand.Next(3,7),(float)rand.NextDouble() * 1.5f)) ;
        }
        emitter = new Bullet(new Vector2(500, 350), 2, 0, 0.5f);
    }

    public void Initialize(Game game, BoundingBox b, PlayerSoul p)
    {
        bps = new BulletParticleSystem(game, emitter, b, p);
        game.Components.Add(bps);
    }

    public override void LoadContent(ContentManager content)
    {
        emitter.LoadContent(content);
        base.LoadContent(content);
    }

    /// <summary>
    /// Updates the state of the attack
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime, BoundingBox b)
    {
        addBall += gameTime.ElapsedGameTime.TotalSeconds;
        _attackTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_attackPhase1 == true)
        {
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

            if(_attackTime >= 10)
            {
                addBall = 0;
                _attackTime = 0;
                _attackPhase1 = false;
                
                //gets the bullets out of the bounding box
                foreach(var x in Bullets)
                {
                    x.Position = new Vector2(0,0);
                    x.Velocity = new Vector2(0,0);
                }
            }
        }
        else
        {
            emitter.Update();
            bps.Update(gameTime);
            emitter.CollideWithBoundingBox(b);

            if(_attackTime >= 20)
            {
                IsDone = true;
                addBall = 0;
                _attackTime = 0;
            }
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
                    //p.death.Play();
                }
                else
                {
                    //p.hit.Play();
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
        if(_attackPhase1 == true)
        {
            for(int i = 0; i < _ballCount; i++)
            {
                Bullets[i].Draw(spriteBatch);
            }
        }
            emitter.Draw(spriteBatch);
        
    }

}

    
    
