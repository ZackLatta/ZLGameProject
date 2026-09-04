using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

    private Vector2 position = new Vector2(200, 200);

    private double animationTimer;

    /// <summary>
    /// adjusts the speed of the animation when moving
    /// </summary>
    private double animationSpeed = 0.3;

    /// <summary>
    /// what animation frame is being shown
    /// </summary>
    private short animationFrame = 1;

    /// <summary>
    /// Direction of the bat 
    /// </summary>
    public Direction Direction;

    /// <summary>
    /// position of the bat
    /// </summary>
    public Vector2 Position = new Vector2(200,200);

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
    public void Update(GameTime gameTime)
    {
        keyboardState = Keyboard.GetState();

        if(animationFrame != 0)
        {
            if(keyboardState.IsKeyDown(Keys.W))
            {
                Direction = Direction.Up;
                Position += new Vector2(0,-2);
                animationSpeed = 0.15;
            }
            if(keyboardState.IsKeyDown(Keys.A))
            {
                Direction = Direction.Left;
                Position += new Vector2(-2,0);
                animationSpeed = 0.15;
            }
            if(keyboardState.IsKeyDown(Keys.S))
            {
                Direction = Direction.Down;
                Position += new Vector2(0,2);
                animationSpeed = 0.15;
            }
            if(keyboardState.IsKeyDown(Keys.D))
            {
                Direction = Direction.Right;
                Position += new Vector2(2,0);
                animationSpeed = 0.15;
            }
            //Kills bat and prevents any more input from being read
            if (keyboardState.IsKeyDown(Keys.K))
            {
                animationFrame = 0;
            }
            if(keyboardState.GetPressedKeyCount() == 0)
            {
                animationSpeed = 0.3;
            }
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