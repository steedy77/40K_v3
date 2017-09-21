using UnityEngine;
using System.Collections;

public class bolterSquadMovementV001    : MonoBehaviour
{
    Transform target;               // Reference to the player's position.
    Transform attackTarget;
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float followTarget = 3;
    public float attackEnemyRate = 3;
    float attackR;
    float attackEnemyR;

    bool following;
    bool attackEnemy;

    public float followRange = 3;
    public float attackEnemyRange = 3;
    public float rotationSpeed = 10f;

    public GameObject damageCollider;
    public GameObject attackCollider;

    Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        nav.stoppingDistance = followRange;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {

        

        float distance = Vector3.Distance(transform.position, target.position);
        

        if (distance < followRange + 2f)
        {
            following = true;
        }
        else
        {
            following = false;
        }
        if (attackR > followTarget)
        {
            anim.SetBool("Followed", true);
            anim.SetBool("IsWalking", false);
            StartCoroutine("CloseAttack");
            attackR = 0;
        }

        else
        {
            anim.SetBool("Followed", false);
            anim.SetBool("IsWalking", false);

        }
        if (!following)
        {

            nav.Resume();
            nav.SetDestination(target.position);
            anim.SetBool("IsWalking", true);

        }
        else
        {
            //RotateTowards(target);

            attackEnemyR += Time.deltaTime;

            if (attackEnemyR > followTarget)
            {
                attackEnemyR = 0;
                anim.SetBool("Followed", true);
                StartCoroutine("CloseAttack");
            }
        }

        float distanceToEnemy = Vector3.Distance(transform.position, attackTarget.position);
        attackTarget = GameObject.FindGameObjectWithTag("BolterEnemy").transform;

        if (distanceToEnemy < attackEnemyRange + 2f)
        {
            attackEnemy = true;
        }
        else
        {
            attackEnemy = false;
        }
        if (attackEnemyR > attackEnemyRate)
        {

            anim.SetBool("Attack", true);
            StartCoroutine("CloseAttack");
            attackEnemyR = 0;
        }

        else
        {
            anim.SetBool("Attack", false);
            

        }
        if (!attackEnemy)
        {


            anim.SetBool("Attack", true);

        }
        else
        {
            //RotateTowards(target);

            attackEnemyR += Time.deltaTime;

            if (attackEnemyR > attackEnemyRate)
            {
                attackEnemyR = 0;
                anim.SetBool("Attack", true);
                StartCoroutine("CloseEnemyAttack");
            }
        }
    }


    void OnTriggerStay(Collider attackCollider)
    {
        if (attackCollider.gameObject.tag == "Enemy")
        {
            attackTarget = GameObject.FindGameObjectWithTag("BolterEnemy").transform;
            RotateTowards(attackTarget);
            anim.SetBool("Attack", true);

        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }



    private void RotateTowards(Transform attackTarget)
    {
        Vector3 direction = (attackTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    IEnumerator CloseAttack()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Followed", false);
    }
    IEnumerator CloseEnemyAttack()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Attack", false);
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