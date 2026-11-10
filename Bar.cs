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


namespace ZLGameProject;

/// <summary>
/// Bar classed, used for health and mana bars
/// </summary>
public class Bar
{

    public Rectangle BackgroundBar;

    private Rectangle _foregroundBar;

    public Color BackgroundColor;

    public Color ForegroundColor;

    private Texture2D _pixel;

    /// <summary>
    /// Creates the Bar object
    /// </summary>
    /// <param name="gd">Graphics Device</param>
    /// <param name="position">Position of the bars</param>
    /// <param name="lengthOfBar">Length of the bar</param>
    /// <param name="foregroundColor">Color of the forgroundBar</param>
    /// <param name="backgroundColor">Color of the backgroundBar</param>
    public Bar(Texture2D pixel, Vector2 position, int lengthOfBar, Color foregroundColor, Color backgroundColor)
    {
        BackgroundBar = new Rectangle((int)position.X, (int)position.Y, lengthOfBar*2, 25);
        _foregroundBar = new Rectangle((int)position.X, (int)position.Y, lengthOfBar*2, 25);
        _pixel = pixel;
        BackgroundColor = backgroundColor;
        ForegroundColor = foregroundColor;
    }
    
    /// <summary>
    /// loads the pixel texture
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
    }

    /// <summary>
    /// updates the foreground bar
    /// </summary>
    /// <param name="length">new length</param>
    public void Update(int length)
    {
        ///length is multiplied by two because the width was multiplied by 2 during construction
        if(_foregroundBar.Width != length * 2)
        {
            _foregroundBar = new Rectangle(_foregroundBar.X, _foregroundBar.Y, length *2, 25);
        }
    }

    /// <summary>
    /// Draws the bar
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, BackgroundBar, BackgroundColor);
        spriteBatch.Draw(_pixel, _foregroundBar, ForegroundColor);

    }

    /// <summary>
    /// helper method that modifies the bar the maximum capacity of a stat has been changed
    /// </summary>
    /// <param name="newMaxStat">The new maximum stat value</param>
    public Bar ModifyBar(int newMaxStat, Bar b)
    {
        return new Bar(_pixel, new Vector2(b.BackgroundBar.X, b.BackgroundBar.Y), newMaxStat, b.ForegroundColor,b.BackgroundColor);
    }
    
}