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
                    enemy.Health--;
                    if (enemy.Health <= 0)
                    {
                        listOfEnemies.Remove(enemy);
                        numberOfKilledEnemies++;
                        window.Title = $"Killed Enemies: {numberOfKilledEnemies}";
                    }

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
            enemy.Orientation = enemyDirection;
            enemy.Center = enemy.Center + enemyDirection * enemy.Speed * elapsedTime;
        }
    }

    private static void MovePlayer(Player player, float elapsedTime)
    {
        Vector2 newCenter = new Vector2();

        newCenter = player.Center + player.Direction * player.Speed * elapsedTime;
        /*Vector2 direction = camera.Center - player.Center;
        player.Direction = direction;
        player.Direction.Normalize();
        if (direction.Length > 0.05f)
        {
            //player.Center = player.Center + player.Direction * (1 + direction.Length) * player.Speed * elapsedTime;
            newCenter = player.Center + player.Direction * player.Speed * elapsedTime;
        }*/

        if (newCenter.X > 5 || newCenter.X < -5 || newCenter.Y > 5 || newCenter.Y < -5)
        {
            return;
        }
        player.Center = newCenter;

    }

    private static void MoveCamera(Player player, Camera camera)
    {
        Vector2 newCenter = new Vector2();
        newCenter = player.Center;
        if (newCenter.X < 4 && newCenter.X > -4)
        {
            camera.Center.X = newCenter.X;
        }
        if (newCenter.Y < 4 && newCenter.Y > -4)
        {
            camera.Center.Y = newCenter.Y;
        }
    }


    public void update(FrameEventArgs args, int gameState)
    {
        var elapsedTime = (float)args.Time;
        camera.UpdateMatrix(elapsedTime);
        MoveEnemies(listOfEnemies, elapsedTime, player);
        MoveBullets(elapsedTime, listOfBullets);
        MovePlayer(player, elapsedTime);
        MoveCamera(player, camera);
        Collissions(gameWindow, listOfEnemies, listOfBullets, player);
    }
}