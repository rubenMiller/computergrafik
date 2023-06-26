using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System.Linq;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal class Update
{
    public Update()
    {
    }
    private int Collissions(List<Enemy> listOfEnemies, List<ParticleSystem> listOfEnemyBullets, Player player, List<Bullet> listOfPlayerBullets, List<BloodSplash> listOfBloodSplashes, GameState gameState, Wave wave)
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
                    BloodSplash newBloodSplash = new BloodSplash(bullet.Center, bullet.Direction);
                    listOfBloodSplashes.Add(newBloodSplash);
                    enemy.Health--;
                    if (enemy.Health <= 0)
                    {
                        listOfEnemies.Remove(enemy);
                        wave.RemainingEnemies--;
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
                wave.RemainingEnemies--;
                listOfEnemies.Remove(enemy);
                player.Health--;
                if (player.Health < 1)
                {
                    gameState.transitionToState(GameState.STATE.STATE_DEAD);
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
        foreach (var bullet in listOfEnemyBullets.ToList())
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
                    gameState.transitionToState(GameState.STATE.STATE_DEAD);
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

    private void UpdateBloodSplasList(List<BloodSplash> listOfBloodSplashes, float elapsedTime)
    {
        foreach (BloodSplash bloodSplash in listOfBloodSplashes.ToList())
        {
            bloodSplash.Update(elapsedTime);
            if (bloodSplash.TimeAlive > bloodSplash.TimeToLive)
            {
                listOfBloodSplashes.Remove(bloodSplash);
            }
        }
    }


    public void update(FrameEventArgs args, GameWindow window, GameState gameState, List<Enemy> listOfEnemies, List<ParticleSystem> listOfEnemyBullets, List<BloodSplash> listOfBloodSplashes, Camera camera, Player player, Wave wave, GameBorder gameBorder, UpgradeMenu upgradeMenu)
    {
        switch (gameState.CurrentState)
        {
            case GameState.STATE.STATE_PLAYING:
                {
                    var elapsedTime = (float)args.Time;
                    wave.Update(elapsedTime, player, listOfEnemies, gameBorder, gameState);
                    player.Update(elapsedTime, window, camera, gameBorder);
                    MoveCamera(player, camera, gameBorder);
                    camera.UpdateMatrix(elapsedTime);

                    foreach (Enemy enemy in listOfEnemies)
                    {
                        enemy.Update(elapsedTime, player);
                        if (enemy is shootingEnemy se)
                        {
                            var bullet = se.Shoot(player.Center);
                            if (bullet != null) { listOfEnemyBullets.Add(bullet); }
                            foreach (var ps in se.listOfBullets)
                            {
                                ps.Update(elapsedTime);
                            }
                        }
                    }
                    foreach (var bullet in listOfEnemyBullets)
                    {
                        bullet.Update(elapsedTime);
                    }



                    Collissions(listOfEnemies, listOfEnemyBullets, player, player.listOfBullets, listOfBloodSplashes, gameState, wave);
                    UpdateBloodSplasList(listOfBloodSplashes, elapsedTime);

                    if (listOfEnemies.Count == 0 && wave.readyForNewWave == false)
                    {
                        wave.readyForNewWave = true;
                        wave.waveTime = 0f;
                    }
                    break;
                }
            case GameState.STATE.STATE_WAVEOVER:
                {
                    var elapsedTime = (float)args.Time;
                    camera.Center = new Vector2(0, 0);
                    camera.Direction = new Vector2(0, 0);
                    player.Center = new Vector2(0, 0);
                    camera.UpdateMatrix(elapsedTime);
                    player.Update(elapsedTime, window, camera, gameBorder);
                    upgradeMenu.upgradesPossible = 1;
                    break;
                }

            case GameState.STATE.STATE_UPGRADEMENU:
                {
                    upgradeMenu.Update(player, window, camera, window.MouseState, gameState);
                    player.Health = player.maxHealth;
                    break;
                }
        }
    }
}