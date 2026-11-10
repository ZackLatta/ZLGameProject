using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;


namespace ZLGameProject;

/// <summary>
/// Abstract class defining each combat scene
/// </summary>
public class TurnBasedScreen1 : TurnBasedScreen
{
    public TurnBasedScreen1(GraphicsDevice gd, Vector2 c, Player p, SpriteFont f): base(gd, c, p, f)
    {
        
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
        Enemy._testAttack.bullets = new List<TempBullet>(){new TempRectBullet(_pixel, false, new Vector2(_center.X - 100, _center.Y), 60, 60,new(10,10), 5)};
        _player.Soul.SetMovement(PlayerSoul.State.Normal);
        _player.Soul.Position = new Vector2(_center.X, _center.Y);
    }
    
}