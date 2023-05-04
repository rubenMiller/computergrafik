using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal class Update
{
    private static void Collissions(GameWindow window, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player)
    {
        foreach (Enemy enemy in listOfEnemies.ToList())
        {
            foreach (Bullet bullet in listOfBullets.ToList())
            {
                var deltaX = bullet.Center.X - enemy.Center.X;
                var deltaY = bullet.Center.Y - enemy.Center.Y;

                var distanceSq = deltaX * deltaX + deltaY * deltaY;
                var distance = MathF.Sqrt(distanceSq);
                if (distance < bullet.Radius + enemy.Radius)
                {
                    listOfEnemies.Remove(enemy);
                    listOfBullets.Remove(bullet);
                    break;
                }
            }

            var dX = enemy.Center.X - player.Center.X;
            var dY = enemy.Center.Y - player.Center.Y;
            var distanceToPlayer = MathF.Sqrt(dX * dX + dY * dY);
            if (distanceToPlayer < enemy.Radius + player.Radius)
            {
                window.Close();
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
        Console.Write($"player: {player.Center}, camera: {camera.Center}");
        if (player.Center != camera.Center)
        {
            Vector2 direction = camera.Center - player.Center;
            direction.Normalize();
            player.Direction = direction;
            player.Center = player.Center + player.Direction * player.Speed * elapsedTime;
        }
        //player.Direction = camera.Movement;
        //player.Center = player.Center + player.Direction;
    }

    public Update(FrameEventArgs args, GameWindow window, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        var elapsedTime = (float)args.Time;
        MoveEnemies(listOfEnemies, elapsedTime, player);
        MoveBullets(elapsedTime, listOfBullets);
        MovePlayer(player, camera, elapsedTime);
        Collissions(window, listOfEnemies, listOfBullets, player);
    }
}