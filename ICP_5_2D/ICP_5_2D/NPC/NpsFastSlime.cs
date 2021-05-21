using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D.NPC
{
    class NpsFastSlime : NpcSlime
    {
        public NpsFastSlime(World world) : base(world)
        {
            rect.FillColor = new Color(255, 0, 0, 200);
        }


        public override Vector2f GetJumpVelocity()
        {
            return new Vector2f(Direction * Program.Rand.Next(15, 100), -Program.Rand.Next(6, 9));
        }
    }
}
