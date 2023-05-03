using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Player player = new Player(0.1f);
        window.UpdateFrame += Update;
        window.Resize += Resize;
        window.KeyDown += args => { if (Keys.Escape == args.Key) window.Close(); };
        window.RenderFrame += Draw; // called once each frame; callback should contain drawing code
        window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering
        window.MouseDown += _ => shootBullet();

        // setup code executed once
        GL.ClearColor(Color4.LightGray);

        window.Run();

        List<Enemy> MakeEnemies(int numberOfEnemies)
        {
            List<Enemy> listOfEnemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 center = new Vector2((numberOfEnemies / 2) - i, 2);
                Enemy enemy = new Enemy(center, 0.1f, 0.2f);
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
            DrawBullets();
        }


        void DrawPlayer()
        {
            GL.Color4(Color4.BlanchedAlmond);
            DrawCircle(new Vector2(0, 0), player.Radius);
        }

        void DrawCircle(Vector2 center, float radius)
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

        void DrawEnemies(List<Enemy> listOfEnemies)
        {
            foreach (Enemy enemy in listOfEnemies)
            {
                GL.Color4(Color4.Red);
                DrawCircle(enemy.Center, enemy.Radius);
            }
        }

        void DrawBullets()
        {
            foreach (Bullet bullet in listOfBullets)
            {
                GL.Color4(Color4.BlueViolet);
                DrawCircle(bullet.Center, bullet.Radius);
            }
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

        void MoveBullets(float elapsedTime)
        {
            foreach (Bullet bullet in listOfBullets)
            {
                bullet.Center = bullet.Center + bullet.Direction * bullet.Speed * elapsedTime;
            }
        }

        void shootBullet()
        {
            var mousePosition = window.MousePosition;
            var posX = (mousePosition.X * 2f / window.Size.X) - 1;
            var posY = (mousePosition.Y * -2f / window.Size.Y) + 1;
            Console.WriteLine($"mouseX: {posX}, mouseY: {posY}");
            Vector2 direction = new Vector2(posX, posY);
            direction.Normalize();
            Bullet bullet = new Bullet(new Vector2(0, 0), direction);
            listOfBullets.Add(bullet);
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
            MoveBullets(elapsedTime);
            Collissions();
        }

        void Collissions()
        {
            foreach (Enemy enemy in listOfEnemies.ToList())
            {
                foreach (Bullet bullet in listOfBullets.ToList())
                {
                    var deltaX = bullet.Center.X - enemy.Center.X;
                    var deltaY = bullet.Center.Y - enemy.Center.Y;

                    var distanceSq = deltaX * deltaX + deltaY * deltaY;
                    var distance = MathF.Sqrt(distanceSq);
                    if (distance < bullet.Radius + enemy.Radius)
                    {
                        listOfEnemies.Remove(enemy);
                        listOfBullets.Remove(bullet);
                        break;
                    }
                }

                var distanceToPlayer = MathF.Sqrt(enemy.Center.X * enemy.Center.X + enemy.Center.Y * enemy.Center.Y);
                if (distanceToPlayer < enemy.Radius + player.Radius)
                {
                    window.Close();
                }
            }
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
    public float Radius = 0.02f;
    public float Speed = 0.4f;

    public Bullet(Vector2 center, Vector2 direction)
    {
        Center = center;
        Direction = direction;
    }
}

internal class Player
{
    public float Radius;

    public Player(float radius)
    {
        Radius = radius;
    }
}