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


namespace ZLGameProject;

/// <summary>
/// Temporary bullet class
/// </summary>
public abstract class TempBullet
{   
    protected int _damage;
    public bool IsBreakable;

    protected Texture2D _pixel;

    public Vector2 Position;

    public Vector2 Velocity;

    protected Color _color;   

    public TempBullet(Texture2D pixel, bool breakable, int damage)
    {
        _pixel = pixel;
        IsBreakable = breakable;
        if(IsBreakable == true)
        {
            _color = Color.Yellow;
        }
        else
        {
            _color = Color.White;
        }
        _damage = damage;
    }

    /// <summary>
    /// creates the related TempBullet content
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {
        
    }

    /// <summary>
    /// Updates the state of TempBullet
    /// </summary>
    public virtual void Update(GameTime gameTime, Player p)
    {

    }
    

    /// <summary>
    /// Draws the button
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {

    }

    public virtual bool CollidesWithPlayer(PlayerSoul p)
    {
        return false;
    }
}