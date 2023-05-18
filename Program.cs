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
        Camera camera = new Camera();
        Wave wave = new Wave();
        Update update = new Update(enemySpawner);
        Draw draw = new Draw();
        bool reset = true;
        window.UpdateFrame += args =>
        {
            if (wave.timePlayed >= 2f && wave.readyForNewWave)
            {
                wave.readyForNewWave = false;
                listOfEnemies = enemySpawner.MakeEnemies(player);
                wave.WaveCount++;
            }
            player.movePlayer(window.KeyboardState);
            gameState = update.update(args, gameState, listOfEnemies, camera, player, listOfBullets, wave);
            if (gameState == 2 && reset)
            {
                reset = false;

                listOfEnemies = new List<Enemy>();
                listOfBullets = new List<Bullet>();
                player = new Player(0.1f, 4);
                wave = new Wave();
                //Do not reset the camera!!!!!
                //camera = new Camera();       
            }
        };

        window.Resize += args1 => camera.Resize(args1);
        window.KeyDown += args => { if (Keys.Escape == args.Key) window.Close(); };
        window.RenderFrame += args1 => draw.draw(listOfEnemies, listOfBullets, player, camera, gameState, wave.WaveCount, (int)wave.timePlayed); // called once each frame; callback should contain drawing code
        window.KeyDown += args => { if (gameState != 1) gameState = 1; reset = true; };
        window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering
        window.MouseDown += _ => player.shootBullet(window, listOfBullets, player, camera);

        // setup code executed once
        GL.ClearColor(Color4.LightGray);

        window.Run();
    }

}
