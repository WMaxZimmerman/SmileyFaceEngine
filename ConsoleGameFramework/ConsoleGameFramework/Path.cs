using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework
{
    public class Path : GameObject
    {
        public Path(Coordinate initialCoordinate)
        {
            Init(initialCoordinate);
        }

        private void Init(Coordinate initialCoordinate)
        {
            Texture = Textures.Path;
            Color = ConsoleColor.Gray;
            Coordinate = initialCoordinate;
        }
    }
}
