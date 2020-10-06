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
        shape.color = color;
        shapeGlow.color = color;

        ParticleSystem.MainModule ma = particles.main;
        ParticleSystem.MainModule maGlow = particlesGlow.main;

        ma.startColor = colorParticles;
        maGlow.startColor = colorParticles;
    }
}
