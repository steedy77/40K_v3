using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squadTriggerAttack : MonoBehaviour
{
    Animator anim;
    Transform attackTarget;
    public float rotationSpeed = 10f;

    private void Start()
    {
        attackTarget = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetBool("Attack", true);
            StartCoroutine("CloseAttack");
            RotateTowards(attackTarget);
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
}
