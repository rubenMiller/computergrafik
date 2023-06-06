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
            // TODO: iterate over button list
            // when weapon is choosen,, iterate over other list
            if (Button1.Position.ContainsInclusive(transformedPosition))
            {
                Button1.Upgrade.action(player);
                upgradesPossible--;
            }
            if (Button2.Position.ContainsInclusive(transformedPosition))
            {
                Button2.Upgrade.action(player);
                upgradesPossible--;
            }
            if (Button3.Position.ContainsInclusive(transformedPosition))
            {
                Button3.Upgrade.action(player);
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
    public Button Button1 = new Button(new Box2(-0.9f, -0.3f, -0.4f, 0.2f), "Upgrade Health", new UpgradeHealth());
    public Button Button2 = new Button(new Box2(-0.25f, -0.3f, 0.25f, 0.2f), "Pick the Shotgun.", new UpgradeToShotgun());
    public Button Button3 = new Button(new Box2(0.4f, -0.3f, 0.9f, 0.2f), "Pick the Rifle", new UpgradeToRifle());
    public int upgradesPossible = 1;

    public UpgradeMenu()
    {

    }
}