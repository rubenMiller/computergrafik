using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

internal class DrawInterface
{


    public void DrawButton(Button button, Camera camera)
    {
        GL.Color4(Color4.White);
        Draw.DrawRect(button.Position, new Box2(0, 0, 1, 1));

        GL.BindTexture(TextureTarget.Texture2D, button.TexButton.Handle);
        Draw.DrawRect(button.Position, new Box2(0, 0, 1, 1));
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }
    public DrawInterface()
    {

    }
}