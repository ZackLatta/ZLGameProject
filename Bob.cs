using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Text.Json;
using System.Reflection.Metadata;


namespace ZLGameProject;

public class Bob : IEnemy
{
    public int Hp = 100;

    /// <summary>
    /// what animation frame is being shown
    /// </summary>
    private short animationFrame = 0;

    private float _rotation = 0;

    private const float _ROTATION_AMOUNT = 0.05f;

    private float _rotationDirection = 1;

    private float _rotationTimer = 0;


    /// <summary>
    /// position of the enemy
    /// </summary>
    private Vector2 _position;

    private Texture2D _texture;

    public BobAttack Attack1;

    private ContentManager _content;

    public bool Turn = true; 
    private bool _reset = false;

    public Bob(float center)
    {
        Attack1 = new();
        _position = new Vector2(center, 150);
    }

    public void Initialize(Game game, BoundingBox b, PlayerSoul p)
    {
        Attack1.Initialize(game,b, p);
    }

    //Loads the required content for the enemy
    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Bob");
        Attack1.LoadContent(content);
        _content = content;
        
    }

    //Updates the state of the enemy
    public void Update(GameTime gameTime, BoundingBox b)
    {
        if(Hp >= 75)
        {
            animationFrame = 0;
        }
        else if(Hp >= 50 && Hp < 75)
        {
            animationFrame = 1;
        }
        else if (Hp >= 0 && Hp < 50)
        {
            animationFrame = 2;
        }
        else if (Hp <= 0)
        {
            animationFrame = 0;
        }
        
        if(Turn && !_reset)
        {
            Attack1.LoadContent(_content);
            _reset = true;
            Attack1.IsDone = false;
        }
        if (Turn)
        {
            Attack1.Update(gameTime, b);   
        }
        else
        {
            //clears the reset flag so the attack can be reset again
            _reset = false;
        }

        if (Attack1.IsDone)
        {
            Turn = false;
            animationFrame = 1;
        }
        
    }

    /// <summary>
    /// Draws the enemy
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var source = new Rectangle(animationFrame * 64, 0, 64, 64);
        
        _rotationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_rotationTimer >= 0.1f)
        {
            _rotation += _ROTATION_AMOUNT * _rotationDirection;
                if(_rotation >= 0.25f || _rotation <= -0.25f)
            {
                _rotationDirection *= -1;
            }
            _rotationTimer = 0;
        }
        
        


        
        if (Turn)
        {
            spriteBatch.Draw(_texture, _position, source, Color.White, _rotation, new Vector2(32,32), 3f,SpriteEffects.None, 0);
            Attack1.Draw(spriteBatch); 
        }
        else
        {
            spriteBatch.Draw(_texture, _position, source, Color.White, 0, new Vector2(32,32), 3f,SpriteEffects.None, 0);
        }
        
    }

}
