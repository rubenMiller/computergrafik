using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static Zenseless.OpenTK.Transformation2d;

internal class Camera
{
    public void Resize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
        _invAspectRatio = args.Height / (float)args.Width;
        //var scaleWindow = Scale(_invAspectRatio, 1);
        //GL.LoadMatrix(ref scaleWindow);
        UpdateMatrix(0f);
    }



    public Vector2 Center;
    public Matrix4 CameraMatrix { get => _cameraMatrix; }

    public void SetMatrix() => GL.LoadMatrix(ref _cameraMatrix);
    private Matrix4 _cameraMatrix = Matrix4.Identity;
    public Vector2 Direction = new Vector2(0, 0);
    private float _invAspectRatio;

    internal void UpdateMatrix(float elapsedTime)
    {
        Center = Center + Direction * elapsedTime;
        var translate = Translate(-Center);
        var scale = Scale(1f / 1f);
        var aspect = Scale(_invAspectRatio, 1);
        _cameraMatrix = Combine(translate, scale, aspect);
        //Console.Write($"camera: {_position}");
    }

    public Camera()
    {
        Center = new Vector2(0, 0);
    }

}