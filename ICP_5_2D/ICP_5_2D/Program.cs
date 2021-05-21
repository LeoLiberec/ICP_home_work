using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ICP_5_2D
{
    class Program
    {
        static RenderWindow window;

        public static RenderWindow Window { get { return window; } }
        public static Game Game { private set; get; }
        public static Random Rand { private set; get; }
        static void Main(string[] args)
        {
            window = new RenderWindow(new SFML.Window.VideoMode(800, 600), "ICP_5 2D Hra"); // okno pro aplikace
            window.SetVerticalSyncEnabled(true); // vertikalni sinhronizace zapnuto 

            window.Closed += Window_Closed;
            window.Resized += Window_Resize;
           
            Content.Load();  // spusteni kontentu

            Rand = new Random(); // new random trida
            Game = new Game(); // novy object tridy hry

            while (window.IsOpen) // pokud okno otevreno
            {
                window.DispatchEvents(); // koukame na vsehny udaloti(eventy) okna:(mys, zavrit okno atd)

                Game.Update(); // obnovovat 

                window.Clear(Color.Black); // cisteme oblast okna

                Game.Draw(); // zobrazime hru

                window.Display(); // zobrazime vsehno 
            }
        }

        private static void Window_Resize(object sender, SizeEventArgs e)
        {
            window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height))); // aby tiles nezmenili rozmer
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            window.Close(); // zavirame okno
        }
    }
}
