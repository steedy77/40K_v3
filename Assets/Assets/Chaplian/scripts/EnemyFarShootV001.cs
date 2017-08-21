using System.Collections;
using UnityEngine;

public class EnemyFarShootV001 : MonoBehaviour
{
    Transform target;               // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    //EnemyHealth enemyHealth;        // Reference to this enemy's health.
    //UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float attackRate = 3;
    float attackR;
    bool attacking;

    public float attackRange = 3;
    //public float rotSpeed = 5;

    public GameObject damageCollider;

    Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();
            
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
            anim.SetBool("Attack2", true);
            StartCoroutine("CloseAttack");

            attackR = 0;
        }
        else
        {
            anim.SetBool("Attack2", false);
        }
        

    }
    IEnumerator CloseAttack()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Attack2", false);
    }

    public void OpenDamageColllider()
    {
        damageCollider.SetActive(true);
    }

    public void CloseDamageCollider()
    {
        damageCollider.SetActive(false);
    }
}