using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class SpriteSheet
    {
        public int SubWight { get { return subW; } }  // dostavat rozmery mimi tridu 
        public int SubHeight { get { return subH; } }

        int subW, subH;   //Sirka a vyska fragmenta textury 
        int borderSize;   //Tloustka hranici mezi fragmentami

        public SpriteSheet(int a, int b, int borderSize, int texW = 0, int texH = 0)
        {
            if (borderSize > 0)
            {
                this.borderSize = borderSize + 1;
            }
            else
            {
                this.borderSize = 0;
            }


            if (texW != 0 && texH != 0)
            {
                subW = (int)Math.Ceiling((float)texW / a);
                subH = (int)Math.Ceiling((float)texH / b);
            }
            else
            {
                subW = a;
                subH = b;
            }
        }

        public IntRect GetTextureRect(int i, int j)
        {
            int x = i * subW + i * borderSize;
            int y = j * subH + j * borderSize;
            return new IntRect(x, y, subW, subH);
        }
    }
}
