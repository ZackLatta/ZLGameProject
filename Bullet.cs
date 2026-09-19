using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Runtime.CompilerServices;


namespace ZLGameProject;

public class Bullet
{
    public Vector2 Center = new Vector2(32,32);
    
    public Vector2 Position;

    private Texture2D _texture;

    private Vector2 _velocityX;
    private Vector2 _velocityY;

    private float _scale;

    public float Radius;

    public Bullet(Vector2 p, float vX, float vY, float s)
    {
        Position = p;
        _velocityX = new Vector2(vX, 0);
        _velocityY = new Vector2(0, vY);
        _scale = s;
        Radius = 32f * _scale;
    }

    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }


    public void Update()
    {
        Position += _velocityX;
        Position += _velocityY;

    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(_texture, Position, null, Color.White, 0, Center, _scale, SpriteEffects.None, 0);  
    }

    public void CollideWithBoundingBox(BoundingBox b)
    {
        

        float nearestX = MathHelper.Clamp(Position.X, b.Left.X, b.Left.X + b.Left.Width);
        float nearestY = MathHelper.Clamp(Position.Y, b.Left.Y, b.Left.Y + b.Left.Height);

        
        if(Math.Pow(Radius, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            _velocityX *= -1; 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Right.X, b.Right.X + b.Right.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Right.Y, b.Right.Y + b.Right.Height);

        
        if(Math.Pow(Radius, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            _velocityX *= -1; 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Top.X, b.Top.X + b.Top.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Top.Y, b.Top.Y + b.Top.Height);

         
        if(Math.Pow(Radius, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            _velocityY *= -1; 
        }

        nearestX = MathHelper.Clamp(Position.X, b.Bottom.X, b.Bottom.X + b.Bottom.Width);
        nearestY = MathHelper.Clamp(Position.Y, b.Bottom.Y, b.Bottom.Y + b.Bottom.Height);

        
        if(Math.Pow(Radius, 2) >= Math.Pow(Position.X - nearestX, 2) + Math.Pow(Position.Y - nearestY, 2))
        {
            _velocityY *= -1;  
        }
  
    }
}