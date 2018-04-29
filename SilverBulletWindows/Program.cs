using MknGames;
using MknGames.FPSWahtever;
using System;

namespace SilverBullet
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameMG())
            {
                game.Components.Add(new SmallFPS(game));
                //game.Components.Add(new MknGames.NonGames.CapsuleCollisionRoom(game));
                game.Run();
            }
        }
    }
#endif
}
