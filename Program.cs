using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;


internal class Program
{
    private static void Main(string[] args)
    {
        GameWindow window = new(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

        int gameState = 0;
        List<Enemy> listOfEnemies = new List<Enemy>();
        List<Bullet> listOfBullets = new List<Bullet>();
        Player player = new Player(0.1f, 4);
        EnemySpawner enemySpawner = new EnemySpawner();
        listOfEnemies = enemySpawner.MakeEnemies(player);
        Camera camera = new Camera();
        Update update = new Update(window, listOfEnemies, listOfBullets, player, camera);
        Draw draw = new Draw();
        window.UpdateFrame += args =>
        {
            update.update(args, gameState);
            player.movePlayer(window.KeyboardState);
        };
        window.Resize += args1 => camera.Resize(args1);
        window.KeyDown += args => { if (Keys.Escape == args.Key) window.Close(); };
        window.RenderFrame += args1 => draw.draw(listOfEnemies, listOfBullets, player, camera); // called once each frame; callback should contain drawing code
        window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering
        window.MouseDown += _ => player.shootBullet(window, listOfBullets, player, camera);

        // setup code executed once
        GL.ClearColor(Color4.LightGray);

        window.Run();
    }
}
