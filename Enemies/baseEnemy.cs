using OpenTK.Mathematics;

internal class baseEnemy : Enemy
{
    public baseEnemy(Vector2 center) : base(center, 2, 0.1f, 0.2f)
    {
        
    }
    public override void Update(float elapsedTime, Player player)
    {
        base.Update(elapsedTime, player);
    }
}