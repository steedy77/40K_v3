using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFire : MonoBehaviour {

    public ParticleSystem particleSystem;
    public float fireRate = 0.5F;
    public AudioClip GunSound;
    AudioSource audio;
    private float nextFire = 0.0F;

    // emits projectile when fire button pushed

    void Update()
    {
        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            particleSystem.Emit(1);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

        }
    }
}