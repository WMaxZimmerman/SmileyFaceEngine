using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    public class Character : GameObject
    {
        public Directions Directions { get; set; }
        public string Direction { get; set; }
        public static int SpeedDelay { get; set; }
        public int SpeedCounter { get; set; }
    }
}
