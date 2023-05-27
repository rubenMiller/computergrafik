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
            if (button1Position.ContainsInclusive(transformedPosition))
            {
                UpgradeHealth upgrade = new UpgradeHealth();
                upgrade.action(player);
                upgradesPossible--;
            }
            if (button2Position.ContainsInclusive(transformedPosition))
            {
                UpgradeToRifle upgrade = new UpgradeToRifle();
                upgrade.action(player);
                upgradesPossible--;
            }
            if (button3Position.ContainsInclusive(transformedPosition))
            {
                UpgradeToShotgun upgrade = new UpgradeToShotgun();
                upgrade.action(player);
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
    public Box2 button1Position { get; private set; }
    public Box2 button2Position { get; private set; }
    public Box2 button3Position { get; private set; }
    public int upgradesPossible = 1;

    public UpgradeMenu()
    {
        button1Position = new Box2(-0.9f, -0.3f, -0.4f, 0.2f);
        button2Position = new Box2(-0.25f, -0.3f, 0.25f, 0.2f);
        button3Position = new Box2(0.4f, -0.3f, 0.9f, 0.2f);
    }
}