using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PTrigger : MonoBehaviour
{
    public bool isDiscard ;
    public List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private ParticleSystem particleS;
    public int count = 0;
    private void Start() {
        particleS = GetComponent<ParticleSystem>();
    }
    public void TriggerOn()
    {
        particleS.Play();
        count = 0;
    }
    private void OnParticleTrigger() {
        int GetTriggerParticles = particleS.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        count++;
        if(particleS.particleCount > 9)
        {
            particleS.Stop();
            Debug.Log("Stop");
            return; 
        }
        if( particleS.particleCount > 2 && isDiscard)
        {
            particleS.Stop();
            Debug.Log("Stop");
            return;
        }
        for(int i =0 ; i < GetTriggerParticles ; i++)
        {
            Debug.Log("1 particle");
            ParticleSystem.Particle particle = particles[i];
            particle.remainingLifetime = 0 ;
            particles[i] = particle;
        }
        particleS.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}

