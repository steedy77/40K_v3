using UnityEngine;
using System.Collections;

public class EnemyMovementV002 : MonoBehaviour
{
    Transform target;               // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    //EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    public float attackRate = 3;
    float attackR;
    bool attacking;

    public float attackRange = 3;
    //public float rotSpeed = 5;

    public GameObject damageCollider;
    public float rotationSpeed = 10f;

    Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        nav.stoppingDistance = attackRange;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

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
            StartCoroutine("CloseAttack");

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
            nav.Stop();
            RotateTowards(target);
            Vector3 relativePosition = transform.InverseTransformDirection(nav.desiredVelocity);

            float hor = relativePosition.z;
            float vert = relativePosition.x;

            anim.SetFloat("Horizontal", hor, 0.6f, Time.deltaTime);
            anim.SetFloat("Vertical", vert, 0.6f, Time.deltaTime);


            attackR += Time.deltaTime;

            if (attackR > attackRate)
            {
                anim.SetBool("Attack", true);
                StartCoroutine("CloseAttack");

                attackR = 0;
            }
        }
        // If the enemy and the player have health left...
        //if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
        // ... set the destination of the nav mesh agent to the player.
        nav.SetDestination(target.position);
        //}
        // Otherwise...
        //else
        //{
        // ... disable the nav mesh agent.
        //    nav.enabled = false;
        //}
    }
    IEnumerator CloseAttack()
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
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}