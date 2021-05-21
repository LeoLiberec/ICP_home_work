using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D.NPC
{
    class NpcSlime : Npc
    {
        const float TIME_WAIT_JUMP = 1f;

        SpriteSheet spriteSheet;
        float waitTimer = 0f;
        public NpcSlime(World world) : base(world)
        {
            spriteSheet = new SpriteSheet(1, 2, 0, (int)Content.texNpcSlime.Size.X, (int)Content.texNpcSlime.Size.Y);

            rect = new RectangleShape(new Vector2f(spriteSheet.SubWight / 1.5f, spriteSheet.SubHeight / 1.5f)); // rozmery
            //rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE *1f)); // rozmery
            rect.Origin = new Vector2f(rect.Size.X / 2, 0); // stred NPC
            rect.FillColor = new Color(75, 150, 188, 175); // pruhlednost

            rect.Texture = Content.texNpcSlime;
            rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
        }

        public override void OnKill()
        {
            Spawn();
        }

        public override void OnWallCollided()
        {
            Direction *= -1; // opacny smer 
            velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y); // treti zakon Newtonu :)
        }

        public override void UpdateNPC()
        {
            if (!IsFly)
            {
                if (waitTimer >= TIME_WAIT_JUMP)
                {
                    //velocity = new Vector2f(Direction * Program.Rand.Next(1, 10), -Program.Rand.Next(6, 9));
                    velocity = GetJumpVelocity();
                    waitTimer = 0f;
                }
                else
                {
                    waitTimer += 0.05f;
                    velocity.X = 0f;
                }
                rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
            }
            else
                rect.TextureRect = spriteSheet.GetTextureRect(0, 1);
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {

        }

        public virtual Vector2f GetJumpVelocity()
        {
            return new Vector2f(Direction * Program.Rand.Next(1, 10), -Program.Rand.Next(6, 9));
        }
    }
}
