using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

internal class Draw
{
    private static List<Vector2> CreateCirclePoints()
    {
        List<Vector2> pointList = new List<Vector2>();

        int corners = 25;
        float delta = 2f * MathF.PI / corners;
        for (int i = 0; i < corners; ++i)
        {
            var alpha = i * delta;

            var x = MathF.Cos(alpha);
            var y = MathF.Sin(alpha);

            var pointOnCircle = new Vector2(x, y);
            pointList.Add(pointOnCircle);
        }
        return pointList;
    }

    private static void DrawBullets(List<Bullet> listOfBullets)
    {
        foreach (Bullet bullet in listOfBullets)
        {
            GL.Color4(Color4.BlueViolet);
            DrawCircle(bullet.Center, bullet.Radius);
        }
    }

    private static void DrawEnemies(List<Enemy> listOfEnemies)
    {
        foreach (Enemy enemy in listOfEnemies)
        {
            GL.Color4(Color4.Red);
            DrawCircle(enemy.Center, enemy.Radius);
        }
    }

    private static void DrawCircle(Vector2 center, float radius)
    {

        GL.Begin(PrimitiveType.TriangleFan);
        GL.Vertex2(center);
        var circle = CreateCirclePoints();
        foreach (var point in circle)
        {
            GL.Vertex2(center + radius * point);
        }
        GL.Vertex2(center + radius * circle[0]);
        GL.End();
    }

    private static void DrawPlayer(Player player)
    {
        GL.Color4(Color4.BlanchedAlmond);
        DrawCircle(player.Center, player.Radius);
    }

    private static void DrawGrid()
    {
        GL.Begin(PrimitiveType.Lines);
        for (int i = -10; i <= 10; i++)
        {
            GL.Vertex2(i, -10);
            GL.Vertex2(i, 10);
            GL.Vertex2(-10, i);
            GL.Vertex2(10, i);
        }
        GL.End();
    }

    public Draw(FrameEventArgs args, List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        //clear screen - what happens without?
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //draw a quad
        camera.SetMatrix();
        DrawPlayer(player);
        DrawEnemies(listOfEnemies);
        DrawBullets(listOfBullets);
        DrawGrid();

    }
}