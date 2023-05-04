using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

internal class Player
{

    public void shootBullet(GameWindow window, List<Bullet> listOfBullets, Player player)
    {
        var mousePosition = window.MousePosition;
        var posX = (mousePosition.X * 2f / window.Size.X) - 1;
        var posY = (mousePosition.Y * -2f / window.Size.Y) + 1;
        Console.WriteLine($"mouseX: {posX}, mouseY: {posY}");
        Vector2 direction = new Vector2(posX, posY);
        direction.Normalize();
        Bullet bullet = new Bullet(player.Center, direction);
        listOfBullets.Add(bullet);
    }


    internal float shootingTimer;
    internal float shootingInterval = 0.25f;
    public Vector2 Center = new Vector2(0, 0);
    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 0.8f;
    public float Radius;

    public Player(float radius)
    {
        Radius = radius;
    }
}