using System;
using OpenTK.Mathematics;
public class Particle
{
    public Vector2 Center;
    int OffsetDirection;
    public float TimeAlive = 0f;
    public float TimeToLive = 1f;
    public void Update(float elapsedTime, Vector2 direction)
    {
        TimeAlive += elapsedTime;
        Center = Center + TimeAlive * OffsetVector(Center, direction) * OffsetDirection;
    }
    public bool IsDead()
    {
        return TimeAlive >= TimeToLive;
    }
    public static Vector2 OffsetVector(Vector2 center, Vector2 direction)
    {
        // Convert the angle to radians
        float angleRadians = (float)(MathF.PI / 2);

        // Calculate the offset vector
        float offsetX = direction.X * (float)MathF.Cos(angleRadians) - direction.Y * (float)Math.Sin(angleRadians);
        float offsetY = direction.X * (float)MathF.Sin(angleRadians) + direction.Y * (float)Math.Cos(angleRadians);

        // Create the offset vector by adding the offset to the center
        Vector2 offset = new Vector2(center.X + offsetX, center.Y + offsetY);

        Random rand = new Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)

        return (float)randStdNormal * offset / 50f;
    }
    public Particle(Vector2 center, int offsetDirection)
    {
        Center = center;
        OffsetDirection = offsetDirection;
    }
}