using OpenTK.Mathematics;

internal class Bullet
{
    public Vector2 Center;
    public Vector2 Direction;
    public float Radius = 0.02f;
    public float Speed;

    public Bullet(Vector2 center, Vector2 direction, float speed)
    {
        Center = center;
        Direction = direction;
        Speed = speed;
    }
}
