using Framework;
using OpenTK.Mathematics;

internal class bigEnemy : Enemy
{
    public bigEnemy(Vector2 center) : base(center, 10, 0.3f, 0.15f, new Animation(1, 1, 1, EmbeddedResource.LoadTexture("bigEnemy.png"), 0.4f))
    {
        
    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}