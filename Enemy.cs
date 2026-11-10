using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Text.Json;
using System.Reflection.Metadata;



namespace ZLGameProject;

/// <summary>
/// This is supposed to be an abstract class
/// </summary>
public  class Enemy
{
    /// <summary>
    /// enemy health
    /// </summary>
    public int Health;

    private int _maxHealth;

    /// <summary>
    /// visual representation of enemy health
    /// </summary>
    protected Bar _healthBar;

    /// <summary>
    /// name of the sprite
    /// </summary>
    protected string _spriteName;

    /// <summary>
    /// enemy sprite
    /// </summary>
    protected Texture2D _sprite;
    
    /// <summary>
    /// center of the sprite
    /// **HARDCODED FOR NOW, MAKE DYNAMIC IF SPRITES ARE MORE THAN 64x64
    /// </summary>
    protected Vector2 _spriteOrigin = new Vector2(32,32);


    protected Vector2 _spritePosition;

    protected Player _player;

    /// <summary>
    ///protected Attack[] attacks; ****REFACTOR ATTACK CLASS, GET RID OF TEMPATTACK****
    /// </summary>
    
    public TestAttack _testAttack;

    public bool IsEnemyTurn;

    protected Vector2 _screenCenter;

    protected Texture2D _pixel;

    /// <summary>
    /// **THIS IS JUST TO CONFIRM THE TURNBASED SYSTEM WORKS DELETE WHEN COMPLETED**
    /// </summary>
    private float timer = 0;

    public bool IsDead = false;

    public Enemy(Texture2D pixel, string spriteName, Vector2 c, Player p)
    {   
        _pixel = pixel;
        _spriteName = spriteName;
        _screenCenter = c;
        _player = p;
        _spritePosition = new Vector2(_screenCenter.X, _screenCenter.Y/2 - 50);
        _testAttack = new TestAttack(15f,_pixel, p, c);
        _maxHealth = 100;
        _healthBar = new Bar(pixel, new Vector2(_spritePosition.X+100, _spritePosition.Y),_maxHealth,Color.Yellow, Color.DarkOrange);
        Health = _maxHealth;
        
    }


    /// <summary>
    /// loads the font for the text
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {
        _sprite = content.Load<Texture2D>(_spriteName);
        _testAttack.LoadContent(content);
        
    }

    /// <summary>
    /// Updates enemy class
    /// </summary>
    public virtual void Update(GameTime gameTime, bool enemyTurn)
    {
        IsEnemyTurn = enemyTurn;
        if (IsEnemyTurn)
        {
            if (!IsDead)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(!_testAttack.IsDone)
                {
                    _testAttack.Update(gameTime);
                }
                else
                {
                    IsEnemyTurn = false;
                    _testAttack.IsDone = false;
                }

                _healthBar.Update(Health);
            }
            if(Health <= 0)
            {
                IsDead = true;
            }

            
        }
    }

    /// <summary>
    /// Draws the enemies assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        Rectangle source = new Rectangle(0,0,64,64);
        if (IsEnemyTurn)
        { 
           _testAttack.Draw(spriteBatch);
        }
        if (IsDead)
        {
            spriteBatch.DrawString(font, "He is dead!", new Vector2(_healthBar.BackgroundBar.X, _healthBar.BackgroundBar.Y), Color.White);
            spriteBatch.Draw(_sprite, _spritePosition, new Rectangle(192, 0, source.X, source.Y), Color.White, 0f, _spriteOrigin, 3f, SpriteEffects.None, 0);
        }
        else
        {
            spriteBatch.Draw(_sprite, _spritePosition, source, Color.White, 0f, _spriteOrigin, 3f, SpriteEffects.None, 0);
            _healthBar.Draw(spriteBatch);
        }
    }
}