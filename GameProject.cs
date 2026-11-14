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
public enum ScreenState
{
    Title,
    StageSelect,
    KitSelect1,
    KitSelect2,
    KitSelect3,
    TurnBased1,
    TurnBased2,
    TurnBased3
}
public class GameProject : Game
{
    private GraphicsDeviceManager _graphics;
    
    private ScreenState _screen = ScreenState.StageSelect;
    
    //triangle to draw
    private Triangle triangle;

    private KeyboardState _keyboardState;
    private KeyboardState _prevKeyboardState;
    
    private SpriteBatch _spriteBatch;
    
    private Player _player1;
    private Player _player2;
    private Player _player3;

    private Texture2D _atlas;

    private SpriteFont _font;


    private TitleScreen _title;

    private StageSelectScreen _stageSelect;

    private KitSelectScreen1 _kitSelect1;
    private KitSelectScreen2 _kitSelect2;
    private KitSelectScreen3 _kitSelect3;

    private TurnBasedScreen1 _testTurnBased1;
    private TurnBasedScreen2 _testTurnBased2;
    private TurnBasedScreen3 _testTurnBased3;

    private BoundingBox _box;


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
        triangle = new Triangle(this);
        _font = Content.Load<SpriteFont>("font");
        _title = new TitleScreen(new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _stageSelect = new StageSelectScreen(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _kitSelect1 = new KitSelectScreen1(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _kitSelect2 = new KitSelectScreen2(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _kitSelect3 = new KitSelectScreen3(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _player1 = new Player(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _player2 = new Player(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _player3 = new Player(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _font);
        _testTurnBased1 = new TurnBasedScreen1(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _player1, _font);
        _testTurnBased2 = new TurnBasedScreen2(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _player2, _font);
        _testTurnBased3 = new TurnBasedScreen3(GraphicsDevice, new Vector2(_graphics.PreferredBackBufferWidth / 2,  _graphics.PreferredBackBufferHeight / 2), _player3, _font);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _title.LoadContent(Content);
        _stageSelect.LoadContent(Content);
        _kitSelect1.LoadContent(Content);
        _kitSelect2.LoadContent(Content);
        _kitSelect3.LoadContent(Content);
        _testTurnBased1.LoadContent(Content);
        _testTurnBased2.LoadContent(Content);
        _testTurnBased3.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back))
            Exit();
        
        _keyboardState = Keyboard.GetState();

        ///Switch statement determines what screen is shown
        switch (_screen)
        {
            case ScreenState.Title:
                _title.Update(gameTime);
                break;
            case ScreenState.StageSelect:
                _stageSelect.Update(gameTime);
                
                switch (_stageSelect.StageSelected)
                {
                    case StageSelected.Boss1:
                        _screen = ScreenState.TurnBased1;
                        break;
                    case StageSelected.Boss2:
                        _screen = ScreenState.TurnBased2;
                        break;
                    case StageSelected.Boss3:
                        _screen = ScreenState.TurnBased3;
                        break;
                }
                _stageSelect.StageSelected = StageSelected.None;
                break;
            case ScreenState.KitSelect1:
                _kitSelect1.Update(gameTime);
                if(_kitSelect1.ScreenProgression == ScreenProgression.Forward)
                {
                    _screen = ScreenState.TurnBased1;
                }
                else if(_kitSelect1.ScreenProgression == ScreenProgression.Backward)
                {
                    _screen = ScreenState.StageSelect;
                    _kitSelect1.Clear();
                    
                }
                _kitSelect1.ScreenProgression = ScreenProgression.None;
                break;
            case ScreenState.KitSelect2:
                _kitSelect2.Update(gameTime);
                if(_kitSelect2.ScreenProgression == ScreenProgression.Forward)
                {
                    _screen = ScreenState.TurnBased2;
                }
                else if(_kitSelect2.ScreenProgression == ScreenProgression.Backward)
                {
                    _screen = ScreenState.StageSelect;
                    _kitSelect2.Clear();
                    
                }
                _kitSelect2.ScreenProgression = ScreenProgression.None;
                break;
            case ScreenState.KitSelect3:
                _kitSelect3.Update(gameTime);
                if(_kitSelect3.ScreenProgression == ScreenProgression.Forward)
                {
                    _screen = ScreenState.TurnBased3;
                }
                else if(_kitSelect3.ScreenProgression == ScreenProgression.Backward)
                {
                    _screen = ScreenState.StageSelect;
                    _kitSelect3.Clear();
                    
                }
                _kitSelect3.ScreenProgression = ScreenProgression.None;
                break;
            case ScreenState.TurnBased1:
                _testTurnBased1.Update(gameTime);
                break;
            case ScreenState.TurnBased2:
                _testTurnBased2.Update(gameTime);
                break;
            case ScreenState.TurnBased3:
                _testTurnBased3.Update(gameTime);
                break;
        }
        base.Update(gameTime);
        
    }


    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        switch (_screen)
        {
            case ScreenState.Title:
                _title.Draw(_spriteBatch);
                break;
            case ScreenState.StageSelect:
                _stageSelect.Draw(_spriteBatch);
                break;
            case ScreenState.KitSelect1:
                _kitSelect1.Draw(_spriteBatch);
                break;
            case ScreenState.KitSelect2:
                _kitSelect2.Draw(_spriteBatch);
                break;
            case ScreenState.KitSelect3:
                _kitSelect3.Draw(_spriteBatch);
                break;
            case ScreenState.TurnBased1:
                _testTurnBased1.Draw(_spriteBatch, gameTime);
                break;
            case ScreenState.TurnBased2:
            _testTurnBased2.Draw(_spriteBatch, gameTime);
                break;
            case ScreenState.TurnBased3:
            _testTurnBased3.Draw(_spriteBatch, gameTime);
                break;
        }

        triangle.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
