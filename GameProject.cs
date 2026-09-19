using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ZLGameProject;

public class GameProject : Game
{
    private GraphicsDeviceManager _graphics;
    
    private KeyboardState _keyboardState;
    
    private SpriteBatch _spriteBatch;

    private TempBatSprite _bat;
    
    private PlayerSoul _player;
    private Bob _bob;

    private Texture2D _atlas;

    private SpriteFont _font;
    private bool _showTitle = true;

    private bool _playerTurn = true;

    private TitleScreenText _title;

    private BoundingBox _box;


    public GameProject()
    {
        _graphics = new GraphicsDeviceManager(this);
        //_graphics.IsFullScreen = true;

        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 960;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.ApplyChanges();
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _bat = new TempBatSprite(new Vector2(-100, _graphics.PreferredBackBufferHeight * 0.5f + 160), _graphics.PreferredBackBufferWidth / 2);
        _title = new TitleScreenText(new Vector2(_graphics.PreferredBackBufferWidth / 2 - 160,  _graphics.PreferredBackBufferHeight * 0.25f));
        _player = new PlayerSoul(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 0.5f));
        _box = new BoundingBox(_graphics);
        _bob = new Bob(_graphics.PreferredBackBufferWidth / 2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _atlas = Content.Load<Texture2D>("colored_packed");
        _font = Content.Load<SpriteFont>("font");
        _title.LoadContent(Content);
        _bat.LoadContent(Content);
        _player.LoadContent(Content);
        _box.LoadContent(GraphicsDevice, _spriteBatch);
        _bob.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back))
            Exit();
        
        _keyboardState = Keyboard.GetState();

        // TODO: Add your update logic here
        if (_keyboardState.IsKeyDown(Keys.Space))
        {
            _showTitle = false;
            Console.WriteLine("Space Pressed");
        }

        if (!_showTitle)
        {   
            
            if (!_player.IsDead)
            {
                _bob.Update(gameTime, _box);
                _player.Update(gameTime);
                _player.CollideWithBoundingBox(_box);
                if (_bob.Turn)
                {
                    _bob.Attack1.CollideWithBullet(_player);
                }
                
            }
            
        }
        else
        {
            
            _bat.Update();
            _title.Update(gameTime); 
        }
        

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        if(_showTitle == true)
        {
            
        
            //Draws bat first so it will be put behind the other objects
            _bat.Draw(gameTime, _spriteBatch);

            //Draws the torah gate and the two soldiers on either side
            //specific pixel addresses so it can scale to full screen well
            _spriteBatch.Draw(_atlas, new Vector2(1600 / 2, 960 / 2 + 100), new Rectangle(22*16, 11*16, 16, 16), Color.White, 0 , new Vector2(8,8), 4.0f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_atlas, new Vector2(1600 / 2 - 64, 960 / 2 + 106), new Rectangle(31*16, 0, 16, 16), Color.OrangeRed, 0 , new Vector2(8,8), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_atlas, new Vector2(1600 / 2 + 64, 960 / 2 + 106), new Rectangle(31*16, 0, 16, 16), Color.OrangeRed, 0, new Vector2(8,8), 3.0f, SpriteEffects.None, 0);
        
            //Draws instructions
            _spriteBatch.DrawString(_font, $"Press Backspace or Esc to exit. \nPress space play", new Vector2(2,2), Color.White);
        
            //Draws title screen text
            _title.Draw(_spriteBatch);
        }
        else
        {
            
            _spriteBatch.DrawString(_font, $"Health: {_player.Health}", new Vector2(_graphics.PreferredBackBufferWidth/2, 9 * _graphics.PreferredBackBufferHeight / 10), Color.White);
            _box.Draw(gameTime);
            _bob.Draw(gameTime, _spriteBatch);
            if (!_player.IsDead)
            {
                _player.Draw(gameTime, _spriteBatch);
            }
            else
            {
                _spriteBatch.DrawString(_font, "You succumbed to the wrath of Bob >:)", _player.Position, Color.White);
            }
            if (!_bob.Turn && !_player.IsDead)
            {
                _spriteBatch.DrawString(_font, "You survived his fury :)", new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2), Color.White);
            }
            
            
        }
        
        
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
