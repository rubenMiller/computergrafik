using OpenTK.Mathematics;

public class PlayerBullet : Bullet
{
    public PlayerBullet(Vector2 center, Vector2 direction) : base(center, direction, 0.02f, 3f)
    {
        
    }
    public override void Update(float elapsedTime)
    {
        base.Update(elapsedTime);
    }
}