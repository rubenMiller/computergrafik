using OpenTK.Mathematics;

public class Bullet
{
    public Vector2 Center;
    public Vector2 Direction;
    public float Radius;
    public float Speed;

    public Bullet(Vector2 center, Vector2 direction, float radius, float speed)
    {
        Center = center;
        Direction = direction;
        Radius = radius;
        Speed = speed;
    }

    public virtual void Update(float elapsedTime)
    {
        Center = Center + Direction * Speed * elapsedTime;
    }
    
}
