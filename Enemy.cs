using OpenTK.Mathematics;

internal class Enemy
{
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Vector2 Orientation = new Vector2(0, 0);
    public Enemy(Vector2 center, float radius, float speed)
    {
        Center = center;
        Radius = radius;
        Speed = speed;
    }
}
