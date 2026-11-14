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

        if(_player.Soul.SoulState == PlayerSoul.State.Blue && _player.Soul.JumpState != PlayerSoul.BlueState.Jumping)
        {
            if(_player.Soul.CheckIfGrounded(_player.Soul.GroundTracking, _boundingBox.Bottom))
            {
                _player.Soul.JumpState = PlayerSoul.BlueState.Grounded;
            }
            else 
            {
                _player.Soul.JumpState = PlayerSoul.BlueState.Falling;
            }

        }
        
        _boundingBox.PlayerCollidesBoundingBox(_player.Soul);


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


}