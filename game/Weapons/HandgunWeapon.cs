using Framework;
using OpenTK.Mathematics;
using Zenseless.OpenTK;

public class HandgunWeapon : Weapon
{
    public HandgunWeapon() : base(0.04f, 2f, 3f, 0.3f, new Vector2(0.11f, -0.045f), new Animation(5, 4, 3f, EmbeddedResource.LoadTexture("handgun-move-sheet.png"), 0.115f, 0.85f))
    {

    }
}