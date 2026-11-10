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
/// Abstract class defining each combat scene
/// </summary>
public abstract class TurnBasedScreen : GameScreen
{   
    /// <summary>
    /// player will always start first, most of the time
    /// </summary>
    private bool _isTurn = true;
    /// <summary>
    /// current enemy for the room
    /// </summary>
    public Enemy Enemy;

    /// <summary>
    /// player
    /// </summary>
    protected Player _player;

    protected GraphicsDevice _graphicsDevice;

    protected Texture2D _pixel;


    /*
    Current Plan: Test out primitive TurnBasedScreen build, as well as primitive builds for player and enemy class
    Main thing to test at the moment: Basic turn-switching works, assets of the player are drawn properly (not necessarily in the proper position)
                                      When an action is taken(attack button is clicked), will draw proper asset for enemy turn (right now it should just be the player soul
                                      and a string of text indicating that it is currently the enemies turn),
                                      after appropriate time has elapsed, will switch back to players turn, where the process will be repeat to test repeatability
    */

    public TurnBasedScreen(GraphicsDevice gd, Vector2 c, Player p, SpriteFont f)
    {
        _player = p;
        _center = c;
        _graphicsDevice = gd;
        _font = f;
    }

    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        _pixel = new Texture2D(_graphicsDevice, 1,1);
        _pixel.SetData(new Color[] {Color.White});

        Enemy = new Enemy(_pixel, "Bob", _center, _player);
        Enemy.LoadContent(content);
        _player.LoadContent(content, Enemy);

        
    }

    /// <summary>
    /// Updates the screen (if necessary)
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        _player.Update(gameTime, _isTurn);
        Enemy.Update(gameTime, !_isTurn);
        
        if(_isTurn)
        {
            _isTurn = _player.IsPlayerTurn;
        }
        else
        {
            if(Enemy.IsEnemyTurn == true)
            {
                _isTurn = false;
            }
            if(Enemy.IsEnemyTurn == false)
            {
                _isTurn = true;
            }
        }
        
    }

    /// <summary>
    /// Draws the screen
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch, GameTime gt)
    {
        _player.Draw(spriteBatch, gt);
        Enemy.Draw(spriteBatch, _font);
    }


}