using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class AnimationFrame // Frame 
    {
        public int i, j;
        public float time;

        public AnimationFrame(int i, int j, float time)
        {
            this.i = i;
            this.j = j;
            this.time = time;
        }
    }
    class Animation 
    {
        AnimationFrame[] frames;

        float timer;
        AnimationFrame currFrame; // skutecni frame
        int currFrameIndex; // index framu v arr

        public Animation(params AnimationFrame[] frames)
        {
            this.frames = frames;
            Reset();
        }
        // Animation od zacatku 
        public void Reset()
        {
            timer = 0f;
            currFrameIndex = 0;
            currFrame = frames[currFrameIndex];
        }
        // sledujici frame
        public void NextFrame()
        {
            timer = 0f;
            currFrameIndex++;

            if (currFrameIndex == frames.Length)
                currFrameIndex = 0;

            currFrame = frames[currFrameIndex];
        }
        // Skutecni frame
        public AnimationFrame GetFrame(float speed)
        {
            timer += speed;

            if (timer >= currFrame.time)
                NextFrame();

            return currFrame;
        }


    }
    class AnimSprite : Transformable, Drawable
    {
        public float Speed = 0.05f;

        RectangleShape rectShape;
        SpriteSheet ss;                // soubor spritu
        SortedDictionary<string, Animation> animations = new SortedDictionary<string, Animation>();
        Animation currAnim;            // Skutecna animace
        string currAnimName;           // Jmeno skutecny animaci

        // Barva sprita
        public Color Color
        {
            set { rectShape.FillColor = value; }
            get { return rectShape.FillColor; }
        }

        public AnimSprite(Texture texture, SpriteSheet ss)
        {
            this.ss = ss;

            rectShape = new RectangleShape(new Vector2f(ss.SubWight, ss.SubHeight));
            rectShape.Origin = new Vector2f(ss.SubWight / 2, ss.SubHeight / 2);
            rectShape.Texture = texture;
        }

        public void AddAnimation(string name, Animation animation) // Add animation
        {
            animations[name] = animation;
            currAnim = animation;
            currAnimName = name;
        }

        public void Play(string name) // play animation se konkretnim jmenem
        {
            if (currAnimName == name)
                return;

            currAnim = animations[name];
            currAnimName = name;
            currAnim.Reset();
        }

        public IntRect GetTextureRect()
        {
            var currFrame = currAnim.GetFrame(Speed);
            return ss.GetTextureRect(currFrame.i, currFrame.j);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            rectShape.TextureRect = GetTextureRect();

            states.Transform *= Transform;
            target.Draw(rectShape, states); // tady kreslime figuru - Shape s texturou
        }
    }
}
