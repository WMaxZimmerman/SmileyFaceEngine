using SmileyFaceGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameExample
{
    public class TestGame : Game
    {
        private Map Map;
        private Player Player;
        private List<Enemy> Enemy;
        private List<Bullet> Bullets;

        public TestGame(int worldWidth, int worldHeight) : base(worldWidth, worldHeight)
        {

        }
        
        public override void Init(int worldWidth, int worldHeight)
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
            State = GameStates.Play;
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

        public override void UpdateLose(ConsoleKey? keyPress)
        {
            throw new NotImplementedException();
        }

        public override void UpdateMenu(ConsoleKey? keyPress)
        {
            throw new NotImplementedException();
        }

        public override void UpdatePause(ConsoleKey? keyPress)
        {
            throw new NotImplementedException();
        }

        #region Play

        public override void UpdatePlay(ConsoleKey? keyPress)
        {
            Player.Update(keyPress, World);

            UpdateEnemies();

            UpdateBullets();

            Fire(keyPress);

            UpdateWorld();

            if (Enemy.Count == 0) State = GameStates.Win;
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

        #endregion

        public override void UpdateWin(ConsoleKey? keyPress)
        {
            
        }

        public override void DrawLose()
        {
            Console.Clear();
            Console.WriteLine("You Lose.");
        }

        public override void DrawMenu()
        {
            throw new NotImplementedException();
        }

        public override void DrawPause()
        {
            throw new NotImplementedException();
        }

        public override void DrawPlay()
        {
            DrawWorld();
        }

        public override void DrawWin()
        {
            Console.Clear();
            Console.WriteLine("You Won.");
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


    }
}
