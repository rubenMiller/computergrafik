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
        if (AnimationParts.Contains((int)spriteId) == false)
        {
            spriteId = (uint)AnimationParts[0];
        }
        return SpriteSheetTools.CalcTexCoords(spriteId, Columns, Rows);
    }

    private uint Columns;
    private uint Rows;
    public int[] AnimationParts;
    public float AnimationLength { get; }
    public float NormalizedAnimationTime { get; private set; } = 0f;
    public Texture2D Texture;
    public float SpriteSize;
    public float WidthToHeigth;
    public Animation(uint columns, uint rows, float animationLength, Texture2D texture, float spriteSize, float widthToHeigth)
    {
        Columns = columns;
        Rows = rows;
        AnimationLength = animationLength;
        Texture = texture;
        SpriteSize = spriteSize;
        WidthToHeigth = widthToHeigth;
        AnimationParts = Enumerable.Range(0, (int)(columns * rows)).ToArray<int>();
    }

    public Animation(uint columns, uint rows, float animationLength, Texture2D texture, float spriteSize, float widthToHeigth, int SheetStart, int SheetEnd)
    {
        Columns = columns;
        Rows = rows;
        AnimationLength = animationLength;
        Texture = texture;
        SpriteSize = spriteSize;
        WidthToHeigth = widthToHeigth;
        AnimationParts = Enumerable.Range(SheetStart, SheetEnd).ToArray<int>();
    }
}