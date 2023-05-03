using OpenTK.Mathematics;

internal class Enemy
{
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Enemy(Vector2 center, float radius, float speed)
    {
        Center = center;
        Radius = radius;
        Speed = speed;
    }
}
