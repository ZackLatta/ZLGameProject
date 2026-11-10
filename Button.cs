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
using System.Xml.Serialization;


namespace ZLGameProject;

/// <summary>
/// Button class
/// </summary>
public class Button : Box
{   
    /// <summary>
    /// is the button selected
    /// </summary>
    public bool IsSelected = false;

    /// <summary>
    /// text that the button will contain
    /// </summary>
    private string _text;

    /// <summary>
    /// coords of the mouse, used for both update and draw method
    /// </summary>
    private Point _mouseCoords;

    /// <summary>
    /// Creates a button
    /// </summary>
    /// <param name="pixel"></param>
    /// <param name="position"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="thickness"></param>
    /// <param name="text"></param>
    public Button(Texture2D pixel, Vector2 position, int width, int height, float thickness, string text):base(pixel, position, width, height, thickness)
    {
        _text = text;
    }

    /// <summary>
    /// creates the rectangles for the box
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
    }

    /// <summary>
    /// Updates the state of the button
    /// </summary>
    /// <param name="currentState"></param>
    /// <param name="prevState"></param>
    /// <param name="mouseCoords"></param>
    /// <param name="b"></param>
    public void Update(MouseState currentState, MouseState prevState, Point mouseCoords, List<Button> b)
    {
        _mouseCoords = mouseCoords;

        if(_outer.Contains(_mouseCoords) && (int)currentState.LeftButton == 1 && (int)prevState.LeftButton == 0 && IsSelected == false)
        {
            IsSelected = true;
            RemoveOtherSelected(b);
        }
        else if(_outer.Contains(_mouseCoords) && (int)currentState.LeftButton == 1 && (int)prevState.LeftButton == 0)
        {
            IsSelected = false;
        }

    }
    

    /// <summary>
    /// Draws the button
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        if(_outer.Contains(_mouseCoords) || IsSelected == true)
        {
            spriteBatch.Draw(_pixel, _outer, Color.Green);
            spriteBatch.Draw(_pixel, _inner, Color.Black);
        }
        else
        {
            base.Draw(spriteBatch);
        }
        spriteBatch.DrawString(font, _text, new Vector2(_inner.X, _inner.Y), Color.White);
    }

    /// <summary>
    /// makes it that the button is the only one of it's group that is selected
    /// </summary>
    /// <param name="buttons">list of all buttons in its group</param>
    public void RemoveOtherSelected(List<Button> buttons)
    {
        if(buttons == null)
        {
            return;
        }

        foreach(Button b in buttons)
        {
            if(b != this)
            {
                if (b.IsSelected == true)
                {
                    b.IsSelected = false;
                }
            }
        }
    }

}