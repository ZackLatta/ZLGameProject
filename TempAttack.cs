using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ZLGameProject;

public abstract class TempAttack
{
    
    protected double _attackTime = 0;

    protected double _attackDuration;

    public bool IsDone;

    protected Player _player;

    public List<TempBullet> bullets;


    /// <summary>
    /// pixel texture
    /// </summary>
    protected Texture2D _pixel;

    /// <summary>
    /// bounding box for the attack
    /// </summary>
    protected BoundingBox _boundingBox;

    /// <summary>
    /// center of the screen
    /// </summary>
    protected Vector2 _center;

    /// <summary>
    /// sets the position of where the player will start
    /// </summary>
    public Vector2 PlayerStartingPosition;


    
    public TempAttack(float attackDuration, Texture2D pixel, Player p, Vector2 c)
    {
        _attackDuration = attackDuration;
        _pixel = pixel;
        _player = p;
        _center = c;
        _boundingBox = new BoundingBox(c);
    }

    /// <summary>
    /// loads the bullets
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public virtual void LoadContent(ContentManager content)
    {

        _boundingBox.LoadContent(_pixel);

    }

   
    /// <summary>
    /// Updates the state of the attack
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public virtual void Update(GameTime gameTime)
    {
        if(_player.CurrentHealth <= 0)
        {
            _player.IsDead = true;
            IsDone = true;
            _attackTime = 0;
            return;
        }

        PlayerCollidesBoundingBox(_player.Soul);


        _attackTime += gameTime.ElapsedGameTime.TotalSeconds;

        if(_attackTime >= _attackDuration)
        {
            IsDone = true;
            _attackTime = 0;
        }
 
    }


    /// <summary>
    /// Draws the attack
    /// </summary>
    /// <param name="gameTime">the game time</param>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _boundingBox.Draw(spriteBatch);
    }

    /// <summary>
    /// Checks to see if there is a collision between the playersoul and the bounding box, and resolves accordingly depending on the state that the soul is in
    /// </summary>
    /// <param name="p">playersoul</param>
    public void PlayerCollidesBoundingBox(PlayerSoul p)
    {
        if(p.SoulState == PlayerSoul.State.Normal)
        {
            if(CollisionHelper.Collides(_boundingBox.Left, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(_boundingBox.Left.X + _boundingBox.Left.Width + p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }

            if(CollisionHelper.Collides(_boundingBox.Right, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(_boundingBox.Right.X - p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }

            if(CollisionHelper.Collides(_boundingBox.Top, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Top.Y + _boundingBox.Top.Height + p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }
            if(CollisionHelper.Collides(_boundingBox.Bottom, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Bottom.Y - p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }
        }
        else if(p.SoulState == PlayerSoul.State.Orange)
        {
            if(CollisionHelper.Collides(_boundingBox.Left, p.SoulBoundingCircle))
            {
                ///REMINDER: the X and Y of a rectangle is the location of the top leftmost pixel
                p.Position = new Vector2(_boundingBox.Left.X + _boundingBox.Left.Width + p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.Velocity = new Vector2(0,p.Velocity.Y);
            }

            if(CollisionHelper.Collides(_boundingBox.Right, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(_boundingBox.Right.X - p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.Velocity = new Vector2(0,p.Velocity.Y);
            }

            if(CollisionHelper.Collides(_boundingBox.Top, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Top.Y + _boundingBox.Top.Height + p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.Velocity = new Vector2(p.Velocity.X,0);
            }
            if(CollisionHelper.Collides(_boundingBox.Bottom, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Bottom.Y - p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.Velocity = new Vector2(p.Velocity.X,0);
            }
        }
        else if(p.SoulState == PlayerSoul.State.Blue)
        {
            if(CollisionHelper.Collides(_boundingBox.Left, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(_boundingBox.Left.X + _boundingBox.Left.Width + p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }

            if(CollisionHelper.Collides(_boundingBox.Right, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(_boundingBox.Right.X - p.SoulBoundingCircle.Radius, p.Position.Y);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
            }

            if(CollisionHelper.Collides(_boundingBox.Top, p.SoulBoundingCircle))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Top.Y + _boundingBox.Top.Height + p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.Velocity = new Vector2(p.Velocity.X,0);
            }
            if(CollisionHelper.Collides(_boundingBox.Bottom, p.GroundTracking))
            {
                p.Position = new Vector2(p.Position.X, _boundingBox.Bottom.Y - p.SoulBoundingCircle.Radius);
                p.SoulBoundingCircle.Center = p.Position;
                p.GroundTracking.X = p.Position.X - 8;
                p.GroundTracking.Y = p.Position.Y + p.SoulBoundingCircle.Radius;
                p.JumpState = PlayerSoul.BlueState.Grounded;
            }
        }


    }



}