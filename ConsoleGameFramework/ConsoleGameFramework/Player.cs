using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework
{
    public class Player : Character
    {
        public string Direction { get; set; }

        public Player(Coordinate initialCoordinate)
        {
            Init(initialCoordinate);
        }

        private void Init(Coordinate initialCoordinate)
        {
            Direction = "up";
            Texture = Textures.Player;
            Directions = new Directions();
            Coordinate = initialCoordinate;
            Color = ConsoleColor.Cyan;
        }

        public void Update(ConsoleKey? key, List<List<char>> map)
        {
            SetDirections(map);
            Walk(key, map);
        }

        private void SetDirections(List<List<char>> map)
        {
            if (map[Coordinate.Y - 1][Coordinate.X] == Textures.Path || map[Coordinate.Y - 1][Coordinate.X] == Textures.Enemy) Directions.CanUp = true;
            else Directions.CanUp = false;

            if (map[Coordinate.Y + 1][Coordinate.X] == Textures.Path || map[Coordinate.Y + 1][Coordinate.X] == Textures.Enemy) Directions.CanDown = true;
            else Directions.CanDown = false;

            if (map[Coordinate.Y][Coordinate.X + 1] == Textures.Path || map[Coordinate.Y][Coordinate.X + 1] == Textures.Enemy) Directions.CanRight = true;
            else Directions.CanRight = false;

            if (map[Coordinate.Y][Coordinate.X - 1] == Textures.Path || map[Coordinate.Y][Coordinate.X - 1] == Textures.Enemy) Directions.CanLeft = true;
            else Directions.CanLeft = false;
        }

        private void Walk(ConsoleKey? keyPress, List<List<char>> map)
        {
            if (keyPress != null)
            {
                switch (keyPress)
                {
                    case ConsoleKey.W:
                        if (Directions.CanUp)
                        {
                            Coordinate.Y--;
                        }
                        Direction = "up";
                        break;
                    case ConsoleKey.S:
                        if (Directions.CanDown)
                        {
                            Coordinate.Y++;
                        }
                        Direction = "down";
                        break;
                    case ConsoleKey.A:
                        if (Directions.CanLeft)
                        {
                            Coordinate.X--;
                        }
                        Direction = "left";
                        break;
                    case ConsoleKey.D:
                        if (Directions.CanRight)
                        {
                            Coordinate.X++;
                        }
                        Direction = "right";
                        break;
                }
            }
        }

        

    }
}
