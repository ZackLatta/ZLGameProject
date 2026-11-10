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
public class KitSelectScreen1 : KitSelectScreen
{
    public KitSelectScreen1(GraphicsDevice gd, Vector2 c, SpriteFont f) : base(gd, c, f)
    {
        _max_points = 15;
    }

    protected override void CreateList(List<ItemSelect> items)
    {
        items.Add(new ItemSelect("playersoul", new Vector2(_center.X, _center.Y), "test", "test description", ItemType.Relic, ItemRarity.Common, false, _graphicsDevice));
        items.Add(new ItemSelect("playersoul", new Vector2(_center.X, _center.Y - 75), "test", "test description", ItemType.Item, ItemRarity.Rare, false, _graphicsDevice));
        items.Add(new ItemSelect("playersoul", new Vector2(_center.X, _center.Y - 150), "test", "test description", ItemType.Skill, ItemRarity.Epic, false, _graphicsDevice));
        items.Add(new ItemSelect("playersoul", new Vector2(_center.X, _center.Y - 225), "test", "test description", ItemType.Weapon, ItemRarity.Legendary, false, _graphicsDevice));
        items.Add(new ItemSelect("playersoul", new Vector2(_center.X, _center.Y - 300), "test", "test description", ItemType.Weapon, ItemRarity.Common, false, _graphicsDevice));
    }

} 