using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Framework;
using Zenseless.OpenTK;

internal class Draw
{

    private readonly Texture2D texBackground;
    private readonly Texture2D texPlayer;
    private readonly Texture2D texZombie;
    private readonly Texture2D texBullet;
    public Draw()
    {
        texBackground = EmbeddedResource.LoadTexture("Cartoon_green_texture_grass.jpg");
        GL.BindTexture(TextureTarget.Texture2D, texBackground.Handle);
        texBackground.Function = TextureFunction.Repeat;
        //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        texPlayer = EmbeddedResource.LoadTexture("survivor-idle_rifle_0.png");
        texZombie = EmbeddedResource.LoadTexture("zombie.png");
        texBullet = EmbeddedResource.LoadTexture("bullet.png");

        GL.Enable(EnableCap.Texture2D);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);

    }

    public void draw(List<Enemy> listOfEnemies, List<Bullet> listOfBullets, Player player, Camera camera)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //draw a quad
        camera.SetMatrix();
        DrawBackground();
        DrawPlayer(player, camera);
        DrawEnemies(listOfEnemies, camera);
        DrawBullets(listOfBullets, camera);
        //DrawGrid();
    }
    private static List<Vector2> CreateCirclePoints()
    {
        List<Vector2> pointList = new List<Vector2>();

        int corners = 32;
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

    private void DrawBullets(List<Bullet> listOfBullets, Camera camera)
    {
        foreach (Bullet bullet in listOfBullets)
        {

            var cam = camera.CameraMatrix;

            cam = Transformation2d.Combine(Transformation2d.Rotation(bullet.Direction.PolarAngle()), Transformation2d.Translate(bullet.Center), cam);
            GL.LoadMatrix(ref cam);


            GL.Color4(1f, 1f, 1f, 1f);
            GL.BindTexture(TextureTarget.Texture2D, texBullet.Handle);
            var bulletBox = new Box2(-bullet.Radius, -bullet.Radius, bullet.Radius, bullet.Radius);
            DrawRect(bulletBox, new Box2(0f, 0f, 1f, 1f));

            cam = camera.CameraMatrix;
            GL.LoadMatrix(ref cam);
        }
    }

    private void DrawEnemies(List<Enemy> listOfEnemies, Camera camera)
    {
        foreach (Enemy enemy in listOfEnemies)
        {
            GL.Color4(0f, 0f, 0f, 0.25f);
            DrawCircle(enemy.Center, enemy.Radius, 1f);

            var cam = camera.CameraMatrix;

            cam = Transformation2d.Combine(Transformation2d.Rotation(enemy.Orientation.PolarAngle()), Transformation2d.Translate(enemy.Center), cam);
            GL.LoadMatrix(ref cam);


            GL.Color4(1f, 1f, 1f, 1f);
            GL.BindTexture(TextureTarget.Texture2D, texZombie.Handle);
            var enemyBox = new Box2(-enemy.Radius, -enemy.Radius, enemy.Radius, enemy.Radius);
            DrawRect(enemyBox, new Box2(0f, 0f, 1f, 1f));

            cam = camera.CameraMatrix;
            GL.LoadMatrix(ref cam);
        }
    }

    private static void DrawCircle(Vector2 center, float radius, float fraction)
    {

        GL.Begin(PrimitiveType.TriangleFan);
        GL.Vertex2(center);
        var circle = CreateCirclePoints();
        for (int i = 0; i < circle.Count; i++)
        {
            float current = i / (float)circle.Count;
            if (current > fraction)
            {
                GL.End();
                return;
            }
            GL.Vertex2(center + radius * circle[i]);
        }
        GL.Vertex2(center + radius * circle[0]);
        GL.End();
    }

    private static void DrawRectangle(Vector2 center, float width, float height)
    {
        GL.Begin(PrimitiveType.Quads);
        GL.Vertex2(center.X, center.Y);
        GL.Vertex2(center.X + width, center.Y);
        GL.Vertex2(center.X + width, center.Y + height);
        GL.Vertex2(center.X, center.Y + height);
        GL.End();
    }


    private void DrawPlayer(Player player, Camera camera)
    {
        GL.Color4(0f, 0f, 0f, 0.25f);
        DrawCircle(player.Center, player.Radius, 1f);

        var cam = camera.CameraMatrix;

        cam = Transformation2d.Combine(Transformation2d.Rotation(player.Orientation.PolarAngle()), Transformation2d.Translate(player.Center), cam);
        GL.LoadMatrix(ref cam);


        GL.Color4(1f, 1f, 1f, 1f);
        GL.BindTexture(TextureTarget.Texture2D, texPlayer.Handle);
        var playerBox = new Box2(-player.Radius, -player.Radius, player.Radius, player.Radius);
        DrawRect(playerBox, new Box2(0f, 0f, 1f, 1f));

        cam = camera.CameraMatrix;
        GL.LoadMatrix(ref cam);

        GL.Color4(Color4.Red);
        Vector2 offsetCenter = new Vector2(player.Center.X - 0.1f, player.Center.Y + 0.1f);
        DrawRectangle(offsetCenter, 0.05f * player.Health, 0.03f);

    }



    private void DrawBackground()
    {
        //GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
        GL.BindTexture(TextureTarget.Texture2D, texBackground.Handle);

        static Box2 SizedBox(float minX, float minY, float sizeX, float sizeY) => new(minX, minY, minX + sizeX, minY + sizeY);

        GL.Color4(1f, 1f, 1f, 1f);
        var rect = new Box2(-10f, -10f, 10f, 10f);
        DrawRect(rect, SizedBox(0, 0f, 10f, 10f));
    }

    private static void DrawRect(Box2 rectangle, Box2 texCoords)
    {
        GL.Begin(PrimitiveType.Quads);
        GL.TexCoord2(texCoords.Min);
        GL.Vertex2(rectangle.Min);
        GL.TexCoord2(texCoords.Max.X, texCoords.Min.Y);
        GL.Vertex2(rectangle.Max.X, rectangle.Min.Y);
        GL.TexCoord2(texCoords.Max);
        GL.Vertex2(rectangle.Max);
        GL.TexCoord2(texCoords.Min.X, texCoords.Max.Y);
        GL.Vertex2(rectangle.Min.X, rectangle.Max.Y);
        GL.End();
        GL.BindTexture(TextureTarget.Texture2D, 0);
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


}