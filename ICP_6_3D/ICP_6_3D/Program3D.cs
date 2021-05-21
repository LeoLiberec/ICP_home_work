using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace ICP_6_3D
{
    public static class Program3D
    {
        private static void Main() // Hlavni metoda
        {
            var nativeWinSett = new NativeWindowSettings() // Parametry okna
            {
                Size = new Vector2i(1024, 768),
                Title = "ICP_6_3D Kamera a Světlo",
            };

            using (var win = new Win3D(GameWindowSettings.Default, nativeWinSett))
            {
                win.Run(); // Startueme okno
            }
        }
    }
}
