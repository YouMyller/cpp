using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testgame
{
    public enum TileType { empty, solid, reflector, spring, platform, spikes }

    class StartData
    {
        public int x, y;

        public StartData(int startX, int startY)
        {
            startX = x;
            startY = y;
        }
    }

    class Tile
    {
        public int index;
        public TileType type;
        public Vector2 scale;
        public Vector2 offset;
        public float rot;
        public bool overlap;
        public bool standOn;
        public bool isSolid;
        public bool spikes;
        public bool eventActive;
        public MonsterType monsterStart;

        public Tile(int tileIndex, TileType tileType)
        {
            index = tileIndex;
            type = tileType;
            scale = Vector2.One;
            monsterStart = MonsterType.None;
        }

        public void Clear()
        {
            index = 0;
            type = TileType.empty;
            scale = Vector2.One;
            offset = Vector2.Zero;
            rot = 0;

            overlap = false;
            standOn = false;
            spikes = false;
            eventActive = false;

            monsterStart = MonsterType.None;
        }

    }

    class ProcessedTile
    {
        public Vector2 pos;
        public Vector2 scale;
        public float rot;
        public Rectangle rect;

        public void Add(Vector2 position, Rectangle srtRect, float rotation, Vector2 size)
        {
            pos = position;
            rect = srtRect;
            rot = rotation;
            scale = size;
        }
    }

}
