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
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;


namespace ZLGameProject;

/// <summary>
/// Temporary bullet class
/// </summary>
public class TempCircBullet : TempBullet
{
    public BoundingCircle HitCircle;

    public Texture2D _ball;

    private float _drawRadius;

    private Rectangle _source = new Rectangle(0,0,64,64);

    private Vector2 _origin = new Vector2(32,32);

    public TempCircBullet(Texture2D pixel, bool breakable, Vector2 position, float radius, Vector2 velocity, int damage) : base(pixel,breakable, damage)
    {
        Position = position;
        HitCircle = new BoundingCircle(position, radius);
        Velocity = velocity; 
        _drawRadius = radius/32;
    }

    /// <summary>
    /// creates the related Bullet content
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public override void LoadContent(ContentManager content)
    {
        _ball = content.Load<Texture2D>("Ball");
    }

    /// <summary>
    /// Updates the state of the bullet
    /// </summary>
    public override void Update(GameTime gameTime, Player p)
    {
        Position += Velocity;
        HitCircle.Center = Position;
    }

    /// <summary>
    /// Draws the bullet
    /// </summary>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_ball, Position, _source, _color, 0f, _origin, _drawRadius, SpriteEffects.None, 0f);
    }


    public override bool CollidesWithPlayer(PlayerSoul p)
    {
        return CollisionHelper.Collides(p.SoulBoundingCircle, HitCircle);
    }
}