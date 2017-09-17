using UnityEngine;
using System.Collections;

public class bolterSquadAttack : MonoBehaviour
{
    Transform attackTarget;               // Reference to the enemy's position.
    
    public float rotationSpeed = 10f;

    public GameObject damageCollider;
    public GameObject attackCollider;

    bool attacking;

    Animator anim;

    

void OnTriggerStay(Collider attackCollider)
    {
        if (attackCollider.gameObject.tag == "Enemy")
        {
            //attackTarget = GameObject.FindGameObjectWithTag("Enemy").transform;
            //RotateTowards(attackTarget);
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



    public void OpenDamageColllider()
    {
        damageCollider.SetActive(true);
    }
    public void CloseDamageCollider()
    {
        damageCollider.SetActive(false);
    }


}