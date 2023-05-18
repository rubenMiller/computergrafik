using OpenTK.Mathematics;

internal class Enemy
{
    public int Type;
    public int Health;
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Vector2 Orientation = new Vector2(0, 0);
    public Enemy(Vector2 center, int type)
    {
        Center = center;
        Type = type;
        if (type == 1)
        {
            Health = 2;
            Radius = 0.1f;
            Speed = 0.2f;
        }
        else if (type == 2)
        {
            Health = 1;
            Radius = 0.05f;
            Speed = 0.5f;
        }
        else if (type == 3)
        {
            Health = 10;
            Radius = 0.3f;
            Speed = 0.15f;
        }
    }
}
