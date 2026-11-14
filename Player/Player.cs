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

public class Player
{
    /// <summary>
    /// starting max health
    /// </summary>
    private int _maxHealth = 100;

    /// <summary>
    /// currenthHealth
    /// </summary>
    public int CurrentHealth;

    private Bar _healthBar;

    /// <summary>
    /// starting max health
    /// </summary>
    private int _maxMana = 100;

    /// <summary>
    /// currenthHealth
    /// </summary>
    public int CurrentMana;

    private Bar _manaBar;

    /// <summary>
    /// Equipped weapon
    /// </summary>
    private Weapon _weapon;

    /// <summary>
    /// list of obtained items
    /// </summary>
    private List<Item> _items;

    /// <summary>
    /// list of active relics
    /// </summary>
    private List<Relics> _relics;

    /// <summary>
    /// list of learned skills
    /// </summary>
    private List<Skills> _skills;

    /// <summary>
    /// player soul the player will use to dodge bullets
    /// </summary>
    public PlayerSoul Soul;
    
    /// <summary>
    /// sound effect of getting hit
    /// </summary>
    public SoundEffect hit;

    /// <summary>
    /// sound effect of death
    /// </summary>
    public SoundEffect death;

    /// <summary>
    /// is the player in a state of invunerabiltiy
    /// </summary>
    public bool Invunerable {get; set;} = false;

    /// <summary>
    /// current timer for invunerability
    /// </summary>
    private float _iTimer = 0;

    /// <summary>
    /// Time the player will spend in invunerability, 1.5 seconds by default
    /// </summary>
    public float InvincibleTime = 1.5f;

    /// <summary>
    /// has the player died
    /// </summary>
    public bool IsDead {get; set;}

    /// <summary>
    /// can the player be saved from death
    /// </summary>
    private bool _deathSave;

    /// <summary>
    /// is it the player's turn
    /// </summary>
    public bool IsPlayerTurn;


    /// <summary>
    /// font the class will write with
    /// </summary>
    private SpriteFont _font;
    
    /// <summary>
    /// pixel texture
    /// </summary>
    private Texture2D _pixel;

    /// <summary>
    /// center of the screen
    /// </summary>
    private Vector2 _center;

    /// <summary>
    /// graphics device
    /// </summary>
    private GraphicsDevice _graphicsDevice;


    private Box _displayBox;

    private List<Button> _buttons;

    /// <summary>
    /// Attack power of the player, starts at 10
    /// </summary>
    private int _attackPower = 10;
    private Button _attackButton;

    private Button _itemButton;

    private Button _skillButton;

    private MouseState _currentMouse;

    private MouseState _prevMouse;

    private Point _mousePosition;

    /// <summary>
    /// to modify enemy states when needed (like health)
    /// </summary>
    private Enemy _enemy;

    private Texture2D _seeBoundingCircles;
    

    /// <summary>
    /// Constructs the player class
    /// </summary>
    /// <param name="gd">graphics device</param>
    /// <param name="c">center of the screen</param>
    /// <param name="f">font that will be used</param>
    public Player(GraphicsDevice gd, Vector2 c, SpriteFont f)
    {
        _graphicsDevice = gd;
        _center = c;
        _font = f;
        Soul = new PlayerSoul(_center);
        CurrentHealth = _maxHealth;
        CurrentMana = _maxMana;
    }
    
