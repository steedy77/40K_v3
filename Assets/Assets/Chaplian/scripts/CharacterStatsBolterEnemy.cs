﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsBolterEnemy : MonoBehaviour {

    public float health = 100;
    bool dealDamage;
    bool substractOnce;
    bool dead;
    public string deathAnim = "death";
    public float damageTimer = .4f;
    public ParticleSystem BloodPool;
    public ParticleSystem hitSparks;
    public ParticleSystem hitBlood;
    WaitForSeconds damageT;

    Animator anim;
    GameManager gm;

    public GameObject sliderPrefab;
    public GameObject damageCollider;

    Slider healthSlider;
    RectTransform healthTrans;

    void Start()
    {
        damageT = new WaitForSeconds(damageTimer);
        anim = GetComponent<Animator>();

        GameObject slid = Instantiate(sliderPrefab, transform.position, Quaternion.identity) as GameObject;
        slid.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        healthSlider = slid.GetComponentInChildren<Slider>();
        healthTrans = slid.GetComponent<RectTransform>();
   
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
    
    
    void Update()
    {
        healthSlider.value = health/100;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        healthTrans.transform.position = screenPoint;

        if(dealDamage)
        {
            if(!substractOnce)
            {
                health -= 10;
                anim.SetTrigger("Hit");
                substractOnce = true;
                if (health < 30)
                {
                    hitBlood.Play();
                }
                else
                {
                    hitSparks.Play();
                }
            }

            StartCoroutine("CloseDamage");
        }

        if(health < 0)
        {
            if (!dead)
            {
                anim.SetBool("dead", true);
                anim.SetTrigger("death");
                healthTrans.gameObject.SetActive(false);
                dealDamage = true;
                damageCollider.SetActive(false);
                BloodPool.Play();
                //note capsule issue with camera push in
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;

                if(GetComponent<bolterEnemyMovementV001>()) 
                {
                    GetComponent<bolterEnemyMovementV001>().enabled = false;
                    GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

                }
                
                else
                {
                    GetComponent<PlayerInput>().enabled = false;
                    GetComponent<PlayerMovementV002>().enabled = false;
                    GetComponent<PlayerAttackV3>().enabled = false;
                }

                FindObjectOfType<GameManager>().enemiesSpawned.Remove(transform);

                dead = true;
            }
        }
    }

    public void checkToApplyDamage()
    {
        if(!dealDamage)
        {
            dealDamage = true;
        }
    }
    
    IEnumerator CloseDamage()
    {
        yield return damageT;
        dealDamage = false;
        substractOnce = false;
               

    }
}
