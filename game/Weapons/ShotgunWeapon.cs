using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

public class ShotgunWeapon : Weapon
{
    public static Vector2 Rotate(Vector2 vector, float angle)
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
    }
    public override List<Bullet> shoot(Vector2 startingPoint, Vector2 direction)
    {
        List<Bullet> newList = new List<Bullet>();
        newList.Add(new PlayerBullet(startingPoint, direction, BulletRadius, BulletSpeed, Range));
        var rotation = Rotate(direction, 0.5f);
        newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));
        rotation = Rotate(direction, 0.25f);
        newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));
        rotation = Rotate(direction, -0.5f);
        newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));
        rotation = Rotate(direction, -0.25f);
        newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));

        return newList;
    }

    public ShotgunWeapon() : base(0.04f, 4f, 1f, 0.5f)
    {

    }
}