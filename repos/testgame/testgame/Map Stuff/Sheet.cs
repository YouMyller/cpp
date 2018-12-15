using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testgame
{
    class Sheet
    {
        public TileType type;
        public Rectangle rect;

        public int tiles_wide, tiles_high;
        public Vector2 offset;

        public Sheet (int x, int y, int x2, int y2, TileType Type, int tiles_wide_passed, int tiles_high_passed, float top_left_corner_x, 
            float top_left_corner_y)
        {
            int width = x2 - x + 1;
            int height = y2 - y + 1;

            rect = new Rectangle(x, y, width, height);
            type = Type;
            tiles_wide = tiles_wide_passed;
            tiles_high = tiles_high_passed;
            offset = new Vector2(rect.X, rect.Y) - new Vector2(top_left_corner_x, top_left_corner_y);
        }
    }

    class SheetManager
    {
        int num_sheet_parts;

        public SheetManager()
        {
            num_sheet_parts = 0;
        }

        public void Setup_Sheet_Level1(ref Sheet[] sheet)
        {
            num_sheet_parts = 0;
            int n = 0;

            sheet[n] = new Sheet(0, 0, 1, 1, TileType.empty, 1, 1, 0f, 0f); //nothing - 0
            n++;
            sheet[n] = new Sheet(0,16,255,111,TileType.solid, 4, 1, 0f, 32f);   //grass 1 - q (NOT ADDED TO THE PROJECT YET)
            n++;
            sheet[n] = new Sheet(0, 128, 127, 255, TileType.solid, 2, 2, 0f, 128f); //solid block - w (NOT ADDED TO THE PROJECT YET)
            n++;
            sheet[n] = new Sheet(166, 128, 344, 255, TileType.spring, 1, 1, 224f, 160f); //spring block - e (NOT ADDED TO THE PROJECT YET)
            n++;
            sheet[n] = new Sheet(448, 128, 575, 254, TileType.reflector, 1, 1, 480f, 160f); //reflector block - r (NOT ADDED TO THE PROJECT YET)
            n++;
        }
    }
}
