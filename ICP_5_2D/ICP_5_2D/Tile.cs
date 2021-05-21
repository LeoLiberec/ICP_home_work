using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    enum TileType
    {
        NONE, // nic
        GROUND, // puda
        GRASS // trava
    }
    class Tile : Transformable, Drawable
    {
        public const int TILE_SIZE = 16; // rozmer tilu

        TileType type = TileType.GROUND; // druh tileu 
        RectangleShape rectShape; // tvar tile
        SpriteSheet spriteSheet; // Sprite

        // sousedi 
        Tile upTile = null;
        Tile downTile = null;
        Tile leftTile = null;
        Tile rightTile = null;

        public Tile UpTile
        {
            set
            {
                upTile = value;
                UpdateView();
            }
            get
            {
                return upTile;
            }
        }

        public Tile DownTile
        {
            set
            {
                downTile = value;
                UpdateView();
            }
            get
            {
                return downTile;
            }
        }
        public Tile LeftTile
        {
            set
            {
                leftTile = value;
                UpdateView();
            }
            get
            {
                return leftTile;
            }
        }
        public Tile RightTile
        {
            set
            {
                rightTile = value;
                UpdateView();
            }
            get
            {
                return rightTile;
            }
        }

        public Tile(TileType type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {
            this.type = type;

            // oznacime sousedi
            if (upTile != null)
            {
                this.upTile = upTile;
                this.upTile.DownTile = this; // pro souseda nahore to je dolni soused
            }

            if (downTile != null)
            {
                this.downTile = downTile;
                this.downTile.UpTile = this; // pro souseda dolu to je horni soused
            }

            if (leftTile != null)
            {
                this.leftTile = leftTile;
                this.leftTile.RightTile = this; // pro souseda zleva to je soused zprava
            }

            if (rightTile != null)
            {
                this.rightTile = rightTile;
                this.rightTile.LeftTile = this; // pro souseda zprava to je soused zleva
            }

            rectShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            switch (type)
            {
                case TileType.GROUND:
                    rectShape.Texture = Content.texTile0; // ground
                    break;
                case TileType.GRASS:
                    rectShape.Texture = Content.texTile1; // grass
                    break;
            }

            // Soubor srpitu
            spriteSheet = new SpriteSheet(TILE_SIZE, TILE_SIZE, 1);

            //rectShape.TextureRect = GetTextureRect(1, 1);
            UpdateView();
        }

        public void UpdateView() // jak vypada tiles v souvislosti od sousedu
        {
            if (upTile != null && downTile != null && leftTile != null && rightTile != null) // jestli tiles ma 4 souseda
            {
                int i = Program.Rand.Next(0, 3);
                //rectShape.TextureRect = GetTextureRect(1 + i, 1);
                rectShape.TextureRect = spriteSheet.GetTextureRect(1 + i, 1);
            }
            else if (upTile == null && downTile == null && leftTile == null && rightTile == null) // jestli tiles nema 4 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(9 + i, 3);
            }
            //////////
            else if (upTile == null && downTile != null && leftTile != null && rightTile != null) // jestli tiles ma 3 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(1 + i, 0);
            }
            else if (upTile != null && downTile == null && leftTile != null && rightTile != null) // jestli tiles ma 3 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(1 + i, 2);
            }
            else if (upTile != null && downTile != null && leftTile == null && rightTile != null) // jestli tiles ma 3 souseda
            {
                int i = Program.Rand.Next(0, 3);    
                rectShape.TextureRect = spriteSheet.GetTextureRect(0, i); 
            }
            else if (upTile != null && downTile != null && leftTile != null && rightTile == null) // jestli tiles ma 3 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(4, i);
            }
            ////////// 
            else if (upTile == null && downTile != null && leftTile == null && rightTile != null) // jestli tiles ma 2 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(0 + i * 2, 3);
            }
            else if (upTile == null && downTile == null && leftTile != null && rightTile == null) // jestli tiles ma 2 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(1 + i * 2, 3);
            }
            else if (upTile != null && downTile == null && leftTile == null && rightTile != null) // jestli tiles ma 2 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(0 + i * 2, 4);
            }
            else if (upTile != null && downTile == null && leftTile != null && rightTile == null) // jestli tiles ma 2 souseda
            {
                int i = Program.Rand.Next(0, 3);
                rectShape.TextureRect = spriteSheet.GetTextureRect(1 + i * 2, 4);
            }
        }

        //public IntRect GetTextureRect(int i, int j) // fragment textury 
        //{
        //    int x = i * TILE_SIZE + i * 2;
        //    int y = j * TILE_SIZE + j * 2;
        //    return new IntRect(x, y, TILE_SIZE, TILE_SIZE);
        //}

        public void Draw(RenderTarget target, RenderStates states) // kreslime tilesy
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(TILE_SIZE, TILE_SIZE));
        }
    }
}
