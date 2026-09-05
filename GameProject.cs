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

    private TitleScreenText _title;

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
        _bat = new TempBatSprite(new Vector2(-100, GraphicsDevice.Viewport.Height * 0.75f), GraphicsDevice.Viewport.Width / 2);
        _title = new TitleScreenText(new Vector2(GraphicsDevice.Viewport.Width / 3 - 20,  GraphicsDevice.Viewport.Height * 0.25f));
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
        _title.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        //Draws bat first so it will be put behind the other objects
        _bat.Draw(gameTime, _spriteBatch);

        //Draws the tora gate and the two soldiers on either side
        _spriteBatch.Draw(_atlas, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 50), new Rectangle(22*16, 11*16, 16, 16), Color.White, 0 , new Vector2(8,8), 4.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(_atlas, new Vector2(GraphicsDevice.Viewport.Width / 2 - 64,GraphicsDevice.Viewport.Height / 2 + 58), new Rectangle(31*16, 0, 16, 16), Color.OrangeRed, 0 , new Vector2(8,8), 3.0f, SpriteEffects.None, 0);
        _spriteBatch.Draw(_atlas, new Vector2(GraphicsDevice.Viewport.Width / 2 + 64,GraphicsDevice.Viewport.Height / 2 + 58), new Rectangle(31*16, 0, 16, 16), Color.OrangeRed, 0, new Vector2(8,8), 3.0f, SpriteEffects.None, 0);
        
        //Draws instructions
        _spriteBatch.DrawString(_font, $"Press Backspace or Esc to exit. \nPress K to kill the bat", new Vector2(2,2), Color.White);
        
        //draws dead bat if dead
        if (_isDead)
        {
            _spriteBatch.DrawString(_font, $"Why? :(", new Vector2(_bat.Position.X - 20, _bat.Position.Y - 15), Color.White);
        }
        
        //Draws title screen text
        _title.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
