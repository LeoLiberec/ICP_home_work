using ICP_5_2D.NPC;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class Player : Npc
    {
        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Color HairColor = new Color(255, 0, 255);   //Magenta
        public Color BodyColor = new Color(255, 229, 186); //Plet
        public Color ShirtColor = new Color(255, 255, 0);  //Zluty
        public Color LegsColor = new Color(0, 76, 135);    //Modry

        //public Vector2f StartPosition;
        //RectangleShape rect;
        //RectangleShape rectDirection;
        //Vector2f velocity; // zryhleni hrace
        //Vector2f movement; // pohyb hrace
        //World world;
        //public int Direction
        //{
        //    set
        //    {
        //        int dir = value >= 0 ? 1 : -1;
        //        Scale = new Vector2f(dir, 1);
        //    }
        //    get
        //    {
        //        int dir = Scale.X >= 0 ? 1 : -1;
        //        return dir;
        //    }
        //}

        // Sprite se animace
        AnimSprite asHair;
        AnimSprite asHead;
        AnimSprite asShirt;
        AnimSprite asUndershirt;
        AnimSprite asHands;
        AnimSprite asLegs;
        AnimSprite asShose;


        public Player(World world) : base(world)
        {
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f));
            //rect.FillColor = Color.Transparent;
            rect.Origin = new Vector2f(rect.Size.X/ 2, 0);
            isRectVisible = false;


            // Vlasy
            asHair = new AnimSprite(Content.texPlayerHair, new SpriteSheet(1, 14, 0, (int)Content.texPlayerHair.Size.X, (int)Content.texPlayerHair.Size.Y));
            asHair.Position = new Vector2f(0, 19);
            asHair.Color = HairColor;
            asHair.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHair.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 0, 0.1f),
                new AnimationFrame(0, 1, 0.1f),
                new AnimationFrame(0, 2, 0.1f),
                new AnimationFrame(0, 3, 0.1f),
                new AnimationFrame(0, 4, 0.1f),
                new AnimationFrame(0, 5, 0.1f),
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f)
            ));


            // Hlava
            asHead = new AnimSprite(Content.texPlayerHead, new SpriteSheet(1, 20, 0, (int)Content.texPlayerHead.Size.X, (int)Content.texPlayerHead.Size.Y));
            asHead.Position = new Vector2f(0, 19);
            asHead.Color = BodyColor;
            asHead.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHead.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            //Kosile
            asShirt = new AnimSprite(Content.texPlayerShirt, new SpriteSheet(1, 20, 0, (int)Content.texPlayerShirt.Size.X, (int)Content.texPlayerShirt.Size.Y));
            asShirt.Position = new Vector2f(0, 19);
            asShirt.Color = ShirtColor;
            asShirt.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asShirt.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Plet
            asUndershirt = new AnimSprite(Content.texPlayerUndershirt, new SpriteSheet(1, 20, 0, (int)Content.texPlayerUndershirt.Size.X, (int)Content.texPlayerUndershirt.Size.Y));
            asUndershirt.Position = new Vector2f(0, 19);
            asUndershirt.Color = BodyColor;
            asUndershirt.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asUndershirt.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Ruce
            asHands = new AnimSprite(Content.texPlayerHands, new SpriteSheet(1, 20, 0, (int)Content.texPlayerHands.Size.X, (int)Content.texPlayerHands.Size.Y));
            asHands.Position = new Vector2f(0, 19);
            asHands.Color = BodyColor;
            asHands.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHands.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Kalhoty
            asLegs = new AnimSprite(Content.texPlayerLegs, new SpriteSheet(1, 20, 0, (int)Content.texPlayerLegs.Size.X, (int)Content.texPlayerLegs.Size.Y));
            asLegs.Position = new Vector2f(0, 19);
            asLegs.Color = LegsColor;
            asLegs.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asLegs.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Obuv
            asShose = new AnimSprite(Content.texPlayerShoes, new SpriteSheet(1, 20, 0, (int)Content.texPlayerShoes.Size.X, (int)Content.texPlayerShoes.Size.Y));
            asShose.Position = new Vector2f(0, 19);
            asShose.Color = Color.Magenta;
            asShose.AddAnimation("idle", new Animation(         // klid
                new AnimationFrame(0, 0, 0.1f)
            ));
            asShose.AddAnimation("run", new Animation(          // beh
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));
            //rectDirection = new RectangleShape(new Vector2f(50, 3));
            //rectDirection.FillColor = Color.Red;
            //rectDirection.Position = new Vector2f(-10, rect.Size.Y / 4 - 1);
            //this.world = world;
        }
        //public void Spawn() // restart player
        //{
        //    Position = StartPosition;
        //    velocity = new Vector2f();
        //}

        //public void Update()
        //{
        //    updateMovement();
        //    updatePhysics();
        //    //updateMovement(); 

        //    Position += movement + velocity;

        //    if (Position.Y > Program.Window.Size.Y)
        //        Spawn();
        //}

        public override void OnKill()
        {
            Spawn();
        }

        public override void OnWallCollided()
        {
            
        }

        public override void UpdateNPC()
        {
            updateMovement();

            var mousePos = Mouse.GetPosition(Program.Window);
            var tile = world.GetTileByWorldPos(mousePos);
            if (tile != null)
            {
                FloatRect tileRect = tile.GetFloatRect();
                DebugRender.AddRectangle(tileRect, Color.Green);

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    int i = (int)(mousePos.X / Tile.TILE_SIZE);
                    int j = (int)(mousePos.Y / Tile.TILE_SIZE);
                    world.SetTile(TileType.NONE, i, j);
                }
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                int i = (int)(mousePos.X / Tile.TILE_SIZE);
                int j = (int)(mousePos.Y / Tile.TILE_SIZE);
                world.SetTile(TileType.GROUND, i, j);
            }
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            target.Draw(asHair, states);
            target.Draw(asHands, states);
            target.Draw(asHead, states);
            target.Draw(asLegs, states);
            target.Draw(asShirt, states);
            target.Draw(asUndershirt, states);
            target.Draw(asShose, states);
            //target.Draw(rectDirection, states); // kam smeruje hrac 
        }

        //private void updatePhysics()
        //{
        //    bool isFall = true;

        //    velocity += new Vector2f(0, 0.15f);

        //    Vector2f nextPos = Position + velocity - rect.Origin;
        //    FloatRect playerRect = new FloatRect(nextPos, rect.Size);

        //    int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
        //    int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);
            
        //    Tile tile = world.GetTile(pX, pY);

        //    if (tile != null)
        //    {
        //        //Vector2f nextPos = Position + velocity - rect.Origin;
        //        //FloatRect playerRect = new FloatRect(nextPos, rect.Size);
        //        FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
        //        DebugRender.AddRectangle(tileRect, Color.Red);
        //        isFall = !playerRect.Intersects(tileRect); 
        //    }

        //    if (!isFall)
        //    {
        //        velocity.Y = 0;
        //    }

        //    updatePhysicsWall(playerRect, pX, pY);
        //}

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
        //                movement.X = ((tileRect.Left + tileRect.Width) - playerRect.Left);
        //                velocity.X = 0;
        //            }
        //            else if (offset.X < 0)
        //            {
        //                movement.X = (tileRect.Left - (playerRect.Left + playerRect.Width));
        //                velocity.X = 0;
        //            }
        //        }
        //    }
        //}

        private void updateMovement()
        {
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isJupm = Keyboard.IsKeyPressed(Keyboard.Key.Space);

            bool isStartDebugKey = Keyboard.IsKeyPressed(Keyboard.Key.Num0);
            if (isStartDebugKey)  // Zapiname kresleni objectu pro visual debuging
                DebugRender.Enabled = true;
            else
                DebugRender.Enabled = false;

            bool isMove = isMoveLeft || isMoveRight;

            if(isJupm && !IsFly)
            {
                velocity.Y = -8.5f; // gravitace
            }

            if(isMove)
            {
                if(isMoveLeft)
                {
                    if (movement.X > 0)
                        movement.X = 0;

                    movement.X -= PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = -1;
                }
                else if (isMoveRight)
                {
                    if (movement.X < 0)
                        movement.X = 0;

                    movement.X += PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = 1;
                }

                if (movement.X > PLAYER_MOVE_SPEED)
                    movement.X = PLAYER_MOVE_SPEED;
                else if (movement.X < -PLAYER_MOVE_SPEED)
                    movement.X = -PLAYER_MOVE_SPEED;

                // Animace
                asHair.Play("run");
                asHands.Play("run");
                asLegs.Play("run");
                asHead.Play("run");
                asShirt.Play("run");
                asUndershirt.Play("run");
                asShose.Play("run");

            }
            else
            {
                movement = new Vector2f();
                asHair.Play("idle");
                asHands.Play("idle");
                asLegs.Play("idle");
                asHead.Play("idle");
                asShirt.Play("idle");
                asUndershirt.Play("idle");
                asShose.Play("idle");
            }
        }
        //public void Draw(RenderTarget target, RenderStates states)
        //{
        //    states.Transform *= Transform;
        //    target.Draw(rect, states);
        //    target.Draw(rectDirection, states);
        //}
    }
}
