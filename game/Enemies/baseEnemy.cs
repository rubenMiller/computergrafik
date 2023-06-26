using Framework;
using OpenTK.Mathematics;

internal class baseEnemy : Enemy
{
    public baseEnemy(Vector2 center) : base(center, 2, 0.1f, 0.2f, new Animation(3, 6, 2f, EmbeddedResource.LoadTexture("zombie-move-sheet.png"), 0.15f, 1f, 0, 17))
    {

    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}