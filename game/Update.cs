using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System.Linq;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal class Update
{
    int numberOfKilledEnemies;
    public Update()
    {
    }
    private int Collissions(List<Enemy> listOfEnemies, List<Bullet> listOfEnemyBullets, Player player, List<Bullet> listOfPlayerBullets, GameState gameState)
    {
        foreach (Enemy enemy in listOfEnemies.ToList())
        {
            foreach (Bullet bullet in listOfPlayerBullets.ToList())
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

                    listOfPlayerBullets.Remove(bullet);
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
                    gameState.State = GameState.STATE.STATE_DEAD;
                    return 2;
                }
            }
        }
        foreach (Bullet bullet in listOfPlayerBullets.ToList())
        {
            //checks whether it is far away from player
            var deltaX = bullet.Center.X - player.Center.X;
            var deltaY = bullet.Center.Y - player.Center.Y;
            if (deltaX > player.weapon.Range || deltaY > player.weapon.Range || deltaX < -player.weapon.Range || deltaY < -player.weapon.Range)
            {
                listOfPlayerBullets.Remove(bullet);
            }
        }
        foreach (Bullet bullet in listOfEnemyBullets.ToList())
        {
            var deltaX = bullet.Center.X - player.Center.X;
            var deltaY = bullet.Center.Y - player.Center.Y;
            var distanceSq = deltaX * deltaX + deltaY * deltaY;
            var distance = MathF.Sqrt(distanceSq);
            if (distance < bullet.Radius + player.Radius)
            {
                listOfEnemyBullets.Remove(bullet);
                player.Health--;
                if (player.Health < 1)
                {
                    gameState.State = GameState.STATE.STATE_DEAD;
                    return 2;
                }
            }
        }
        return 1;
    }


    private static void MoveCamera(Player player, Camera camera, GameBorder gameBorder)
    {
        Vector2 newCenter = new Vector2();
        newCenter = player.Center;
        //Console.WriteLine($"ratio: {camera.cameraAspectRatio}, stop: {-1 / camera.cameraAspectRatio + 4}, camera.X: {camera.Center.X}, player.Center.X {player.Center.X}");
        if (newCenter.X < -1 / camera.cameraAspectRatio + gameBorder.MaxX && newCenter.X > 1 / camera.cameraAspectRatio + gameBorder.MinX)
        {
            camera.Center.X = newCenter.X;
        }
        if (newCenter.Y < gameBorder.MaxY - 1 && newCenter.Y > gameBorder.MinY + 1)
        {
            camera.Center.Y = newCenter.Y;
        }
    }


    public void update(FrameEventArgs args, GameWindow window, GameState gameState, List<Enemy> listOfEnemies, List<Bullet> listOfEnemyBullets, Camera camera, Player player, Wave wave, GameBorder gameBorder)
    {
        switch (gameState.State)
        {
            case GameState.STATE.STATE_PLAYING:
                {
                    var elapsedTime = (float)args.Time;
                    wave.Update(elapsedTime, player, listOfEnemies, gameBorder);
                    camera.UpdateMatrix(elapsedTime);
                    player.Update(elapsedTime, window, camera, gameBorder);

                    foreach (Enemy enemy in listOfEnemies)
                    {
                        enemy.Update(elapsedTime, player);
                        if (enemy is shootingEnemy se)
                        {
                            var bullet = se.Shoot(player.Center);
                            if (bullet != null) { listOfEnemyBullets.Add(bullet); }
                        }
                    }
                    foreach (Bullet bullet in listOfEnemyBullets)
                    {
                        bullet.Update(elapsedTime);
                    }


                    MoveCamera(player, camera, gameBorder);
                    Collissions(listOfEnemies, listOfEnemyBullets, player, player.listOfBullets, gameState);

                    if (listOfEnemies.Count == 0 && wave.readyForNewWave == false)
                    {
                        wave.readyForNewWave = true;
                        wave.waveTime = 0f;
                    }
                    break;
                }
            default:
                return;
        }
    }
}