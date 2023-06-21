using System;
using System.Collections.Generic;
using Framework;
using OpenTK.Mathematics;

internal class shootingEnemy : Enemy
{
    private float timeSinceShoot = 0;
    private float reloadTime = 3f;
    //private float timeStanding = 0;
    //public List<Bullet> listOfBullets = new List<Bullet>();
    public List<ParticleSystem> listOfBullets = new List<ParticleSystem>();
    public shootingEnemy(Vector2 center) : base(center, 1, 0.2f, 0.3f, new Animation(1, 1, 1, EmbeddedResource.LoadTexture("tempShooter.png"), 0.2f))
    {

    }

    public override void Update(float elapsedTime, Player player)
    {
        timeSinceShoot += elapsedTime;
        foreach (var ps in listOfBullets)
        {
            ps.Update(elapsedTime);
        }
        base.Update(elapsedTime, player);
    }

    public ParticleSystem Shoot(Vector2 playerCenter)
    {
        if (timeSinceShoot > reloadTime)
        {
            timeSinceShoot = 0;
            Vector2 direction = playerCenter - Center;
            direction.Normalize();
            return new ParticleSystem(Center, direction);
        }
        return null;
    }

/*    public Bullet Shoot(Vector2 playerCenter)
    {
        if (timeSinceShoot > reloadTime)
        {
            timeSinceShoot = 0;
            Vector2 direction = playerCenter - Center;
            direction.Normalize();
            return new EnemyBullet(Center, direction);
        }
        return null;
    }*/
}