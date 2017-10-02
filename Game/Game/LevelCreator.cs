using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game
{
    class LevelCreator
    {
        private List<Wall> boundaries;
        private Point cursorPosition;

        public LevelCreator()
        {
            boundaries = new List<Wall>();
            cursorPosition = new Point(14, 59);
        }

        public void Start()
        {
            GuiUpdater.ClearScreen();
            Console.CursorVisible = true;
            Console.SetCursorPosition(1, 1);
            SetBoundaries();

            while(true)
            {
                while(Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch(keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            MoveCursor(new Point(0, -1));
                            break;
                        case ConsoleKey.DownArrow:
                            MoveCursor(new Point(0, 1));
                            break;
                        case ConsoleKey.LeftArrow:
                            MoveCursor(new Point(-1, 0));
                            break;
                        case ConsoleKey.RightArrow:
                            MoveCursor(new Point(1, 0));
                            break;
                    }
                }
            }
        }

        private void SetBoundaries()
        {
            boundaries.Add(new Wall(new Point(0, 0), new Point(119, 0)));        // Main frame top
            boundaries.Add(new Wall(new Point(0, 0), new Point(0, 29)));         // Main frame left
            boundaries.Add(new Wall(new Point(0, 29), new Point(120, 29)));      // Main frame bottom
            boundaries.Add(new Wall(new Point(119, 0), new Point(120, 29)));     // Main frame right

            boundaries.Add(new Wall(new Point(107, 1), new Point(107, 4)));      // Top right square left
            boundaries.Add(new Wall(new Point(107, 4), new Point(120, 4)));      // Top right square bottom
            boundaries.Add(new Wall(new Point(0, 2), new Point(107, 2)));        // Top strip

            foreach (Wall wall in boundaries)
                wall.Paint();
        }

        private void ShowToolbar()
        {

        }

        private void MoveCursor(Point point)
        {
            Point newPosition = new Point(cursorPosition.X + point.X, cursorPosition.Y + point.Y);
            if(CanMove(newPosition))
            {
                cursorPosition = newPosition;
                Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
            }
        }

        private bool CanMove(Point point)
        {
            foreach (Wall wall in boundaries)
                if (wall.IsOccupying(point))
                    return false;
            return true;
        }
    }
}
