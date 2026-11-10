using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;

namespace ZLGameProject;


/// <summary>
/// Class representing the Kit Select screen
/// </summary>
public abstract class KitSelectScreen : GameScreen
{

    protected int _max_points;

    protected int _currentPoints = 0;

    protected GraphicsDevice _graphicsDevice;

    /// <summary>
    /// keeps track of mouse information
    /// </summary>
    private MouseState _mouseState;

    /// <summary>
    /// keeps track of mouse information
    /// </summary>
    private MouseState _prevMouseState;

    /// <summary>
    /// mouse cursor position
    /// </summary>
    private Point _mouseCoords;

    private Texture2D _pixel;

    private Rectangle _outerDescription;

    private Rectangle _innerDescription;

    /// <summary>
    /// continue button
    /// </summary>
    private Rectangle _continueOuter;

    private Rectangle _continueInner;

    public ScreenProgression ScreenProgression = ScreenProgression.None;

    /// <summary>
    /// go back button
    /// </summary>
    private Rectangle _goBackOuter;
    private Rectangle _goBackInner;

    public List<ItemSelect> Items = new();

    public List<ItemSelect> SelectedItems = new();

    /// <summary>
    /// error checking, triggers if the player tries to continue with no items
    /// </summary>
    private bool _needItems = false;
    private string _needItemsMessage = "Select at least one item";
    private Vector2 _needItemsMessageCenter;
    
    /// <summary>
    /// error checking, triggers if the player tries to continue with no weapon
    /// </summary>
    private bool _needWeapon = false;
    private string _needWeaponMessage = "You plan on going to battle without a weapon?";
    private Vector2 _needWeaponMessageCenter;

    /// <summary>
    /// Creates a KitSelectScreen
    /// </summary>
    /// <param name="gd">Graphics device, used for creating single pixel textures</param>
    /// <param name="c">center of the screen</param>
    /// <param name="f">font that will be used</param>
    public KitSelectScreen(GraphicsDevice gd, Vector2 c, SpriteFont f)
    {
        _graphicsDevice = gd;
        _center = c;
        _font = f;
        _outerDescription = new Rectangle((int)(c.X * 1.25f), (int)(c.Y * 0.15), 350, 500);
        _innerDescription = new Rectangle((int)(c.X * 1.25f + 10), (int)(c.Y*0.15 + 10), 330, 480);

        
        _goBackOuter = new Rectangle((int)(c.X * 1.25f), 600, 150, 75);
        _goBackInner = new Rectangle((int)(c.X * 1.25f + 5), 600+5, 140, 65);
        _continueOuter = new Rectangle((int)(c.X * 1.25f) + 150 + 50, 600, 150, 75);
        _continueInner = new Rectangle((int)(c.X * 1.25f + 5) + 150 + 50, 600+5, 140, 65);

        _needItemsMessageCenter = _font.MeasureString(_needItemsMessage);
        _needItemsMessageCenter = new Vector2(_needItemsMessageCenter.X/2, _needItemsMessageCenter.Y/2);

        _needWeaponMessageCenter = _font.MeasureString(_needWeaponMessage);
        _needWeaponMessageCenter = new Vector2(_needWeaponMessageCenter.X/2, _needWeaponMessageCenter.Y/2);
        


        CreateList(Items);
    }

    /// <summary>
    /// loads the content for the screen
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        _screenMusic =  content.Load<Song>("Nicolaas Stulting - Revenge");

        foreach(ItemSelect i in Items)
        {
            i.LoadContent(content);
        }

