using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using Zenseless.Patterns;
using static Zenseless.OpenTK.Transformation2d;

internal class Program
{
    private static void Main(string[] args)
    {
        GameWindow window = new(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

        float x = 0f;
        List<Enemy> listOfEnemies = MakeEnemies(6);
        List<Bullet> listOfBullets = new List<Bullet>();
        window.UpdateFrame += Update;
        window.Resize += Resize;
        window.KeyDown += args => { if (Keys.Escape == args.Key) window.Close(); };
        window.RenderFrame += Draw; // called once each frame; callback should contain drawing code
        window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering
        //window.MouseDown += _ => shootBullet();

        // setup code executed once
        GL.ClearColor(Color4.LightGray);

        window.Run();

        List<Enemy> MakeEnemies(int numberOfEnemies)
        {
            List<Enemy> listOfEnemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 center = new Vector2((numberOfEnemies / 2) - i, 2);
                Enemy enemy = new Enemy(center, 0.1f, 0.1f);
                listOfEnemies.Add(enemy);
            }
            return listOfEnemies;
        }

        void Draw(FrameEventArgs args)
        {
            //clear screen - what happens without?
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //draw a quad
            DrawPlayer();
            DrawEnemies(listOfEnemies);
        }

        List<Vector2> CreateCirclePoints()
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

        void MoveEnemies(List<Enemy> listOfEnemies, float elapsedTime)
        {
            foreach (Enemy enemy in listOfEnemies)
            {
                Vector2 enemyDirection = enemy.Center * -1;
                enemyDirection.Normalize();
                enemy.Center = enemy.Center + enemyDirection * enemy.Speed * elapsedTime;
            }
        }

        void shootBullet()
        {
            var mousePosition = window.MousePosition;
            var posX = (2f / window.Size.X) - 1; var posY = (-2f / window.Size.Y) + 1;
            Console.WriteLine($"mouseX: {posX}, mouseY: {posY}");
        }

        Matrix4 InvViewPortMatrix = new Matrix4();

        void Resize(ResizeEventArgs args)
        {
            GL.Viewport(0, 0, args.Width, args.Height);
            Matrix4 matrix = Translate(-1f, 1f);
            Matrix4 matrix2 = Scale(2f / (args.Width - 1), -2f / (args.Height - 1f));
            InvViewPortMatrix = matrix * matrix2;
        }

        void Update(FrameEventArgs args)
        {
            var elapsedTime = (float)args.Time;
            MoveEnemies(listOfEnemies, elapsedTime);
        }
    }
}

internal class Enemy
{
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Enemy(Vector2 center, float radius, float speed)
    {
        Center = center;
        Radius = radius;
        Speed = speed;
    }
}

internal class Bullet
{
    public Vector2 Center;
    public Vector2 Direction;
    public float Radius = 0.1f;
    public float Speed = 0.4f;

    public Bullet(Vector2 center, Vector2 direction)
    {
        Center = center;
        Direction = direction;
    }
}