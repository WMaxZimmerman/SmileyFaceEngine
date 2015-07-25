using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    public class Bullet : GameObject
    {
        private string Direction { get; set; }
        public int SpeedDelay { get; set; }
        private int SpeedCounter { get; set; }

        public Bullet(string direction, Coordinate coordinate, int speedDelay)
        {
            Init(direction, coordinate, speedDelay);   
        }

        private void Init(string direction, Coordinate coordinate, int speedDelay)
        {
            SpeedDelay = speedDelay;
            SpeedCounter = 0;
            Direction = direction;
            Coordinate = coordinate;
            SetTexture();
        }

        private void SetTexture()
        {
            if (Direction == "up" || Direction == "down")
            {
                Texture = Textures.BulletVertical;
            }
            else Texture = Textures.BulletHorizontal;
        }

        public void Update()
        {
            SpeedCounter++;
            if (SpeedCounter >= SpeedDelay)
            {
                Fly();
                SpeedCounter = 0;
            }
        }

        private void Fly()
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
        }
    }
}
