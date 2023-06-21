using Framework;
using Zenseless.OpenTK;

public class HandgunWeapon : Weapon
{
    public HandgunWeapon() : base(0.04f, 2f, 3f, 0.3f, new Animation(5, 4, 3f, EmbeddedResource.LoadTexture("handgun-move-sheet.png"), 0.15f))
    {

    }
}