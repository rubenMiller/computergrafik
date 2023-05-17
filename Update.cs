using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal class Update
{


    GameWindow gameWindow;
    List<Enemy> listOfEnemies;
    int numberOfKilledEnemies;
    List<Bullet> listOfBullets;
    Player player;
    Camera camera;
    public Update(GameWindow window, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        gameWindow = window;
        this.listOfEnemies = listOfEnemies;
        this.listOfBullets = listOfBullets;
        this.player = player;
        this.camera = camera;

    }
    private void Collissions(GameWindow window, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player)
    {
        foreach (Enemy enemy in listOfEnemies.ToList())
        {
            foreach (Bullet bullet in listOfBullets.ToList())
            {
                //checks whether it collides with an enemy
                var deltaX = bullet.Center.X - enemy.Center.X;
                var deltaY = bullet.Center.Y - enemy.Center.Y;

                var distanceSq = deltaX * deltaX + deltaY * deltaY;
                var distance = MathF.Sqrt(distanceSq);
                if (distance < bullet.Radius + enemy.Radius)
                {
                    listOfEnemies.Remove(enemy);
                    numberOfKilledEnemies++;
                    window.Title = $"Killed Enemies: {numberOfKilledEnemies}";
                    listOfBullets.Remove(bullet);
                    break;
                }
            }

            var dX = enemy.Center.X - player.Center.X;
            var dY = enemy.Center.Y - player.Center.Y;
            var distanceToPlayer = MathF.Sqrt(dX * dX + dY * dY);
            if (distanceToPlayer < enemy.Radius + player.Radius)
            {
                listOfEnemies.Remove(enemy);
                player.Health--;
                if (player.Health < 1)
                {
                    window.Close();
                }
            }
        }
        foreach (Bullet bullet in listOfBullets.ToList())
        {
            //checks whether it is far away from player
            var deltaX = bullet.Center.X - player.Center.X;
            var deltaY = bullet.Center.Y - player.Center.Y;
            if (deltaX > 4 || deltaY > 4 || deltaX < -4 || deltaY < -4)
            {
                listOfBullets.Remove(bullet);
                //Console.WriteLine($"Removed bullet, remaining bullets: {listOfBullets.Count}");
            }
        }
    }

    private static void MoveBullets(float elapsedTime, List<Bullet> listOfBullets)
    {
        foreach (Bullet bullet in listOfBullets)
        {
            bullet.Center = bullet.Center + bullet.Direction * bullet.Speed * elapsedTime;
        }
    }

    private static void MoveEnemies(List<Enemy> listOfEnemies, float elapsedTime, Player player)
    {
        foreach (Enemy enemy in listOfEnemies)
        {
            Vector2 enemyDirection = player.Center - enemy.Center;
            enemyDirection.Normalize();
            enemy.Center = enemy.Center + enemyDirection * enemy.Speed * elapsedTime;
        }
    }

    private static void MovePlayer(Player player, Camera camera, float elapsedTime)
    {
        //Console.Write($"player: {player.Center}, camera: {camera.Center}");
        if (player.Center != camera.Center)
        {
            Vector2 direction = camera.Center - player.Center;
            player.Direction = direction;
            player.Direction.Normalize();
            if (direction.Length > 0.05f)
            {
                player.Center = player.Center + player.Direction * (1 + direction.Length) * player.Speed * elapsedTime;
            }
        }
    }

    Random random = new Random();
    private List<Enemy> SpawnEnemies(int numberOfEnemies, Player player)
    {
        List<Enemy> listOfEnemies = new List<Enemy>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomNumber = random.Next(0, 2) == 0 ? -1 : 1;
            Vector2 center = new Vector2((numberOfEnemies / 2) - i + player.Center.X, 2 * randomNumber + player.Center.Y);
            Enemy enemy = new Enemy(center, 0.1f, 0.2f);
            listOfEnemies.Add(enemy);
        }
        return listOfEnemies;
    }

    private float timeSinceLastSpawn = 0;
    private void MakeEnemies(Player player, List<Enemy> listOfEnemies, float elapsedTime)
    {
        timeSinceLastSpawn = timeSinceLastSpawn + elapsedTime;
        if (timeSinceLastSpawn > 2)
        {
            timeSinceLastSpawn = 0;
            List<Enemy> newEnemies = SpawnEnemies(8, player);
            listOfEnemies.AddRange(newEnemies);
        }
    }

    public void update(FrameEventArgs args)
    {
        var elapsedTime = (float)args.Time;
        MakeEnemies(player, listOfEnemies, elapsedTime);
        MoveEnemies(listOfEnemies, elapsedTime, player);
        MoveBullets(elapsedTime, listOfBullets);
        MovePlayer(player, camera, elapsedTime);
        Collissions(gameWindow, listOfEnemies, listOfBullets, player);
    }
}