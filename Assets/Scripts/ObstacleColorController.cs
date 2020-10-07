using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColorController : MonoBehaviour
{

    [SerializeField] Color color;
    [SerializeField] Color colorParticles;

    [SerializeField] SpriteRenderer shape;
    [SerializeField] SpriteRenderer shapeGlow;
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem particlesGlow;

    public Color Color { get => color; set => color = value; }
    public Color ColorParticles { get => colorParticles; set => colorParticles = value; }

    // Start is called before the first frame update
    void Start()
    {
        particles.Stop(true);
        particlesGlow.Stop(true);

        int seed = Random.Range(0, int.MaxValue);
        particles.randomSeed = (uint)seed;
        particlesGlow.randomSeed = (uint)seed;


        particles.Play();
        particlesGlow.Play();
    }

    // Update is called once per frame
    void Update()
    {
        shape.color = Color;
        shapeGlow.color = Color;

        ParticleSystem.MainModule ma = particles.main;
        ParticleSystem.MainModule maGlow = particlesGlow.main;

        ma.startColor = ColorParticles;
        maGlow.startColor = ColorParticles;
    }
}
