using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testgame.Map_Stuff
{
    class Map
    {
        public const int tilesWide = 100, tilesHigh = 50;
        public const int tilesSpanX = 14, tilesSpanY = 10;

        public Tile[,] tiles;
        public StartData startData;
        public Point loc;

        Sheet[] sheet;
        ProcessedTile[] overlapTiles;
        int overlapCount;

        public Vector2 scroll_offset;
        public int a1, b1, a2, b2;
        public float sx, sy;

        Texture2D tilesImage;
        Vector2 screenCenter;
        SpriteBatch spriteBatch;

        public Map(Sheet[] sht, SpriteBatch sprBatch)
        {
            sheet = sht;
            spriteBatch = sprBatch;
            tiles = new Tile[tilesWide,tilesHigh];

            for (int b = 0; b < tilesHigh; b++)
            {
                for (int a = 0; a < tilesWide; a++)
                {
                    tiles[a, b] = new Tile(0, TileType.empty);
                }
            }

            overlapTiles = new ProcessedTile[250];

            for (int a = 0; a < 250; a++)
            {
                overlapTiles[a] = new ProcessedTile();
            }

            loc = new Point(5, 5);
            startData = new StartData(loc.X, loc.Y);
            screenCenter = Game1.screen_center;
        }

        public void SetTile(Texture2D tilesPic)
        {
            tilesImage = tilesPic;
        }

        public void AddTile(int i)
        {
            DeleteTile();
            tiles[loc.X, loc.Y].index = i;
            tiles[loc.X, loc.Y].offset = sheet[i].offset;

            for (int b = loc.Y; b < loc.Y + sheet[i].tiles_high; b++)
            {
                if (b >= tilesHigh - 1)
                {
                    break;
                }
                for (int a = loc.X; a < loc.X + sheet[i].tiles_wide; a++)
                {
                    if (a >= tilesWide - 1)
                    {
                        break;
                    }

                    TileType type = sheet[i].type;
                    tiles[a, b].type = type;

                    if (type == TileType.solid || type == TileType.spring || type == TileType.platform || type == TileType.spikes)
                    {
                        tiles[a, b].overlap = true;
                        tiles[a, b].standOn = true;

                        if (type == TileType.spikes)
                        {
                            tiles[a, b].spikes = true;
                            tiles[a, b].isSolid = true;
                        }
                        else if (type == TileType.solid)
                        {
                            tiles[a, b].isSolid = true;
                        }
                    }
                }
            }
        }

        public void DeleteTile()
        {
            int i = tiles[loc.X, loc.Y].index;

            for (int b = loc.Y; b < loc.Y + sheet[i].tiles_high; b++)
            {
                if (b >= tilesHigh - 1)
                {
                    break;
                }
                for (int a = loc.X; a < loc.X + sheet[i].tiles_wide; a++)
                {
                    if (a >= tilesWide - 1)
                    {
                        break;
                    }

                    tiles[a, b].Clear();

                }
            }
        }
    }
}
