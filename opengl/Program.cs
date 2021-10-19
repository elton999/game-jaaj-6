using System;

namespace opengl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new game_jaaj_6.Game1())
                game.Run();
        }
    }
}
