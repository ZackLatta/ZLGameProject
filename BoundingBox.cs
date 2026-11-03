using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;


namespace ZLGameProject;

public class BoundingBox
{
    //sprite batch
    private SpriteBatch _spriteBatch;

    //1x1 pixel used to draw the rectangles
    private Texture2D _pixel;

    //left wall of the box
    public Rectangle Left;

    //right side of the box
    public Rectangle Right;

    //Top of the box
    public Rectangle Top;

    //Bottom of the box
    public Rectangle Bottom;

    //As of now, the rectangles are at a set length, however in the future the rectangles' length will be more dynamic
    public BoundingBox(Vector2 center)
    {
        Left = new Rectangle((int)(center.X * .33), (int)(center.Y / 1.5f), 10, 400);
        Right = new Rectangle((int)(center.X * 1.57), (int)(center.Y / 1.5f), 10, 400);
        Top = new Rectangle((int)(center.X * .33), (int)(center.Y / 1.5), 800, 10);
        Bottom = new Rectangle((int)(center.X * .33), (int)(center.Y * 1.683), 800, 10);
    }


    public void LoadContent(GraphicsDevice gd, SpriteBatch sb)
    {
        _spriteBatch = sb;

        //1x1 texture
        _pixel = new Texture2D(gd, 1,1);

        //set texture color to white
        _pixel.SetData(new Color[] {Color.White});


    }

    /// <summary>
    /// Updates the state of the bounding box
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime)
    {
        
 
    }
    public void Draw(GameTime gameTime)
    {
        _spriteBatch.Draw(_pixel, Left, Color.White);
        _spriteBatch.Draw(_pixel, Right, Color.White);
        _spriteBatch.Draw(_pixel, Top, Color.White);
        _spriteBatch.Draw(_pixel, Bottom, Color.White);
    }

    
    }