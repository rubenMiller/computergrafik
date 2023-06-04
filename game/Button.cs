using OpenTK.Mathematics;

internal class Button
{
    public Box2 Position;
    public string ButtonText;
    public Upgrade Upgrade;
    internal Button(Box2 position, string buttonText, Upgrade upgrade)
    {
        Position = position;
        ButtonText = buttonText;
        Upgrade = upgrade;
    }
}