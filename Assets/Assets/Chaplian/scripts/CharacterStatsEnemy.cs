using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatsEnemy : MonoBehaviour {

    public float health = 100;
    bool dealDamage;
    bool dealPowerDamage;
    bool substractOnce;
    bool subtractPowerDamage;
    bool dead;
    public float damageTimer = .4f;
    public ParticleSystem BloodPool;
    public ParticleSystem hitSparks;
    public ParticleSystem hitBlood;
    public ParticleSystem Death;
    WaitForSeconds damageT;

    Animator anim;
    GameManager gm;

    public GameObject sliderPrefab;

    Slider healthSlider;
    RectTransform healthTrans;
    public GameObject destroyBulletCollider;

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
            if (!substractOnce)
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
        if (dealPowerDamage)
        {
            if (!subtractPowerDamage)
            {
                health -= 60;
                anim.SetTrigger("Hit");
                subtractPowerDamage = true;
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

    IEnumerator CloseDamage()
    {
        yield return damageT;
        dealDamage = false;
        substractOnce = false;
        dealPowerDamage = false;
        subtractPowerDamage = false;

    }

}
