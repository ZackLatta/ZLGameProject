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
public class TitleScreen
{
    private TitleScreenText _text;
    private Tilemap _tilemap;


    public TitleScreen(Vector2 center)
    {
        _text = new TitleScreenText(center);
        _tilemap = new Tilemap("titlemap.txt", center);
    }

    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        _text.LoadContent(content);
        _tilemap.LoadContent(content);

    }

    /// <summary>
    /// Updates the title screen text
    /// </summary>
    public void Update(GameTime gameTime)
    {
        _text.Update(gameTime);       
    }

    /// <summary>
    /// Draws The title screen Assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
        _text.Draw(spriteBatch);

    }
}

