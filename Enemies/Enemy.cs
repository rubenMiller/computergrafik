using OpenTK.Mathematics;

internal class Enemy
{
    public int Health;
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Vector2 Orientation = new Vector2(0, 0);
    public Enemy(Vector2 center, int health, float radius, float speed)
    {
        Center = center;
        Health = health;
        Radius = radius;
        Speed = speed;
    }

    public virtual void Update(float elapsedTime, Player player)
    {
        Vector2 enemyDirection = player.Center - Center;
        enemyDirection.Normalize();
        Orientation = enemyDirection;
        Center = Center + enemyDirection * Speed * elapsedTime;
    }

}
