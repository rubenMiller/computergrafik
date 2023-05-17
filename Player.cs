using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Zenseless.OpenTK;

internal class Player
{

    public void shootBullet(GameWindow window, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        var pixelMousePosition = window.MousePosition;
        var posX = (pixelMousePosition.X * 2f / window.Size.X) - 1;
        var posY = (pixelMousePosition.Y * -2f / window.Size.Y) + 1;
        Vector2 mousePosition = new Vector2(posX, posY);
        var transformedPosition = mousePosition.Transform(camera.CameraMatrix.Inverted());
        var direction = transformedPosition - player.Center;
        direction.Normalize();

        Orientation = direction;
        var rotation = Rotate(new Vector2(0.07f, -0.05f), direction.PolarAngle());
        Vector2 bulletStart = new Vector2(player.Center.X + rotation.X, player.Center.Y + rotation.Y);
        Bullet bullet = new Bullet(bulletStart, direction);
        listOfBullets.Add(bullet);
    }

    public static Vector2 Rotate(Vector2 vector, float angle)
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
    }


    public Vector2 Center = new Vector2(0, 0);
    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 1f;
    public float Radius;
    public int Health;
    public Vector2 Orientation = new Vector2(0, 0);

    public Player(float radius, int health)
    {
        Radius = radius;
        Health = health;
    }
}