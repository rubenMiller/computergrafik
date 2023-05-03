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

            var distanceToPlayer = MathF.Sqrt(enemy.Center.X * enemy.Center.X + enemy.Center.Y * enemy.Center.Y);
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

    private static void MoveEnemies(List<Enemy> listOfEnemies, float elapsedTime)
    {
        foreach (Enemy enemy in listOfEnemies)
        {
            Vector2 enemyDirection = enemy.Center * -1;
            enemyDirection.Normalize();
            enemy.Center = enemy.Center + enemyDirection * enemy.Speed * elapsedTime;
        }
    }

    public Update(FrameEventArgs args, GameWindow window, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        var elapsedTime = (float)args.Time;
        MoveEnemies(listOfEnemies, elapsedTime);
        MoveBullets(elapsedTime, listOfBullets);
        Collissions(window, listOfEnemies, listOfBullets, player);
        camera.Move(window.KeyboardState, elapsedTime);
        camera.UpdateCameraMatrix();
    }
}