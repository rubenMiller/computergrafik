using OpenTK.Mathematics;

public class EnemyBullet : Bullet
{
    public EnemyBullet(Vector2 center, Vector2 direction) : base(center, direction, 0.1f, 1f, 20f)
    {

    }
    public override void Update(float elapsedTime)
    {
        base.Update(elapsedTime);
    }
}