using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework
{
    public class Wall : GameObject
    {
        public Wall(Coordinate initialCoordinate)
        {
            Init(initialCoordinate);
        }

        private void Init(Coordinate initialCoordinate)
        {
            Texture = Textures.Wall;
            Color = ConsoleColor.White;
            Coordinate = initialCoordinate;
        }
    }
}
