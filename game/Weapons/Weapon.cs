using System.Collections.Generic;
using OpenTK.Mathematics;

public class Weapon
{
    public virtual List<Bullet> shoot(Vector2 startingPoint, Vector2 direction)
    {
        List<Bullet> newList = new List<Bullet>();
        newList.Add(new PlayerBullet(startingPoint, direction, BulletRadius, BulletSpeed, Range));
        return newList;
    }
    internal float BulletRadius;
    internal float BulletSpeed;
    internal float Range;
    internal float ReloadTime;
    public Weapon(float bulletRadius, float bulletSpeed, float range, float reloadTime)
    {
        BulletRadius = bulletRadius;
        BulletSpeed = bulletSpeed;
        Range = range;
        ReloadTime = reloadTime;
    }
}