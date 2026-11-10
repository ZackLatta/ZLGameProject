using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;

namespace ZLGameProject;

/// <summary>
/// Abstract class defining what should be in each screen
/// </summary>
public abstract class GameScreen
{
    /// <summary>
    /// Song that will play on the screen
    /// </summary>
    protected Song _screenMusic;

    /// <summary>
    /// keeps track of if the song is playing
    /// </summary>
    public bool SongPlaying = false;

    /// <summary>
    /// font that will be used
    /// </summary>
    protected SpriteFont _font;

    /// <summary>
    /// Center of the screen
    /// </summary>
    protected Vector2 _center;


    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {

    }

    /// <summary>
    /// Updates the screen (if necessary)
    /// </summary>
    public virtual void Update(GameTime gameTime)
    {
 
    }

    /// <summary>
    /// Draws the screen
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {

    }
}