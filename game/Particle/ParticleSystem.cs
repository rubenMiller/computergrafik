using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using OpenTK.Mathematics;
using Zenseless.OpenTK;

public class ParticleSystem
{
    public Vector2 Center;
    public Vector2 Direction;
    public float Speed = 1f;
    public float Radius = 0.1f;
    public float SpriteSize = 0.12f;
    private float TimeSinceLstSpawn = 0f;
    private float TimeToSpawn = 0.02f;
    public Texture2D SmokeTexture;
    public Animation FireAnimation;
    private static Random random = new Random();
    public List<Particle> listOfParticles = new List<Particle>();
    public void Update(float elapsedTime)
    {
        TimeSinceLstSpawn += elapsedTime;
        Center = Center + Direction * Speed * elapsedTime;
        FireAnimation.Update(elapsedTime);
        foreach (Particle particle in listOfParticles.ToList())
        {
            particle.Update(elapsedTime, Direction);
            if (particle.IsDead())
            {
                listOfParticles.Remove(particle);
            }
        }
        if (TimeSinceLstSpawn >= TimeToSpawn)
        {
            TimeSinceLstSpawn = 0;
            Particle particle = new Particle(Center, random.Next(2) * 2 - 1);
            listOfParticles.Add(particle);
        }
    }
    public ParticleSystem(Vector2 center, Vector2 direction)
    {
        Center = center;
        Direction = direction;
        SmokeTexture = EmbeddedResource.LoadTexture("smoke_256a.png");
        FireAnimation = new Animation(2, 2, 0.5f, EmbeddedResource.LoadTexture("fireball.png"), Radius * 2, 1);
    }
}