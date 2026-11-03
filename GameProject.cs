using System;
using System.IO;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace ZLGameProject;

public class GameProject : Game
{
    private GraphicsDeviceManager _graphics;
    
    private KeyboardState _keyboardState;
    private KeyboardState _prevKeyboardState;
    
    private SpriteBatch _spriteBatch;
    
    private PlayerSoul _player;
    private Bob _bob;

    private Texture2D _atlas;

    private SpriteFont _font;
    private bool _showTitle = true;

    private bool _playerTurn = true;

    private TitleScreen _title;

    private BoundingBox _box;

    private Song _titleSong;

    private Song _battleSong;

    private Song _currentSong;

    private int _translationDirection = 1;

    private float _currentTranslation = 0;

    private const float _translationAmount = 10;


    public GameProject()
    {
        _graphics = new GraphicsDeviceManager(this);
        //_graphics.IsFullScreen = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 768;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.ApplyChanges();
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _title = new TitleScreen(new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2));
        _player = new PlayerSoul(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 0.5f));
        _box = new BoundingBox(new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2));
        _bob = new Bob(_graphics.PreferredBackBufferWidth / 2);
        _bob.Initialize(this, _box, _player);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _atlas = Content.Load<Texture2D>("colored_packed");
        _font = Content.Load<SpriteFont>("font");
        _title.LoadContent(Content);
        _player.LoadContent(Content);
        _box.LoadContent(GraphicsDevice, _spriteBatch);
        _bob.LoadContent(Content);
        _titleSong = Content.Load<Song>("Nicolaas Stulting - Revenge");
        _battleSong = Content.Load<Song>("Rhapsody - The Death of Privacy");
        _currentSong = _titleSong;
        MediaPlayer.Play(_currentSong);
        MediaPlayer.IsRepeating = true;
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
        }
        if (_keyboardState.IsKeyDown(Keys.Q) && !_prevKeyboardState.IsKeyDown(Keys.Q))
        {
            QuickSave(_player, _bob);
            _prevKeyboardState = _keyboardState;
        }
        if (_keyboardState.IsKeyDown(Keys.E) && !_prevKeyboardState.IsKeyDown(Keys.E))
        {
            QuickLoad();
            _prevKeyboardState = _keyboardState;
        }

        if (!_showTitle)
        {
 
            
            if (!_player.IsDead)
            {
                //Changes the song when the screens change
                if(_currentSong != _battleSong)
                {
                    //lowers song volume so the other sound effects can be heard
                    MediaPlayer.Volume = 0.35f;
                    _currentSong = _battleSong;
                    MediaPlayer.Play(_currentSong);
                }
                
                
                _bob.Update(gameTime, _box);
                _player.Update(gameTime);
                    
                if (_bob.Turn)
                {
                    _player.CollideWithBoundingBox(_box);
                    _bob.Attack1.CollideWithBullet(_player);
                }
                else
                {
                    MediaPlayer.Stop();
                }
                
            }
            else
            {
                MediaPlayer.Stop();
            }
            
        }
        else
        {
            //Changes song if the screen changes
            if(_currentSong != _titleSong)
            {
                _currentSong = _titleSong;
                MediaPlayer.Play(_currentSong);
            }
            _title.Update(gameTime); 
        }
        

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        /* Translation code needs work
        if(_bob.Attack1.AttackPhase1 == false)
        {
            _currentTranslation += _translationAmount * _translationDirection;
            if(_currentTranslation >= 300 || _currentTranslation <= -300)
            {
                _translationDirection *= -1;
            }
            
        }

        Matrix transform;
        transform = Matrix.CreateTranslation(_currentTranslation , 0, 0);
        if(_bob.Attack1.IsDone == true)
        {
            transform = Matrix.CreateTranslation(0 , 0, 0);
        }
        */
        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        
        
        if(_showTitle == true)
        {
            
        
            //Draws instructions
            _spriteBatch.DrawString(_font, $"Press Backspace or Esc to exit. \nPress space play", new Vector2(2,2), Color.White);
        
            //Draws title screen
            _title.Draw(_spriteBatch);
        }
        else
        {
            //Draws instructions
            _spriteBatch.DrawString(_font, $"Press Q to save position. \nPress E to load saved position (1 use per save)", new Vector2(2,2), Color.White);
            
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

    public void QuickSave(PlayerSoul s, Bob b)
    {
        var options = new JsonSerializerOptions 
        { 
            IncludeFields = true,
            WriteIndented = true 
        };
        string jsonFileName = "player.json";
        string jsonserialize = JsonSerializer.Serialize(s.Position, options);
        File.WriteAllText(jsonFileName, jsonserialize);
        /*jsonFileName = "bob.json";
        jsonserialize = JsonSerializer.Serialize(b);
        File.WriteAllText(jsonFileName, jsonserialize);*/
    }

    public void QuickLoad()
    {
        var options = new JsonSerializerOptions 
        { 
            IncludeFields = true,
            WriteIndented = true 
        };
        string jsonString = File.ReadAllText("player.json");
        _player.Position = JsonSerializer.Deserialize<Vector2>(jsonString, options);

        //_bob = JsonSerializer.Deserialize<Bob>("bob.json");
    }
}
