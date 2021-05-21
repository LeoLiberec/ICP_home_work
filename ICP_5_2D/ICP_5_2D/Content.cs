using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_5_2D
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content\\";
        public static Texture texTile0; // textura tiles Graund
        public static Texture texTile1; // textura tiles Grass
        public static Texture texNpcSlime; // textura tiles Slime

        public static Texture texPlayerHair; // textura tiles Hrac
        public static Texture texPlayerHands; // textura tiles Hrac
        public static Texture texPlayerHead; // textura tiles Hrac
        public static Texture texPlayerLegs; // textura tiles Hrac
        public static Texture texPlayerShirt; // textura tiles Hrac
        public static Texture texPlayerShoes; // textura tiles Hrac
        public static Texture texPlayerUndershirt; // textura tiles Hrac

        public static void Load() // metoda pro nacitani dat do app
        {
            texTile0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
            texTile1 = new Texture(CONTENT_DIR + "Textures\\Tiles_2.png");

            // NPC
            texNpcSlime = new Texture(CONTENT_DIR + "Textures\\npc\\slime.png");
            //Player
            texPlayerHair = new Texture(CONTENT_DIR + "Textures\\player\\hair.png"); 
            texPlayerHands = new Texture(CONTENT_DIR + "Textures\\player\\hands.png");
            texPlayerHead = new Texture(CONTENT_DIR + "Textures\\player\\head.png"); // textura tiles Hrac
            texPlayerLegs = new Texture(CONTENT_DIR + "Textures\\player\\legs.png"); // textura tiles Hrac
            texPlayerShirt = new Texture(CONTENT_DIR + "Textures\\player\\shirt.png"); // textura tiles Hrac
            texPlayerShoes = new Texture(CONTENT_DIR + "Textures\\player\\shoes.png"); // textura tiles Hrac
            texPlayerUndershirt = new Texture(CONTENT_DIR + "Textures\\player\\undershirt.png"); // textura tiles Hrac
        }
    }
}
