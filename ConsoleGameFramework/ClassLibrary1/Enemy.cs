using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    public class Enemy : Character
    {
        public Enemy(Coordinate initialCoordinate, int speedDelay)
        {
            Init(initialCoordinate, speedDelay);
        }

        private void Init(Coordinate initialCoordinate, int speedDelay)
        {
            Texture = Textures.Enemy;
            Directions = new Directions();
            Coordinate = initialCoordinate;
            Direction = "";
            Color = ConsoleColor.Red;
            SpeedDelay = speedDelay;
            SpeedCounter = 0;
        }

        public void Update(List<List<char>> map)
        {
            SpeedCounter++;
            if(SpeedCounter >= SpeedDelay)
            {
                SetDirections(map);
                Direction = SetDirection();
                Walk();
                SpeedCounter = 0;
            }      
        }

        private void SetDirections(List<List<char>> map)
        {
            if (map[Coordinate.Y - 1][Coordinate.X] == Textures.Path) Directions.CanUp = true;
            else Directions.CanUp = false;

            if (map[Coordinate.Y + 1][Coordinate.X] == Textures.Path) Directions.CanDown = true;
            else Directions.CanDown = false;

            if (map[Coordinate.Y][Coordinate.X + 1] == Textures.Path) Directions.CanRight = true;
            else Directions.CanRight = false;

            if (map[Coordinate.Y][Coordinate.X - 1] == Textures.Path) Directions.CanLeft = true;
            else Directions.CanLeft = false;

        }      

        private string SetDirection()
        {
            var directionList = new List<string>();
            if (Directions.CanLeft) directionList.Add("left");
            if (Directions.CanRight) directionList.Add("right");
            if (Directions.CanUp) directionList.Add("up");
            if (Directions.CanDown) directionList.Add("down");

            var index = rand.Next(0, directionList.Count);
            return directionList.Count != 0 ? directionList[index] : "";
        }

        private void Walk()
        {
            switch (Direction)
            {
                case "up":
                    Coordinate.Y--;
                    break;
                case "down":
                    Coordinate.Y++;
                    break;
                case "left":
                    Coordinate.X--;
                    break;
                case "right":
                    Coordinate.X++;
                    break;
            }

            //return map;
        }

    }
}
