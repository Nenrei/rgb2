using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColorController : MonoBehaviour
{

    [SerializeField] Color color;
    [SerializeField] Color colorParticles;
    [SerializeField] Color colorLight;

    [SerializeField] SpriteRenderer shape;
    [SerializeField] SpriteRenderer shapeGlow;
    [SerializeField] ParticleSystem particlesGlow;
    [SerializeField] ParticleSystem particles;

    [SerializeField] SpriteRenderer shapeLight;

    public Color Color { get => color; set => color = value; }
    public Color ColorParticles { get => colorParticles; set => colorParticles = value; }
    public Color ColorLight { get => colorLight; set => colorLight = value; }

    // Start is called before the first frame update
    void Start()
    {
        particlesGlow.Stop(true);
        particles.Stop(true);

        int seed = Random.Range(0, int.MaxValue);
        particlesGlow.randomSeed = (uint)seed;
        particles.randomSeed = (uint)seed;
        
        particlesGlow.Play();
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        shape.color = Color;
        shapeGlow.color = Color;

        shapeLight.color = ColorLight;

        ParticleSystem.MainModule maGlow = particlesGlow.main;
        ParticleSystem.MainModule ma = particles.main;

        maGlow.startColor = ColorParticles;
        ma.startColor = ColorParticles;
    }
}
