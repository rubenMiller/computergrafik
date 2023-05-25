using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

internal class shootingEnemy : Enemy
{
    private float timeSinceShoot = 0;
    private float reloadTime = 3f;
    //private float timeStanding = 0;
    public List<Bullet> listOfBullets = new List<Bullet>();
    public shootingEnemy(Vector2 center) : base(center, 1, 0.2f, 0.3f)
    {

    }

    public override void Update(float elapsedTime, Player player)
    {
        timeSinceShoot += elapsedTime;
        foreach (Bullet bullet in listOfBullets)
        {
            bullet.Update(elapsedTime);
        }
        base.Update(elapsedTime, player);
    }

    public Bullet Shoot(Vector2 playerCenter)
    {
        if (timeSinceShoot > reloadTime)
        {
            timeSinceShoot = 0;
            Vector2 direction = playerCenter - Center;
            direction.Normalize();
            return new EnemyBullet(Center, direction);
        }
        return null;
    }
}