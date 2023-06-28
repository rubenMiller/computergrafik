using Framework;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Zenseless.OpenTK;

internal class UpgradeMenu
{
    private void mouseClicked(Player player, GameWindow window, Camera camera, MouseState mouseState)
    {
        if (mouseState.IsButtonDown(MouseButton.Left))
        {
            var pixelMousePosition = window.MousePosition;
            var posX = (pixelMousePosition.X * 2f / window.Size.X) - 1;
            var posY = (pixelMousePosition.Y * -2f / window.Size.Y) + 1;
            Vector2 mousePosition = new Vector2(posX, posY);
            var transformedPosition = mousePosition.Transform(camera.CameraMatrix.Inverted());

            if (ButtonHealth.Position.ContainsInclusive(transformedPosition))
            {
                ButtonHealth.ButtonAction.Upgrade.action(player);
                upgradesPossible--;
            }
            if (Button2.Position.ContainsInclusive(transformedPosition))
            {
                Button2.ButtonAction.Upgrade.action(player);
                if (Button2.ButtonAction == ShotgunAction)
                {
                    Button2.ButtonAction = UpgradeShotgunBulletsAction;
                    Button3.ButtonAction = UpgradeShotgunReloadAction;
                }
                upgradesPossible--;
            }
            if (Button3.Position.ContainsInclusive(transformedPosition))
            {
                Button3.ButtonAction.Upgrade.action(player);
                if (Button3.ButtonAction == RifleAction)
                {
                    Button2.ButtonAction = UpgradeRifleRangeAction;
                    Button3.ButtonAction = UpgradeRifleReloadAction;
                }
                upgradesPossible--;
            }
        }
    }

    public void Update(Player player, GameWindow window, Camera camera, MouseState mouseState, GameState gameState)
    {
        mouseClicked(player, window, camera, mouseState);
        if (upgradesPossible <= 0)
        {
            gameState.transitionToState(GameState.STATE.STATE_PLAYING);
        }
    }
    public ButtonAction HealthAction = new ButtonAction("Upgrade Health", new UpgradeHealth(), EmbeddedResource.LoadTexture("health-upgrade.png"));
    public ButtonAction ShotgunAction = new ButtonAction("Pick the Shotgun.", new UpgradeToShotgun(), EmbeddedResource.LoadTexture("shotgun_sideview.png"));
    public ButtonAction UpgradeShotgunReloadAction = new ButtonAction("Upgrade Reload, -0.05", new UpgradeShotgunReload(), EmbeddedResource.LoadTexture("shotgun_sideview.png"));
    public ButtonAction UpgradeShotgunBulletsAction = new ButtonAction("Add 2 Bullets \nto each Shot", new UpgradeShotgunBullets(), EmbeddedResource.LoadTexture("shotgun_sideview.png"));
    public ButtonAction RifleAction = new ButtonAction("Pick the Rifle", new UpgradeToRifle(), EmbeddedResource.LoadTexture("rifle_sideview.png"));
    public ButtonAction UpgradeRifleReloadAction = new ButtonAction("Upgrade Reload, -0.025", new UpgradeRifleReload(), EmbeddedResource.LoadTexture("rifle_sideview.png"));
    public ButtonAction UpgradeRifleRangeAction = new ButtonAction("Upgrade Range, +2", new UpgradeRifleRange(), EmbeddedResource.LoadTexture("rifle_sideview.png"));


    public Button ButtonHealth;
    public Button Button2;
    public Button Button3;


    public int upgradesPossible = 1;

    public UpgradeMenu()
    {
        ButtonHealth = new Button(new Box2(-0.9f, -0.3f, -0.4f, 0.2f), HealthAction);
        Button2 = new Button(new Box2(-0.25f, -0.3f, 0.25f, 0.2f), ShotgunAction);
        Button3 = new Button(new Box2(0.4f, -0.3f, 0.9f, 0.2f), RifleAction);
    }
}