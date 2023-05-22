using OpenTK.Mathematics;

internal class Enemy
{
    // these are neede only for type 4, should be outsorced
    public float timeSinceShoot = 0;
    public float reloadTime = 3f;
    public float timeStanding = 0;
    public Vector2 arrowOrientation;

    public Enemy Shoot(Vector2 playerCenter, Vector2 originCenter)
    {
        arrowOrientation = playerCenter - Center;
        arrowOrientation.Normalize();
        return new Enemy(originCenter, 5, arrowOrientation);
    }

    // until here
    public int Type;
    public int Health;
    public Vector2 Center;
    public float Radius;
    public float Speed;
    public Vector2 Orientation = new Vector2(0, 0);
    public bool unkillable = false;
    public Enemy(Vector2 center, int type, Vector2 arrowOrientation)
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
        // Shooter
        else if (type == 4)
        {
            Health = 1;
            Radius = 0.2f;
            Speed = 0.3f;
        }
        //Arrow
        else if (type == 5)
        {
            Health = 1;
            Radius = 0.03f;
            Speed = 1f;
            unkillable = true;
            this.arrowOrientation = arrowOrientation;
        }
    }
}
