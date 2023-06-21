using OpenTK.Graphics.OpenGL;
using Framework;
using Zenseless.OpenTK;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System;
using System.Linq;

internal class DrawPlaying
{
    internal void DrawBullets(List<Bullet> listOfBullets, Camera camera)
    {
        foreach (Bullet bullet in listOfBullets)
        {

            var cam = camera.CameraMatrix;

            cam = Transformation2d.Combine(Transformation2d.Rotation(bullet.Direction.PolarAngle()), Transformation2d.Translate(bullet.Center), cam);
            GL.LoadMatrix(ref cam);


            GL.Color4(1f, 1f, 1f, 1f);
            if (bullet is PlayerBullet)
            {
                GL.BindTexture(TextureTarget.Texture2D, texBullet.Handle);
            }
            if (bullet is EnemyBullet)
            {
                GL.BindTexture(TextureTarget.Texture2D, texEnergyBall.Handle);
            }

            var bulletBox = new Box2(-bullet.Radius, -bullet.Radius, bullet.Radius, bullet.Radius);
            Draw.DrawRect(bulletBox, new Box2(0f, 0f, 1f, 1f));
            GL.BindTexture(TextureTarget.Texture2D, 0);

            cam = camera.CameraMatrix;
            GL.LoadMatrix(ref cam);
        }
    }
    internal void DrawEnemies(List<Enemy> listOfEnemies, Camera camera)
    {
        foreach (Enemy enemy in listOfEnemies)
        {
            DrawEntity(enemy.Orientation, enemy.Animation, enemy.Center, camera);


            if (enemy is bigEnemy)
            {
                GL.Color4(Color4.Red);
                Vector2 offsetCenter = new Vector2(enemy.Center.X - 0.2f, enemy.Center.Y + 0.2f);
                Draw.DrawRectangle(offsetCenter, 0.05f * enemy.Health, 0.03f);
            }
            if (enemy is shootingEnemy se)
            {
                DrawBullets(se.listOfBullets, camera);
            }
        }
    }

    internal void DrawPlayer(Player player, Camera camera)
    {
        DrawEntity(player.Orientation, player.weapon.Animation, player.Center, camera);

        // Healthbar
        GL.Color4(Color4.Red);
        Vector2 offsetCenter = new Vector2(player.Center.X - 0.1f, player.Center.Y + 0.1f);
        Draw.DrawRectangle(offsetCenter, 0.05f * player.Health, 0.03f);

    }

    internal void DrawBloodSplashes(List<BloodSplash> listOfBloodSplashes, Camera camera)
    {
        foreach (BloodSplash bloodSplash in listOfBloodSplashes)
        {
            var cam = camera.CameraMatrix;

            var test = bloodSplash.Orientation.PolarAngle();
            //Console.WriteLine(test);
            cam = Transformation2d.Combine(Transformation2d.Rotation((float)(bloodSplash.Orientation.PolarAngle() + (Math.PI / 2))), Transformation2d.Translate(bloodSplash.Center), cam);
            GL.LoadMatrix(ref cam);
            GL.Color4(1f, 1f, 1f, 1f);
            GL.BindTexture(TextureTarget.Texture2D, bloodSplash.Animation.Texture.Handle);


            var texCoords = bloodSplash.Animation.getTexCoords();

            var playerBox = new Box2(-bloodSplash.Radius, -bloodSplash.Radius, bloodSplash.Radius, bloodSplash.Radius);
            Draw.DrawRect(playerBox, texCoords);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            cam = camera.CameraMatrix;
            GL.LoadMatrix(ref cam);
        }

    }


    internal void DrawEntity(Vector2 Orientation, Animation animation, Vector2 Center, Camera camera)
    {
        //GL.Color4(0f, 0f, 0f, 0.25f);
        //Draw.DrawCircle(player.Center, player.Radius, 1f);

        var cam = camera.CameraMatrix;

        cam = Transformation2d.Combine(Transformation2d.Rotation(Orientation.PolarAngle()), Transformation2d.Translate(Center), cam);
        GL.LoadMatrix(ref cam);


        GL.Color4(1f, 1f, 1f, 1f);
        GL.BindTexture(TextureTarget.Texture2D, animation.Texture.Handle);


        var texCoords = animation.getTexCoords();

        var playerBox = new Box2(-animation.SpriteSize, -animation.SpriteSize, animation.SpriteSize, animation.SpriteSize);
        Draw.DrawRect(playerBox, texCoords);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        cam = camera.CameraMatrix;
        GL.LoadMatrix(ref cam);
    }



    private readonly Texture2D texBullet;
    private readonly Texture2D texEnergyBall;

    internal DrawPlaying()
    {
        texBullet = EmbeddedResource.LoadTexture("bullet.png");
        texEnergyBall = EmbeddedResource.LoadTexture("energy-ball.png");
    }
}