using OpenTK.Mathematics;

internal class bigEnemy : Enemy
{
    public bigEnemy(Vector2 center) : base(center, 10, 0.3f, 0.15f)
    {
        
    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}