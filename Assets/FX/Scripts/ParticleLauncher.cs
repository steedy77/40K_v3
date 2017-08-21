using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;

    List<ParticleCollisionEvent> collisionEvents;

    // List variable initialization
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    //
    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents (particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    // Gets Position & Rotation from CollisionEvent list then emits splatterparticles from there
    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
    splatterParticles.transform.position = particleCollisionEvent.intersection;
    splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
    splatterParticles.Emit(1);
    }
	
	// emits projectile when fire button pushed
	void Update ()
    {
        if (Input.GetButton("Fire2"))
        {
        particleLauncher.Emit (1);
    }
   }
}