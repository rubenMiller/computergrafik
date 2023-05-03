using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input;
using static Zenseless.OpenTK.Transformation2d;

internal class Camera
{
    public void Resize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
        var _invAspectRatio = args.Height / (float)args.Width;
        var scaleWindow = Scale(_invAspectRatio, 1);
        GL.LoadMatrix(ref scaleWindow);
    }

    public void Move(KeyboardState keyboardState, float deltaTime)
    {
        float moveSpeed = 2.0f; // Adjust the move speed as needed
        float rotateSpeed = 2.0f; // Adjust the rotate speed as needed

        // Move forward (along the camera's look direction)
        if (keyboardState.IsKeyDown(Keys.W))
        {
            Vector2 forward = GetForwardVector();
            position += forward * moveSpeed * deltaTime;
        }

        // Move backward
        if (keyboardState.IsKeyDown(Keys.S))
        {
            Vector2 backward = -GetForwardVector();
            position += backward * moveSpeed * deltaTime;
        }

        // Strafe left
        if (keyboardState.IsKeyDown(Keys.A))
        {
            Vector2 left = -GetRightVector();
            position += left * moveSpeed * deltaTime;
        }

        // Strafe right
        if (keyboardState.IsKeyDown(Keys.D))
        {
            Vector2 right = GetRightVector();
            position += right * moveSpeed * deltaTime;
        }

        // Rotate left
        if (keyboardState.IsKeyDown(Keys.Q))
        {
            rotation += rotateSpeed * deltaTime;
        }

        // Rotate right
        if (keyboardState.IsKeyDown(Keys.E))
        {
            rotation -= rotateSpeed * deltaTime;
        }
    }

    // Get the forward vector (the direction the camera is facing)
    private Vector2 GetForwardVector()
    {
        return new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
    }

    // Get the right vector (perpendicular to the camera's forward vector, pointing right)
    private Vector2 GetRightVector()
    {
        return new Vector2(-MathF.Sin(rotation), MathF.Cos(rotation));
    }

    // Update the camera matrix
    public void UpdateCameraMatrix()
    {
        Matrix4 viewMatrix = Matrix4.LookAt(new Vector3(position.X, position.Y, 0.0f), new Vector3(position.X, position.Y, -1.0f), Vector3.UnitY);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref viewMatrix);
    }

    private Vector2 position;
    private float rotation;

    public Camera()
    {
        position = new Vector2(0, 0);
        rotation = 0.0f;
    }

}