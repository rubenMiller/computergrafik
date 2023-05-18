using OpenTK.Mathematics;
using System.Collections.Generic;
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
}
