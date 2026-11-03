using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ZLGameProject;

public class Tilemap
{
    //dimensions of tiles and map
    int _tileWidth, _tileHeight; //_mapWidth, _mapHeight; ***Refactor to include these***

    //tileset texture
    Texture2D _tilesetTexture;

    //The tile info in the tileset
    Rectangle[] _tiles;

    //the tile map
    int[] _map;

    // name of the map file
    string _filename;

    Vector2 _center;

    public Tilemap(string filename, Vector2 center)
    {
        _filename = filename;
        _center = center;
    }

    public void LoadContent(ContentManager content)
    {
        string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
        var lines = data.Split('\n');

        // First line is the tileset file name
        var tilesetFileName = lines[0].Trim();
        _tilesetTexture = content.Load<Texture2D>(tilesetFileName);

        //second line is the tile size
        var secondLine = lines[1].Split(',');
        _tileWidth = int.Parse(secondLine[0]);
        _tileHeight = int.Parse(secondLine[1]);
    }

    public void Draw(SpriteBatch sb)
    {
        //Moon
        sb.Draw(_tilesetTexture, new Vector2(_center.X - 150,_center.Y * .75f), new Rectangle(_tileWidth * 28,_tileHeight * 0, _tileWidth * 4, _tileHeight * 4), Color.White, 0f, new Vector2(32,32), 3f, SpriteEffects.None, 0);

        //Clouds
        sb.Draw(_tilesetTexture, new Vector2(_center.X - 100,_center.Y * .9f), new Rectangle(_tileWidth * 27,_tileHeight * 13, _tileWidth * 5, _tileHeight * 3), Color.White, 0f, new Vector2(40,16), 3f, SpriteEffects.None, 0);
        sb.Draw(_tilesetTexture, new Vector2(_center.X - 250,_center.Y * .9f), new Rectangle(_tileWidth * 25,_tileHeight * 14, _tileWidth * 2, _tileHeight), Color.White, 0f, new Vector2(16,8), 3f, SpriteEffects.None, 0);

        //Mountians
        sb.Draw(_tilesetTexture, new Vector2(0,_center.Y * 2 - 60), new Rectangle(_tileWidth * 18,_tileHeight * 10, _tileWidth * 8, _tileHeight*3), Color.White, 0f, new Vector2(0,48), 5f, SpriteEffects.None, 0);

        //ground
        sb.Draw(_tilesetTexture, new Vector2(0,_center.Y * 2 - 54), new Rectangle(_tileWidth * 19,_tileHeight * 7, _tileWidth * 13, _tileHeight), Color.White, 0f, new Vector2(0,0), 4f, SpriteEffects.None, 0);
        sb.Draw(_tilesetTexture, new Vector2(832,_center.Y * 2 - 54), new Rectangle(_tileWidth * 19,_tileHeight * 7, _tileWidth * 13, _tileHeight), Color.White, 0f, new Vector2(), 4f, SpriteEffects.None, 0);
        
        //background hills
        sb.Draw(_tilesetTexture, new Vector2(-12,_center.Y * 2 - 54), new Rectangle(_tileWidth * 20,_tileHeight * 9, _tileWidth * 12, _tileHeight), Color.White, 0f, new Vector2(0,16), 3f, SpriteEffects.None, 0);
        sb.Draw(_tilesetTexture, new Vector2(564,_center.Y * 2 - 54), new Rectangle(_tileWidth * 20,_tileHeight * 9, _tileWidth * 12, _tileHeight), Color.White, 0f, new Vector2(0,16), 3f, SpriteEffects.None, 0);
        sb.Draw(_tilesetTexture, new Vector2(1128,_center.Y * 2 - 54), new Rectangle(_tileWidth * 20,_tileHeight * 9, _tileWidth * 12, _tileHeight), Color.White, 0f, new Vector2(0,16), 3f, SpriteEffects.FlipHorizontally, 0);
        
        

    }
}