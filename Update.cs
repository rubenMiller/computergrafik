using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal class Update
{
    int numberOfKilledEnemies;
    EnemySpawner enemySpawner;
    public Update(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
    }
    private int Collissions(List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, int gameState)
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
                    gameState = 2;
                    return 2;
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
        return 1;
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

        if (newCenter.X > 5 - player.Radius || newCenter.X < -5 + player.Radius || newCenter.Y > 5 - player.Radius || newCenter.Y < -5 +dd player.Radius)
        {
            return;
        }
        player.Center = newCenter;

    }

    private static void MoveCamera(Player player, Camera camera)
    {
        Vector2 newCenter = new Vector2();
        newCenter = player.Center;
        Console.WriteLine(4 - camera.cameraAspectRatio / 2f);
        if (newCenter.X < 4.1f - camera.cameraAspectRatio / 2f && newCenter.X > -4.1f + camera.cameraAspectRatio / 2f)
        {
            camera.Center.X = newCenter.X;
        }
        if (newCenter.Y < 4 && newCenter.Y > -4)
        {
            camera.Center.Y = newCenter.Y;
        }
    }


    public int update(FrameEventArgs args, int gameState, List<Enemy> listOfEnemies, Camera camera, Player player, List<Bullet> listOfBullets, Wave wave)
    {
        if (gameState == 1)
        {
            var elapsedTime = (float)args.Time;
            wave.timePlayed = wave.timePlayed + elapsedTime;
            camera.UpdateMatrix(elapsedTime);
            MoveEnemies(listOfEnemies, elapsedTime, player);
            MoveBullets(elapsedTime, listOfBullets);
            MovePlayer(player, elapsedTime);
            MoveCamera(player, camera);
            gameState = Collissions(listOfEnemies, listOfBullets, player, gameState);

            if (listOfEnemies.Count == 0 && wave.readyForNewWave == false)
            {
                wave.readyForNewWave = true;
                wave.timePlayed = 0f;
            }
        }

        return gameState;
    }
}