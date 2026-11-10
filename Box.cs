using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.Dynamic;
using System.Text.Json.Serialization;


namespace ZLGameProject;

/// <summary>
/// Box class
/// </summary>
public class Box
{   
    /// <summary>
    /// outer rectangle
    /// </summary>
    protected Rectangle _outer;

    /// <summary>
    /// inner rectangle, will be a black square 90% of the time
    /// </summary>
    protected Rectangle _inner;

    /// <summary>
    /// position of the box
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// offset between outer and inner rectangles
    /// </summary>
    private float _offset;

    /// <summary>
    /// width of box
    /// </summary>
    protected int _width;

    /// <summary>
    /// height of the box
    /// </summary>
    protected int _height;

    protected Vector2 _rectCenter;

    /// <summary>
    /// pixel texture that will be used to draw the box
    /// </summary>
    protected Texture2D _pixel;

    /// <summary>
    /// source rectangle for 1x1 texture
    /// </summary>
    protected Rectangle _pixelRectangle = new Rectangle(0,0,1,1);

    /// <summary>
    /// Creates a box
    /// </summary>
    /// <param name="pixel">pixel texture that will be used</param>
    /// <param name="position">position of the rectangle</param>
    /// <param name="width">width of the rectangle</param>
    /// <param name="height">height of the rectangle</param>
    /// <param name="thickness">thickness of the boarders</param>
    public Box(Texture2D pixel, Vector2 position, int width, int height, float thickness)
    {
        _pixel = pixel;
        Position = position;
        _width = width;
        _height = height;
        _offset = thickness;
    }
    
    /// <summary>
    /// creates the rectangles for the box
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {
        _outer = new Rectangle((int)Position.X - _width/2, (int)Position.Y -_height/2, _width, _height);
        _inner = new Rectangle((int)(Position.X + _offset - _width/2), (int)(Position.Y + _offset - _height/2), _width - (int)_offset*2, _height - (int)_offset*2);
        _rectCenter = new Vector2(_outer.Center.X, _outer.Center.Y);

    }

    /// <summary>
    /// Draws the box
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, _outer, Color.White);
        spriteBatch.Draw(_pixel, _inner, Color.Black);
    }
}