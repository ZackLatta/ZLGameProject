    using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;


namespace ZLGameProject;

public interface IEnemy
{
    //Loads the required content for the enemy
    public void LoadContent(ContentManager content);

    //Updates the state of the enemy
    public void Update(GameTime gameTime, BoundingBox b);

    //Draws the enemy
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

}
