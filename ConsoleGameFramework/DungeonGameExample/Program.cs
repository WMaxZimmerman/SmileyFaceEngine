using SmileyFaceGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGame.Main();
            var game = new TestGame(79, 24);

            game.GameLoop();
        }
    }
}
