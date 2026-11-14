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
/// class defining a combat scene
/// </summary>
public class TurnBasedScreen2 : TurnBasedScreen
{
    public TurnBasedScreen2(GraphicsDevice gd, Vector2 c, Player p, SpriteFont f): base(gd, c, p, f)
    {
        
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
        Enemy._testAttack.bullets = new List<TempBullet>(){new TempRectBullet(_pixel, false, new Vector2(_center.X, _center.Y+160), 100, 100, new(0,0), 5)};
        _player.Soul.SetMovement(PlayerSoul.State.Blue);
        _player.Soul.Position = new Vector2(_center.X + 200, _center.Y- 50);
        _player.Soul.HasSword = true; 
    }
    
}