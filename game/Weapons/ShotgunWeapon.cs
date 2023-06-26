using System;
using System.Collections.Generic;
using Framework;
using OpenTK.Mathematics;

public class ShotgunWeapon : Weapon
{
    public int AdditionalBulletsPerSide = 2;
    float SpreadingFactor = 0.125f;
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
        var rotation = Rotate(direction, 0f); ;
        for (int i = 1; i <= AdditionalBulletsPerSide; i++)
        {
            rotation = Rotate(direction, i * SpreadingFactor);
            newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));
            rotation = Rotate(direction, -i * SpreadingFactor);
            newList.Add(new PlayerBullet(startingPoint, rotation, BulletRadius, BulletSpeed, Range));
        }


        return newList;
    }

    public ShotgunWeapon() : base(0.04f, 4f, 1f, 0.5f, new Vector2(0.10f, -0.055f), new Animation(5, 4, 3f, EmbeddedResource.LoadTexture("shotgun-move-sheet.png"), 0.15f, 0.65f))
    {

    }
}