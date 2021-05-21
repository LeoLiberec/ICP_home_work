using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using System.IO;

namespace ICP_4_Everoment
{
    class TextureCube
    {
        private const int TextureCount = 6;
        private readonly TextureTarget[] targets =
        {
            TextureTarget.TextureCubeMapPositiveX,
            TextureTarget.TextureCubeMapNegativeX,
            TextureTarget.TextureCubeMapPositiveY,
            TextureTarget.TextureCubeMapNegativeY,
            TextureTarget.TextureCubeMapPositiveZ,
            TextureTarget.TextureCubeMapNegativeZ
        };

        private readonly string[] filenames =
        {
            "Right.jpg",
            "Left.jpg",
            "Top.jpg",
            "Bottom.jpg",
            "Front.jpg",
            "Back.jpg"

            //"Right.png",
            //"Left.png",
            //"Top.png",
            //"Bottom.png",
            //"Front.png",
            //"Back.png"
        };

        private int index;

        public TextureCube()
        {
            index = GL.GenTexture();
        }
        public void LoadFromFile(string path)
        {
            GL.BindTexture(TextureTarget.TextureCubeMap, index);

            for(int i = 0; i < TextureCount; i++)
            {
                string globalPath = Path.Combine(path, filenames[i]);
                Image image = new Image(globalPath);

                  GL.TexImage2D(targets[i], 0, PixelInternalFormat.Rgba, (int)image.Size.X,
                    (int)image.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Pixels);

                GL.TextureParameter(index, TextureParameterName.TextureMagFilter, 
                    (int)TextureMagFilter.Linear);
                GL.TextureParameter(index, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);

                GL.TextureParameter(index, TextureParameterName.TextureWrapS,
                    (int)TextureWrapMode.Repeat);
                GL.TextureParameter(index, TextureParameterName.TextureWrapR,
                    (int)TextureWrapMode.Repeat);
                GL.TextureParameter(index, TextureParameterName.TextureWrapT,
                    (int)TextureWrapMode.Repeat);
            }
        }

        public void Activate()
        {
            GL.BindTexture(TextureTarget.TextureCubeMap, index);
        }

        public static void Disable()
        {
            //GL.BindTexture(TextureTarget.TextureCubeMap, 0);
        }
    }
}
