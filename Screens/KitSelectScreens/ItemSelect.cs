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

namespace ZLGameProject;

public enum ItemType
{
    Relic,
    Item,
    Skill,
    Weapon,

}

public enum ItemRarity
{
    Common = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4
}

/// <summary>
/// Class representing the stage select screen
/// </summary>
public class ItemSelect
{
    /// <summary>
    /// name of the texture
    /// </summary>
    private string _textureName;

    /// <summary>
    /// texture for the item
    /// </summary>
    private Texture2D _itemTexture;

    /// <summary>
    /// outer rectangle for the box
    /// </summary>
    public Rectangle OuterRectangle;

    /// <summary>
    /// inner rectangle for the box
    /// </summary>
    private Rectangle _innerRectangle;

    /// <summary>
    /// name of the item
    /// </summary>
    public string ItemName;

    /// <summary>
    /// description for the item
    /// </summary>
    public string ItemDescription;

    /// <summary>
    /// 
    /// </summary>
    public ItemType ItemType;

    public ItemRarity ItemRarity;

    public bool CanSelectOne;

    public bool Selected;

    private GraphicsDevice _graphicsDevice;

    private Texture2D _pixel;

    public ItemSelect(string texture, Vector2 location, string itemName, string itemDescription, ItemType itemType, ItemRarity itemRarity, bool canSelectOne, GraphicsDevice gd)
    {
        _textureName = texture;
        OuterRectangle = new Rectangle((int)location.X, (int)location.Y, 74, 74);
        _innerRectangle = new Rectangle((int)location.X+5, (int)location.Y + 5, 64, 64);
        ItemName = itemName;
        ItemDescription = itemDescription;
        ItemType = itemType;
        ItemRarity = itemRarity;
        CanSelectOne = canSelectOne;
        _graphicsDevice = gd;
    }

    /// <summary>
    /// loads the font and textures
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {
        _itemTexture = content.Load<Texture2D>(_textureName); 

        _pixel = new Texture2D(_graphicsDevice, 1,1);
        _pixel.SetData(new Color[] {Color.White});
    }

    /// <summary>
    /// Draws the weapon select box
    /// </summary>
    /// <param name="cursorCoords"></param>
    public virtual void Update(MouseState prevMouseState, MouseState mouse, Point cursorCoords)
    {
        if (OuterRectangle.Contains(cursorCoords) && (int)mouse.LeftButton == 1 && (int)prevMouseState.LeftButton == 0 )
        {
            if (Selected)
            {
                Selected = false;
            }
            else
            {
                Selected = true;
            }
        }
    }

    /// <summary>
    /// Draws the weapon select box
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    /// <param name="cursorCoords"></param>
    public virtual void Draw(SpriteBatch spriteBatch, Point cursorCoords)
    {
        if(Selected || OuterRectangle.Contains(cursorCoords))
        {
            switch (ItemRarity)
            {
                case ItemRarity.Common:
                    spriteBatch.Draw(_pixel, OuterRectangle, Color.Green);
                    break;
                case ItemRarity.Rare:
                    spriteBatch.Draw(_pixel, OuterRectangle, Color.Blue);
                    break;
                case ItemRarity.Epic:
                    spriteBatch.Draw(_pixel, OuterRectangle, Color.Purple);
                    break;
                case ItemRarity.Legendary:
                    spriteBatch.Draw(_pixel, OuterRectangle, Color.OrangeRed);
                    break;
            }
            
        }
        else
        {
            spriteBatch.Draw(_pixel, OuterRectangle, Color.White);
        }

        spriteBatch.Draw(_pixel, _innerRectangle, Color.Black);
        spriteBatch.Draw(_itemTexture, new Vector2( _innerRectangle.X, _innerRectangle.Y), new Rectangle(0, 0, 64, 64), Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.None, 0);
    }

    
    public override string ToString()
    {
        switch (ItemType)
        {
            case ItemType.Relic:
                return $"RELIC {ItemName}";
            case ItemType.Skill:
                return $"SKILL {ItemName}";
            case ItemType.Item:
                return $"ITEM {ItemName}";
            case ItemType.Weapon:
                return $"WEAPON {ItemName}";
            default:
            return null;
        }
    }

    
}