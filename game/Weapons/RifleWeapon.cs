using Framework;
using OpenTK.Mathematics;

public class RifleWeapon : Weapon
{
    public RifleWeapon() : base(0.04f, 4f, 2f, 0.2f, new Vector2(0.10f, -0.085f), new Animation(5, 4, 3f, EmbeddedResource.LoadTexture("rifle-move-sheet.png"), 0.15f))
    {

    }
}