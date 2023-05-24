using System;
using OpenTK.Mathematics;

internal class shootingEnemy : Enemy
{
    public float timeSinceShoot = 0;
    public float reloadTime = 3f;
    public float timeStanding = 0;
    public shootingEnemy(Vector2 center) : base(center, 1, 0.2f, 0.3f)
    {

    }

    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
    public void Shoot()
    {
        Console.Write("SHooting Enemy would have shot");
    }
}