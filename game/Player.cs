using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Zenseless.OpenTK;

internal class Player
{
    public void shootBullet(GameWindow window, Camera camera, MouseState mouseState)
    {
        if (mouseState.IsButtonDown(MouseButton.Left) && timeSinceLastShot > reloadTime)
        {
            timeSinceLastShot = 0;
            var pixelMousePosition = window.MousePosition;
            var posX = (pixelMousePosition.X * 2f / window.Size.X) - 1;
            var posY = (pixelMousePosition.Y * -2f / window.Size.Y) + 1;
            Vector2 mousePosition = new Vector2(posX, posY);
            var transformedPosition = mousePosition.Transform(camera.CameraMatrix.Inverted());
            var direction = transformedPosition - Center;
            direction.Normalize();

            Orientation = direction;
            var rotation = Rotate(new Vector2(0.07f, -0.05f), direction.PolarAngle());
            Vector2 bulletStart = new Vector2(Center.X + rotation.X, Center.Y + rotation.Y);
            Bullet bullet = new PlayerBullet(bulletStart, direction);

            listOfBullets.Add(bullet);
        }
    }

    public static Vector2 Rotate(Vector2 vector, float angle)
    {
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
    }

    public void movePlayer(KeyboardState keyBoardState)
    {
        int movX = 0;
        int movY = 0;
        if (keyBoardState.IsKeyDown(Keys.W))
        {
            movX = 1;
        }
        else if (keyBoardState.IsKeyDown(Keys.S))
        {
            movX = -1;
        }
        else
        {
            movX = 0;
        }
        if (keyBoardState.IsKeyDown(Keys.D))
        {
            movY = 1;
        }
        else if (keyBoardState.IsKeyDown(Keys.A))
        {
            movY = -1;
        }
        else
        {
            movY = 0;
        }

        Vector2 direction = new Vector2(movY, movX);
        if (direction.Length == 0)
        {
            Direction = new Vector2(0, 0);
            return;
        }
        direction.Normalize();
        Direction = direction;
    }

    public Vector2 Center = new Vector2(0, 0);
    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 1f;
    public float Radius;
    public int Health;
    public Vector2 Orientation = new Vector2(0, 0);
    public float timeSinceLastShot = 0f;
    private float reloadTime = 0.2f;
    public List<Bullet> listOfBullets = new List<Bullet>();

    public Player(float radius, int health)
    {
        Radius = radius;
        Health = health;
    }
}