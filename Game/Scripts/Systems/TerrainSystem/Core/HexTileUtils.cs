using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Terrain
{
    public static class HexTileUtils
    {
        public static List<HexTile> CircularRetrieval(int i, int j, int iterations = 1)
        {
            List<HexTile> hex_list = new List<HexTile>();
            Vector2 map_size = MapManager.GetMapSize();

            // Check immediate neighbors
            if (j - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i, j - 1)]);
            if (j + 1 < map_size.y) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i, j + 1)]);
            if (i - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i - 1, j)]);
            if (i + 1 < map_size.x) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i + 1, j)]);
            if (i + 1 < map_size.x && j - 1 >= 0) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i + 1, j - 1)]);
            if (i - 1 >= 0 && j + 1 < map_size.y) hex_list.Add(HexManager.col_row_to_hex[new Vector2(i - 1, j + 1)]);

            return hex_list;
        }

    }
}