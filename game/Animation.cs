using System;
using System.Linq;
using OpenTK.Mathematics;
using Zenseless.OpenTK;

public class Animation
{
    public void Update(float elapsedTime)
    {
        NormalizedAnimationTime += elapsedTime / AnimationLength;
        NormalizedAnimationTime %= 1f;
    }
    public Box2 getTexCoords()
    {
        var spriteId = (uint)MathF.Round(NormalizedAnimationTime * (Columns * Rows - 1));
        return SpriteSheetTools.CalcTexCoords(spriteId, Columns, Rows);
    }

    private uint Columns;
    private uint Rows;
    public int[] AnimationParts = Enumerable.Range(0, 20).ToArray<int>();
    public float AnimationLength { get; }
    public float NormalizedAnimationTime { get; private set; } = 0f;
    public Texture2D Texture;
    public Animation(uint columns, uint rows, float animationLength, Texture2D texture)
    {
        Columns = columns;
        Rows = rows;
        AnimationLength = animationLength;
        Texture = texture;
    }
}