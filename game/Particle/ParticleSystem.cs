using System.Collections.Generic;
using OpenTK.Mathematics;
using Zenseless.OpenTK;

class ParticleSystem
{
    List<Particle> particles; // A List for all the particles
    Vector2 origin; // An origin point for where particles are birthed
    Texture2D img; // Assuming you have a Texture2D class for loading and storing textures

    public ParticleSystem(int num, Vector2 v, Texture2D img_)
    {
        particles = new List<Particle>(); // Initialize the list
        origin = v;
        img = img_;
        for (int i = 0; i < num; i++)
        {
            particles.Add(new Particle(origin, img)); // Add "num" amount of particles to the list
        }
    }

    public void Run()
    {
        for (int i = particles.Count - 1; i >= 0; i--)
        {
            Particle p = particles[i];
            p.Run();
            if (p.IsDead())
            {
                particles.RemoveAt(i);
            }
        }
    }

    // Method to add a force vector to all particles currently in the system
    public void ApplyForce(Vector2 dir)
    {
        foreach (Particle p in particles)
        {
            p.ApplyForce(dir);
        }
    }

    public void AddParticle()
    {
        particles.Add(new Particle(origin, img));
    }
}
