using OpenTK.Mathematics;

internal class runnerEnemy : Enemy
{
    public runnerEnemy(Vector2 center) : base(center, 1, 0.05f, 0.5f)
    {
        
    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}