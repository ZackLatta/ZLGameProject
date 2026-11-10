using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ZLGameProject;

public abstract class Attack
{

    protected float _attackTime = 0;

    protected float _attackDuration;

    public bool IsDone;

    private List<ParticleSystem> _attackPattern;
    private List<IParticleEmitter> _emitterObjects;

    
    public Attack(float attackDuration)
    {
        
    }

    /// <summary>
    /// loads the bullets
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {
    }

   
    /// <summary>
    /// Updates the state of the attack
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public virtual void Update(GameTime gameTime)
    {
        
 
    }


    /// <summary>
    /// Draws the attack
    /// </summary>
    /// <param name="gameTime">the game time</param>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }

}

    
    
