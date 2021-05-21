using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class World : Transformable, Drawable
    {
        public const int WORLD_SIZE = 5; // kolik chunku bude

        Chunk[][] chunks;

        public World() /// construktor tridy
        {
            chunks = new Chunk[WORLD_SIZE][];

            for (int i = 0; i < WORLD_SIZE; i++)
                chunks[i] = new Chunk[WORLD_SIZE]; // array se stejnim obsahem
            //chunks[0][0] = new Chunk();

            //for (int x = 0; x < Chunk.CHUNK_SIZE; x++) // Rozmery GROUND
            //    for (int y = 0; y < Chunk.CHUNK_SIZE; y++) // Rozmery GROUND
            //        SetTile(TileType.GROUND, x, y);

            //for (int x = Chunk.CHUNK_SIZE; x < Chunk.CHUNK_SIZE * 2; x++) // Rozmery GRASS
            //    for (int y = 0; y < Chunk.CHUNK_SIZE; y++) // Rozmery GRASS
            //        SetTile(TileType.GRASS, x, y);
        }

        public void GenerateWorld() // vytvareme novy svet - world
        {
            // World 3*3
            for (int x = 3; x <= 46; x++) // Rozmery GRASS
                for (int y = 17; y <= 17; y++) // Rozmery GRASS
                    SetTile(TileType.GRASS, x, y);

            for (int x = 3; x <= 46; x++) // Rozmery GROUND
                for (int y = 18; y <= 32; y++) // Rozmery GROUND
                    SetTile(TileType.GROUND, x, y);

            for (int x = 3; x <= 4; x++) // Rozmery GROUND
                for (int y = 1; y <= 17; y++) // Rozmery GROUND
                    SetTile(TileType.GROUND, x, y);

            for (int x = 45; x <= 46; x++) // Rozmery GROUND
                for (int y = 1; y <= 17; y++) // Rozmery GROUND
                    SetTile(TileType.GROUND, x, y); 

            //for (int x = 2; x < Chunk.CHUNK_SIZE; x++) // Rozmery GROUND
            //    for (int y = 2; y < Chunk.CHUNK_SIZE; y++) // Rozmery GROUND
            //        SetTile(TileType.GROUND, x, y);
            //for (int x = Chunk.CHUNK_SIZE; x < Chunk.CHUNK_SIZE * 2; x++) // Rozmery GRASS
            //    for (int y = 0; y < Chunk.CHUNK_SIZE; y++) // Rozmery GRASS
            //        SetTile(TileType.GRASS, x, y);
        }
        public void SetTile(TileType type, int i, int j) 
        {
            var chunk = GetChunk(i, j);
            var tilePos = GetTilePosFromChunk(i, j); // 
                                                     // sousedi 
            Tile upTile = GetTile(i, j - 1);
            Tile downTile = GetTile(i, j + 1);
            Tile leftTile = GetTile(i - 1, j);
            Tile rightTile = GetTile(i + 1, j);

            chunk.SetTile(type, tilePos.X, tilePos.Y, upTile, downTile, leftTile, rightTile);
        }

        public Tile GetTileByWorldPos(float x, float y)
        {
            int i = (int)(x / Tile.TILE_SIZE);
            int j = (int)(y / Tile.TILE_SIZE);
            return GetTile(i, j);
        }
        public Tile GetTileByWorldPos(Vector2f pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }
        public Tile GetTileByWorldPos(Vector2i pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }

        public Tile GetTile(int x, int y)
        {
            var chunk = GetChunk(x, y);
            if (chunk == null) // jestli chunk nezjisten
                return null;


            var tilePos = GetTilePosFromChunk(x, y); // pozice tile v arr chunk

            return chunk.GetTile(tilePos.X, tilePos.Y);
        }

        public Chunk GetChunk(int x, int y)
        {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;

            if(X >= WORLD_SIZE || Y >= WORLD_SIZE || X < 0 || Y < 0)
            {
                return null;
            }

            if (chunks[X][Y] == null)
            {
                chunks[X][Y] = new Chunk(new Vector2i(X, Y));
            }

            return chunks[X][Y];
        }

        public Vector2i GetTilePosFromChunk(int x, int y) // pozice tileu uvnitr chunku 
        {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;

            return new Vector2i(x - X * Chunk.CHUNK_SIZE, y - Y * Chunk.CHUNK_SIZE);
        }

        public void Draw(RenderTarget target, RenderStates states) // metoda pro zobrazovani 
        {
            for (int x = 0; x < WORLD_SIZE; x++)
            {
                for (int y = 0; y < WORLD_SIZE; y++)
                {
                    if (chunks[x][y] == null) continue;
                    target.Draw(chunks[x][y]);
                }
            }
        }
    }
}
