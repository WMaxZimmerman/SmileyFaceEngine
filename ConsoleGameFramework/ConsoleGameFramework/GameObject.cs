using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework
{
    public class GameObject
    {
        public Coordinate Coordinate { get; set; }
        public char Texture { get; set; }
        public Random rand = new Random();
        public ConsoleColor Color = ConsoleColor.Black;

        public void Draw()
        {
            Console.SetCursorPosition(Coordinate.X, Coordinate.Y);
            Console.BackgroundColor = Color;
            Console.ForegroundColor = Color;
            Console.Write(Texture);
        }
    }
}
