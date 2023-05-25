using OpenTK.Mathematics;

public class PlayerBullet : Bullet
{
    //0.02f, 3f
    public PlayerBullet(Vector2 center, Vector2 direction, float radius, float speed, float range) : base(center, direction, radius, speed, range)
    {

    }
    public override void Update(float elapsedTime)
    {
        base.Update(elapsedTime);
    }
}