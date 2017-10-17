using Game.GameObjects;
using Game.Interfaces;
using Game.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Game.Game
{
    class LevelCreatorScreen : IScreen
    {
        private enum GameObject { Wall, EnemyHorizontal, EnemyVertical, Coin, Player, Finish }

        private List<Wall> boundaries;
        private List<Wall> walls;
        private List<Enemy> enemies;
        private List<Coin> coins;
        private Player player;
        private List<Finish> finishes;

        private Thread toolbarThread;
        private Point cursorPosition;
        private GameObject selectedItem;
        private char selectedItemAvatar;
        private ConsoleColor selectedItemColor;
        volatile private int warningDisplayTime;

        public LevelCreatorScreen()
        {
            boundaries = new List<Wall>();
            walls = new List<Wall>();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            finishes = new List<Finish>();
            player = null;

            cursorPosition = new Point(59, 14);
            selectedItem = GameObject.Wall;
            selectedItemAvatar = GameSettings.ObstacleAvatar;
            selectedItemColor = GameSettings.ObstacleColor;
            warningDisplayTime = 2000;
        }

        public int Show()
        {
            GuiUpdater.ClearScreen();
            SetBoundaries();
            ShowToolbar();
            
            MoveCursor(new Point(0, 0));

            while (true)
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
                    case ConsoleKey.F1:
                        selectedItem = GameObject.Wall;
                        selectedItemAvatar = GameSettings.ObstacleAvatar;
                        selectedItemColor = GameSettings.ObstacleColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.F2:
                        selectedItem = GameObject.EnemyHorizontal;
                        selectedItemAvatar = GameSettings.EnemyAvatar;
                        selectedItemColor = GameSettings.EnemyColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.F3:
                        selectedItem = GameObject.EnemyVertical;
                        selectedItemAvatar = GameSettings.EnemyAvatar;
                        selectedItemColor = GameSettings.EnemyColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.F4:
                        selectedItem = GameObject.Coin;
                        selectedItemAvatar = GameSettings.CoinAvatar;
                        selectedItemColor = GameSettings.CoinColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.F5:
                        selectedItem = GameObject.Player;
                        selectedItemAvatar = GameSettings.PlayerAvatar;
                        selectedItemColor = GameSettings.PlayerColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.F6:
                        selectedItem = GameObject.Finish;
                        selectedItemAvatar = GameSettings.FinishAvatar;
                        selectedItemColor = GameSettings.FinishColor;
                        MoveCursor(new Point(0, 0));
                        break;
                    case ConsoleKey.Enter:
                        PlaceObject();
                        break;
                    case ConsoleKey.Delete:
                        DeleteObject();
                        break;
                    case ConsoleKey.F9:
                        TestLevel();
                        break;
                    case ConsoleKey.F10:
                        SaveLevel();
                        break;
                    case ConsoleKey.Escape:
                        return 1;
                }
            }
        }

        public void Paint()
        {
            GuiUpdater.ClearScreen();
            SetBoundaries();
            ShowToolbar();

            foreach (Wall w in boundaries)
                w.Paint();
            foreach (Wall w in walls)
                w.Paint();
            foreach (Enemy e in enemies)
                e.Paint();
            foreach (Coin c in coins)
                c.Paint();
            foreach (Finish f in finishes)
                f.Paint();
            player.Paint();

            MoveCursor(new Point(0, 0));
        }

        private void PlaceObject()
        {
            if(!CanPlace(cursorPosition))
            {
                DisplayWarning("Field is already occupied by different object!", 2000);
                return;
            }

            switch(selectedItem)
            {
                case GameObject.Wall:
                    WallPlacementMode();
                    break;
                case GameObject.EnemyHorizontal:
                    Enemy enemyHorizontal = new Enemy(new Point(cursorPosition.X, cursorPosition.Y), Enemy.FacingHorizontal);
                    enemies.Add(enemyHorizontal);
                    enemyHorizontal.Paint();
                    break;
                case GameObject.EnemyVertical:
                    Enemy enemyVertical = new Enemy(new Point(cursorPosition.X, cursorPosition.Y), Enemy.FacingVertical);
                    enemies.Add(enemyVertical);
                    enemyVertical.Paint();
                    break;
                case GameObject.Coin:
                    Coin coin = new Coin(new Point(cursorPosition.X, cursorPosition.Y), 1);
                    coins.Add(coin);
                    coin.Paint();
                    break;
                case GameObject.Player:
                    if (player != null)
                    {
                        DisplayWarning("Can't place more than one player object!", 2000);
                        break;
                    }
                    player = new Player(new Point(cursorPosition.X, cursorPosition.Y));
                    player.Paint();
                    break;
                case GameObject.Finish:
                    Finish finish = new Finish(new Point(cursorPosition.X, cursorPosition.Y));
                    finishes.Add(finish);
                    finish.Paint();
                    break;
            }
        }

        private void DeleteObject()
        {
            foreach (Wall w in walls)
                if (w.IsOccupying(cursorPosition))
                {
                    w.Remove();
                    walls.Remove(w);
                    MoveCursor(new Point(0, 0));
                    return;
                }
            foreach (Enemy e in enemies)
                if (e.IsOccupying(cursorPosition))
                {
                    enemies.Remove(e);
                    return;
                }
            foreach (Coin c in coins)
                if (c.IsOccupying(cursorPosition))
                {
                    coins.Remove(c);
                    return;
                }
            foreach (Finish f in finishes)
                if (f.IsOccupying(cursorPosition))
                {
                    finishes.Remove(f);
                    return;
                }
            if (player != null && player.IsOccupying(cursorPosition))
                player = null;
        }

        private void SetBoundaries()
        {
            boundaries.Add(new Wall(new Point(0, 0), new Point(119, 0)));        // Main frame top
            boundaries.Add(new Wall(new Point(0, 0), new Point(0, 29)));         // Main frame left
            boundaries.Add(new Wall(new Point(0, 29), new Point(119, 29)));      // Main frame bottom
            boundaries.Add(new Wall(new Point(119, 0), new Point(119, 28)));     // Main frame right

            boundaries.Add(new Wall(new Point(107, 1), new Point(107, 4)));      // Top right square left
            boundaries.Add(new Wall(new Point(107, 4), new Point(119, 4)));      // Top right square bottom
            boundaries.Add(new Wall(new Point(0, 2), new Point(107, 2)));        // Top strip

            foreach (Wall wall in boundaries)
                wall.Paint();
        }

        private void ShowToolbar()
        {
            Console.SetCursorPosition(2, 1);

            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(GameSettings.ObstacleAvatar);
            Console.Write(" F1 | ");

            Console.ForegroundColor = GameSettings.EnemyColor;
            Console.Write("<" + GameSettings.EnemyAvatar);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(" F2 | ");

            Console.ForegroundColor = GameSettings.EnemyColor;
            Console.Write("^" + GameSettings.EnemyAvatar);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(" F3 | ");

            Console.ForegroundColor = GameSettings.CoinColor;
            Console.Write(GameSettings.CoinAvatar);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(" F4 | ");

            Console.ForegroundColor = GameSettings.PlayerColor;
            Console.Write(GameSettings.PlayerAvatar);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(" F5 | ");

            Console.ForegroundColor = GameSettings.FinishColor;
            Console.Write(GameSettings.FinishAvatar);
            Console.ForegroundColor = GameSettings.ObstacleColor;
            Console.Write(" F6 | ");

            Console.Write("Place [Ent] | Delete [Del] | Test F9 | Save F10 | Exit [Esc]");

            Console.SetCursorPosition(108, 1);
            Console.Write("Mrowka Game");
            Console.SetCursorPosition(111, 2);
            Console.Write("Level");
            Console.SetCursorPosition(110, 3);
            Console.WriteLine("Creator");
        }

        private void ClearToolbar()
        {
            Console.SetCursorPosition(1, 1);
            Console.Write("                                                                                                         ");
        }

        private void MoveCursor(Point point)
        {
            Point newPosition = new Point(cursorPosition.X + point.X, cursorPosition.Y + point.Y);
            if(!CanMove(newPosition))
            {
                DisplayWarning("Can't move cursor outside the predefined boundaries!", 2000);
                return;
            }

            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
            Console.Write(" ");

            RepaintObjects();

            cursorPosition = newPosition;
            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
            Console.ForegroundColor = selectedItemColor;
            Console.Write(selectedItemAvatar);
        }

        private void RepaintObjects()
        {
            foreach (Wall w in walls)
                if (w.IsOccupying(cursorPosition))
                {
                    w.Paint();
                    return;
                }
            foreach (Enemy e in enemies)
                if (e.IsOccupying(cursorPosition))
                {
                    e.Paint();
                    return;
                }
            foreach (Coin c in coins)
                if (c.IsOccupying(cursorPosition))
                {
                    c.Paint();
                    return;
                }
            foreach (Finish f in finishes)
                if (f.IsOccupying(cursorPosition))
                {
                    f.Paint();
                    return;
                }
            if (player != null && player.IsOccupying(cursorPosition))
                player.Paint();
        }

        private bool CanMove(Point point)
        {
            foreach (Wall wall in boundaries)
                if (wall.IsOccupying(point))
                    return false;
            return true;
        }

        private bool CanPlace(Point point)
        {
            foreach (Wall w in walls)
                if (w.IsOccupying(point))
                    return false;
            foreach (Enemy e in enemies)
                if (e.IsOccupying(point))
                    return false;
            foreach (Coin c in coins)
                if (c.IsOccupying(point))
                    return false;
            foreach (Finish f in finishes)
                if (f.IsOccupying(point))
                    return false;
            if (player != null && player.IsOccupying(point))
                return false;

            return true;
        }

        private void DisplayWarning(string text, int time)
        {
            if (toolbarThread != null && toolbarThread.IsAlive) return;

            ClearToolbar();
            Console.SetCursorPosition(2, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);

            toolbarThread = new Thread(RedrawToolbar);
            warningDisplayTime = time;
            toolbarThread.Start();
        }

        private void RedrawToolbar()
        {
            Thread.Sleep(warningDisplayTime);
            ShowToolbar();
        }

        private void WallPlacementMode()
        {
            Point p1 = new Point(cursorPosition.X, cursorPosition.Y);
            Point p2 = new Point(cursorPosition.X, cursorPosition.Y);
            Wall wall = new Wall(p1, p2);
            wall.Paint();

            while(true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch(keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        Point pointUp = new Point(p2.X, p2.Y - 1);
                        if(!CanMove(pointUp) || !CanPlace(pointUp))
                        {
                            DisplayWarning("Field is already occupied by different object!", 2000);
                            break;
                        }
                        if(wall.Obstacles[0].Pos.X != wall.Obstacles[wall.Obstacles.Count - 1].Pos.X)
                        {
                            DisplayWarning("Walls can only be 1 field width!", 2000);
                            break;
                        }
                        p2 = pointUp;
                        break;
                    case ConsoleKey.DownArrow:
                        Point pointDown = new Point(p2.X, p2.Y + 1);
                        if (!CanMove(pointDown) || !CanPlace(pointDown))
                        {
                            DisplayWarning("Field is already occupied by different object!", 2000);
                            break;
                        }
                        if (wall.Obstacles[0].Pos.X != wall.Obstacles[wall.Obstacles.Count - 1].Pos.X)
                        {
                            DisplayWarning("Walls can only be 1 field width!", 2000);
                            break;
                        }
                        p2 = pointDown;
                        break;
                    case ConsoleKey.LeftArrow:
                        Point pointLeft = new Point(p2.X - 1, p2.Y);
                        if (!CanMove(pointLeft) || !CanPlace(pointLeft))
                        {
                            DisplayWarning("Field is already occupied by different object!", 2000);
                            break;
                        }
                        if (wall.Obstacles[0].Pos.Y != wall.Obstacles[wall.Obstacles.Count - 1].Pos.Y)
                        {
                            DisplayWarning("Walls can only be 1 field width!", 2000);
                            break;
                        }
                        p2 = pointLeft;
                        break;
                    case ConsoleKey.RightArrow:
                        Point pointRight = new Point(p2.X + 1, p2.Y);
                        if (!CanMove(pointRight) || !CanPlace(pointRight))
                        {
                            DisplayWarning("Field is already occupied by different object!", 2000);
                            break;
                        }
                        if (wall.Obstacles[0].Pos.Y != wall.Obstacles[wall.Obstacles.Count - 1].Pos.Y)
                        {
                            DisplayWarning("Walls can only be 1 field width!", 2000);
                            break;
                        }
                        p2 = pointRight;
                        break;
                    case ConsoleKey.Enter:
                        walls.Add(wall);
                        cursorPosition = p2;
                        return;
                    case ConsoleKey.Escape:
                        wall.Remove();
                        return;
                }

                wall.Remove();
                wall = new Wall(p1, p2);
                wall.Paint();
            }
        }

        private void TestLevel()
        {
            if (player == null)
            {
                DisplayWarning("Can't test a level without a player object!", 2000);
                return;
            }

            foreach (Wall w in boundaries)
                walls.Add(w);
            Level level = new Level(walls, enemies, coins, player, finishes);
            string xml = XmlConvert.Serialize(level);

            Directory.CreateDirectory("temp");
            File.WriteAllText("temp/level_creator_test_level.mtglvl", xml);

            walls.RemoveRange(walls.Count - 5, 4);

            GameScreen game = new GameScreen("temp/level_creator_test_level.mtglvl");
            game.Lives = 999;
            game.Show();
            Paint();
            File.Delete("temp/level_creator_test_level.mtglvl");
        }

        private void SaveLevel()
        {
            if(player == null)
            {
                DisplayWarning("Can't create a level without a player object!", 2000);
                return;
            }

            ClearToolbar();
            Console.SetCursorPosition(2, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("File name: ");

            Console.ForegroundColor = ConsoleColor.White;
            string filename = Console.ReadLine();

            foreach (Wall w in boundaries)
                walls.Add(w);
            Level level = new Level(walls, enemies, coins, player, finishes);
            level.Name = "test";
            string xml = XmlConvert.Serialize(level);

            Directory.CreateDirectory("custom_maps");
            File.WriteAllText("custom_maps/" + filename + ".mtglvl", xml);

            walls.RemoveRange(walls.Count - 5, 4);
            DisplayWarning("Map saved successfully!", 1000);
        }
    }
}
