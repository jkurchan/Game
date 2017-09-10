using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Game
{
    class MusicPlayer
    {
        private static MusicPlayer player;
        private static Thread thread;

        protected enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        private MusicPlayer() { thread = new Thread(Music); }

        public static MusicPlayer GetInstance()
        {
            if (player == null)
                player = new MusicPlayer();
            return player;
        }

        public void Play() { thread.Start(); }

        public void Stop()
        {
            thread.Suspend();
            thread.Resume();
            thread.Abort();
        }

        public void Pause() { thread.Suspend(); }

        public void Resume() { thread.Resume(); }

        private static void Music()
        {
            while (true)
            {
                Console.Beep(200, 800);
                Console.Beep(300, 800);
                Console.Beep(400, 800);
                Console.Beep(500, 800);
                
                Console.Beep(200, 800);
                Console.Beep(300, 800);
                Console.Beep(400, 800);
                Console.Beep(500, 800);

                Console.Beep(100, 800);
                Console.Beep(200, 800);
                Console.Beep(300, 800);
                Console.Beep(400, 800);

                Console.Beep(100, 800);
                Console.Beep(200, 800);
                Console.Beep(300, 800);
                Console.Beep(400, 800);
            }
        }
    }
}
