using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Collections.Generic;


namespace ZLGameProject;

public class PlayerSoul
{
    private enum Sprite
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4

    }
    private KeyboardState _keyboardState;

    private Texture2D _texture;

    public Vector2 Center = new Vector2(32,32);
    public bool IsDead = false;

    public bool Invunerable = false;

    private float _iTimer = 0;


    /// <summary>
    /// position of the soul
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// health of the soul
    /// </summary>
    public int Health = 30;

    private Sprite _sprite = Sprite.Zero;

    /// <summary>
    /// adjusts the speed of the animation
    /// </summary>
    private double _animationSpeed = 0.15;

    private double _animationTimer = 0;

    /// <summary>
    /// what animation frame is being shown
    /// </summary>
    private short animationFrame = 1;

    public PlayerSoul(Vector2 v)
    {

        Position = v;
        
    }


    /// <summary>
    /// loads the bat sprite texture
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("PlayerSoul");
    }

   
    /// <summary>
    /// Updates the state of the Player's Soul
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime)
    {

        _keyboardState = Keyboard.GetState();
        if(Health > 0)
        {
            
        
            if (_keyboardState.IsKeyDown(Keys.W))
            {
                Position += new Vector2(0,-5);
            
            }
            if (_keyboardState.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0,5);
            
            }
            if (_keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-5,0);
   
            }
            if (_keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2(5,0);

            }

            if(Invunerable == true)
            {
                _iTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
                if(_iTimer >= 1.5f)
                {
                    _iTimer = 0;
                    Invunerable = false;
                }
            }
        }
        else
        {
            IsDead = true;
        }
    }


/// <summary>
/// Draws animated bat sprite
/// </summary>
/// <param name="gameTime">the game time</param>
/// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
       //update animationTimer
        _animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        

            //update animaitonFrame
            if(_animationTimer > _animationSpeed)
            {
                animationFrame++;
                if(animationFrame > 4)
                {
                    
                    animationFrame = 0;
                }
                _animationTimer -= _animationSpeed;
            }
        
        var source = new Rectangle(animationFrame*64, (int)_sprite * 64, 64, 64);
        
        //gives a visual cue that the player has invunerablility
        if (!Invunerable)
        {
            spriteBatch.Draw(_texture, Position, source, Color.White, 0, Center, .75f, SpriteEffects.None, 0);
        }
        else
        {
            spriteBatch.Draw(_texture, Position, source, Color.Red, 0, Center, .75f, SpriteEffects.None, 0);
        }
        
    }


    public void CollideWithBoundingBox(BoundingBox b)
    {
        
        float nearestX = MathHelper.Clamp(Position.X, b.Left.X, b.Left.X + b.Left.Width);
        float nearestY = MathHelper.Clamp(Position.Y, b.Left.Y, b.Left.Y + b.Left.Height);

        //24f is the radius because the 64px sprite is being multiplied by 
        if(Math.Pow(24f, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            Position -= new Vector2(-5,0); 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Right.X, b.Right.X + b.Right.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Right.Y, b.Right.Y + b.Right.Height);

        //24f is the radius because the 64px sprite is being multiplied by 
        if(Math.Pow(24f, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            Position -= new Vector2(5,0); 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Top.X, b.Top.X + b.Top.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Top.Y, b.Top.Y + b.Top.Height);

        //24f is the radius because the 64px sprite is being multiplied by 
        if(Math.Pow(24f, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            Position -= new Vector2(0,-5); 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Bottom.X, b.Bottom.X + b.Bottom.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Bottom.Y, b.Bottom.Y + b.Bottom.Height);

        //24f is the radius because the 64px sprite is being multiplied by 
        if(Math.Pow(24f, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            Position -= new Vector2(0,5); 
        }
  
    }
}