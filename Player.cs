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
        Bullet bullet = new Bullet(player.Center, direction);
        listOfBullets.Add(bullet);
        //Console.WriteLine($"Number of Bullets: {listOfBullets.Count}");
    }


    internal float shootingTimer;
    internal float shootingInterval = 0.25f;
    public Vector2 Center = new Vector2(0, 0);
    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 1f;
    public float Radius;

    public Player(float radius)
    {
        Radius = radius;
    }
}