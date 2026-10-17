using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ZLGameProject;

public class BulletParticleSystem: ParticleSystem
{
    Random rand = new Random();
    IParticleEmitter _emitter;

    private float _addParticle;

    public BulletParticleSystem(Game game, IParticleEmitter emitter, BoundingBox b, PlayerSoul p) : base(game, 100, b, p)
    {
        _emitter = emitter;

    }

    protected override void InitializeConstants()
    {
        textureFilename = "Ball";

        minNumParticles = 1;
        maxNumParticles = 1;

        blendState = BlendState.Additive;
        DrawOrder = AdditiveBlendDrawOrder;
    }

    protected override void InitializeParticle(ref Particle p, Vector2 where)
    {
        Vector2 velocity;
        if(rand.Next(0,2) == 1)
        {
            velocity = new Vector2(_emitter.Velocity.X * 40, 40);
        }
        else
        {
            velocity = -1 * new Vector2(_emitter.Velocity.X * 40, 40);
        }
        
        
        

        //var acceleration = Vector2.UnitY * 400;

        var scale = 0.25f;

        var lifetime = 15;
        p.Center = new Vector2(32,32) * scale;

        p.Color = Color.Blue;

        p.Initialize(where, velocity, scale: scale, lifetime:lifetime);
    }

    public override void Update(GameTime gameTime)
    {

        base.Update(gameTime);
        _addParticle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_addParticle >= 1)
        {
            AddParticles(_emitter.Position);
            _addParticle = 0;
        }
        
    }

    
}