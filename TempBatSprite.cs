using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace ZLGameProject;

public enum Direction
{
    Down = 0,
    Right = 1,
    Up = 2,
    Left = 3
}

/// <summary>
/// A class representing a bat sprite
/// </summary>
public class TempBatSprite
{

    private KeyboardState keyboardState;

    private Texture2D texture;

    private double animationTimer;

    /// <summary>
    /// adjusts the speed of the animation when moving
    /// </summary>
    private double animationSpeed = 0.15;

    /// <summary>
    /// what animation frame is being shown
    /// </summary>
    private short animationFrame = 1;

    /// <summary>
    /// Direction of the bat 
    /// </summary>
    public Direction Direction;

    /// <summary>
    /// marks the center of the screen
    /// </summary>
    private float _screenCenter;

    private bool _loop;


    /// <summary>
    /// bat's starting position
    /// </summary>
    private Vector2 _initialPosition;
    /// <summary>
    /// position of the bat
    /// </summary>
    public Vector2 Position;

    public TempBatSprite(Vector2 v, float s)
    {
        _screenCenter = s;
        _initialPosition = v;
        Position = v;
    }

    /// <summary>
    /// loads the bat sprite texture
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("32x32-bat-sprite");
    }

    /// <summary>
    /// Updates the bat sprite to move
    /// </summary>
    /// <param name="gameTime">the game time</param>
    public void Update()
    {
        keyboardState = Keyboard.GetState();

        if(animationFrame == 0) return;

        if (!_loop)
        {
            Direction = Direction.Right;
            Position += new Vector2(3,0);
            
            ///if bat is past the center, loops around
            if(Position.X == _screenCenter + 130)
            {
                _loop = true;
                Direction = Direction.Up;
            }
        }
        else
        {
            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0,-3);
                    if(Position.Y <= _initialPosition.Y - 105)
                    {
                        Direction = Direction.Left;
                    }
                    break;
                case Direction.Left:
                    Position += new Vector2(-3,0);
                    if(Position.X <= _screenCenter - 125)
                    {
                        Direction = Direction.Down;
                    }
                    break;
                case Direction.Down:
                    Position += new Vector2(0,3);
                    if(Position.Y >= _initialPosition.Y)
                    {
                        Direction = Direction.Right;
                    }
                    break;
                case Direction.Right:
                    Position += new Vector2(3,0);
                    if(Position.X >= _screenCenter + 135)
                    {
                        _loop = false;
                    }
                    break;
            }
        }
        
        
        //bat flies until it is offscreen, then gets teleported back to the starting position
        if(Position.X >= _screenCenter * 2 + 50)
        {
            Position = _initialPosition;
        }
        //Kills bat and prevents any more animation or movement
        if (keyboardState.IsKeyDown(Keys.K))
        {
            animationFrame = 0;
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
        animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
        
        if(animationFrame != 0)
        {
            //update animaitonFrame
            if(animationTimer > animationSpeed)
            {
                animationFrame++;
                if(animationFrame > 3)
                {
                    //reset to one because zero is the dead bat sprites
                    animationFrame = 1;
                }
                animationTimer -= animationSpeed;
            }
        }
        var source = new Rectangle(animationFrame*32, (int)Direction * 32, 32, 32);
        spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(16,16), 2.0f, SpriteEffects.None, 0);
    }

}