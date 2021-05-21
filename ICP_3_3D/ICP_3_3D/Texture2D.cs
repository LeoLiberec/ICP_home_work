using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ICP_3_3D
{
    class Texture2D
    {
        private int index;
        public Texture2D(string File)
        {
            using(Image image = new Image(File))
            {
                index = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, index);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
                    (int)image.Size.X, (int)image.Size.Y, 0, PixelFormat.Rgba, 
                    PixelType.UnsignedByte, image.Pixels);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, 
                    (float)TextureWrapMode.MirroredRepeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                    (float)TextureWrapMode.MirroredRepeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
        }

        public void BindTexture()
        {
            GL.BindTexture(TextureTarget.Texture2D, index);
        }

        public static void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
