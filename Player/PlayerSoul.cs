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

public class PlayerSoul
{
    private enum Sprite
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4

    }

    /// <summary>
    /// the state that the soul is in
    /// </summary>
    public enum State
    {
        Normal,
        Orange,
        Blue,
    }

    /// <summary>
    /// different state that the blue player can be in
    /// </summary>
    public enum BlueState
    {
        Jumping,
        Falling,
        Grounded
    }

    /// <summary>
    /// the direction the sword is facing
    /// </summary>
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }

    /// <summary>
    /// current keyboard state
    /// </summary>
    private KeyboardState _keyboardState;

    /// <summary>
    /// previous keyboard state
    /// </summary>
    private KeyboardState _prevKeyboardState;

    /// <summary>
    /// texture of the soul
    /// </summary>
    private Texture2D _texture;

    /// <summary>
    /// hit circle for the soul
    /// </summary>
    public BoundingCircle SoulBoundingCircle;

    /// <summary>
    /// tracks if the player is in contact with the ground. sits just below the player sprite
    /// </summary>
    public BoundingRectangle GroundTracking;

    /// <summary>
    /// hit circle for the sword
    /// </summary>
    public BoundingCircle SwordHitCircle;

    /// <summary>
    /// current sprite of the player
    /// </summary>
    private Sprite _sprite = Sprite.Zero;

    /// <summary>
    /// position of the player soul
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// Velocity of the player (changes with state)
    /// </summary>
    public Vector2 Velocity = new Vector2(5,5);

    /// <summary>
    /// normal velocity (normal state)
    /// </summary>
    private Vector2 _normalVelocity = new Vector2(5,5);

    /// <summary>
    /// takeoff Velocity when jumping (blue state)
    /// </summary>
    private float _initialTakeOffVelocity = 7;

    /// <summary>
    /// jump decay (blue state)
    /// </summary>
    private float _jumpAccelerationDecay = 7.5f;

    /// <summary>
    /// soul acceleration (orange state)
    /// </summary>
    public Vector2 Acceleration = new Vector2(5,5);

    /// <summary>
    /// turning acceleration for the player (can be modified with relics)
    /// </summary>
    public Vector2 TurningAcceleration = new Vector2(4f,4f);

    /// <summary>
    /// whether or not the sword is toggled
    /// </summary>
    public bool HasSword;

    /// <summary>
    /// indicating if the sword has been swung, which will be broadcasted to any relevant bullets
    /// </summary>
    public bool HasSwung = false;

    /// <summary>
    /// the initial speed at which the soul is reflected, may become dynamic later
    /// </summary>
    public Vector2 ReflectedInitialVelocity ;

    /// <summary>
    /// rate of decay of the reflected velocitye
    /// </summary>
    private float _reflectedAccelerationDecay = 4;

    /// <summary>
    /// Determins if the sword is on cooldown
    /// </summary>
    public bool OnCoolDown = false;

    /// <summary>
    /// counts up until the sword cool down threshold has been hit
    /// </summary>
    private float _swordCoolDown = 0;

    /// <summary>
    /// cool down for the sword (base: 0.5 secs)
    /// </summary>
    private float _swordCoolDownThreshold = 0.5f;

    /// <summary>
    /// direction the sword is facing (defualt: left)
    /// </summary>
    public Direction Facing = Direction.Left;

    
    /// <summary>
    /// current state of the soul 
    /// </summary>
    public State SoulState;

    /// <summary>
    /// keeps track of what jump state the player is in
    /// </summary>
    public BlueState JumpState;

    /// <summary>
    /// adjusts the speed of the animation
    /// </summary>
    private double _animationSpeed = 0.15;

    private double _animationTimer = 0;

    /// <summary>
    /// what animation frame is being shown
    /// </summary>
    private short animationFrame = 1;



    public PlayerSoul(Vector2 v)
    {

        Position = v;
        
    }


    /// <summary>
    /// loads the bat sprite texture
    /// </summary>
    /// <param name="content">content manager to load with</param>
    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("PlayerSoul");
        SoulBoundingCircle = new BoundingCircle(Position, 24f);
        SwordHitCircle = new BoundingCircle(new Vector2(Position.X - 12f, Position.Y), 36f);
        GroundTracking = new BoundingRectangle(Position.X - 8, Position.Y + SoulBoundingCircle.Radius, 16f, 5f);
        Facing = Direction.Left;
        Velocity = new Vector2(5,5);
        Acceleration = new Vector2(5,5);

    }

   
    /// <summary>
    /// Updates the state of the Player's Soul
    /// </summary>
    /// <param name="gameTime">The game time</param>
    public void Update(GameTime gameTime)
    {
        float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _keyboardState = Keyboard.GetState();
        switch (SoulState)
        {
            case State.Normal:

                if (_keyboardState.IsKeyDown(Keys.A))
                {
                    Position.X -= Velocity.X;
                    Facing = Direction.Left;
                }
                if (_keyboardState.IsKeyDown(Keys.D))
                {
                    Position.X += Velocity.X;
                    Facing = Direction.Right; 
                }
                if (_keyboardState.IsKeyDown(Keys.W))
                {
                    Position.Y -= Velocity.Y;
                    Facing = Direction.Up;
                }
                if (_keyboardState.IsKeyDown(Keys.S))
                {
                    Position.Y += Velocity.Y;
                    Facing = Direction.Down;
                }
                break;

            case State.Orange:

                if (_keyboardState.IsKeyDown(Keys.A) && Velocity.Y >= -8)
                {
                    if(Velocity.X > 0)
                    {
                        Velocity.X -= TurningAcceleration.X * t;
                    }
                    else
                    {
                        Velocity.X -= Acceleration.X * t;
                    }
                    Facing = Direction.Left;
                }
                if (_keyboardState.IsKeyDown(Keys.D) && Velocity.Y <= 8)
                {
                    if(Velocity.X < 0)
                    {
                        Velocity.X += TurningAcceleration.X * t;
                    }
                    else
                    {
                        Velocity.X += Acceleration.X * t;
                    }
                    Facing = Direction.Right; 
                }
                if (_keyboardState.IsKeyDown(Keys.W) && Velocity.Y >= -8)
                {
                    if(Velocity.Y > 0)
                    {
                        Velocity.Y -= TurningAcceleration.Y * t;
                    }
                    else
                    {
                        Velocity.Y -= Acceleration.Y * t;
                    }
                    Facing = Direction.Up;
                }
                if (_keyboardState.IsKeyDown(Keys.S) && Velocity.Y <= 8)
                {
                    if(Velocity.Y < 0)
                    {
                        Velocity.Y += TurningAcceleration.Y * t;
                    }
                    else
                    {
                        Velocity.Y += Acceleration.Y * t;
                    }
                    Facing = Direction.Down;
                }

                Position += Velocity;
                break;

            case State.Blue:
                /// resets Velocity if the player has just landed
                if(JumpState == BlueState.Grounded && Velocity.Y != _initialTakeOffVelocity)
                {
                    Velocity.Y = _initialTakeOffVelocity;
                }

                if(!_keyboardState.IsKeyDown(Keys.W) && JumpState == BlueState.Jumping)
                {
                    //cancels any remaining velocity if released earlier
                    Velocity.Y = 1f;
                    JumpState = BlueState.Falling;
                }

                if(JumpState == BlueState.Falling)
                {
                    ///caps fall speed
                    if(Velocity.Y > -6)
                    {
                        Velocity.Y -= _jumpAccelerationDecay * t;
                    }
                    Position.Y -= Velocity.Y;
                    
                }

                if (JumpState != BlueState.Falling && _keyboardState.IsKeyDown(Keys.W) )
                {
                    if(JumpState == BlueState.Grounded)
                    {
                        Position.Y -= Velocity.Y;
                        JumpState = BlueState.Jumping;
                    }
                    if(JumpState == BlueState.Jumping)
                    {
                        ///if velocity is less than 0, then it is falling
                        if(Velocity.Y > 0)
                        {
                            Velocity.Y -= _jumpAccelerationDecay * t;
                            Position.Y -= Velocity.Y;
                        }
                        else
                        {
                            JumpState = BlueState.Falling;
                        }
                    }
                    Facing = Direction.Up; 
                }

                if (_keyboardState.IsKeyDown(Keys.A))
                {
                    Position.X -= Velocity.X;
                    Facing = Direction.Left; 
                }
                if (_keyboardState.IsKeyDown(Keys.D))
                {
                    Position.X += Velocity.X;
                    Facing = Direction.Right; 
                    
                }
                if (_keyboardState.IsKeyDown(Keys.S))
                {
                    Facing = Direction.Down; 
                }
                break;
        }
        
        Position += ReflectedInitialVelocity;

        ///Reflected velocity will always be acted on in this instance, but currently do not have a better way
        if (HasSword)
        {
            if(ReflectedInitialVelocity.X < 0)
            {
                ReflectedInitialVelocity.X +=_reflectedAccelerationDecay * t;
            }
            if(ReflectedInitialVelocity.X > 0)
            {
                ReflectedInitialVelocity.X -=_reflectedAccelerationDecay * t;
            }
            if(ReflectedInitialVelocity.Y < 0)
            {
                ReflectedInitialVelocity.Y +=_reflectedAccelerationDecay * t;
            }
            if(ReflectedInitialVelocity.Y > 0)
            {
                ReflectedInitialVelocity.Y -=_reflectedAccelerationDecay * t;
            }
        }

        ///resets if the velocity gets too low
        if((ReflectedInitialVelocity.X < 0.1f && ReflectedInitialVelocity.X > 0) || (ReflectedInitialVelocity.X > -0.1f && ReflectedInitialVelocity.X < 0))
        {
            ReflectedInitialVelocity.X = 0;
        }
        if((ReflectedInitialVelocity.Y < 0.1f && ReflectedInitialVelocity.Y > 0) || (ReflectedInitialVelocity.Y > -0.1f && ReflectedInitialVelocity.Y < 0))
        {
            ReflectedInitialVelocity.Y = 0;
        }

        if (HasSword && !OnCoolDown && _keyboardState.IsKeyDown(Keys.J) && !_prevKeyboardState.IsKeyDown(Keys.J))
        {
            if(SoulState == State.Blue && Facing == Direction.Down)
            {
                JumpState = BlueState.Falling;
            }
            OnCoolDown = true;
            HasSwung = true;
        }

        if (OnCoolDown)
        {
            _swordCoolDown += t;
            ///slight lingering hitbox for the sword
            if(HasSwung && _swordCoolDown >= 0.02f)
            {
                HasSwung = false;
            }
        }
        if(_swordCoolDown >= _swordCoolDownThreshold)
        {
            OnCoolDown = false;
            _swordCoolDown = 0;
        }
    


        SoulBoundingCircle.Center = Position;
        GroundTracking.X = Position.X - 8;
        GroundTracking.Y = Position.Y + SoulBoundingCircle.Radius;
        switch (Facing)
        {
            case Direction.Left:
                SwordHitCircle.Center = new Vector2(Position.X - 12, Position.Y);
                break;
            case Direction.Right:
                SwordHitCircle.Center = new Vector2(Position.X + 12, Position.Y);
                break;
            case Direction.Up:
                SwordHitCircle.Center = new Vector2(Position.X, Position.Y - 12);
                break;
            case Direction.Down:
                SwordHitCircle.Center = new Vector2(Position.X, Position.Y + 12);
                break;

        }
        _prevKeyboardState = _keyboardState;
    }


    /// <summary>
    /// Draws animated bat sprite
    /// </summary>
    /// <param name="gameTime">the game time</param>
    /// <param name="spriteBatch">the spriteBatch to draw with</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool isInvunerable)
    {
       //update animationTimer
        _animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        

            //update animaitonFrame
            if(_animationTimer > _animationSpeed)
            {
                animationFrame++;
                if(animationFrame > 4)
                {
                    
                    animationFrame = 0;
                }
                _animationTimer -= _animationSpeed;
            }
        
        var source = new Rectangle(animationFrame*64, (int)_sprite * 64, 64, 64);
        
        //gives a visual cue that the player has invunerablility
        if (!isInvunerable)
        {
            switch (SoulState)
            {
                case (State.Normal):
                    spriteBatch.Draw(_texture, Position, source, Color.White, 0, new Vector2(32,32), 0.75f, SpriteEffects.None, 0);
                    break;
                case (State.Orange):
                    spriteBatch.Draw(_texture, Position, source, Color.Orange, 0, new Vector2(32,32), 0.75f, SpriteEffects.None, 0);
                    break;
                case (State.Blue):
                    spriteBatch.Draw(_texture, Position, source, Color.Blue, 0, new Vector2(32,32), 0.75f, SpriteEffects.None, 0);
                    break;
            }
            
        }
        else
        {
            spriteBatch.Draw(_texture, Position, source, Color.Red, 0, new Vector2(32,32), 0.75f, SpriteEffects.None, 0);
        }
        
    }
    
    /// <summary>
    /// sets the state and movement of the soul
    /// </summary>
    public void SetMovement(State newState)
    {   
        SoulState = newState;
        if(SoulState == State.Normal)
        {
            Velocity = _normalVelocity;
            
        }
        else if(SoulState == State.Orange)
        {
            Velocity = new Vector2(0,0);
        }
        else if (SoulState == State.Blue)
        {
            Velocity = new Vector2(_normalVelocity.X, _initialTakeOffVelocity);
        }
    }
}