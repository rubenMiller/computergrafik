using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Zenseless.OpenTK;

class Particle
{
    Vector2 loc;
    Vector2 vel;
    Vector2 acc;
    float lifespan;
    Texture2D img; // Assuming you have a Texture2D class for loading and storing textures

    public Particle(Vector2 l, Texture2D img_)
    {
        acc = Vector2.Zero;
        float vx = (float)(RandomGaussian() * 0.3);
        float vy = (float)(RandomGaussian() * 0.3 - 1.0);
        vel = new Vector2(vx, vy);
        loc = l;
        lifespan = 100.0f;
        img = img_;
    }

    public void Run()
    {
        Update();
        Render();
    }

    // Method to apply a force vector to the Particle object
    // Note we are ignoring "mass" here
    public void ApplyForce(Vector2 f)
    {
        acc += f;
    }

    // Method to update position
    public void Update()
    {
        vel += acc;
        loc += vel;
        lifespan -= 2.5f;
        acc = Vector2.Zero; // clear Acceleration
    }

    // Method to display
    public void Render()
    {
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
        GL.Ortho(0, 800, 0, 600, -1, 1); // Assuming a 800x600 window size, adjust as needed

        GL.Enable(EnableCap.Texture2D);
        GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);

        img.Bind(); // Assuming you have a method to bind the texture
        GL.Color4(1.0f, 1.0f, 1.0f, lifespan / 255.0f);

        GL.Begin(PrimitiveType.Quads);
        GL.TexCoord2(0, 0);
        GL.Vertex2(loc.X - img.Width / 2, loc.Y - img.Height / 2);
        GL.TexCoord2(1, 0);
        GL.Vertex2(loc.X + img.Width / 2, loc.Y - img.Height / 2);
        GL.TexCoord2(1, 1);
        GL.Vertex2(loc.X + img.Width / 2, loc.Y + img.Height / 2);
        GL.TexCoord2(0, 1);
        GL.Vertex2(loc.X - img.Width / 2, loc.Y + img.Height / 2);
        GL.End();

        GL.Disable(EnableCap.Blend);
        GL.Disable(EnableCap.Texture2D);
    }

    // Is the particle still useful?
    public bool IsDead()
    {
        return lifespan <= 0.0f;
    }

    // Helper function for generating random Gaussian values
    private double RandomGaussian()
    {
        // Implement your random Gaussian distribution function here
        return 0.0;
    }
}
