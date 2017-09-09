﻿using Game.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            GuiUpdater.ShowCompanyNameScreen();
            GuiUpdater.ShowTitleScreen();
            Application.State = GameState.NextLevel;
            GameLoop.GetInstance().Start();

            switch(Application.State)
            {
                case GameState.Exit:
                    break;
                case GameState.Lost:
                    GuiUpdater.ShowGameLostScreen();
                    Console.ReadKey(true);
                    break;
                case GameState.Won:
                    GuiUpdater.ShowGameWonScreen();
                    Console.ReadKey(true);
                    break;
                default:
                    break;
            }
        }
    }
}