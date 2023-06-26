using Framework;
using OpenTK.Mathematics;

internal class runnerEnemy : Enemy
{
    public runnerEnemy(Vector2 center) : base(center, 1, 0.05f, 0.5f, new Animation(1, 1, 1, EmbeddedResource.LoadTexture("Topdown-Monster-Token-jule-cat.png"), 0.1f, 1))
    {

    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}