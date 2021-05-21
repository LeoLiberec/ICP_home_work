using ICP_5_2D.NPC;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class Game
    {
        World world; // world
        Player player; // hrac
        //NpcSlime slime;
        NpsFastSlime slime;

        List<NpcSlime> slimes = new List<NpcSlime>();


        public Game()
        {
            world = new World(); // vytvoreme world
            world.GenerateWorld();

            player = new Player(world); // vytvoreme hrace
            player.StartPosition = new Vector2f(300, 50);
            player.Spawn();

            slime = new NpsFastSlime(world); // vytvoreme NPC
            slime.StartPosition = new Vector2f(500, 0);
            slime.Spawn();

            for (int i = 0; i < 3; i++)
            {
                var s = new NpcSlime(world);
                s.StartPosition = new Vector2f(Program.Rand.Next(150, 600), 150);
                s.Direction = Program.Rand.Next(0, 2) == 0 ? 1 : -1;
                s.Spawn();
                slimes.Add(s);
            }

            //bool isStartDebugKey = Keyboard.IsKeyPressed(Keyboard.Key.Num0);
            //if (isStartDebugKey)  // Zapiname kresleni objectu pro visual debuging
            //    DebugRender.Enabled = true;
            //else
            //    DebugRender.Enabled = false;
        }

        public void Update()
        {
            player.Update();
            slime.Update();

            foreach (var s in slimes)
            {
                s.Update();
            }
        }

        public void Draw() // kresleni
        {
            Program.Window.Draw(world);         // kreslime world 
            Program.Window.Draw(player);        // kreslime hrace 
            Program.Window.Draw(slime);         // kreslime NPC
            foreach (var s in slimes)
            {
                Program.Window.Draw(s);
            }
            DebugRender.Draw(Program.Window);   // kreslime objecty pro debuging 
        }
    }
}
