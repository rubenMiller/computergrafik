using Framework;
using OpenTK.Mathematics;

public class BloodSplash
{
    public void Update(float elapsedTime)
    {
        TimeAlive += elapsedTime;
        Animation.Update(elapsedTime);
    }
    public Vector2 Center;
    public Vector2 Orientation;
    public float Radius = 0.2f;
    public float TimeToLive = 0.5f;
    public float TimeAlive = 0f;

    public Animation Animation;
    public BloodSplash(Vector2 center, Vector2 orientation)
    {
        Center = center;
        Orientation = orientation;
        Animation = new Animation(8, 1, TimeToLive, EmbeddedResource.LoadTexture("blood-splatter-sheet.png"), Radius);
    }
}