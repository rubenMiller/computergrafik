using System;
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
        UpdateMatrix();
    }

    public void moveCamera(KeyboardState keyBoardState)
    {
        int movX = 0;
        int movY = 0;
        if (keyBoardState.IsKeyDown(Keys.W))
        {
            movX = 1;
        }
        else if (keyBoardState.IsKeyDown(Keys.S))
        {
            movX = -1;
        }
        else
        {
            movX = 0;
        }
        if (keyBoardState.IsKeyDown(Keys.D))
        {
            movY = 1;
        }
        else if (keyBoardState.IsKeyDown(Keys.A))
        {
            movY = -1;
        }
        else
        {
            movY = 0;
        }

        Vector2 movement = new Vector2(movY, movX);
        if (movement.Length == 0)
        {
            return;
        }
        Console.Write("Hello");
        movement.Normalize();
        Movement = movement * 0.02f;
        UpdateMatrix();
    }


    public Vector2 Center
    {
        get => _position;
    }

    public void SetMatrix() => GL.LoadMatrix(ref _cameraMatrix);
    private Matrix4 _cameraMatrix = Matrix4.Identity;
    private Vector2 _position;
    public Vector2 Movement = new Vector2(0, 0);
    private float _invAspectRatio;

    private void UpdateMatrix()
    {
        Console.Write("THis hould be prtinted");
        _position = _position + Movement;
        var translate = Translate(-_position);
        var scale = Scale(1f / 1f);
        var aspect = Scale(_invAspectRatio, 1);
        _cameraMatrix = Combine(translate, scale, aspect);
        Console.Write($"camera: {_position}");
    }

    public Camera()
    {
        _position = new Vector2(0, 0);
    }

}