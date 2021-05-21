using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D.NPC
{
    enum DirectionType
    {
        Left, Right, Up, Down
    }
    abstract class Npc : Transformable, Drawable
    {
        public Vector2f StartPosition;

        protected RectangleShape rect;
        protected Vector2f velocity;
        protected Vector2f movement;
        protected World world;
        protected bool IsFly = true;
        protected bool isRectVisible = true;

        public int Direction
        {
            set
            {
                int dir = value >= 0 ? 1 : -1;
                Scale = new Vector2f(dir, 1);
            }
            get
            {
                int dir = Scale.X >= 0 ? 1 : -1;
                return dir;
            }
        }

        public Npc(World world) // kosruktor prijma parametr odkaz world 
        {
            this.world = world;
        }

        public void Spawn() // restart bot NPC
        {
            Position = StartPosition;
            velocity = new Vector2f();
        }

        public void Update()
        {
            UpdateNPC();
            updatePhysics();

            //Position += movement + velocity;

            if (Position.Y > Program.Window.Size.Y) // spadnul li hrac
                OnKill();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if (isRectVisible)
                target.Draw(rect, states);

            DrawNPC(target, states);
        }

        private void updatePhysics()
        {
            //bool isFall = true;

            velocity.X *= 0.99f;
            //Gravitace
            velocity.Y += 0.25f;

            var offset = velocity + movement;
            float dist = MathHelper.GetDistance(offset);

            int countStep = 1;
            float stepSize = (float)Tile.TILE_SIZE / 2;
            if (dist > stepSize)
                countStep = (int)(dist / stepSize);

            //Vector2f nextPos = Position + velocity - rect.Origin;
            //FloatRect playerRect = new FloatRect(nextPos, rect.Size);
            //int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
            //int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);
            //Tile tile = world.GetTile(pX, pY);

            Vector2f nextPos = Position + offset;
            Vector2f stepPos = Position - rect.Origin; 
            FloatRect stepRect = new FloatRect(stepPos, rect.Size);
            Vector2f stepVec = (nextPos - Position) / countStep;

            for (int step = 0; step < countStep; step++)
            {
                bool isBreakStep = false;

                stepPos += stepVec;
                stepRect = new FloatRect(stepPos, rect.Size);

                DebugRender.AddRectangle(stepRect, Color.Blue);

                //int i = (int)((Position.X + rect.Size.X / 2) / Tile.TILE_SIZE);
                //int j = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);
                int i = (int)((stepPos.X + rect.Size.X / 2) / Tile.TILE_SIZE);
                int j = (int)((stepPos.Y + rect.Size.Y) / Tile.TILE_SIZE);
                Tile tile = world.GetTile(i, j);

                if (tile != null)
                {
                    FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
                    DebugRender.AddRectangle(tileRect, Color.Red);

                    if (updateCollision(stepRect, tileRect, DirectionType.Down, ref stepPos))
                    {
                        velocity.Y = 0;
                        IsFly = false;
                        isBreakStep = true;
                    }
                    else
                        IsFly = true;
                }
                else
                    IsFly = true;

                if (updateWallColision(i, j, -1, ref stepPos, stepRect) || updateWallColision(i, j, 1, ref stepPos, stepRect))
                {
                    OnWallCollided();
                    isBreakStep = true;
                }
                if (isBreakStep)
                    break;
            }

            Position = stepPos + rect.Origin;
            //int i = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
            //int j = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);
            //Tile tile = world.GetTile(i, j);

            //if (tile != null)
            //{
            //    FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
            //    DebugRender.AddRectangle(tileRect, Color.Red);
            //    isFall = !playerRect.Intersects(tileRect);
            //    IsFly = isFall;
            //}
            //if (!isFall)
            //{
            //    velocity.Y = 0;
            //}
            //updatePhysicsWall(playerRect, pX, pY);
        }

        //private void updatePhysicsWall(FloatRect playerRect, int pX, int pY)
        //{
        //    Tile[] walls = new Tile[]
        //    {
        //        world.GetTile(pX - 1, pY - 1),
        //        world.GetTile(pX - 1, pY - 2),
        //        world.GetTile(pX - 1, pY - 3),
        //        world.GetTile(pX + 1, pY - 1),
        //        world.GetTile(pX + 1, pY - 2),
        //        world.GetTile(pX + 1, pY - 3),
        //    };

        //    foreach (Tile tile in walls)
        //    {
        //        if (tile == null) continue;

        //        FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
        //        DebugRender.AddRectangle(tileRect, Color.Yellow);

        //        if (playerRect.Intersects(tileRect))
        //        {
        //            Vector2f offset = new Vector2f(playerRect.Left - tileRect.Left, 0);
        //            offset.X /= Math.Abs(offset.X);

        //            float speed = Math.Abs(movement.X);

        //            if (offset.X > 0)
        //            {
        //                Position = new Vector2f((tileRect.Left + tileRect.Width) + playerRect.Width / 2, Position.Y);
        //                movement.X = 0;
        //                //velocity.X = 0;
        //            }
        //            else if (offset.X < 0)
        //            {
        //                Position = new Vector2f(tileRect.Left - playerRect.Width / 2, Position.Y);
        //                movement.X = 0;
        //                //velocity.X = 0;
        //            }

        //            OnWallCollided();
        //        }
        //    }
        //}

            // KOntrola kolizi s tiles
        bool updateWallColision(int i, int j, int iOffset, ref Vector2f stepPos, FloatRect stepRect)
        {
            var dirType = iOffset > 0 ? DirectionType.Right : DirectionType.Left;

            Tile[] walls = new Tile[]
            {
                world.GetTile(i + iOffset, j - 1),
                world.GetTile(i + iOffset, j - 2),
                world.GetTile(i + iOffset, j - 3)
            };

            bool isWallCollided = false;
            foreach(Tile t in walls)
            {
                if (t == null) continue; 

                FloatRect tileRect = new FloatRect(t.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
                DebugRender.AddRectangle(tileRect, Color.Yellow);

                if (updateCollision(stepRect, tileRect, dirType, ref stepPos))
                    isWallCollided = true; // mame collisie
            }
            return isWallCollided;
        }

        bool updateCollision(FloatRect rectNPC, FloatRect rectTile, DirectionType direction, ref Vector2f pos )
        {
            if (rectNPC.Intersects(rectTile))
            {
                switch(direction)
                {
                    case DirectionType.Up: pos = new Vector2f(pos.X, rectTile.Top + rectTile.Height - 1); break;
                    case DirectionType.Down: pos = new Vector2f(pos.X, rectTile.Top - rectNPC.Height + 1); break;
                    case DirectionType.Left: pos = new Vector2f(rectTile.Left + rectTile.Width - 1, pos.Y); break;
                    case DirectionType.Right: pos = new Vector2f(rectTile.Left - rectNPC.Width + 1, pos.Y); break;
                }
                return true;
            }
            return false;
        }
        public abstract void OnKill();         //restart
        public abstract void OnWallCollided(); // kolize
        public abstract void UpdateNPC();      // update 
        public abstract void DrawNPC(RenderTarget target, RenderStates states); // restart logic NPC  

    }
}
