using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsEnemy : MonoBehaviour {

    public float health = 100;
    bool dealDamage;
    bool dealPowerDamage;
    bool dealHeavyDamage;
    bool subtractOnce;
    bool subtractPowerDamage;
    bool subtractHeavyDamage;
    bool dead;
    public float damageTimer = .4f;
    public float damageHeavyTimer = .4f;
    public float damagePowerTimer = .4f;
    public ParticleSystem BloodPool;
    public ParticleSystem hitSparks;
    public ParticleSystem hitBlood;
    public ParticleSystem Death;
    WaitForSeconds damageT;
    WaitForSeconds damageHeavy;
    WaitForSeconds damagePower;

    Animator anim;
    GameManager gm;

    public GameObject sliderPrefab;

    Slider healthSlider;
    RectTransform healthTrans;
    public GameObject destroyBulletCollider;

    AudioSource audio;
    public AudioClip hitAudio;
    public AudioClip deathAudio;
    UnityEngine.AI.NavMeshAgent nav;
    public float speed = 6f;


    void Start()
    {
        damageT = new WaitForSeconds(damageTimer);
        damageHeavy = new WaitForSeconds(damageHeavyTimer);
        damagePower = new WaitForSeconds(damagePowerTimer);
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        GameObject slid = Instantiate(sliderPrefab, transform.position, Quaternion.identity) as GameObject;
        slid.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        healthSlider = slid.GetComponentInChildren<Slider>();
        healthTrans = slid.GetComponent<RectTransform>();
   
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    
    
    void Update()
    {
        healthSlider.value = health/100;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        healthTrans.transform.position = screenPoint;

        if(dealDamage)
        {
            if (!subtractOnce)
            {
                health -= 10;
                anim.SetTrigger("Hit");
                subtractOnce = true;
                if (health < 30)
                {
                    hitBlood.Play();
                    anim.SetBool("IsStumbling", true);
                    nav.speed = (speed * 0.25f);
                }
                 else
                {
                    hitSparks.Play();
                    anim.SetBool("IsStumbling", false);
                    nav.speed = speed;
                }
            }
            StartCoroutine("CloseDamage");
        }
        if (dealPowerDamage)
        {
            if (!subtractPowerDamage)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                health -= 60;
                anim.SetTrigger("PowerHit");
                subtractPowerDamage = true;
                if (health < 30)
                {
                    hitBlood.Play();
                    anim.SetBool("IsStumbling", true);
                    nav.speed = (speed * 0.25f);
                }
                else
                {
                    hitSparks.Play();
                    anim.SetBool("IsStumbling", false);
                    nav.speed = speed;
                }
            }

            StartCoroutine("CloseDamage");
            StartCoroutine("ClosePowerDamage");
        }
        if (dealHeavyDamage)
        {
            if (!subtractHeavyDamage)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                health -= 25;
                anim.SetTrigger("HeavyHit");
                audio.Play();
                if (health < 30)
                {
                    hitBlood.Play();
                    anim.SetBool("IsStumbling", true);
                    nav.speed = (speed * 0.25f);
                }
                else
                {
                    hitSparks.Play();
                    anim.SetBool("IsStumbling", false);
                    nav.speed = speed;
                }
            }

            StartCoroutine("CloseDamage");
            StartCoroutine("CloseHeavyDamage");
        }

        if (health < 0)
        {
            if (!dead)
            {
                anim.SetBool("dead", true);
                anim.SetTrigger("death");
                healthTrans.gameObject.SetActive(false);
                dealDamage = true;
                destroyBulletCollider.SetActive(false);
                BloodPool.Play();
                //note capsule issue with camera push in
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                StartCoroutine(waitThenDestroy());
                audio.clip = deathAudio;
                audio.Play();
            }
                if (GetComponent<EnemyMovementV002>()) 
                {
                    GetComponent<EnemyMovementV002>().enabled = false;
                    GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

                }
                
               // else
               // {
              //     GetComponent<PlayerInput>().enabled = false;
               //     GetComponent<PlayerMovementV002>().enabled = false;
               //     GetComponent<PlayerAttackV3>().enabled = false;
               // }

                FindObjectOfType<GameManager>().enemiesSpawned.Remove(transform);

                dead = true;
            }
        }

        IEnumerator waitThenDestroy()
        {

            yield return new WaitForSeconds(10);
            anim.SetTrigger("FadeAnim");
            Death.Play();

            yield return new WaitForSeconds(20);
            Destroy(this.gameObject);
        }
    public void checkToApplyDamage()
    {
        if(!dealDamage)
        {
            dealDamage = true;
        }
    }
    public void checkToApplyPowerDamage()
    {
        if (!dealPowerDamage)
        {
            dealPowerDamage = true;
        }
    }
    public void checkToApplyHeavyDamage()
    {
        if (!dealHeavyDamage)
        {
            dealHeavyDamage = true;
        }
    }
    public void HitSFXEnemy()
    {
        audio.clip = hitAudio;
        audio.Play();
    }

    IEnumerator CloseDamage()
    {
        yield return damageT;
        dealDamage = false;
        subtractOnce = false;
        dealPowerDamage = false;
        subtractPowerDamage = false;
        dealHeavyDamage = false;
        subtractHeavyDamage = false;

    }
    IEnumerator ClosePowerDamage()
    {
        yield return damagePower;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

    }

    IEnumerator CloseHeavyDamage()
    {
        yield return damageHeavy;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

    }

}
