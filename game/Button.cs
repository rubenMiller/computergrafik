using OpenTK.Mathematics;
using Zenseless.OpenTK;

internal class Button
{
    public Box2 Position;
    public string ButtonText;
    public Upgrade Upgrade;
    public readonly Texture2D TexButton;
    internal Button(Box2 position, string buttonText, Upgrade upgrade, Texture2D texButton)
    {
        Position = position;
        ButtonText = buttonText;
        Upgrade = upgrade;
        TexButton = texButton;
    }
}