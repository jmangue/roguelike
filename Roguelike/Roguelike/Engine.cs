using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Roguelike
{
    public class Engine
    {
        private RLRootConsole _rootConsole;
        private Player _player;
        private static IMap _map;

        public Engine(RLRootConsole console)
        {
            _player = new Player() { X = 5, Y = 5 };
            _rootConsole = console;
            _map = Map.Create(new CaveMapCreationStrategy<Map>(_rootConsole.Width, _rootConsole.Height, 45, 4, 3));
            _rootConsole.Render += Render;
            _rootConsole.Update += Update;
        }

        private void Render(object sender, UpdateEventArgs e)
        {
            _rootConsole.Clear();

            // Calculate FOV
            _map.ComputeFov(_player.X, _player.Y, 50, true);

            foreach (var cell in _map.GetAllCells())
            {
                // If the cell is in the FOV, set it to a brighter color
                if (cell.IsInFov)
                {
                    _map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                    if (cell.IsWalkable) // floor
                    {
                        _rootConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '.');
                    }
                    else // wall
                    {
                        _rootConsole.Set(cell.X, cell.Y, RLColor.LightGray, null, '#');
                    }
                }
                // If the cell is NOT in the FOV but has already been explored, set it to a darker color
                else if (cell.IsExplored)
                {
                    if (cell.IsWalkable)
                    {
                        _rootConsole.Set(cell.X, cell.Y, new RLColor(30,30,30), null, '.');
                    }
                    else
                    {
                        _rootConsole.Set(cell.X, cell.Y, RLColor.Gray, null, '#');
                    }
                }
            }

            // Draw the player
            _rootConsole.Set(_player.X, _player.Y, RLColor.LightGreen, null, '@');

            // Render the scene
            _rootConsole.Draw();
        }

        private void Update(object sender, UpdateEventArgs e)
        {
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();
            if (keyPress != null)
            {
                switch (keyPress.Key)
                {
                    case RLKey.Up:
                        if (_map.GetCell(_player.X, _player.Y - 1).IsWalkable)
                        {
                            _player.Y--;
                        }
                        break;
                    case RLKey.Down:
                        if (_map.GetCell(_player.X, _player.Y + 1).IsWalkable)
                        {
                            _player.Y++;
                        }
                        break;
                    case RLKey.Left:
                        if (_map.GetCell(_player.X - 1, _player.Y).IsWalkable) {
                            _player.X--; 
                        }
                        break;
                    case RLKey.Right:
                        if (_map.GetCell(_player.X + 1, _player.Y).IsWalkable)
                        {
                            _player.X++;
                        }
                        break;
                }
            }
        }
    }
}
