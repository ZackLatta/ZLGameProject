using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace ZLGameProject;

/// <summary>
/// Class representing the Title Screen Text
/// </summary>
public class TitleScreenText
{
    private SpriteFont _font;

    private bool _falling = false;

    /// <summary>
    /// initial position of the text
    /// </summary>
    private Vector2 _initialPosition;


    /// <summary>
    /// position of the text
    /// </summary>
    public Vector2 Position;

    public TitleScreenText(Vector2 center)
    {
        _initialPosition = center;
        Position = _initialPosition;
    }

    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        _font = content.Load<SpriteFont>("title");

    }

    /// <summary>
    /// Updates the title screen text
    /// </summary>
    public void Update(GameTime gameTime)
    {
        if (!_falling)
        {
            Position += new Vector2(0,-0.1f);
            if(Position.Y <= _initialPosition.Y - 10)
            {
                _falling = true;
            }
        }
        else
        {
            Position += new Vector2(0,0.1f);
            if(Position.Y >= _initialPosition.Y)
            {
                _falling = false;
            }
        }
            
    }

    /// <summary>
    /// Draws The title screen text
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, $"GAME PROJECT", Position, Color.White);
    }
}

