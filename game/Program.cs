using Framework;
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
        window.WindowState = WindowState.Fullscreen;
        //int gameState = 0;
        List<Enemy> listOfEnemies = new List<Enemy>();
        List<ParticleSystem> listOfEnemyBullets = new List<ParticleSystem>();
        List<BloodSplash> listOfBloodSplashes = new List<BloodSplash>();
        Player player = new Player(0.1f, 4);
        Camera camera = new Camera();
        Wave wave = new Wave();
        Update update = new Update();
        Draw draw = new Draw();
        GameBorder gameBorder = new GameBorder(3, 3, -3, -3);
        GameState gameState = new GameState(GameState.STATE.STATE_START);
        UpgradeMenu upgradeMenu = new UpgradeMenu();
        Animation EnemyIconAnimation = new Animation(1, 1, 1, EmbeddedResource.LoadTexture("Enemy_Icon.png"), 0.07f, 1);
        bool reset = true;
        window.UpdateFrame += args =>
            {
                update.update(args, window, gameState, listOfEnemies, listOfEnemyBullets, listOfBloodSplashes, EnemyIconAnimation, camera, player, wave, gameBorder, upgradeMenu);
                if (reset && gameState.CurrentState is GameState.STATE.STATE_DEAD)
                {
                    reset = false;

                    listOfEnemies = new List<Enemy>();
                    player.listOfBullets = new List<Bullet>();
                    player = new Player(0.1f, 4);
                    wave = new Wave();
                    listOfEnemyBullets = new List<ParticleSystem>();
                    listOfBloodSplashes = new List<BloodSplash>();
                    upgradeMenu = new UpgradeMenu();
                    //Do not reset the camera!
                    //camera = new Camera();       
                }
            };

        window.Resize += args1 => camera.Resize(args1);
        window.KeyDown += args => { if (Keys.Escape == args.Key) window.Close(); };
        window.RenderFrame += args1 => draw.draw(listOfEnemies, listOfEnemyBullets, player.listOfBullets, player, listOfBloodSplashes, EnemyIconAnimation, camera, gameState, wave.WaveCount, wave.waveTime, wave.RemainingEnemies, gameBorder, upgradeMenu); // called once each frame; callback should contain drawing code
        window.KeyDown += args =>
        {
            if (Keys.Space != args.Key)
            {
                return;
            }
            if (gameState.CurrentState is GameState.STATE.STATE_DEAD || gameState.CurrentState is GameState.STATE.STATE_START)
            {
                gameState.transitionToState(GameState.STATE.STATE_PLAYING);
                reset = true;
            }
            if (gameState.CurrentState is GameState.STATE.STATE_WON)
            {
                gameState.transitionToState(GameState.STATE.STATE_PLAYING);
                reset = true;
            }
            if (gameState.CurrentState is GameState.STATE.STATE_WAVEOVER)
            {
                if (wave.WaveCount > 6)
                {
                    gameState.transitionToState(GameState.STATE.STATE_WON);
                }
                else
                {
                    gameState.transitionToState(GameState.STATE.STATE_UPGRADEMENU);
                }

            }

        };
        window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering
                                                         //window.MouseDown += args => player.shootBullet(window, listOfBullets, player, camera);

        // setup code executed once
        GL.ClearColor(Color4.LightGray);

        window.Run();
    }

}
