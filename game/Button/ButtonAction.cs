using OpenTK.Mathematics;
using Zenseless.OpenTK;

internal class ButtonAction
{
    public string ButtonText;
    public Upgrade Upgrade;
    public readonly Texture2D TexButton;
    internal ButtonAction(string buttonText, Upgrade upgrade, Texture2D texButton)
    {
        ButtonText = buttonText;
        Upgrade = upgrade;
        TexButton = texButton;
    }
}