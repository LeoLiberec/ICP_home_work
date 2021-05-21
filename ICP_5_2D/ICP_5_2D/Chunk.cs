using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class Chunk : Transformable, Drawable
    {
        public const int CHUNK_SIZE = 25; // kolik tilsu bude

        Tile[][] tiles; // array tiles
        Vector2i chunkPos; // pozice chunku v array world 

        public Chunk(Vector2i chunkPos)
        {
            // pozice chunku
            this.chunkPos = chunkPos;
            Position = new Vector2f(chunkPos.X * CHUNK_SIZE * Tile.TILE_SIZE, chunkPos.Y * CHUNK_SIZE * Tile.TILE_SIZE);
            tiles = new Tile[CHUNK_SIZE][];

            // vytvarime 2*2 array tilesu
            for(int i = 0; i < CHUNK_SIZE; i++)
            {
                tiles[i] = new Tile[CHUNK_SIZE]; // array se stejnim obsahem
            }

            //for (int x = 0; x < CHUNK_SIZE; x++)
            //    for (int y = 0; y < CHUNK_SIZE; y++)
            //        SetTile(TileType.GROUND, x, y);
        }

        public void SetTile(TileType type, int i, int j, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {
            if (type != TileType.NONE)
            {
                tiles[i][j] = new Tile(type, upTile, downTile, leftTile, rightTile);
                tiles[i][j].Position = new Vector2f(i * Tile.TILE_SIZE, j * Tile.TILE_SIZE) + Position;
            }
            else
            {
                tiles[i][j] = null;

                if (upTile != null) upTile.DownTile = null;
                if (downTile != null) downTile.UpTile = null;
                if (leftTile != null) leftTile.RightTile = null;
                if (rightTile != null) rightTile.LeftTile = null;
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= CHUNK_SIZE || y >= CHUNK_SIZE) // jesti pozice tile za hranici chunku
                return null;
            
            return tiles[x][y];
        }

        public void Draw(RenderTarget target, RenderStates states) // zobrazime chunky
        {
            states.Transform *= Transform; // pro svravne zobrazovani objektu

            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    if (tiles[x][y] == null) continue;

                    target.Draw(tiles[x][y]); // pro svravne zobrazovani objektu
                }
            }
        }
    }
}
