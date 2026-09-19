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

    public bool IsDone;

    public List<Bullet> Bullets;

    /// <summary>
    /// loads the bullets
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        foreach(var b in Bullets)
        {
            b.LoadContent(content);
        }
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

    
    
