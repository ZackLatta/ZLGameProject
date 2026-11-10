using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;


namespace ZLGameProject;

public abstract class Bullet : IParticleEmitter
{

    public Vector2 Position {get; set;}

    private Texture2D _texture;

    public Vector2 Velocity{get; set;}

    //private SoundEffect _bounce;

    private float _scale;

    public float Radius;

    public Bullet(Vector2 p, float vX, float vY, float s)
    {
        Position = p;
        Velocity = new Vector2(vX, vY);
        _scale = s;
        Radius = 32f * _scale;
    }

    public virtual void LoadContent(ContentManager content)
    {

    }


    public virtual void Update()
    {


    }

    public virtual void Draw(SpriteBatch sb)
    {
        
    }


}