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
    public Update()
    {
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
            }
        }
        return 1;
    }



    private static void MovePlayer(Player player, float elapsedTime)
    {
        Vector2 newCenter = new Vector2();

        newCenter = player.Center + player.Direction * player.Speed * elapsedTime;
        if (newCenter.X < 5 - player.Radius && newCenter.X > -5 + player.Radius)
        {
            player.Center.X = newCenter.X;
        }
        if (newCenter.Y < 5 - player.Radius && newCenter.Y > -5 + player.Radius)
        {
            player.Center.Y = newCenter.Y;
        }
    }

    private static void MoveCamera(Player player, Camera camera)
    {
        Vector2 newCenter = new Vector2();
        newCenter = player.Center;
        //Console.WriteLine($"ratio: {camera.cameraAspectRatio}, stop: {-1 / camera.cameraAspectRatio + 4}, camera.X: {camera.Center.X}, player.Center.X {player.Center.X}");
        if (newCenter.X < -1 / camera.cameraAspectRatio + 5 && newCenter.X > 1 / camera.cameraAspectRatio - 5)
        {
            camera.Center.X = newCenter.X;
        }
        if (newCenter.Y < 4 && newCenter.Y > -4)
        {
            camera.Center.Y = newCenter.Y;
        }
    }


    public int update(FrameEventArgs args, int gameState, List<Enemy> listOfEnemies, Camera camera, Player player, Wave wave)
    {
        if (gameState == 1)
        {
            var elapsedTime = (float)args.Time;
            wave.Update(elapsedTime, player, listOfEnemies);
            player.timeSinceLastShot = player.timeSinceLastShot + elapsedTime;
            camera.UpdateMatrix(elapsedTime);
            foreach (Enemy enemy in listOfEnemies)
            {
                enemy.Update(elapsedTime, player);
            }
            foreach (Bullet bullet in player.listOfBullets)
            {
                bullet.Update(elapsedTime);
            }
            MovePlayer(player, elapsedTime);
            MoveCamera(player, camera);
            gameState = Collissions(listOfEnemies, player.listOfBullets, player, gameState);

            if (listOfEnemies.Count == 0 && wave.readyForNewWave == false)
            {
                wave.readyForNewWave = true;
                wave.waveTime = 0f;
            }
        }

        return gameState;
    }
}