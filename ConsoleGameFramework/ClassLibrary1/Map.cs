using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileyFaceGameEngine
{
    public class Map
    {
        private readonly int _mapLength;
        private readonly int _mapWidth;
        private const int WallDensity = 35;
        public List<List<char>> MapList;
        public List<GameObject> GameObjects;

        public Map(int mapWidth, int mapLength)
        {
            _mapLength = mapLength;
            _mapWidth = mapWidth;
            Init();
        }

        private void Init()
        {            
            MapList = new List<List<char>>();
            GameObjects = new List<GameObject>();
            Create();
        }

        public void Create()
        {
            var map = Generate();
            //var list = ConvertMapToListOfStrings(map);
            //SetMap(list);

            //list.ForEach(Console.WriteLine);
        }

        public char[,] Generate()
        {
            var map = new char[_mapWidth, _mapLength];
            var rand = new Random();

            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapLength; y++)
                {
                    var coordinate = new Coordinate { X = x, Y = y };

                    if (x == 0 || y == 0)
                    {
                        GameObjects.Add(new Wall(coordinate));
                        //map[x, y] = Textures.Wall;
                    }
                    else if (x == _mapWidth - 1 || y == _mapLength - 1)
                    {
                        GameObjects.Add(new Wall(coordinate));
                        //map[x, y] = Textures.Wall;
                    }
                    else
                    {
                        var number = rand.Next(0, 101);
                        
                        if (number < WallDensity)
                        {
                            GameObjects.Add(new Wall(coordinate));
                            //map[x, y] = Textures.Wall;
                        }
                        else
                        {
                            GameObjects.Add(new Path(coordinate));
                            //map[x, y] = Textures.Path;
                        }
                    }
                }
            }

            return map;
        }

        private List<string> ConvertMapToListOfStrings(char[,] map)
        {
            var list = new List<string>();

            for (var i = 0; i < _mapLength; i++)
            {
                var line = string.Empty;
                for (var j = 0; j < _mapWidth; j++)
                {
                    line += map[i, j];
                }
                list.Add(line);
            }

            return list;
        }

        private void SetMap(List<string> stringList)
        {
            foreach (var s in stringList)
            {
                MapList.Add(s.ToList());
            }
        }
        
        public void Update()
        {
            
        }

        public void Draw()
        {

        }
    }
}
