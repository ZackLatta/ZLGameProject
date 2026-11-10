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

namespace ZLGameProject;

/// <summary>
/// Class representing the Title Screen Text
/// </summary>
public class TitleScreen : GameScreen
{
    private TitleScreenText _text;
    private Tilemap _tilemap;


    public TitleScreen(Vector2 center, SpriteFont f)
    {
        _font = f;
        _text = new TitleScreenText(center, _font);
        _tilemap = new Tilemap("titlemap.txt", center);
   
    }

    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        _tilemap.LoadContent(content);
        _screenMusic =  content.Load<Song>("Nicolaas Stulting - Revenge");

    }

    /// <summary>
    /// Updates the title screen text
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        if (!SongPlaying)
        {
            MediaPlayer.Play(_screenMusic);
            SongPlaying = true;
        }
        _text.Update(gameTime);       
    }

    /// <summary>
    /// Draws The title screen Assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        _tilemap.Draw(spriteBatch);
        _text.Draw(spriteBatch);
        

    }
}

