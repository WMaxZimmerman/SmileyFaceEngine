using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    abstract public class Game
    {
        public List<List<char>> World;
        public int WorldWidth, WorldHeight;
        public GameStates State;

        public Game(int worldWidth, int worldHeight)
        {
            Init(worldWidth, worldHeight);
        }

        abstract public void Init(int worldWidth, int worldHeight);

        #region Update Methods

        public void Update(ConsoleKey? keyPress)
        {
            switch (State)
            {
                case GameStates.Play:
                    UpdatePlay(keyPress);
                    break;
                case GameStates.Menu:
                    UpdateMenu(keyPress);
                    break;
                case GameStates.Win:
                    UpdateWin(keyPress);
                    break;
                case GameStates.Lose:
                    UpdateLose(keyPress);
                    break;
                case GameStates.Pause:
                    UpdatePause(keyPress);
                    break;
            }
        }
        abstract public void UpdatePlay(ConsoleKey? keyPress);
        abstract public void UpdateMenu(ConsoleKey? keyPress);
        abstract public void UpdateWin(ConsoleKey? keyPress);
        abstract public void UpdateLose(ConsoleKey? keyPress);
        abstract public void UpdatePause(ConsoleKey? keyPress);

        #endregion

        #region Draw Methods

        public void Draw()
        {
            switch (State)
            {
                case GameStates.Play:
                    DrawPlay();
                    break;
                case GameStates.Menu:
                    DrawMenu();
                    break;
                case GameStates.Win:
                    DrawWin();
                    break;
                case GameStates.Lose:
                    DrawLose();
                    break;
                case GameStates.Pause:
                    DrawPause();
                    break;
            }
        }
        abstract public void DrawPlay();
        abstract public void DrawMenu();
        abstract public void DrawWin();
        abstract public void DrawLose();
        abstract public void DrawPause();

        #endregion

        public void GameLoop()
        {
            ConsoleKey? keyPress;

            do
            {
                keyPress = Console.ReadKey(true).Key;
                while (!Console.KeyAvailable)
                {
                    Update(keyPress);
                    Draw();
                    keyPress = null;
                }
            } while (keyPress != ConsoleKey.Escape);

        }

    }
}
