using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameFramework
{
    public class Game
    {
        private List<List<char>> World;
        private Map Map;
        private Player Player;
        private List<Enemy> Enemy;
        private List<Bullet> Bullets;
        private int WorldWidth, WorldHeight;
        private int State;

        public Game(int worldWidth, int worldHeight)
        {
            Init(worldWidth, worldHeight);
        }

        private void Init(int worldWidth, int worldHeight)
        {
            var enemySpeed = 200;
            WorldWidth = worldWidth;
            WorldHeight = worldHeight;
            Map = new Map(WorldWidth, WorldHeight);
            Player = new Player(new Coordinate { X = (WorldWidth / 4), Y = (WorldHeight / 4) });
            Bullets = new List<Bullet>();
            Enemy = new List<Enemy>();
            Enemy.Add(new Enemy(new Coordinate { X = (WorldWidth / 4), Y = (WorldHeight / 4) }, enemySpeed));
            Enemy.Add(new Enemy(new Coordinate { X = (WorldWidth / 4), Y = ((WorldHeight / 4) * 3) }, enemySpeed));
            Enemy.Add(new Enemy(new Coordinate { X = ((WorldWidth / 4) * 3), Y = ((WorldHeight / 4) * 3) }, enemySpeed));
            Enemy.Add(new Enemy(new Coordinate { X = ((WorldWidth / 4) * 3), Y = (WorldHeight / 4) }, enemySpeed));
            InitializeWorld();
            State = GameState.Play;
        }

        private void InitializeWorld()
        {
            World = new List<List<char>>();

            for (var y = 0; y < WorldHeight; y++)
            {
                World.Add(new List<char>());
                for (var x = 0; x < WorldWidth; x++)
                {
                    World[y].Add(Textures.Path);
                }
            }

            UpdateWorld();
        }

        public void Update(ConsoleKey? keyPress)
        {
            if (State == GameState.Play)
            {
                Player.Update(keyPress, World);

                UpdateEnemies();

                UpdateBullets();

                UpdateWorld();

                if (Enemy.Count == 0) State = GameState.Win;
            }
            else if (State == GameState.Win)
            {
                Console.Clear();
                Console.WriteLine("You Won.");
            }
            else if (State == GameState.Lose)
            {
                Console.Clear();
                Console.WriteLine("You Lose.");
            }
            Fire(keyPress);
        }
        
        public void Draw()
        {
            if (State == GameState.Play)
            {
                //Toggel what is commented to change if you draw individual objects or the world.
                DrawWorld();
                //DrawObjects();
                //Using DrawObjects will severally slow down the processing
                //Todo: Convert DrawObjects to only update gameobjects that have changed
            }
        }

        private void DrawObjects()
        {
            foreach (var obj in Map.GameObjects)
            {
                obj.Draw();
            }

            foreach (var enemy in Enemy)
            {
                enemy.Draw();
            }

            foreach (var bullet in Bullets)
            {
                bullet.Draw();
            }

            Player.Draw();
        }

        private void UpdateWorld()
        {
            foreach (var obj in Map.GameObjects)
            {
                World[obj.Coordinate.Y][obj.Coordinate.X] = obj.Texture;
            }

            foreach (var enemy in Enemy)
            {
                World[enemy.Coordinate.Y][enemy.Coordinate.X] = enemy.Texture;
            }

            World[Player.Coordinate.Y][Player.Coordinate.X] = Player.Texture;

            foreach (var bullet in Bullets)
            {
                World[bullet.Coordinate.Y][bullet.Coordinate.X] = bullet.Texture;
            }
        }

        private void DrawWorld()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var s in World.ToList())
            {
                var line = "";
                foreach (var c in s.ToList())
                {
                    line += c;
                }
                Console.WriteLine(line);
            }
        }

        private void Fire(ConsoleKey? keyPress)
        {
            if (keyPress != null && keyPress == ConsoleKey.Spacebar)
            {
                var bulletCoordinate = new Coordinate
                {
                    X = Player.Coordinate.X,
                    Y = Player.Coordinate.Y
                };
                var bulletDirection = Player.Direction;
                Bullets.Add(new Bullet(bulletDirection, bulletCoordinate, 100));
            }
        }

        private void UpdateEnemies()
        {
            var enemiesToRemove = new List<Enemy>();
            foreach (var enemy in Enemy)
            {
                enemy.Update(World);
                if (enemy.Coordinate.Equals(Player.Coordinate))
                {
                    enemiesToRemove.Add(enemy);
                    //State = GameState.Lose;
                }
            }
            foreach (var enemy in enemiesToRemove)
            {
                Enemy.Remove(enemy);
            }
        }

        private void UpdateBullets()
        {
            var bulletsToRemove = new List<Bullet>();
            foreach (var bullet in Bullets)
            {
                bullet.Update();
                if (World[bullet.Coordinate.Y][bullet.Coordinate.X] == Textures.Wall)
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (var bullet in bulletsToRemove)
            {
                Bullets.Remove(bullet);
            }
        }
    }
}
