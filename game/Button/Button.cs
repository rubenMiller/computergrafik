using OpenTK.Mathematics;
using Zenseless.OpenTK;

internal class Button
{
    public Box2 Position;
    public ButtonAction ButtonAction;
    internal Button(Box2 position, ButtonAction buttonAction)
    {
        Position = position;
        ButtonAction = buttonAction;
    }
}