using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;


namespace ZLGameProject;

public class BoundingBox
{
    //1x1 pixel used to draw the rectangles
    private Texture2D _pixel;

    //left wall of the box
    public BoundingRectangle Left;

    //right side of the box
    public BoundingRectangle Right;

    //Top of the box
    public BoundingRectangle Top;

    //Bottom of the box
    public BoundingRectangle Bottom;

    /// <summary>
    /// regular rectangle shapes for ease of drawing
    /// </summary>
    private Rectangle _leftRectangle;
    private Rectangle _rightRectangle;
    private Rectangle _topRectangle;
    private Rectangle _bottomRectangle;


    //As of now, the rectangles are at a set length, however in the future the rectangles' length will be more dynamic
    public BoundingBox(Vector2 center)
    {
        Left = new BoundingRectangle(center.X * .33f, center.Y / 1.5f, 10, 400);
        Right = new BoundingRectangle(center.X * 1.57f, center.Y / 1.5f, 10, 400);
        Top = new BoundingRectangle(center.X * .33f, center.Y / 1.5f, 800, 10);
        Bottom = new BoundingRectangle(center.X * .33f, center.Y * 1.683f, 800, 10);
    }


    public void LoadContent(Texture2D pixel)
    {
        _pixel = pixel;

        _leftRectangle = new Rectangle((int)Left.X, (int)Left.Y, (int)Left.Width, (int)Left.Height);
        _rightRectangle = new Rectangle((int)Right.X, (int)Right.Y, (int)Right.Width, (int)Right.Height);
        _topRectangle = new Rectangle((int)Top.X, (int)Top.Y, (int)Top.Width, (int)Top.Height);
        _bottomRectangle = new Rectangle((int)Bottom.X, (int)Bottom.Y, (int)Bottom.Width, (int)Bottom.Height);
    }

    /// <summary>
    /// Updates the state of the bounding box
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime)
    {
        
 
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, _leftRectangle, Color.White);
        spriteBatch.Draw(_pixel, _rightRectangle, Color.White);
        spriteBatch.Draw(_pixel, _topRectangle, Color.White);
        spriteBatch.Draw(_pixel, _bottomRectangle, Color.White);
    }

    
    }