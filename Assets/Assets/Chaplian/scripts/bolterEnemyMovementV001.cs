﻿using UnityEngine;
using System.Collections;

public class bolterEnemyMovementV001    : MonoBehaviour
{
    Transform target;               // Reference to the player's position.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float attackRate = 3;
    float attackR;

    bool attacking;

    public float attackRange = 3;
    public float rotationSpeed = 10f;

    public ParticleSystem MuzzleFlashEnemy;
    public ParticleSystem ShellCasing;
    public ParticleSystem Smoke;
    Animator anim;
    public GameObject shot;
    public Transform shotSpawn;
    public float fxFireRate;
    private float nextFire;
    public AudioClip gunshot;
    AudioSource audio;



    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        nav.stoppingDistance = attackRange;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {

        

        float distance = Vector3.Distance(transform.position, target.position);
        

        if (distance < attackRange + 2f)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        if (attackR > attackRate)
        {
            anim.SetBool("Attack", true);
            anim.SetBool("IsWalking", false);
            StartCoroutine("CloseBEAttack");
            attackR = 0;
        }

        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("IsWalking", false);

        }
        if (!attacking)
        {

            nav.Resume();
            nav.SetDestination(target.position);
            anim.SetBool("IsWalking", true);

        }
        else
        {
            RotateTowards(target);

            attackR += Time.deltaTime;

            if (attackR > attackRate)
            {
                attackR = 0;
                anim.SetBool("Attack", true);
                StartCoroutine("CloseBEAttack");
            }
        }
        
        
    }
    IEnumerator CloseBEAttack()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Attack", false);
    }

    public void OpenBolterEnemyAttack()
    {
        nextFire = Time.time + fxFireRate;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        MuzzleFlashEnemy.Emit(1);
        ShellCasing.Emit(1);
        Smoke.Emit(1);
        audio.clip = gunshot;
        audio.Play();
    }
    
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}