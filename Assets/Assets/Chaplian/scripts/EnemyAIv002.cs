using UnityEngine;
using System.Collections;

public class EnemyAIv002 : MonoBehaviour {

    public float attackRate = 3;
    float attackR;
    bool attacking;

    public float attackRange = 3;
    //public float rotSpeed = 5;

    public GameObject damageCollider;

    Animator anim;
    UnityEngine.AI.NavMeshAgent agent;

    Transform target;
    //bool lookleft;

    void Start()
    {
         anim = GetComponent<Animator>();
         agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      
         agent.stoppingDistance = attackRange;
         agent.updateRotation = false;

         target = GameObject.FindGameObjectWithTag("Player").transform;
    }

void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < attackRange + 0.5f)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }

        if (!attacking)
        {

            agent.Resume();
            agent.SetDestination(target.position);


            //Vector3 relativePosition = transform.InverseTransformDirection(agent.desiredVelocity);

            //float hor = relativePosition.z;
            //float vert = relativePosition.x;

            //anim.SetFloat("Horizontal", hor, 0.6f, Time.deltaTime);
            //anim.SetFloat("Vertical", vert, 0.6f, Time.deltaTime);

            //lookleft = (target.position.z < transform.position.z) ? true : false;

            //Quaternion targetRot = transform.rotation;

            //if (lookleft)
            //{
            //targetRot = Quaternion.Euler(0, 180, 0);
            //}
            //else
            //{
            //targetRot = Quaternion.Euler(0, 0, 0);
            //}

            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);
        }
        else
        {
            agent.Stop();
            //Vector3 relativePosition = transform.InverseTransformDirection(agent.desiredVelocity);

            //float hor = relativePosition.z;
            //float vert = relativePosition.x;

            //anim.SetFloat("Horizontal", hor, 0.6f, Time.deltaTime);
            //anim.SetFloat("Vertical", vert, 0.6f, Time.deltaTime);


            attackR += Time.deltaTime;

            if (attackR > attackRate)
            {
                anim.SetBool("Attack", true);
                StartCoroutine("CloseAttack");

                attackR = 0;
            }
        }
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
}
