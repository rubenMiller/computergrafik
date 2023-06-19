using System.Collections.Generic;
using OpenTK.Mathematics;
using Zenseless.OpenTK;
using Framework;

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
    public Animation Animation;
    public Weapon(float bulletRadius, float bulletSpeed, float range, float reloadTime, Animation animation)
    {
        BulletRadius = bulletRadius;
        BulletSpeed = bulletSpeed;
        Range = range;
        ReloadTime = reloadTime;
        Animation = animation;
    }
}