using UnityEngine;
using System.Collections;

public class TriggerBolterAttack : MonoBehaviour
{
    Animator anim;

    public GameObject damageCollider;

    void Start()
    {
        anim = GetComponent<Animator>();        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("Attack", true);
            
                       
        }
        else
        {
            anim.SetBool("Attack", false);
        }
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