    /// <summary>
    /// loads the assets for player class
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content, Enemy enemy)
    {   
        _pixel = new Texture2D(_graphicsDevice, 1,1);
        _pixel.SetData(new Color[] {Color.White});

        _enemy = enemy;

        _healthBar = new Bar(_pixel, new Vector2(_center.X/2 * 1.5f - CurrentHealth/2,_center.Y*1.85f), _maxHealth, Color.Green, Color.DarkGreen);
        _manaBar = new Bar(_pixel, new Vector2(_center.X*1.5f - CurrentMana*2, _center.Y*1.85f), _maxHealth, Color.Blue, Color.DarkBlue);

        _attackButton = new Button(_pixel, new Vector2(_center.X/2, _center.Y*1.75f), 100, 30, 5, "ATTACK");
        _itemButton = new Button(_pixel, new Vector2(_center.X, _center.Y*1.75f), 100, 30, 5, "ITEMS");
        _skillButton = new Button(_pixel, new Vector2(_center.X*1.5f, _center.Y*1.75f), 100, 30, 5, "SKILLS");

        _buttons = new List<Button>(){_attackButton, _itemButton, _skillButton};

        foreach(Button b in _buttons)
        {
            b.LoadContent(content);
        }

        _displayBox = new Box(_pixel, new Vector2(_center.X, _center.Y*1.25f), 740, 300, 10);
        _displayBox.LoadContent(content);

        Soul.LoadContent(content);

        _seeBoundingCircles = content.Load<Texture2D>("Ball");
        

    }

    /// <summary>
    /// Updates player class
    /// </summary>
    public void Update(GameTime gameTime, bool playerTurn)
    {
        float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
        IsPlayerTurn = playerTurn;
        if (IsPlayerTurn)
        {
            _currentMouse = Mouse.GetState();
            _mousePosition = new Point(_currentMouse.X, _currentMouse.Y);

            _attackButton.Update(_currentMouse, _prevMouse, _mousePosition, _buttons);
            _itemButton.Update(_currentMouse, _prevMouse, _mousePosition, _buttons);
            _skillButton.Update(_currentMouse, _prevMouse, _mousePosition, _buttons);


            if(_attackButton.IsSelected == true)
            {
                _enemy.Health -= _attackPower;
                IsPlayerTurn = false;
                _attackButton.IsSelected = false;
            }
            else if(_itemButton.IsSelected == true)
            {
                
            }
            else if(_skillButton.IsSelected == true)
            {
                
            }

            _prevMouse = _currentMouse;

        }
        else
        {
            Soul.Update(gameTime);
        }

        if (Invunerable)
        {
            if(_iTimer < InvincibleTime)
            {
                _iTimer += t;
            }
            else
            {
                _iTimer = 0;
                Invunerable = false;
            }
            

        }

        _healthBar.Update(CurrentHealth);
        _manaBar.Update(CurrentMana);
    }

    /// <summary>
    /// Draws the players assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gt)
    {
        if (IsPlayerTurn)
        {
            _attackButton.Draw(spriteBatch, _font);
            _itemButton.Draw(spriteBatch, _font);
            _skillButton.Draw(spriteBatch, _font);
            _displayBox.Draw(spriteBatch);
        }
        else
        {
            if (Soul.HasSword)
            {
                spriteBatch.Draw(_seeBoundingCircles, Soul.SwordHitCircle.Center, new(0,0,64,64), Color.White, 0f, new Vector2(32,32), 1f, SpriteEffects.None, 0);
            }
            Soul.Draw(gt, spriteBatch, Invunerable);
        }
        _healthBar.Draw(spriteBatch);
        spriteBatch.DrawString(_font, $"Health: {CurrentHealth}/{_maxHealth}", new Vector2(_healthBar.BackgroundBar.X, _healthBar.BackgroundBar.Y + 32), Color.White);
        _manaBar.Draw(spriteBatch);
        spriteBatch.DrawString(_font, $"Mana: {CurrentMana}/{_maxMana}", new Vector2(_manaBar.BackgroundBar.X, _manaBar.BackgroundBar.Y + 32), Color.White);

        if(IsDead == true)
        {
            spriteBatch.DrawString(_font, $"You Died :(", new Vector2(_displayBox.Position.X + 3, _displayBox.Position.Y - 5), Color.White);
        }

        
    }

    public void EquipItems(List<ItemSelect> selectedItems)
    {
        foreach (ItemSelect i in selectedItems)
        {
            switch (i.ItemType)
            {
                case ItemType.Relic:
                    
                    break;
                case ItemType.Item:
                    
                    break;
                case ItemType.Skill:
                    
                    break;
                case ItemType.Weapon:
                    
                    break;
            }
        }
    }

}