using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Roguelike
{
    class Program
    {
        // Define the screen width & height in number of tiles
        private static readonly int _screenWidth = 60;
        private static readonly int _screenHeight = 40;
        private static readonly int _tileWidth = 8;
        private static readonly int _tileHeight = 8;

        private static readonly string fontFileName = "ascii_8x8.png";
        private static readonly string consoleTitle = "My first roguelike!";

        private static RLRootConsole _rootConsole;

        public static void Main()
        {
            // Create console window
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, _tileWidth, _tileHeight, 1f, consoleTitle);

            // Initialize game engine
            Engine engine = new Engine(_rootConsole);

            // Begin game loop
            _rootConsole.Run();
        }
    }
}
