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
/// enumeration for each available stage
/// </summary>
public enum StageSelected
{
    None,
    Boss1,
    Boss2,
    Boss3
}


/// <summary>
/// Class representing the stage select screen
/// </summary>
public class StageSelectScreen : GameScreen
{

    /// <summary>
    /// reference to the graphics device
    /// </summary>
    private GraphicsDevice _graphicsDevice;

    /// <summary>
    /// images for each boss
    /// </summary>
    private Texture2D[] _bosses;
    
    /// <summary>
    /// buttons that the user will press to select their screen
    /// </summary>
    private Rectangle[] _buttons; 

    /// <summary>
    /// draws a black square inside the button rectangle to give the illusion of a hollow rectangle
    /// </summary>
    private Rectangle[] _innerButtons;

    private Texture2D _pixel;

    /// <summary>
    /// keeps track of mouse information
    /// </summary>
    private MouseState _mouseState;

    /// <summary>
    /// previous mouse state
    /// </summary>
    private MouseState _prevMouseState;

    /// <summary>
    /// mouse cursor position
    /// </summary>
    private Point _mouseCoords;

    /// <summary>
    /// enumeration for stage selection
    /// </summary>
    public StageSelected StageSelected = StageSelected.None;

    /// <summary>
    /// source rectangle for the sprites
    /// </summary>
    private Rectangle _source = new Rectangle(0, 0, 64, 64);

    /// <summary>
    /// the "center" for the sprites
    /// </summary>
    private Vector2 _spriteOrigin = new Vector2(0,0);

    private string _text = "Click on an Enemy To Fight";

    private Vector2 _stringCenter;

    /// <summary>
    /// creates a StageSelectScreen
    /// </summary>
    /// <param name="gd">graphics device, used for creating a pixel sprite</param>
    /// <param name="c">center of the screen</param>
    /// <param name="f">sprite font that will be used</param>
    public StageSelectScreen(GraphicsDevice gd, Vector2 c, SpriteFont f)
    {
        _graphicsDevice = gd;
        _center = c;
        _font = f;

        _stringCenter = _font.MeasureString(_text);
        _stringCenter = new Vector2 (_stringCenter.X/2, _stringCenter.Y/2);
    }

    /// <summary>
    /// loads the objects for the screen
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        _screenMusic =  content.Load<Song>("Nicolaas Stulting - Revenge");

        _bosses = new Texture2D[]{content.Load<Texture2D>("Bob"), content.Load<Texture2D>("Bob"), content.Load<Texture2D>("Bob")};
        _buttons = new Rectangle[]{new Rectangle((int)(_center.X/2-250), (int)(_center.Y-25), 350, 350), 
                                   new Rectangle((int)_center.X-175, (int)(_center.Y-25), 350, 350), 
                                   new Rectangle((int)(_center.X*1.5 - 100), (int)(_center.Y-25), 350, 350)};
        _innerButtons = new Rectangle[]{new Rectangle((int)(_center.X/2 - 250 + 10),(int)(_center.Y-15), 330, 330), 
                                        new Rectangle((int)(_center.X - 175 + 10),(int)(_center.Y-15), 330, 330), 
                                        new Rectangle((int)(_center.X*1.5 - 100 + 10),(int)(_center.Y-15), 330, 330)};

        _pixel = new Texture2D(_graphicsDevice, 1,1);
        _pixel.SetData(new Color[] {Color.White});

    }

    /// <summary>
    /// Updates the Stage Select Screen
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        if (!SongPlaying)
        {
            MediaPlayer.Play(_screenMusic);
            SongPlaying = true;
        }

        _mouseState = Mouse.GetState();

        _mouseCoords = new Point(_mouseState.X, _mouseState.Y);

        //stage select is ideally temporary, so hard coded values are fine
        for(int i = 0; i < 3; i++)
        {
            if (_buttons[i].Contains(_mouseCoords) && (int)_mouseState.LeftButton == 1 && (int)_prevMouseState.LeftButton == 0)
            {
                StageSelected = (StageSelected)i+1;
            }
        }
        _prevMouseState = _mouseState;
    }

    /// <summary>
    /// Draws The title screen Assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        
        for(int i = 0; i < 3; i++)
        {
            
            if (_buttons[i].Contains(_mouseCoords))
            {
                spriteBatch.Draw(_pixel, _buttons[i], Color.Green);
            }
            else
            {
                spriteBatch.Draw(_pixel, _buttons[i], Color.White);
            }
            spriteBatch.Draw(_pixel, _innerButtons[i], Color.Black);
        }

        spriteBatch.Draw(_bosses[0], new Vector2(_center.X/2 - 250 + 10, (int)(_center.Y-15)), _source, Color.White, 0, _spriteOrigin, 5.156f, SpriteEffects.None, 0);
        spriteBatch.Draw(_bosses[1], new Vector2(_center.X - 175 + 10, (int)(_center.Y-15)), _source, Color.White, 0, _spriteOrigin, 5.156f, SpriteEffects.None, 0);
        spriteBatch.Draw(_bosses[2], new Vector2(_center.X*1.5f - 100 + 10, (int)(_center.Y-15)), _source, Color.White, 0, _spriteOrigin, 5.156f, SpriteEffects.None, 0);

        spriteBatch.DrawString(_font, _text, new Vector2(_center.X, _center.Y/2), Color.White, 0f, _stringCenter, 4f, SpriteEffects.None, 0);

    }
}