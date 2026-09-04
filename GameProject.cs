using System;
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

    private Texture2D _atlas;

    private SpriteFont _font;
    private bool _isDead = false;

    //intsets for random sprites
    private int[] intset1;
    private int[] intset2;
    private int[] intset3;

    public MathHelper.Random Random {get; init;} = new();

    public GameProject()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _bat = new TempBatSprite();
        intset1 = [Random.Next(0,47),Random.Next(0,21)];
        intset2 = [Random.Next(0,47),Random.Next(0,21)];
        intset3 = [Random.Next(0,47),Random.Next(0,21)];
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here
        _atlas = Content.Load<Texture2D>("colored_packed");
        _font = Content.Load<SpriteFont>("font");
        _bat.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back))
            Exit();
        
        _keyboardState = Keyboard.GetState();

        // TODO: Add your update logic here
        if (_keyboardState.IsKeyDown(Keys.K))
        {
            _isDead = true;
        }
        _bat.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        //Draws a random sprite from the sprite sheet each time the game is loaded
        _spriteBatch.Draw(_atlas, new Vector2(300,300), new Rectangle(intset1[0]*16, intset1[1]*16, 16, 16), Color.White, 0 , new Vector2(8,8), 4.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(_atlas, new Vector2(450,450), new Rectangle(intset2[0]*16, intset2[1]*16, 16, 16), Color.White, 0 , new Vector2(8,8), 4.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(_atlas, new Vector2(150,150), new Rectangle(intset3[0]*16, intset3[1]*16, 16, 16), Color.White, 0, new Vector2(8,8), 4.0f, SpriteEffects.None, 0);
        
        _spriteBatch.DrawString(_font, $"Press WASD to move. \nPress Backspace or Esc to exit. \nPress K to kill the bat", new Vector2(2,2), Color.White);
        if (_isDead)
        {
            _spriteBatch.DrawString(_font, $"Why? :(", new Vector2(_bat.Position.X - 20, _bat.Position.Y - 15), Color.White);
        }

        _bat.Draw(gameTime, _spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