        _pixel = new Texture2D(_graphicsDevice, 1,1);
        _pixel.SetData(new Color[] {Color.White});

    }

    /// <summary>
    /// Updates the KitSelectScreen
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        _mouseState = Mouse.GetState();

        _mouseCoords = new Point(_mouseState.X, _mouseState.Y);

        foreach(ItemSelect i in Items)
        {
            i.Update(_prevMouseState, _mouseState ,_mouseCoords);

            if (i.Selected && !SelectedItems.Contains(i))
            {   
                if(i.ItemType == ItemType.Weapon)
                {
                    //creates a temporary list from created from a LINQ query that contains the weapon in the Selected Items.
                    //cannot use SelectedItems list because modifying the list mid enumeration raises an exception
                    List<ItemSelect> weapon = SelectedItems.Where(j => i != j && j.ItemType == ItemType.Weapon).ToList();
                    foreach(ItemSelect j in weapon)
                    {
                            if (i != j && j.ItemType == ItemType.Weapon)
                            {
                                SelectedItems.Remove(j);
                                _currentPoints -= (int)j.ItemRarity;
                                j.Selected = false;
                            }

                        }
                    }
                
                
                if(_currentPoints + (int)i.ItemRarity <= _max_points)
                {
                    SelectedItems.Add(i);
                    _currentPoints += (int)i.ItemRarity;
                }
                else
                {
                    i.Selected = false;
                }
            }
            else if (!i.Selected && SelectedItems.Contains(i))
            {
                SelectedItems.Remove(i);
                _currentPoints -= (int)i.ItemRarity;

            }
        }

        if (_goBackOuter.Contains(_mouseCoords) && (int)_mouseState.LeftButton == 1 && (int)_prevMouseState.LeftButton == 0)
        {
            ScreenProgression = ScreenProgression.Backward;
        }

        if (_continueOuter.Contains(_mouseCoords) && (int)_mouseState.LeftButton == 1 && (int)_prevMouseState.LeftButton == 0)
        {
            if(SelectedItems.Count > 0 )
            {
                if(SelectedItems.Any(s => s.ItemType == ItemType.Weapon))
                {            
                    ScreenProgression = ScreenProgression.Forward;
                    _needItems = false;
                    _needWeapon = false;
                }
                else
                {
                    _needItems = false;
                    _needWeapon = true;
                }
            }
            else
            {
                _needItems = true;
                _needWeapon = false;
            }
        }

        if (_goBackOuter.Contains(_mouseCoords) && (int)_mouseState.LeftButton == 1 && (int)_prevMouseState.LeftButton == 0)
        {
            ScreenProgression = ScreenProgression.Backward;
        }

        _prevMouseState = _mouseState;

    }

    /// <summary>
    /// Draws the screen assets
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, $"Points: {_currentPoints} / {_max_points}", new Vector2(_outerDescription.X,_outerDescription.Y - 20), Color.White);
        spriteBatch.DrawString(_font, $"Build Your Kit!", new Vector2(_center.X/2, _center.Y/10), Color.White, 0f, new Vector2(0,0), 2f, SpriteEffects.None, 0);
        spriteBatch.Draw(_pixel, _outerDescription, Color.White);
        spriteBatch.Draw(_pixel, _innerDescription, Color.Black);
        foreach(ItemSelect i in Items)
        {
            if (i.OuterRectangle.Contains(_mouseCoords))
            {
                // Write Item Description in the Description Box
                spriteBatch.DrawString(_font, i.ItemDescription, new Vector2(_innerDescription.X + 3, _innerDescription.Y + 3), Color.White);
            }
            i.Draw(spriteBatch, _mouseCoords);
        }

        if (_goBackOuter.Contains(_mouseCoords))
        {
            spriteBatch.Draw(_pixel, _goBackOuter, Color.Green);
        }
        else
        {
            spriteBatch.Draw(_pixel, _goBackOuter, Color.White);
        }

        if (_continueOuter.Contains(_mouseCoords))
        {
            spriteBatch.Draw(_pixel, _continueOuter, Color.Green);
        }
        else
        {
            spriteBatch.Draw(_pixel, _continueOuter, Color.White);
        }

            spriteBatch.Draw(_pixel, _goBackInner, Color.Black);
            spriteBatch.Draw(_pixel, _continueInner, Color.Black);

            spriteBatch.DrawString(_font, "Continue", new Vector2(_continueInner.X+5, 618.5f), Color.White, 0f, new Vector2(0,0), 2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Go Back", new Vector2(_goBackInner.X+8, 618.5f), Color.White, 0f, new Vector2(0,0), 2f, SpriteEffects.None, 0);

        if (_needItems)
        {
            spriteBatch.DrawString(_font, _needItemsMessage, new Vector2(_center.X/1.9f, _center.Y*1.5f), Color.White, 0f, _needItemsMessageCenter, 2f, SpriteEffects.None, 0);
        }

        if (_needWeapon)
        {
            spriteBatch.DrawString(_font, _needWeaponMessage, new Vector2(_center.X/1.9f, _center.Y*1.5f), Color.White, 0f, _needWeaponMessageCenter, 2f, SpriteEffects.None, 0);
        }

    }

    /// <summary>
    /// creates the list of items available for the fight
    /// </summary>
    /// <param name="items">list of items available</param>
    protected virtual void CreateList(List<ItemSelect> items)
    {

    }

    /// <summary>
    /// clears all info in the class
    /// </summary>
    public void Clear()
    {
        foreach(ItemSelect i in SelectedItems)
        {
            i.Selected = false;
        }
        SelectedItems.Clear();
        _currentPoints = 0;
    }
}