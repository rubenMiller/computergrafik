using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


internal static class SpriteSheetTools
{
    internal static Box2 CalcTexCoords(uint spriteId, uint columns, uint rows)
    {
        var result = new Box2(0f, 0f, 1f, 1f);

        uint row = spriteId / columns;
        uint col = spriteId % columns;

        float x = col / (float)columns;
        float y = 1f - (row + 1f) / rows;
        float width = 1f / columns;
        float height = 1f / rows;

        result = new Box2(x, y, x + width, y + height);
        return result;
    }

    internal static IEnumerable<uint> StringToSpriteIds(string text, uint firstCharacter)
    {
        byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
        foreach (var asciiCharacter in asciiBytes)
        {
            yield return asciiCharacter - firstCharacter;
        }
    }

    public static Vector2 Intersects(Vector2 start, Vector2 direction, RectangleF rectangle)
    {
        // Convert the rectangle to its minimum and maximum points
        Vector2 minPoint = new Vector2(rectangle.Left, rectangle.Top);
        Vector2 maxPoint = new Vector2(rectangle.Right, rectangle.Bottom);

        // Calculate the inverse of the direction vector components
        float invDirectionX = 1f / direction.X;
        float invDirectionY = 1f / direction.Y;

        // Calculate the distance to the intersection with the left and right edges of the rectangle
        float t1 = (minPoint.X - start.X) * invDirectionX;
        float t2 = (maxPoint.X - start.X) * invDirectionX;

        // Calculate the distance to the intersection with the top and bottom edges of the rectangle
        float t3 = (minPoint.Y - start.Y) * invDirectionY;
        float t4 = (maxPoint.Y - start.Y) * invDirectionY;

        // Find the maximum distance values
        float tmin = MathHelper.Max(MathHelper.Min(t1, t2), MathHelper.Min(t3, t4));
        float tmax = MathHelper.Min(MathHelper.Max(t1, t2), MathHelper.Max(t3, t4));



        // Calculate the intersection point
        float t = tmin >= 0 ? tmin : tmax;
        return start + direction * t;
    }

    internal static bool InCamera(Vector2 CenterPlayer, Vector2 CenterObject, float Size, Camera camera)
    {
        float XDistance = Math.Abs(CenterPlayer.X - CenterObject.X) - Size;
        float YDistance = Math.Abs(CenterPlayer.Y - CenterObject.Y) - Size;
        if (XDistance < 1 / camera.cameraAspectRatio && YDistance < 1)
        {
            return true;
        }
        return false;
    }
}
