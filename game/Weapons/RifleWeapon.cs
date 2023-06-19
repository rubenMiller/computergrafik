using Framework;

public class RifleWeapon : Weapon
{
    public RifleWeapon() : base(0.04f, 4f, 6f, 0.15f, new Animation(5, 4, 3f, EmbeddedResource.LoadTexture("rifle-move-sheet.png")))
    {

    }
}