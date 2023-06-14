using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Framework;
using Zenseless.OpenTK;

internal class Draw
{
    internal void DrawText(string text, float x, float y, float size, Camera camera)
    {

        var cam = camera.CameraMatrix;

        cam = Transformation2d.Combine(Transformation2d.Translate(camera.Center), cam);
        GL.LoadMatrix(ref cam);

        GL.BindTexture(TextureTarget.Texture2D, texFont.Handle);
        GL.Color4(Color4.AntiqueWhite);
        //A string is an array of characters. Each character is a number defined in a code page like the[ASCII](https://en.wikipedia.org/wiki/ASCII) code.
        const uint firstCharacter = 32; // the ASCII code of the first character stored in the bitmap font
        const uint charactersPerColumn = 12; // how many characters are in each column
        const uint charactersPerRow = 8; // how many characters are in each row
        var rect = new Box2(x, y, x + size, y + size); // rectangle of the first character
        foreach (var spriteId in SpriteSheetTools.StringToSpriteIds(text, firstCharacter))
        {
            var texCoords = SpriteSheetTools.CalcTexCoords(spriteId, charactersPerRow, charactersPerColumn);
            //texCoords.Scale(new Vector2(0.98f), texCoords.Center); // if you want to use a linear (or mipmap) filter do not use the texels on the border of one sprite tile
            DrawRect(rect, texCoords);
            rect.Translate(new Vector2(rect.Size.X, 0f));
        }
        GL.BindTexture(TextureTarget.Texture2D, 0);

        cam = camera.CameraMatrix;
        GL.LoadMatrix(ref cam);
    }

    internal static List<Vector2> CreateCirclePoints()
    {
        List<Vector2> pointList = new List<Vector2>();

        int corners = 32;
        float delta = 2f * MathF.PI / corners;
        for (int i = 0; i < corners; ++i)
        {
            var alpha = i * delta;

            var x = MathF.Cos(alpha);
            var y = MathF.Sin(alpha);

            var pointOnCircle = new Vector2(x, y);
            pointList.Add(pointOnCircle);
        }
        return pointList;
    }



    internal static void DrawCircle(Vector2 center, float radius, float fraction)
    {

        GL.Begin(PrimitiveType.TriangleFan);
        GL.Vertex2(center);
        var circle = CreateCirclePoints();
        for (int i = 0; i < circle.Count; i++)
        {
            float current = i / (float)circle.Count;
            if (current > fraction)
            {
                GL.End();
                return;
            }
            GL.Vertex2(center + radius * circle[i]);
        }
        GL.Vertex2(center + radius * circle[0]);
        GL.End();
    }

    internal static void DrawRectangle(Vector2 center, float width, float height)
    {
        GL.Begin(PrimitiveType.Quads);
        GL.Vertex2(center.X, center.Y);
        GL.Vertex2(center.X + width, center.Y);
        GL.Vertex2(center.X + width, center.Y + height);
        GL.Vertex2(center.X, center.Y + height);
        GL.End();
    }



    internal void DrawBackground(GameBorder gameBorder)
    {
        //GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
        GL.BindTexture(TextureTarget.Texture2D, texBackground.Handle);

        static Box2 SizedBox(float minX, float minY, float sizeX, float sizeY) => new(minX, minY, minX + sizeX, minY + sizeY);

        GL.Color4(1f, 1f, 1f, 1f);
        var rect = new Box2(gameBorder.MinX, gameBorder.MinY, gameBorder.MaxX, gameBorder.MaxY);
        DrawRect(rect, SizedBox(0, 0f, gameBorder.MaxX * 2, gameBorder.MaxY * 2));
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    internal static void DrawRect(Box2 rectangle, Box2 texCoords)
    {
        GL.Begin(PrimitiveType.Quads);
        GL.TexCoord2(texCoords.Min);
        GL.Vertex2(rectangle.Min);
        GL.TexCoord2(texCoords.Max.X, texCoords.Min.Y);
        GL.Vertex2(rectangle.Max.X, rectangle.Min.Y);
        GL.TexCoord2(texCoords.Max);
        GL.Vertex2(rectangle.Max);
        GL.TexCoord2(texCoords.Min.X, texCoords.Max.Y);
        GL.Vertex2(rectangle.Min.X, rectangle.Max.Y);
        GL.End();
    }

    internal void drawButton(Button button, Camera camera)
    {
        drawInterface.DrawButton(button, camera);
        DrawText(button.ButtonText, button.Position.Min.X, button.Position.Max.Y + 0.2f, 0.03f, camera);
    }


    public void draw(List<Enemy> listOfEnemies, List<Bullet> listOfEnemyBullets, List<Bullet> listPlayerOfBullets, Player player, Camera camera, GameState gameState, int updateWave, int updatetimePlayed, GameBorder gameBorder, UpgradeMenu upgradeMenu)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //TODO Make multiple Draws: Draw that calls DrawGame, DrawInterface, DrawStart and DrawDead

        switch (gameState.CurrentState)
        {
            case GameState.STATE.STATE_START:
                {
                    DrawText($"To start the game, press Space.", -0.9f, 0, 0.05f, camera);
                    break;
                }
            case GameState.STATE.STATE_PLAYING:
                {
                    DrawBackground(gameBorder);
                    drawPlaying.DrawPlayer(player, camera);
                    drawPlaying.DrawEnemies(listOfEnemies, camera);
                    drawPlaying.DrawBullets(listPlayerOfBullets, camera);
                    drawPlaying.DrawBullets(listOfEnemyBullets, camera);
                    DrawText($"Wave: {updateWave}, time in Wave: {updatetimePlayed} seconds.", -.99f, 0.9f, 0.05f, camera);
                    break;
                }
            case GameState.STATE.STATE_WAVEOVER:
                {
                    DrawText($"Youcompleted a Wave!", -0.5f, 0, 0.1f, camera);
                    DrawText($"To choose an upgrade, press Space.", -0.9f, -0.2f, 0.05f, camera);
                    break;
                }
            case GameState.STATE.STATE_UPGRADEMENU:
                {
                    DrawText($"Choose one of the Upgrades: ", -.99f, 0.9f, 0.05f, camera);
                    DrawBackground(gameBorder);

                    drawButton(upgradeMenu.Button1, camera);
                    drawButton(upgradeMenu.Button2, camera);
                    drawButton(upgradeMenu.Button3, camera);
                    break;
                }
            case GameState.STATE.STATE_DEAD:
                {
                    DrawText($"You died!", -0.5f, 0, 0.1f, camera);
                    DrawText($"To restart the game, press Space.", -0.9f, -0.2f, 0.05f, camera);
                    break;
                }
        }
    }

    private readonly Texture2D texBackground;
    private readonly Texture2D texFont;
    private DrawInterface drawInterface = new DrawInterface();
    private DrawPlaying drawPlaying = new DrawPlaying();
    public Draw()
    {
        texBackground = EmbeddedResource.LoadTexture("dirt-background.jpg");
        GL.BindTexture(TextureTarget.Texture2D, texBackground.Handle);
        texBackground.Function = TextureFunction.Repeat;
        //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        texFont = EmbeddedResource.LoadTexture("nullptr_hq4x.png");

        texFont.MinFilter = Zenseless.OpenTK.TextureMinFilter.Nearest; // avoids problems on the sprite cell borders, but no anti aliased text borders
        texFont.MagFilter = Zenseless.OpenTK.TextureMagFilter.Nearest;

        GL.Enable(EnableCap.Texture2D);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);

    }
}